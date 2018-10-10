using NUnit.Framework;

namespace HucaresServer.Utils.UnitTests
{
    public class PlateNumberValidationTests
    {
        [TestCase("TRV123")]
        [TestCase("ABC456")]
        [TestCase("DET842")]
        [Test]
        public void IsValidPlateNumber_WhenPlateNumberValid_ShouldReturnTrue(string plateNumber)
        {
            //Arrange
            //Act
            //Assert
            Assert.IsTrue(plateNumber.IsValidPlateNumber(), "Regex did not match");
        }

        [TestCase("")]
        [TestCase("TRV-123")]
        [TestCase("ABC:456")]
        [TestCase("DET 842")]
        [TestCase("ABC456 AED782")]
        [Test]
        public void IsValidPlateNumber_WhenPlateNumberInvalid_ShouldReturnFalse(string plateNumber)
        {
            //Arrange
            //Act
            //Assert
            Assert.IsFalse(plateNumber.IsValidPlateNumber(), "Regex did not match");
        }
    }
}
