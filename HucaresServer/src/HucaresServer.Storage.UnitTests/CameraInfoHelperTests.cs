using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HucaresServer.Storage.Helpers;
using FakeItEasy;
using HucaresServer.Storage.Models;
using NUnit.Framework;
using Shouldly;

namespace HucaresServer.Storage.UnitTests
{
    public class CameraInfoHelperTests
    {
        [Test]
        public void InsertCamera_WhenAllDataValid_ShouldSucceedAndReturnExpected()
        {
            //Arrange
            var fakeIQueryable = new List<CameraInfo>().AsQueryable();
            var fakeDbSet = A.Fake<DbSet<CameraInfo>>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var expectedCameraInfo = new CameraInfo()
            {
                HostUrl = "http://localhost:5051/some/cam",
                Latitude = 53.124,
                Longitude = 27.375,
                IsTrustedSource = true
            };
            
            var result = cameraInfoHelper.InsertCamera(expectedCameraInfo.HostUrl, expectedCameraInfo.Latitude,
                expectedCameraInfo.Longitude, expectedCameraInfo.IsTrustedSource);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeDbSet.Add(result))
                .MustHaveHappened();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustHaveHappenedOnceExactly();

            result.ShouldBe(expectedCameraInfo);
        }

        [Test]
        public void InsertCamera_WhenHostUrlIsMalformed_ShouldThrow()
        {
            //Arrange
            var fakeDbSet = A.Fake<DbSet<CameraInfo>>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act & Assert
            Assert.Throws<UriFormatException>(() => cameraInfoHelper.InsertCamera("someInvalidHost", 53.124, 27.375));

            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustNotHaveHappened();
        }

        [Test]
        public void InsertCamera_WhenTrustedSourceValueNotProvided_IsTrustedSourceShouldBeFalse()
        {
            //Arrange
            var fakeDbSet = A.Fake<DbSet<CameraInfo>>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.InsertCamera("http://localhost:5051/some/cam", 53.124, 27.375);

            //Assert
            Assert.IsFalse(result.IsTrustedSource, "Default IsTrustedSource value was not false");
        }


