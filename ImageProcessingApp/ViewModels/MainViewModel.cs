using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using ImageConverterLibrary;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace ImageProcessingApp
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private string _ImagePath;
        private ImageSource _ImageResult;
        private string _Time;
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
        public string Time
        {
            get => _Time;
            set
            {
                _Time = value;
                OnPropertyChanged(() => Time);
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
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save Image";
            saveFileDialog1.ShowDialog();


            if (saveFileDialog1.ShowDialog() ?? false)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapImage)_ImageResult));

                using (var fileStream = new System.IO.FileStream(_ImagePath, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
        }
        private void ConvertRGB()
        {
            var watch = Stopwatch.StartNew();
            ConvertRGB ConvertRGB = new ConvertRGB();
            ImageResult = ConvertRGB.ConvertRGBValue(_ImagePath);
            Time = "Convert working time: " + watch.ElapsedMilliseconds + " ms";
        }
        private async void ConvertRGBAsync()
        {
            var watch = Stopwatch.StartNew();
            ConvertRGB ConvertRGB = new ConvertRGB();
            ImageResult = await ConvertRGB.ConvertRGBValueAsync(_ImagePath);
            Time = "Convert working time: " + watch.ElapsedMilliseconds + " ms";
        }
        #endregion
    }
}
