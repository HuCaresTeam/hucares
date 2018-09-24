using System;
using FakeItEasy;
using HucaresServer.Storage.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace HucaresServer.Storage.UnitTests
{
    [TestClass]
    public class CameraInfoHelperTests
    {
        [TestMethod]
        public void InsertCamera_WhenAllDataValid_ShouldCallFactoryAndReturnExpected()
        {
            //Arrange
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
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

            result.HostUrl.ShouldBe(expectedHost);
            result.Latitude.ShouldBe(expectedLat);
            result.Longitude.ShouldBe(expectedLong);
            result.IsTrustedSource.ShouldBe(expectedTrust);
        }

        [TestMethod]
        public void InsertCamera_WhenHostUrlIsMalformed_ShouldThrow()
        {
            //Arrange
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
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
            var fakeDbContextFactory = A.Fake<IDbContextFactory>();

            var fakeHucaresContext = A.Fake<HucaresContext>();
            A.CallTo(() => fakeDbContextFactory.BuildHucaresContext())
                .Returns(fakeHucaresContext);

            var cameraInfoHelper = new CameraInfoHelper(fakeDbContextFactory);

            //Act
            var result = cameraInfoHelper.InsertCamera("http://localhost:5051/some/cam", 53.124, 27.375);

            //Assert
            Assert.IsFalse(result.IsTrustedSource, "Default IsTrustedSource value was not false");
        }
    }
}
