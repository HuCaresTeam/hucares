using FakeItEasy;
using HucaresServer.DataAcquisition;
using HucaresServer.Storage.Helpers;
using NUnit.Framework;
using OpenAlprApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HucaresServer.TimedProcess.UnitTests
{
    [TestFixture]
    public class DlpCollectionProcessTests
    {
        private ICameraImageDownloading _fakeCameraImageDownloading;
        private IOpenAlprWrapper _fakeOpenAlprWrapper;
        private IDetectedPlateHelper _fakeDlpHelper;
        private IImageManipulator _fakeImageSaver;
        private IImageFileNamer _fakeFileNamer;

        [SetUp]
        public void TestSetup()
        {
            _fakeCameraImageDownloading = A.Fake<ICameraImageDownloading>();
            _fakeOpenAlprWrapper = A.Fake<IOpenAlprWrapper>();
            _fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            _fakeImageSaver = A.Fake<IImageManipulator>();
            _fakeFileNamer = A.Fake<IImageFileNamer>();
        }

        [Test]
        public async Task StartProccess_WhenHappyPath_ShouldCallExpected()
        {
            //Arrange

            var returnedFiles = new List<FileSystemInfo>
            {
                BuildFakeFileSystemInfo("fullName0", "name0"),
                BuildFakeFileSystemInfo("fullName1", "name1")
            };
            A.CallTo(() => _fakeImageSaver.GetTempFiles())
                .Returns(returnedFiles);

            var alprResponses = GetAlprTestData();
            A.CallTo(() => _fakeOpenAlprWrapper.DetectPlateAsync(A<string>._))
                .ReturnsNextFromSequence(alprResponses);

            var filePathAndCamId = new List<Tuple<string, int>>()
            {
                new Tuple<string, int>("0_location", 0),
                new Tuple<string, int>("1_location", 1)
            };

            A.CallTo(() => _fakeImageSaver.MoveFileToPerm(A<FileSystemInfo>.That.Matches(f => returnedFiles.Contains(f)), A<DateTime>._))
                .ReturnsNextFromSequence(filePathAndCamId
                    .Select(pc => pc.Item1)
                    .ToArray());

            A.CallTo(() => _fakeFileNamer.ExtractCameraId(A<string>.That.Matches(fn => returnedFiles.Select(r => r.Name).Contains(fn))))
                .ReturnsNextFromSequence(filePathAndCamId
                    .Select(pc => pc.Item2)
                    .ToArray());

            var dlpProcess = new DlpCollectionProcess(_fakeCameraImageDownloading, _fakeOpenAlprWrapper, _fakeDlpHelper, _fakeFileNamer, _fakeImageSaver);

            //Act
            await dlpProcess.StartProccess();

            //Assert
            A.CallTo(() => _fakeCameraImageDownloading.DownloadImagesFromCameraInfoSources(A<bool>._, A<DateTime>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeOpenAlprWrapper.DetectPlateAsync(A<string>._))
                .MustHaveHappened(returnedFiles.Count, Times.Exactly);

            returnedFiles.ToList().ForEach(f =>
                A.CallTo(() => _fakeOpenAlprWrapper.DetectPlateAsync(f.FullName))
                    .MustHaveHappenedOnceExactly());

            for (int i = 0; i < alprResponses.Count(); i++)
            {
                var plates = alprResponses[i].Results.Select(r => r.Plate);
                var confidences = alprResponses[i].Results.Select(r => r.Confidence);
                var path = filePathAndCamId[i].Item1;
                var camId = filePathAndCamId[i].Item2;
                
                A.CallTo(() => _fakeDlpHelper.InsertNewDetectedPlate(
                    A<string>.That.Matches(s => plates.Contains(s)),
                    A<DateTime>._,
                    camId,
                    path,
                    A<double>.That.Matches(d => confidences.Select(dec => decimal.ToDouble(dec ?? 0m)).Contains(d))
                )).MustHaveHappened(alprResponses[i].Results.Count(), Times.Exactly);
            }
        }

        private FileSystemInfo BuildFakeFileSystemInfo(string fullName, string name)
        {
            var fakeFileSystemInfo = A.Fake<FileSystemInfo>();
            A.CallTo(() => fakeFileSystemInfo.FullName)
                .Returns(fullName);

            A.CallTo(() => fakeFileSystemInfo.Name)
               .Returns(name);

            return fakeFileSystemInfo;
        }

        private InlineResponse200[] GetAlprTestData()
        {
            return new InlineResponse200[]
            {
                new InlineResponse200
                {
                    Results = new List<PlateDetails>
                    {
                        new PlateDetails() { Plate = "ABC123", Confidence = 1.0m },
                        new PlateDetails() { Plate = "DBE456", Confidence = 0.9m  }
                    }
                },
                new InlineResponse200
                {
                    Results = new List<PlateDetails>
                    {
                        new PlateDetails() { Plate = "FFF666", Confidence = 0.8m  },
                    }
                },
            };
        }
    }
}
