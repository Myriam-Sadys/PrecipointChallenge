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
        // Name of the picture
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

        // The picture
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
        
        //
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

        // Placement of the picture in the stack
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
            // Save the picture in grayscale because the sharpness is then esier to calculate
            this.SavedImage = new Mat(name, ImreadModes.Grayscale);
            this.Place = place;
            this.Sharpness = -1;
            this.Name = Path.GetFileNameWithoutExtension(name);
        }

        // Calculate the sharpness of the image
        public void SetSharpness()
        {
            Mat dx = new Mat();
            Mat dy = new Mat();
            // get the horizontal edges
            Cv2.Sobel(this.SavedImage, dx, MatType.CV_32F, 1, 0, 3);
            // get the vertical edges
            Cv2.Sobel(this.SavedImage, dy, MatType.CV_32F, 0, 1, 3);
            // calculate their magnitudes
            Cv2.Magnitude(dx, dy, dx);
            // add every magnitudes
            this.Sharpness = Cv2.Sum(dx)[0];
        }
    }
}
