﻿using System;
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
    public class DetectedPlateHelperTests
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
            
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);

            //Act
            var expectedPlateNumber = "ABC001";
            var expectedDetectedDateTime = new DateTime(2018, 09, 29);
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
            
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);

            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => detectedPlateHelper.InsertNewDetectedPlate(
                "ABC001", new DateTime(2018, 09, 29),
                1, "http://localhost:6969/images/cam01_21080929_235959", 1.1));
            
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustNotHaveHappened();

        }
        
        [TestMethod]
        public void InsertNewDetectedPlate_WhenUriNotCorrect_ShouldThrowError()
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
            
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);

            //Act and Assert
            Assert.ThrowsException<UriFormatException>(() => detectedPlateHelper.InsertNewDetectedPlate(
                "ABC001", new DateTime(2018, 09, 29),
                1, "notValidUri", 0.75));
            
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustNotHaveHappened();

        }

        [TestMethod]
        public void GetAllDetectedMissingPlates_ShouldReturn()
        {
            //Arrange
            
            var expectedDetectedPlate = new DetectedLicensePlate()
            {
                Id = 1, PlateNumber = "ABC002", DetectedDateTime = new DateTime(2018, 09, 30), 
                CamId = 2, ImgUrl = "http://localhost:6969/images", Confidence = 0.80
            };
            
            var fakeDetectedPlateList = new List<DetectedLicensePlate>(){
                expectedDetectedPlate,
                new DetectedLicensePlate()
                {
                    Id = 0, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 09, 29), 
                    CamId = 1, ImgUrl = "http://localhost:6969/images", Confidence = 0.75
                },
                new DetectedLicensePlate()
                {
                    Id = 2, PlateNumber = "ABC003", DetectedDateTime = new DateTime(2018, 09, 30), 
                    CamId = 2, ImgUrl = "http://localhost:6969/images", Confidence = 0.80
                }
            };
            
            var fakeMissingPlateList = new List<MissingLicensePlate>()
            {
                new MissingLicensePlate()
                {
                    Id = 0, 
                    PlateNumber = expectedDetectedPlate.PlateNumber,
                    SearchStartDateTime = new DateTime(2018, 09, 29),
                    SearchEndDateTime = null, 
                    LicensePlateFound = false
                }
            };

            var fakeDbSetDetectedPlates = StorageTestsUtil.SetupFakeDbSet(fakeDetectedPlateList.AsQueryable());
            var fakeHucaresContext = A.Fake<HucaresContext>();
            
            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates)
                .Returns(fakeDbSetDetectedPlates);
            
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .Returns(fakeMissingPlateList);
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllDetectedMissingPlates();
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords()).MustHaveHappened();
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(expectedDetectedPlate);
        }
        
        [TestMethod]
        public void GetAllActiveDetectedPlatesByPlateNumber_WithPlateNumberNoDates_ShouldReturnExpected()
        {       
            //Arrange         
            var expectedDetectedPlate = new DetectedLicensePlate()
            {
                Id = 1, PlateNumber = "ABC002", DetectedDateTime = new DateTime(2018, 09, 30), 
                CamId = 2, ImgUrl = "http://localhost:6969/images", Confidence = 0.80
            };
            
            var fakeDetectedPlateList = new List<DetectedLicensePlate>(){
                expectedDetectedPlate,
                new DetectedLicensePlate()
                {
                    Id = 0, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 09, 29), 
                    CamId = 1, ImgUrl = "http://localhost:6969/images", Confidence = 0.75
                },
                new DetectedLicensePlate()
                {
                    Id = 2, PlateNumber = "ABC003", DetectedDateTime = new DateTime(2018, 09, 30), 
                    CamId = 2, ImgUrl = "http://localhost:6969/images", Confidence = 0.80
                }
            };
            
            var fakeMissingPlateList = new List<MissingLicensePlate>()
            {
                new MissingLicensePlate()
                {
                    Id = 0, 
                    PlateNumber = expectedDetectedPlate.PlateNumber,
                    SearchStartDateTime = new DateTime(2018, 09, 29),
                    SearchEndDateTime = null, 
                    LicensePlateFound = false
                },
                new MissingLicensePlate()
                {
                    Id = 1, PlateNumber = "ABC001", SearchStartDateTime = new DateTime(2018, 04, 15),
                    SearchEndDateTime = null, LicensePlateFound = false
                }
            };

            var fakeDbSetDetectedPlates = StorageTestsUtil.SetupFakeDbSet(fakeDetectedPlateList.AsQueryable());
            
            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates)
                .Returns(fakeDbSetDetectedPlates);
            
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .Returns(fakeMissingPlateList);
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllActiveDetectedPlatesByPlateNumber(plateNumber: expectedDetectedPlate.PlateNumber);
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(expectedDetectedPlate);
        }
        
        [TestMethod]
        public void GetAllActiveDetectedPlatesByPlateNumber_WithPlateNumberWithDates_ShouldReturnExpected()
        {
             //Arrange         
            var expectedDetectedPlate = new DetectedLicensePlate()
            {
                Id = 1, PlateNumber = "ABC002", DetectedDateTime = new DateTime(2018, 10, 10), 
                CamId = 2, ImgUrl = "http://localhost:6969/images", Confidence = 0.80
            };
            
            var fakeDetectedPlateList = new List<DetectedLicensePlate>(){
                expectedDetectedPlate,
                new DetectedLicensePlate()
                {
                    Id = 0, PlateNumber = "ABC002", DetectedDateTime = new DateTime(2018, 09, 30), 
                    CamId = 1, ImgUrl = "http://localhost:6969/images", Confidence = 0.75
                },
                new DetectedLicensePlate()
                {
                    Id = 2, PlateNumber = "ABC002", DetectedDateTime = new DateTime(2018, 09, 20), 
                    CamId = 2, ImgUrl = "http://localhost:6969/images", Confidence = 0.80
                }
            };
            
            var fakeMissingPlateList = new List<MissingLicensePlate>()
            {
                new MissingLicensePlate()
                {
                    Id = 0, 
                    PlateNumber = expectedDetectedPlate.PlateNumber,
                    SearchStartDateTime = new DateTime(2018, 09, 25),
                    SearchEndDateTime = null, 
                    LicensePlateFound = false
                }
            };

            var fakeDbSetDetectedPlates = StorageTestsUtil.SetupFakeDbSet(fakeDetectedPlateList.AsQueryable());
            
            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates)
                .Returns(fakeDbSetDetectedPlates);
            
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .Returns(fakeMissingPlateList);
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllActiveDetectedPlatesByPlateNumber(plateNumber: expectedDetectedPlate.PlateNumber,
                startDateTime: new DateTime(2018, 10, 05));
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(expectedDetectedPlate);
        }
        
        [TestMethod]
        public void GetAllActiveDetectedPlatesByPlateNumber_WithStartDateEarlierThanEndDate_ShouldThrowError()
        {
            //Arrange         
            var fakeHucaresContext = A.Fake<HucaresContext>();
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            Assert.ThrowsException<ArgumentException>(() => detectedPlateHelper.GetAllActiveDetectedPlatesByPlateNumber(plateNumber: "ABC001",
                startDateTime: new DateTime(2018, 10, 05), endDateTime: new DateTime(2018, 09, 05)));
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustNotHaveHappened();
        }
    }
}
