using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace PrecipointChallenge.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        // Update the user on its current state
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

        // Images uploaded by the user
        private ObservableCollection<M8Image> _imageList;
        public ObservableCollection<M8Image> ImageList
        {
            get { return _imageList; }
            set
            {
                if (_imageList != value)
                {
                    _imageList = value;
                    RaisePropertyChanged("ImageList");
                }
            }
        }

        // Linked to the "Load your pictures" button
        public RelayCommand UploadCommand { get; private set; }

        // Initializing variables
        public MainViewModel()
        {
            this.Message = "No images available";
            this.ImageList = new ObservableCollection<M8Image>();
            this.UploadCommand = new RelayCommand(this.UploadCommandMethod);
        }

        // Allow the user to pic several pictures
        public void UploadCommandMethod()
        {
            var dialog = new OpenFileDialog { InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory };
            dialog.DefaultExt = ".png";
            dialog.Filter = "Images (*.jpeg;*.png;*.jpg)|*.jpeg;*.png;*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            dialog.Multiselect = true;
            dialog.Title = "Select your images";
            DialogResult dr = dialog.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                int i = 0;
                foreach (String file in dialog.FileNames)
                {
                    M8Image m8Image = new M8Image(file, i);
                    this.ImageList.Add(m8Image);
                    i++;
                }
                this.Message = "Calculating...";
                CalculateSharpness();
            }
        }

        // Tell to the user which is the sharpest picture and where it is plced in the list
        public void CalculateSharpness()
        {
            foreach (M8Image img in this.ImageList)
            {
                img.SetSharpness();
            }
            M8Image result = ImageList.OrderByDescending(x => x.Sharpness).FirstOrDefault();
            this.Message = "The sharpest image is " + result.Name + " placed " + result.Place.ToString() + " in the list";
        }
    }
}