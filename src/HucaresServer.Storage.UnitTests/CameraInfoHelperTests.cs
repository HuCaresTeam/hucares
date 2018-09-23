using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HucaresServer.Storage.Helpers;
using FakeItEasy;
using HucaresServer.Storage.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace HucaresServer.Storage.UnitTests
{
    
    [TestClass]
    public class CameraInfoHelperTests
    {
        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

            //Act
            var expectedHost = "http://localhost:5051/some/cam";
            var expectedLat = 53.124;
            var expectedLong = 27.375;
            var expectedTrust = true;
            var result = cameraInfoHelper.InsertCamera(expectedHost, expectedLat, expectedLong, expectedTrust);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeDbSet.Add(result))
                .MustHaveHappened();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustHaveHappenedOnceExactly();

            result.HostUrl.ShouldBe(expectedHost);
            result.Latitude.ShouldBe(expectedLat);
            result.Longitude.ShouldBe(expectedLong);
            result.IsTrustedSource.ShouldBe(expectedTrust);
        }

        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

            //Act & Assert
            Assert.ThrowsException<UriFormatException>(() => cameraInfoHelper.InsertCamera("someInvalidHost", 53.124, 27.375));

            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustNotHaveHappened();
        }

        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

            //Act
            var result = cameraInfoHelper.InsertCamera("http://localhost:5051/some/cam", 53.124, 27.375);

            //Assert
            Assert.IsFalse(result.IsTrustedSource, "Default IsTrustedSource value was not false");
        }


        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => cameraInfoHelper.UpdateCameraActivity(0, true));
        }

        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

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

        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

            //Act
            var result = cameraInfoHelper.GetCameraById(camInfoObj.Id);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();

            result.ShouldBe(camInfoObj);
        }

        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

            //Act
            var result = cameraInfoHelper.GetCameraById(0);

            //Assert
            result.ShouldBe(null);
        }

        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

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

        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

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

        [TestMethod]
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

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

            //Act
            var result = cameraInfoHelper.GetAllCameras(true);

            //Assert
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeHucaresContext.SaveChanges())
                .MustNotHaveHappened();

            result.Count().ShouldBe(1);
            result.FirstOrDefault().ShouldBe(camInfoObj);
        }
    }
}
