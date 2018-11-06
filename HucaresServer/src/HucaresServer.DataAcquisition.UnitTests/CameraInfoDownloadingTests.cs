using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FakeItEasy;
using HucaresServer.Storage.Helpers;
using HucaresServer.Storage.Models;
using NUnit.Framework;

namespace HucaresServer.DataAcquisition.UnitTests
{
    [TestFixture]
    public class CameraInfoDownloadingTests
    {
        [Test]
        public void DownloadImagesFromCameraInfoSources_WithTrusted_ShouldDownloadAndReturn()
        {
            // Arrange
            var fakeImageArray = new byte[100];
            Bitmap fakeBitmap;
            var url = "https://some.url";
            
            using (var stream = new MemoryStream(fakeImageArray))
            {
                fakeBitmap = new Bitmap(stream);
            }
            
            var fakeWebClient = A.Fake<IWebClient>();
            A.CallTo(() => fakeWebClient.DownloadData(url))
                .Returns(fakeImageArray);
            
            var fakeWebClientFactory = A.Fake<IWebClientFactory>();
            A.CallTo(() => fakeWebClientFactory.BuildWebClient())
                .Returns(fakeWebClient);
            
            var fakeMissingPlates = new List<CameraInfo>()
            {
                new CameraInfo() {HostUrl = url, IsTrustedSource = true},
                new CameraInfo() {HostUrl = url + "/2", IsTrustedSource = false}
            };
            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();
            A.CallTo(() => fakeCameraInfoHelper.GetActiveCameras(true))
                .Returns(fakeMissingPlates);

            var fakeImageSaver = A.Fake<IImageSaver>();
            A.CallTo(() => fakeImageSaver.SaveImage(fakeBitmap));
            
            var cameraImageDownloading = new CameraImageDownloading(fakeCameraInfoHelper, fakeImageSaver, fakeWebClientFactory);
            
            // Act
            var resultCameraCount = cameraImageDownloading.DownloadImagesFromCameraInfoSources(isTrusted: true);
            
            // Assert
            A.CallTo(() => fakeCameraInfoHelper.GetActiveCameras(true)).MustHaveHappened();
            A.CallTo(() => fakeWebClient.DownloadData(url)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeImageSaver.SaveImage(fakeBitmap)).MustHaveHappened();
        }
    }
}