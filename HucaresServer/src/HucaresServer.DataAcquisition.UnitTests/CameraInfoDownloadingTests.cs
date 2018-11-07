using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FakeItEasy;
using HucaresServer.Storage.Helpers;
using HucaresServer.Storage.Models;
using NUnit.Framework;
using Shouldly;

namespace HucaresServer.DataAcquisition.UnitTests
{
    [TestFixture]
    public class CameraInfoDownloadingTests
    {
        [Test]
        public void DownloadImagesFromCameraInfoSources_WithTrusted_ShouldDownloadAndReturn()
        {
            // Arrange
            var fakeBitmap = new Bitmap(100, 100);
            var url = "https://some.url";
            
            ImageConverter converter = new ImageConverter();
            var fakeImageArray =  (byte[])converter.ConvertTo(fakeBitmap, typeof(byte[]));
            
            
            var fakeWebClient = A.Fake<IWebClient>();
            A.CallTo(() => fakeWebClient.DownloadData(url))
                .Returns(fakeImageArray);
            
            var fakeWebClientFactory = A.Fake<IWebClientFactory>();
            A.CallTo(() => fakeWebClientFactory.BuildWebClient())
                .Returns(fakeWebClient);
            
            var fakeMissingPlates = new List<CameraInfo>()
            {
                new CameraInfo() {Id = 0, HostUrl = url, IsTrustedSource = true},
            };
            
            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();
            A.CallTo(() => fakeCameraInfoHelper.GetActiveCameras(true))
                .Returns(fakeMissingPlates);

            var fakeImageSaver = A.Fake<IImageSaver>();
            
            var cameraImageDownloading = new CameraImageDownloading(fakeCameraInfoHelper, fakeImageSaver, fakeWebClientFactory);
            
            // Act
            var resultCameraCount = cameraImageDownloading.DownloadImagesFromCameraInfoSources(true, new DateTime(2018, 11, 01));
            
            // Assert
            A.CallTo(() => fakeCameraInfoHelper.GetActiveCameras(true)).MustHaveHappened();
            A.CallTo(() => fakeWebClient.DownloadData(url)).MustHaveHappenedOnceExactly();
//            A.CallTo(() => fakeImageSaver.SaveImage(fakeBitmap, 0, new DateTime(2018, 11, 01))).MustHaveHappenedOnceExactly();
            resultCameraCount.ShouldBe(1);
        }
        
        [Test]
        public void DownloadImagesFromCameraInfoSources_WithNoTrust_ShouldDownloadAndReturn()
        {
            // Arrange
            var fakeBitmap = new Bitmap(100, 100);
            var url = "https://some.url";
            
            ImageConverter converter = new ImageConverter();
            var fakeImageArray =  (byte[])converter.ConvertTo(fakeBitmap, typeof(byte[]));
            
            
            var fakeWebClient = A.Fake<IWebClient>();
            A.CallTo(() => fakeWebClient.DownloadData(url))
                .Returns(fakeImageArray);
            
            var fakeWebClientFactory = A.Fake<IWebClientFactory>();
            A.CallTo(() => fakeWebClientFactory.BuildWebClient())
                .Returns(fakeWebClient);
            
            var fakeMissingPlates = new List<CameraInfo>()
            {
                new CameraInfo() {Id = 0, HostUrl = url, IsTrustedSource = true},
                new CameraInfo() {Id = 0, HostUrl = url, IsTrustedSource = false}
            };
            
            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();
            A.CallTo(() => fakeCameraInfoHelper.GetActiveCameras(null))
                .Returns(fakeMissingPlates);

            var fakeImageSaver = A.Fake<IImageSaver>();
            
            var cameraImageDownloading = new CameraImageDownloading(fakeCameraInfoHelper, fakeImageSaver, fakeWebClientFactory);
            
            // Act
            var resultCameraCount = cameraImageDownloading.DownloadImagesFromCameraInfoSources(downloadDateTime: new DateTime(2018, 11, 01));
            
            // Assert
            A.CallTo(() => fakeCameraInfoHelper.GetActiveCameras(null)).MustHaveHappened();
            A.CallTo(() => fakeWebClient.DownloadData(url)).MustHaveHappenedTwiceExactly();
//            A.CallTo(() => fakeImageSaver.SaveImage(fakeBitmap, 0, new DateTime(2018, 11, 01))).MustHaveHappenedOnceExactly();
            resultCameraCount.ShouldBe(2);
        }
    }
}