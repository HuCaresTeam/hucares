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
        public void UpdateCameraActivity_WhenRecordWithIdExists_ShouldSucceedAndReturnExpected()
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
    }
}
