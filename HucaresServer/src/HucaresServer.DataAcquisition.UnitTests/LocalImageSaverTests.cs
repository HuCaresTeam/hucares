using System;
using System.Drawing;
using System.IO;
using FakeItEasy;
using NUnit.Framework;

namespace HucaresServer.DataAcquisition.UnitTests
{
    [TestFixture]
    public class LocalImageSaverTests
    {
        [Test]
        public void SaveImage_WhenCalled_ShouldNotCreateDirectory_AndReturnSuccess()
        {
            var cameraId = 5;
            var captureDateTime = new DateTime(2018, 06, 06);
            var fakeBitmap = new Bitmap(100, 100);
            var fullDirectoryName = "/temporaryImages/" +
                                    cameraId + "/" +
                                    captureDateTime.Year.ToString() + "/" +
                                    captureDateTime.Month.ToString() + "/" +
                                    captureDateTime.Day.ToString();
                                    
            
            var fakeImageSaver = A.Fake<IImageSaver>();
            A.CallTo(() => fakeImageSaver.SaveImage(cameraId, captureDateTime, fakeBitmap)).MustHaveHappened();
            Assert.AreEqual(false, Directory.CreateDirectory(fullDirectoryName));
        }
        
        [Test]
        public void SaveImage_WhenCalled_ShouldCreateDirectory_AndReturnSuccess()
        {
            var cameraId = 5;
            var baseStorageLink = @"C:/temporaryImages/";
            var captureDateTime = new DateTime(2018, 06, 06);
            var fakeBitmap = new Bitmap(100, 100);
            var fullDirectoryName = baseStorageLink +
                                    cameraId + "/" +
                                    captureDateTime.Year.ToString() + "/" +
                                    captureDateTime.Month.ToString() + "/" +
                                    captureDateTime.Day.ToString();
                                    
            
            var fakeImageSaver = A.Fake<IImageSaver>();
            A.CallTo(() => fakeImageSaver.SaveImage(cameraId, captureDateTime, fakeBitmap)).MustHaveHappened();
            Assert.AreEqual(true, Directory.CreateDirectory(fullDirectoryName));
        }
    }
}