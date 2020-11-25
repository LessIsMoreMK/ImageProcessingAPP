using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace ImageProcessingApp
{
    public class MainViewModel : Window
    {
        #region Fields
        private string _ImagePath;
        #endregion

        #region Properties
        public string ImagePath
        {
            get =>_ImagePath; 
            set
            {
                _ImagePath = value;
                OnPropertyChanged(() => ImagePath);
            }
        }
        #endregion

        #region Commands
        public ICommand LoadImageCommand { get; set; }
        public ICommand LoadDefaultImageCommand { get; set; }
        public ICommand SaveImageCommand { get; set; }
        public ICommand ConvertCommand { get; set; }
        public ICommand ConvertAsyncCommand { get; set; }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            LoadImageCommand = new RelayCommand(LoadImage);
            LoadDefaultImageCommand = new RelayCommand(LoadDefaultImage);
            SaveImageCommand = new RelayCommand(SaveImage);

            var t = new TreeViewItem()

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
        #endregion
    }
}
