using NUnit.Framework;
using Shouldly;
using System.IO;

namespace HucaresServer.Utils.UnitTests
{
    [TestFixture]
    public class MemoryStreamFactoryTests
    {
        [Test]
        public void Create_WhenCalled_ShouldReturnMemoryStream ()
        {
            //Arrange
            var factory = new MemoryStreamFactory();

            //Act
            var result = factory.Create();

            //Assert
            result.ShouldBeOfType(typeof(MemoryStream));
            result.ToArray().ShouldBeEmpty();
        }
    }
}
