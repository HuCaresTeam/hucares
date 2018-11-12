using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HucaresServer.Utils
{
    public class ImageWrapper : IImageWrapper
    {
        public Image GetImageFromFile(string pathToPlateImage)
        {
            return Image.FromFile(pathToPlateImage);
        }
    }
}
