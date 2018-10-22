using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FakeItEasy;
using HucaresServer.Storage.Helpers;
using HucaresServer.Storage.Models;
using NUnit.Framework;
using Shouldly;

namespace HucaresServer.Storage.UnitTests
{
    public class DetectedPlateHelperTests
    {

        [Test]
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
            var expectedDetectedPlate = new DetectedLicensePlate()
            {
                PlateNumber = "ABC001",
                DetectedDateTime = new DateTime(2018, 09, 29),
                CamId = 1,
                ImgUrl = "http://localhost:6969/images/cam01_21080929_235959",
                Confidence = 0.75
            };
            
            var result = detectedPlateHelper.InsertNewDetectedPlate(expectedDetectedPlate.PlateNumber, 
                expectedDetectedPlate.DetectedDateTime, expectedDetectedPlate.CamId, expectedDetectedPlate.ImgUrl, 
                expectedDetectedPlate.Confidence);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeDbSet.Add(result))
                .MustHaveHappened();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustHaveHappenedOnceExactly();
            
            result.ShouldBe(expectedDetectedPlate);
        }
        
        [Test]
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
            Assert.Throws<ArgumentException>(() => detectedPlateHelper.InsertNewDetectedPlate(
                "ABC001", new DateTime(2018, 09, 29),
                1, "http://localhost:6969/images/cam01_21080929_235959", 1.1));
            
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustNotHaveHappened();

        }
        
        [Test]
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
            Assert.Throws<UriFormatException>(() => detectedPlateHelper.InsertNewDetectedPlate(
                "ABC001", new DateTime(2018, 09, 29),
                1, "notValidUri", 0.75));
            
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustNotHaveHappened();

        }

        [Test]
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
                    Status = LicensePlateFoundStatus.NotFound
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
        
        [Test]
        public void GetAllActiveDetectedPlatesByPlateNumber_NoDates_ShouldReturnExpected()
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
                    SearchEndDateTime = null
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
            A.CallTo(() => fakeMissingPlateHelper.GetPlateRecordByPlateNumber(expectedDetectedPlate.PlateNumber))
                .Returns(fakeMissingPlateList);
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllActiveDetectedPlatesByPlateNumber(plateNumber: expectedDetectedPlate.PlateNumber);
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(expectedDetectedPlate);
        }
        
        [Test]
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
                    SearchEndDateTime = null
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
            A.CallTo(() => fakeMissingPlateHelper.GetPlateRecordByPlateNumber(expectedDetectedPlate.PlateNumber))
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
        
        [Test]
        public void GetAllActiveDetectedPlatesByPlateNumber_WithStartDateEarlierThanEndDate_ShouldThrowError()
        {
            //Arrange         
            var fakeHucaresContext = A.Fake<HucaresContext>();
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeMissingPlatesRecords = new List<MissingLicensePlate>()
            {
                new MissingLicensePlate() { PlateNumber = "ABC001" }
            };

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            A.CallTo(() => fakeMissingPlateHelper.GetPlateRecordByPlateNumber("ABC001"))
                .Returns(fakeMissingPlatesRecords);
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            Assert.Throws<ArgumentException>(() => detectedPlateHelper.GetAllActiveDetectedPlatesByPlateNumber(plateNumber: "ABC001",
                startDateTime: new DateTime(2018, 10, 05), endDateTime: new DateTime(2018, 09, 05)));
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustNotHaveHappened();
        }

        [Test]
        public void GetAllActiveDetectedPlatesByPlateNumber_WithEndDateNoStartDate_ShouldReturn()
        {
            //Arrange         
            var expectedDetectedPlate = new DetectedLicensePlate()
            {
                Id = 0, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 10, 10)
            };
            
            var fakeDetectedPlateList = new List<DetectedLicensePlate>(){
                expectedDetectedPlate,
                new DetectedLicensePlate()
                {
                    Id = 1, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 09, 10)
                },
                new DetectedLicensePlate()
                {
                    Id = 2, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 11, 10)
                }
            };
            
            var fakeMissingPlateList = new List<MissingLicensePlate>()
            {
                new MissingLicensePlate()
                {
                    Id = 0, 
                    PlateNumber = expectedDetectedPlate.PlateNumber,
                    SearchStartDateTime = new DateTime(2018, 10, 05)
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
            A.CallTo(() => fakeMissingPlateHelper.GetPlateRecordByPlateNumber(expectedDetectedPlate.PlateNumber))
                .Returns(fakeMissingPlateList);
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllActiveDetectedPlatesByPlateNumber(plateNumber: expectedDetectedPlate.PlateNumber,
                endDateTime: new DateTime(2018, 10, 15)).ToList();
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(expectedDetectedPlate);
        }
        
        [Test]
        public void GetAllActiveDetectedPlatesByPlateNumber_WithStartDateBeforeMLP_ShouldReturn()
        {
            //Arrange         
            var expectedDetectedPlate1 = new DetectedLicensePlate()
            {
                Id = 0, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 10, 13)
            };
            
            var expectedDetectedPlate2 = new DetectedLicensePlate()
            {
                Id = 1, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 10, 06)
            };
            
            var fakeDetectedPlateList = new List<DetectedLicensePlate>(){
                expectedDetectedPlate1,
                expectedDetectedPlate2,
                new DetectedLicensePlate()
                {
                    Id = 2, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 09, 10)
                },
                new DetectedLicensePlate()
                {
                    Id = 3, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 11, 10)
                }
            };
            
            var fakeMissingPlateList = new List<MissingLicensePlate>()
            {
                new MissingLicensePlate()
                {
                    Id = 0, 
                    PlateNumber = expectedDetectedPlate1.PlateNumber,
                    SearchStartDateTime = new DateTime(2018, 10, 05)
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
            A.CallTo(() => fakeMissingPlateHelper.GetPlateRecordByPlateNumber(expectedDetectedPlate1.PlateNumber))
                .Returns(fakeMissingPlateList);
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllActiveDetectedPlatesByPlateNumber(plateNumber: expectedDetectedPlate1.PlateNumber,
                startDateTime: new DateTime(2018, 09, 01), endDateTime: new DateTime(2018, 10, 15)).ToList();
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            result.Count().ShouldBe(2);
            result.ShouldBe(new List<DetectedLicensePlate>() { expectedDetectedPlate1, expectedDetectedPlate2 });
        }
        
        [Test]
        public void GetAllActiveDetectedPlatesByPlateNumber_WithEndDateAfterMLP_ShouldReturn()
        {
            //Arrange         
            var expectedDetectedPlate = new DetectedLicensePlate()
            {
                Id = 0, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 10, 10)
            };
            
            var fakeDetectedPlateList = new List<DetectedLicensePlate>(){
                expectedDetectedPlate,
                new DetectedLicensePlate()
                {
                    Id = 2, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 09, 10)
                },
                new DetectedLicensePlate()
                {
                    Id = 3, PlateNumber = "ABC001", DetectedDateTime = new DateTime(2018, 11, 10)
                }
            };
            
            var fakeMissingPlateList = new List<MissingLicensePlate>()
            {
                new MissingLicensePlate()
                {
                    Id = 0, 
                    PlateNumber = expectedDetectedPlate.PlateNumber,
                    SearchStartDateTime = new DateTime(2018, 10, 05),
                    SearchEndDateTime = new DateTime(2018, 10, 15)
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
            A.CallTo(() => fakeMissingPlateHelper.GetPlateRecordByPlateNumber(expectedDetectedPlate.PlateNumber))
                .Returns(fakeMissingPlateList);
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllActiveDetectedPlatesByPlateNumber(plateNumber: expectedDetectedPlate.PlateNumber,
                endDateTime: new DateTime(2018, 11, 25)).ToList();
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(expectedDetectedPlate);
        }
        

        [Test]
        public void GetAllDetectedPlatesByCamera_WithoutDates_ShouldReturn()
        {
            //Arrange      
            var expectedDetectedPlate = new DetectedLicensePlate()
            {
                Id = 0, CamId = 0
            };
            
            var fakeDetectedPlateList = new List<DetectedLicensePlate>(){
                expectedDetectedPlate,
                new DetectedLicensePlate()
                {
                    Id = 1,  CamId = 1
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
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllDetectedPlatesByCamera(cameraId: expectedDetectedPlate.CamId).ToList();
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(expectedDetectedPlate);
        }
        
        [Test]
        public void GetAllDetectedPlatesByCamera_WithDates_ShouldReturn()
        {
            //Arrange      
            var expectedDetectedPlate = new DetectedLicensePlate()
            {
                Id = 0, CamId = 0, DetectedDateTime = new DateTime(2018, 10, 10)
            };
            
            var fakeDetectedPlateList = new List<DetectedLicensePlate>(){
                expectedDetectedPlate,
                new DetectedLicensePlate()
                {
                    Id = 1, CamId = 0, DetectedDateTime = new DateTime(2018, 09, 10)
                }, 
                new DetectedLicensePlate()
                {
                    Id = 2, CamId = 0, DetectedDateTime = new DateTime(2018, 11, 30)
                },
                new DetectedLicensePlate()
                {
                    Id = 3, CamId = 1, DetectedDateTime = new DateTime(2018, 10, 10)
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
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var result = detectedPlateHelper.GetAllDetectedPlatesByCamera(cameraId: expectedDetectedPlate.CamId,
                startDateTime: new DateTime(2018, 10, 05), endDateTime: new DateTime(2018, 10, 15)).ToList();
            
            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustHaveHappened();
            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(expectedDetectedPlate);
        }

        [Test]
        public void GetAllDetectedPlatesByCamera_WithStartLaterThanEnd_ShouldThrow()
        {
            //Arrange         
            var fakeHucaresContext = A.Fake<HucaresContext>();
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act & assert
            Assert.Throws<ArgumentException>(() => detectedPlateHelper.GetAllDetectedPlatesByCamera(cameraId: 0,
                startDateTime: new DateTime(2018, 10, 05), endDateTime: new DateTime(2018, 09, 05)));
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustNotHaveHappened();
        }

        [Test]
        public void DeletePlatesOlderThanDatetime_WithCorrectDateNoMissingNumbers_ShouldReturn()
        {
            //Arrange 
            var expectedRemainingDetectedLicensePlate = new DetectedLicensePlate()
                {DetectedDateTime = new DateTime(2018, 10, 10), PlateNumber = "ABC123"};
            var expectedDeletedDetectedLicensePlate = new DetectedLicensePlate()
                {DetectedDateTime = new DateTime(2018, 09, 10), PlateNumber = "ABC123"};
            
            var fakeDetectedPlatesList = new List<DetectedLicensePlate>()
            {
                expectedRemainingDetectedLicensePlate,
                expectedDeletedDetectedLicensePlate
            };
            
            var fakeDbSetDetectedPlates = StorageTestsUtil.SetupFakeDbSet(fakeDetectedPlatesList.AsQueryable());
            
            var fakeHucaresContext = A.Fake<HucaresContext>();
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates)
                .Returns(fakeDbSetDetectedPlates);
            
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .Returns(new List<MissingLicensePlate>());
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var deletedPlates = detectedPlateHelper.DeletePlatesOlderThanDatetime(new DateTime(2018, 10, 01)).ToList();
            
            //Assert
            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates).MustHaveHappened();
            deletedPlates.Count.ShouldBe(1);
            deletedPlates.FirstOrDefault().ShouldBe(expectedDeletedDetectedLicensePlate);
            
            fakeHucaresContext.DetectedLicensePlates.FirstOrDefault()
                .ShouldBe(expectedRemainingDetectedLicensePlate);
        }

        [Test]
        public void DeletePlatesOlderThanDatetime_WithCorrectDateWithMissingNumber_ShouldReturn()
        {
            //Arrange 
            var expectedRemainingDetectedLicensePlate = new DetectedLicensePlate()
                {DetectedDateTime = new DateTime(2018, 09, 10), PlateNumber = "FFF999"};
            var expectedDeletedDetectedLicensePlate = new DetectedLicensePlate()
                {DetectedDateTime = new DateTime(2018, 09, 10), PlateNumber = "ABC123"};
            
            var fakeDetectedPlatesList = new List<DetectedLicensePlate>()
            {
                expectedRemainingDetectedLicensePlate,
                expectedDeletedDetectedLicensePlate
            };
            
            var fakeDbSetDetectedPlates = StorageTestsUtil.SetupFakeDbSet(fakeDetectedPlatesList.AsQueryable());
            
            var fakeHucaresContext = A.Fake<HucaresContext>();
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates)
                .Returns(fakeDbSetDetectedPlates);
            
            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .Returns(new List<MissingLicensePlate>()
                {
                    new MissingLicensePlate() {PlateNumber = expectedRemainingDetectedLicensePlate.PlateNumber}
                });
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act
            var deletedPlates = detectedPlateHelper.DeletePlatesOlderThanDatetime(new DateTime(2018, 10, 01)).ToList();
            
            //Assert
            A.CallTo(() => fakeHucaresContext.DetectedLicensePlates).MustHaveHappened();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords()).MustHaveHappened();
            
            deletedPlates.Count.ShouldBe(1);
            deletedPlates.FirstOrDefault().ShouldBe(expectedDeletedDetectedLicensePlate);
            
            fakeHucaresContext.DetectedLicensePlates.FirstOrDefault()
                .ShouldBe(expectedRemainingDetectedLicensePlate);
        }
        
        [Test]
        public void DeletePlatesOlderThanDatetime_WithDateInFuture_ShouldThrow()
        {
            //Arrange         
            var fakeHucaresContext = A.Fake<HucaresContext>();
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var fakeMissingPlateHelper = A.Fake<IMissingPlateHelper>();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords())
                .Returns(new List<MissingLicensePlate>());
            
            var detectedPlateHelper = new DetectedPlateHelper(fakeDbContextFactory, fakeMissingPlateHelper);
            
            //Act & assert
            Assert.Throws<ArgumentException>(() => detectedPlateHelper.DeletePlatesOlderThanDatetime(DateTime.Today.AddDays(2)));
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext()).MustNotHaveHappened();
            A.CallTo(() => fakeMissingPlateHelper.GetAllPlateRecords()).MustNotHaveHappened();
        }
    }
}