        [Test]
        public void UpdateCameraActivity_WhenRecordWithIdDoesNotExist_ShouldThrow()
        {
            //Arrange
            var fakeIQueryable = new List<CameraInfo>().AsQueryable();
            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => cameraInfoHelper.UpdateCameraActivity(0, true));

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();
        }

        [Test]
        public void UpdateCameraActivity_WhenRecordWithIdExists_ShouldUpdateAndReturnExpected()
        {
            //Arrange
            var camInfoObj = new CameraInfo() { Id = 0, IsActive = false };
            var fakeIQueryable = new List<CameraInfo>(){ camInfoObj }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var expectedActivity = true;
            var result = cameraInfoHelper.UpdateCameraActivity(camInfoObj.Id, expectedActivity);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeDbSet.Add(A<CameraInfo>.Ignored))
                .MustNotHaveHappened();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustHaveHappenedOnceExactly();

            result.ShouldBe(camInfoObj);
            camInfoObj.IsActive.ShouldBe(expectedActivity);
        }

        [Test]
        public void UpdateCameraSource_WhenHostUrlIsMalformed_ShouldThrow()
        {
            //Arrange
            var fakeDbSet = A.Fake<DbSet<CameraInfo>>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act & Assert
            Assert.Throws<UriFormatException>(() => cameraInfoHelper.UpdateCameraSource(0, "someInvalidHost", true));

            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustNotHaveHappened();
        }

        [Test]
        public void UpdateCameraSource_WhenRecordWithIdDoesNotExist_ShouldThrow()
        {
            //Arrange
            var fakeIQueryable = new List<CameraInfo>().AsQueryable();
            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => cameraInfoHelper.UpdateCameraSource(0, "http://localhost:5051/some/cam", true));

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();
        }

        [Test]
        public void UpdateCameraSource_WhenRecordWithIdExists_ShouldUpdateAndReturnExpected()
        {
            //Arrange
            var camInfoObj = new CameraInfo() { Id = 0, HostUrl = "http://localhost:5051/bad/cam", IsTrustedSource = false };
            var fakeIQueryable = new List<CameraInfo>() { camInfoObj }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var expectedCameraInfo = new CameraInfo()
            {
                Id = 0,
                HostUrl = "http://localhost:5051/some/cam",
                IsTrustedSource = true
            };
            var result = cameraInfoHelper.UpdateCameraSource(expectedCameraInfo.Id, expectedCameraInfo.HostUrl, 
                expectedCameraInfo.IsActive);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeDbSet.Add(A<CameraInfo>.Ignored))
                .MustNotHaveHappened();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustHaveHappenedOnceExactly();

            result.ShouldBe(expectedCameraInfo);
        }

        [Test]
        public void GetCameraById_WhenRecordWithIdExists_ShouldSucceedAndReturnExpected()
        {
            //Arrange
            var camInfoObj = new CameraInfo() { Id = 0, IsActive = false };
            var fakeIQueryable = new List<CameraInfo>()
            {
                camInfoObj,
                new CameraInfo() { Id = 1 }
            }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.GetCameraById(camInfoObj.Id);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();

            result.ShouldBe(camInfoObj);
        }

        [Test]
        public void GetCameraById_WhenRecordWithIdDoesNotExist_ShouldSucceedAndReturnNull()
        {
            //Arrange
            var fakeIQueryable = new List<CameraInfo>()
            {
                new CameraInfo() { Id = 1 }
            }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.GetCameraById(0);

            //Assert
            result.ShouldBe(null);
        }

        [Test]
        public void GetInactiveCameras_WhenInactiveCameraExists_ShouldReturnExpected()
        {
            //Arrange
            var camInfoObj = new CameraInfo() { Id = 0, IsActive = false };
            var fakeIQueryable = new List<CameraInfo>()
            {
                camInfoObj,
                new CameraInfo() { Id = 1, IsActive = true }
            }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.GetInactiveCameras();

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();

            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(camInfoObj);
        }

        [Test]
        public void GetAllCameras_WhenTrustedSourceIsNull_ShouldReturnAll()
        {
            //Arrange
            var fakeIQueryable = new List<CameraInfo>()
            {
                new CameraInfo() { Id = 0, IsTrustedSource = true },
                new CameraInfo() { Id = 1, IsTrustedSource = false }
            }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.GetAllCameras();

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();

            result.Count().ShouldBe(fakeIQueryable.Count());
            Assert.IsTrue(result.SequenceEqual(fakeIQueryable.ToList()), "Lists are not equal");
        }

        [Test]
        public void GetAllCameras_WhenTrustedSourceIsNotNull_ShouldReturnOnlyExpected()
        {
            //Arrange
            var camInfoObj = new CameraInfo() { Id = 0, IsTrustedSource = true };
            var fakeIQueryable = new List<CameraInfo>()
            {
                camInfoObj,
                new CameraInfo() { Id = 1, IsTrustedSource = false }
            }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.GetAllCameras(true);

            //Assert
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(camInfoObj);
        }

        [Test]
        public void GetActiveCameras_WhenTrustedSourceIsNull_ShouldReturnActive()
        {
            //Arrange
            var fakeIQueryable = new List<CameraInfo>()
            {
                new CameraInfo() { Id = 0, IsActive = true, IsTrustedSource = true },
                new CameraInfo() { Id = 0, IsActive = true, IsTrustedSource = false },
                new CameraInfo() { Id = 1, IsActive = false, IsTrustedSource = false }
            }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.GetActiveCameras();

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();

            var expectedResult = fakeIQueryable.Where(c => c.IsActive);
            result.Count().ShouldBe(expectedResult.Count());
            Assert.IsTrue(result.SequenceEqual(expectedResult.ToList()), "Lists are not equal");
        }

        [Test]
        public void GetActiveCameras_WhenTrustedSourceIsNotNull_ShouldReturnOnlyExpected()
        {
            //Arrange
            var camInfoObj = new CameraInfo() { Id = 0, IsActive = true, IsTrustedSource = true };
            var fakeIQueryable = new List<CameraInfo>()
            {
                camInfoObj,
                new CameraInfo() { Id = 1, IsActive = true, IsTrustedSource = false },
                new CameraInfo() { Id = 2, IsActive = false, IsTrustedSource = true },
                new CameraInfo() { Id = 3, IsActive = false, IsTrustedSource = false }
            }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.GetActiveCameras(true);

            //Assert
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(camInfoObj);
        }

        [Test]
        public void DeleteCameraById_WhenRecordWithIdDoesNotExist_ShouldThrow()
        {
            //Arrange
            var fakeIQueryable = new List<CameraInfo>().AsQueryable();
            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => cameraInfoHelper.DeleteCameraById(0));

            A.CallTo(() => fakeDlpHelper.GetAllDetectedPlatesByCamera(A<int>.Ignored, A<DateTime?>.Ignored, A<DateTime?>.Ignored))
                .MustHaveHappened();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();
        }

        [Test]
        public void DeleteCameraById_WhenDLPDependsOnCamera_ShouldThrow()
        {
            //Arrange
            var fakeIQueryable = new List<CameraInfo>().AsQueryable();
            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();

            var expectedId = 0;
            A.CallTo(() => fakeDlpHelper.GetAllDetectedPlatesByCamera(expectedId, A<DateTime?>.Ignored, A<DateTime?>.Ignored))
                .Returns(new List<DetectedLicensePlate>() { new DetectedLicensePlate() });

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act & Assert
            Assert.Throws<AccessViolationException>(() => cameraInfoHelper.DeleteCameraById(expectedId));

            A.CallTo(() => fakeDlpHelper.GetAllDetectedPlatesByCamera(A<int>.Ignored, A<DateTime?>.Ignored, A<DateTime?>.Ignored))
                .MustHaveHappened();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();
        }

        [Test]
        public void DeleteCameraById_WhenRecordWithIdExistsAndDlpHasNoDependency_ShouldAndReturnExpected()
        {
            //Arrange
            var camInfoObj = new CameraInfo() { Id = 1 };
            var fakeIQueryable = new List<CameraInfo>()
            {
                new CameraInfo() { Id = 0 },
                camInfoObj 
                }.AsQueryable();

            var fakeDbSet = StorageTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.CameraInfo)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeDlpHelper = A.Fake<IDetectedPlateHelper>();
            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory, fakeDlpHelper);

            //Act
            var result = cameraInfoHelper.DeleteCameraById(camInfoObj.Id);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeDbSet.Remove(camInfoObj))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustHaveHappenedOnceExactly();

            result.ShouldBe(camInfoObj);
        }
    }
}
