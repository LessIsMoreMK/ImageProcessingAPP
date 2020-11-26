using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using ImageConverterLibrary;

namespace ImageProcessingApp
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private string _ImagePath;
        private ImageSource _ImageResult;
        #endregion

        #region Properties
        public string ImagePath
        {
            get => _ImagePath;
            set
            {
                _ImagePath = value;
                OnPropertyChanged(() => ImagePath);
            }
        }
        public ImageSource ImageResult
        {
            get { return _ImageResult; }
            set
            {
                _ImageResult = value;
                OnPropertyChanged(() => ImageResult);
            }
        }
        #endregion

        #region Commands
        public ICommand LoadImageCommand { get; set; }
        public ICommand LoadDefaultImageCommand { get; set; }
        public ICommand SaveImageCommand { get; set; }
        public ICommand ConvertRGBCommand { get; set; }
        public ICommand ConvertRGBAsyncCommand { get; set; }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            LoadImageCommand = new RelayCommand(LoadImage);
            LoadDefaultImageCommand = new RelayCommand(LoadDefaultImage);
            SaveImageCommand = new RelayCommand(SaveImage);
            ConvertRGBCommand = new RelayCommand(ConvertRGB);
            ConvertRGBAsyncCommand = new RelayCommand(ConvertRGBAsync);
        }
        #endregion

        #region Methods

        private void LoadImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
                ImagePath = open.FileName;
        }

        private void LoadDefaultImage()
        {
            ImagePath = Path.Combine(Environment.CurrentDirectory, "../../Images/default.jpg");
        }

        private void SaveImage()
        {

        }
        private void ConvertRGB()
        {
            ConvertRGB ConvertRGB = new ConvertRGB();
            ImageResult = ConvertRGB.ConvertRGBValue(_ImagePath);
        }
        private async void ConvertRGBAsync()
        {
            ConvertRGB ConvertRGB = new ConvertRGB();
            ImageResult = await ConvertRGB.ConvertRGBValueAsync(_ImagePath);
        }
        #endregion
    }
}
