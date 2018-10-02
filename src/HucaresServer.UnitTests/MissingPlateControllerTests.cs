using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using static HucaresServer.Models.CameraInfoDataModels;

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

            var expectedMLPList = new List<MissingLicensePlate>() { new MissingLicensePlate() { PlateNumber = "ZOA:555" } };
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .Returns(expectedMLPList);

            var missingPlateController = new MissingPlateController() { MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage() };

            //Act
            var result = missingPlateController.GetAllMissingPlates();

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMLPList);

            jsonContent.ShouldBe(expectedJson);
        }

        [TestMethod]
        public async Task GetAllMissingPlatesByPlateNumber_WhenCalled_ShouldCallHelper()
        {
            //Arrange
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();

            var expectedMLPList = new List<MissingLicensePlate>() { new MissingLicensePlate() { Id = 0 } };
            var expectedPlateNumber = "ZOA:111";
            A.CallTo(() => fakeMissingPlateHelper
                .GetPlateRecordByPlateNumber(expectedPlateNumber))
                .Returns(expectedMLPList);

            var missingPlateController = new MissingPlateController() { MissingPlateHelper = fakeMissingPlateHelper, Request = new HttpRequestMessage() };

            //Act
            var result = missingPlateController.GetAllMissingPlatesByPlateNumber(expectedPlateNumber);

            //Assert
            A.CallTo(() => fakeMissingPlateHelper.GetPlateRecordByPlateNumber(expectedPlateNumber))
                .MustHaveHappenedOnceExactly();

            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedMLPList);

            jsonContent.ShouldBe(expectedJson);
        }
    }
}