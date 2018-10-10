using FakeItEasy;
using HucaresServer.Controllers;
using HucaresServer.Storage.Helpers;
using HucaresServer.Storage.Models;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using static HucaresServer.Models.CameraInfoDataModels;

namespace HucaresServer.UnitTests
{
    public class CameraInfoControllerTests
    {
        [Test]
        public async Task InsertCamera_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var expectedHost = "host";
            var expectedLat = 0.0;
            var expectedLong = 0.0;
            var expectedTrust = false;
            var expectedDataModel = new InsertCameraDataModel()
            {
                HostUrl = expectedHost,
                Latitude = expectedLat,
                Longitude = expectedLong,
                IsTrustedSource = expectedTrust
            };

            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();

            var expectedCameraInfo = new CameraInfo() { Id = 0 };
            A.CallTo(() => fakeCameraInfoHelper.InsertCamera(expectedHost, expectedLat, expectedLong, expectedTrust))
                .Returns(expectedCameraInfo);

            var cameraController = new CameraInfoController() { CameraInfoHelper = fakeCameraInfoHelper, Request = new HttpRequestMessage() };

            //Act
            var result = cameraController.InsertCamera(expectedDataModel);

            //Assert
            A.CallTo(() => fakeCameraInfoHelper.InsertCamera(expectedHost, expectedLat, expectedLong, expectedTrust))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedCameraInfo);

            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task UpdateCameraSource_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var expectedHost = "host";
            var expectedTrust = false;
            var expectedDataModel = new UpdateCameraSourceDataModel()
            {
                HostUrl = expectedHost,
                IsTrustedSource = expectedTrust
            };

            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();

            var expectedId = 0;
            var expectedCameraInfo = new CameraInfo() { Id = expectedId};
            A.CallTo(() => fakeCameraInfoHelper.UpdateCameraSource(expectedId, expectedHost, expectedTrust))
                .Returns(expectedCameraInfo);

            var cameraController = new CameraInfoController() { CameraInfoHelper = fakeCameraInfoHelper, Request = new HttpRequestMessage() };

            //Act
            var result = cameraController.UpdateCameraSource(expectedId, expectedDataModel);

            //Assert
            A.CallTo(() => fakeCameraInfoHelper.UpdateCameraSource(expectedId, expectedHost, expectedTrust))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedCameraInfo);

            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task UpdateCameraActivity_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var expectedActivity = false;
            var expectedDataModel = new UpdateCameraActivityDataModel()
            {
                IsActive = expectedActivity
            };

            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();

            var expectedId = 0;
            var expectedCameraInfo = new CameraInfo() { Id = expectedId };
            A.CallTo(() => fakeCameraInfoHelper.UpdateCameraActivity(expectedId, expectedActivity))
                .Returns(expectedCameraInfo);

            var cameraController = new CameraInfoController() { CameraInfoHelper = fakeCameraInfoHelper, Request = new HttpRequestMessage() };

            //Act
            var result = cameraController.UpdateCameraActivity(expectedId, expectedDataModel);

            //Assert
            A.CallTo(() => fakeCameraInfoHelper.UpdateCameraActivity(expectedId, expectedActivity))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedCameraInfo);

            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task DeleteCameraById_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();

            var exptectedId = 0;
            var expectedCameraInfo = new CameraInfo() { Id = exptectedId};
            A.CallTo(() => fakeCameraInfoHelper.DeleteCameraById(exptectedId))
                .Returns(expectedCameraInfo);

            var cameraController = new CameraInfoController() { CameraInfoHelper = fakeCameraInfoHelper, Request = new HttpRequestMessage() };

            //Act
            var result = cameraController.DeleteCameraById(exptectedId);

            //Assert
            A.CallTo(() => fakeCameraInfoHelper.DeleteCameraById(exptectedId))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedCameraInfo);

            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task GetAllCameras_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();

            var expectedTrust = false;
            var expectedCameraInfoList = new List<CameraInfo>() { new CameraInfo() { Id = 0 } };
            A.CallTo(() => fakeCameraInfoHelper.GetAllCameras(expectedTrust))
                .Returns(expectedCameraInfoList);

            var cameraController = new CameraInfoController() { CameraInfoHelper = fakeCameraInfoHelper, Request = new HttpRequestMessage() };

            //Act
            var result = cameraController.GetAllCameras(expectedTrust);

            //Assert
            A.CallTo(() => fakeCameraInfoHelper.GetAllCameras(expectedTrust))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedCameraInfoList);

            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task GetActiveCameras_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();

            var expectedTrust = false;
            var expectedCameraInfoList = new List<CameraInfo>() { new CameraInfo() { Id = 0 } };
            A.CallTo(() => fakeCameraInfoHelper.GetActiveCameras(expectedTrust))
                .Returns(expectedCameraInfoList);

            var cameraController = new CameraInfoController() { CameraInfoHelper = fakeCameraInfoHelper, Request = new HttpRequestMessage() };

            //Act
            var result = cameraController.GetActiveCameras(expectedTrust);

            //Assert
            A.CallTo(() => fakeCameraInfoHelper.GetActiveCameras(expectedTrust))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedCameraInfoList);

            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task GetInactiveCameras_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();

            var expectedCameraInfoList = new List<CameraInfo>() { new CameraInfo() { Id = 0 } };
            A.CallTo(() => fakeCameraInfoHelper.GetInactiveCameras())
                .Returns(expectedCameraInfoList);

            var cameraController = new CameraInfoController() { CameraInfoHelper = fakeCameraInfoHelper, Request = new HttpRequestMessage() };

            //Act
            var result = cameraController.GetInactiveCameras();

            //Assert
            A.CallTo(() => fakeCameraInfoHelper.GetInactiveCameras())
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedCameraInfoList);

            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task GetCameraById_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeCameraInfoHelper = A.Fake<ICameraInfoHelper>();

            var expectedId = 0;
            var expectedCameraInfoList = new CameraInfo() { Id = expectedId };
            A.CallTo(() => fakeCameraInfoHelper.GetCameraById(expectedId))
                .Returns(expectedCameraInfoList);

            var cameraController = new CameraInfoController() { CameraInfoHelper = fakeCameraInfoHelper, Request = new HttpRequestMessage() };

            //Act
            var result = cameraController.GetCameraById(expectedId);

            //Assert
            A.CallTo(() => fakeCameraInfoHelper.GetCameraById(expectedId))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedCameraInfoList);

            jsonContent.ShouldBe(expectedJson);
        }
    }
}
