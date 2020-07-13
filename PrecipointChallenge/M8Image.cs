using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using OpenCvSharp;

namespace PrecipointChallenge
{
    public class M8Image
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                    _name = value;
            }
        }

        private Mat _savedImage;
        public Mat SavedImage
        {
            get { return _savedImage; }
            set
            {
                if (_savedImage != value)
                    _savedImage = value;
            }
        }

        private double _sharpness;
        public double Sharpness
        {
            get { return _sharpness; }
            set
            {
                if (_sharpness != value)
                    _sharpness = value;
            }
        }

        private int _place;
        public int Place
        {
            get { return _place; }
            set
            {
                if (_place != value)
                    _place = value;
            }
        }

        public M8Image(string name, int place)
        {
            this.Name = Path.GetFileNameWithoutExtension(name);
            this.SavedImage = new Mat(name, ImreadModes.Grayscale);
            this.Place = place;
            this.Sharpness = -1;
        }

        public void SetSharpness()
        {
            Mat dx = new Mat();
            Mat dy = new Mat();
            Cv2.Sobel(this.SavedImage, dx, MatType.CV_32F, 1, 0, 3);
            Cv2.Sobel(this.SavedImage, dy, MatType.CV_32F, 0, 1, 3);
            Cv2.Magnitude(dx, dy, dx);
            this.Sharpness = Cv2.Sum(dx)[0];
            //Cv2.Laplacian(SavedImage, dest, MatType.CV_64F);
            //
        }
    }
}
