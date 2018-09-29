using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FakeItEasy;
using HucaresServer.Storage.Helpers;
using HucaresServer.Storage.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace HucaresServer.Storage.UnitTests
{
    [TestClass]
    class DetecteedPlateHelperTests
    {

        [TestMethod]
        public void InsertNewDetectedPlate_WhenAllInfoCorrect_ShouldSucceedAndReturn()
        {

            //Arrange
            var fakeIQueryable = new List<DetectedLicensePlate>().AsQueryable();
            var fakeDbSet = A.Fake<DbSet<DetectedLicensePlate>>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory);

            //Act
            var expectedPlateNumber = "ABC001";
            var expectedDetectedDateTime = DateTime.FromOADate(1538217623);
            var expectedCamId = 1;
            var expectedImgUrl = "http://localhost:6969/images/cam01_21080929_235959";
            var expectedConfidence = 0.75;
            
            var result = detectedPlateHelper.InsertNewDetectedPlate(expectedPlateNumber, expectedDetectedDateTime,
                expectedCamId, expectedImgUrl, expectedConfidence);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeDbSet.Add(result))
                .MustHaveHappened();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustHaveHappenedOnceExactly();
            
            result.PlateNumber.ShouldBe(expectedPlateNumber);
            result.DetectedDateTime.ShouldBe(expectedDetectedDateTime);
            result.CamId.ShouldBe(expectedCamId);
            result.ImgUrl.ShouldBe(expectedImgUrl);
            result.Confidence.ShouldBe(expectedConfidence);

        }
        
        [TestMethod]
        public void InsertNewDetectedPlate_WhenConfidenceNotCorrect_ShouldThrowError()
        {

            //Arrange
            var fakeIQueryable = new List<DetectedLicensePlate>().AsQueryable();
            var fakeDbSet = A.Fake<DbSet<DetectedLicensePlate>>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates)
                .Returns(fakeDbSet);

            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory);

            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => detectedPlateHelper.InsertNewDetectedPlate(
                "ABC001", DateTime.FromOADate(1538217623),
                1, "http://localhost:6969/images/cam01_21080929_235959", 1.1));
            
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustNotHaveHappened();

        }
    }
}
