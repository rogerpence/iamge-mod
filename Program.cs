using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace ResizeImage
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("ResizeImage <imagePath> <newWidth>");
                return;
            }

            string imagePath = args[0];
            int newWidth = Convert.ToInt32(args[1]);

            ImageManager im = new ImageManager();

            im.ResizeImage(imagePath, newWidth);
       }
    }

    class ImageManager
    {
        public int GetImageWidth(string imagePath)
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var image = Image.FromStream(fileStream, false, false))
                {
                    var height = image.Height;
                    var width = image.Width;
                    return width;
                }
            }
        }

        private Image createNewImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public void ResizeImage(string imagePath, int newWidth=650)
        {
            int height;
            int width;
            Bitmap imgbitmap;
            using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var image = Image.FromStream(fileStream, false, false))
                {
                    height = image.Height;
                    width = image.Width;

                    if (width <= newWidth)
                    {
                        return;
                    }
                     imgbitmap = new Bitmap(image);
                }
            }
            int newHeight = (height * newWidth / width);

            Size size = new Size(newWidth, newHeight);

            Image newImage = createNewImage(imgbitmap, size);
            newImage.Save(imagePath);
        }
    }
}