using System.Drawing;

namespace HucaresServer.Utils
{
    public interface IImageWrapper
    {
        Image GetImageFromFile(string pathToPlateImage);
    }
}