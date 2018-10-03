using FakeItEasy;
using HucaresServer.Controllers;
using HucaresServer.Storage.Helpers;
using HucaresServer.Storage.Models;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static HucaresServer.Models.MissingLicensePlateDataModels;

namespace HucaresServer.UnitTests
{
    [TestClass]
    public class MissingPlateControllerTests
    {
        [TestMethod]
        public async Task GetAllMissingPlates_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();

            var expectedMlpList = new List<MissingLicensePlate>() { new MissingLicensePlate() { PlateNumber = "ZOA:555" } };
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .Returns(expectedMlpList);

            var missingPlateController = new MissingPlateController() { MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage() };

            //Act
            var result = missingPlateController.GetAllMissingPlates();

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMlpList);

            jsonContent.ShouldBe(expectedJson);
        }

        [TestMethod]
        public async Task GetAllMissingPlatesByPlateNumber_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();

            var expectedMlpList = new List<MissingLicensePlate>() { new MissingLicensePlate() { Id = 0 } };
            var expectedPlateNumber = "ZOA:111";
            A.CallTo(() => fakeMissingPlateHelper
                .GetPlateRecordByPlateNumber(expectedPlateNumber))
                .Returns(expectedMlpList);

            var missingPlateController = new MissingPlateController() { MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage() };

            //Act
            var result = missingPlateController.GetAllMissingPlatesByPlateNumber(expectedPlateNumber);

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.GetPlateRecordByPlateNumber(expectedPlateNumber))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMlpList);

            jsonContent.ShouldBe(expectedJson);
        }
        
        [TestMethod]
        public async Task InsertMissingPlate_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var expectedPlateNumber = "PZU:555";
            var expectedStartDateTime = new DateTime(2018, 05, 04);
            var expectedDataModel = new PostPlateRecordDataModel()
            {
                PlateNumber = expectedPlateNumber,
                SearchStartDateTime = expectedStartDateTime,
            };

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();

            var expectedMissingPlateInfo = new MissingLicensePlate() { PlateNumber = "WWW:111" };
            A.CallTo(() => fakeMissingPlateHelper.InsertPlateRecord(expectedPlateNumber, expectedStartDateTime))
                .Returns(expectedMissingPlateInfo);

            var mlpController = new MissingPlateController() { MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage() };

            //Act
            var result = mlpController.InsertMissingPlateByNumber(expectedDataModel);

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.InsertPlateRecord(expectedPlateNumber, expectedStartDateTime))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMissingPlateInfo);

            jsonContent.ShouldBe(expectedJson);
        }

        [TestMethod]
        public async Task UpdateMissingPlateRecord_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var expectedPlateNumber = "BAA:254";
            var expectedSearchDateTime = new DateTime(2018, 05, 17);
            var expectedDataModel = new PostPlateRecordDataModel()
            {
                PlateNumber = expectedPlateNumber,
                SearchStartDateTime = expectedSearchDateTime,
            };

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();

            var expectedId = 5;
            var expectedMissingPlateInfo = new MissingLicensePlate() {Id = expectedId};
            A.CallTo(() => fakeMissingPlateHelper.UpdatePlateRecord(expectedId, expectedPlateNumber, expectedSearchDateTime))
                .Returns(expectedMissingPlateInfo);

            var mlpController = new MissingPlateController()
                {MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage()};

            //Act
            var result = mlpController.UpdatePlateRecordById(expectedId, expectedDataModel);

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.UpdatePlateRecord(expectedId, expectedPlateNumber, expectedSearchDateTime))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMissingPlateInfo);

            jsonContent.ShouldBe(expectedJson);
        }

        [TestMethod]
        public async Task MarkFoundMissingPlate_WhenCalled_ShouldCallHelper()
        {
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();

            var expectedId = 5;
            var requestedDateTime = new DateTime(2018, 06, 22);
            var expectedSearch = true;
            var expectedMissingPlateInfo = new MissingLicensePlate() {Id = expectedId};
            A.CallTo(() => fakeMissingPlateHelper.MarkFoundPlate(expectedId, requestedDateTime, expectedSearch))
                .Returns(expectedMissingPlateInfo);

            var mlpController = new MissingPlateController()
                {MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage()};
            //Act
            var result = mlpController.MarkFoundMissingPlate(expectedId, requestedDateTime, expectedSearch);

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.MarkFoundPlate(expectedId, requestedDateTime, expectedSearch))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMissingPlateInfo);

            jsonContent.ShouldBe(expectedJson);
        }

        [TestMethod]
        public async Task DeletePlateById_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();

            var expectedPlateId = 0;
            var expectedMissingPlateInfo = new MissingLicensePlate() { Id = expectedPlateId};
            A.CallTo(() => fakeMissingPlateHelper.DeletePlateById(expectedPlateId))
                .Returns(expectedMissingPlateInfo);

            var mlpController = new MissingPlateController() { MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage() };

            //Act
            var result = mlpController.DeletePlateRecordById(expectedPlateId);

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.DeletePlateById(expectedPlateId))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMissingPlateInfo);

            jsonContent.ShouldBe(expectedJson);
        }
        
        [TestMethod]
        public async Task DeletePlateByNumber_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();

            var expectedPlateNumber = "RRR:587";
            var expectedMissingPlateInfo = new MissingLicensePlate() { PlateNumber = expectedPlateNumber};
            A.CallTo(() => fakeMissingPlateHelper.DeletePlateByNumber(expectedPlateNumber))
                .Returns(expectedMissingPlateInfo);

            var mlpController = new MissingPlateController() { MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage() };

            //Act
            var result = mlpController.DeletePlateRecordByNumber(expectedPlateNumber);

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.DeletePlateByNumber(expectedPlateNumber))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMissingPlateInfo);

            jsonContent.ShouldBe(expectedJson);
        }
    }
}