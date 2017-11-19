// <copyright company="XATA">
//      Copyright (c) 2017 by Shogun, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <email>iivchenko@live.com</email>

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Macphun.PhotoViewer.Wpf;

namespace Macphun.PhotoViewer.ViewModels
{
    public sealed class ThumbnailViewModel : INotifyPropertyChanged
    {
        private Uri _uri;
        private BitmapImage _image;

        public ThumbnailViewModel(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            Uri = uri;

            Image = Load(200);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Uri Uri
        {
            get => _uri;

            private set
            {
                _uri = value;
                
                OnPropertyChanged();
            }
        }

        public BitmapImage Image
        {
            get => _image;

            private set
            {
                _image = value;
                
                OnPropertyChanged();
            }
        }

        public ICommand LoadCommand => new DelegateCommand<int>(LoadAsync);

        public ICommand UnloadCommand => new RelayCommand(UnloadAsync);

        private static BitmapImage Unload()
        {
            return new BitmapImage();
        }

        private async void LoadAsync(int size)
        {
            Image = await Task.Factory.StartNew(() => Load(size));
        }

        private BitmapImage Load(int size)
        {
            var bitmap = new BitmapImage();

            bitmap.BeginInit();

            bitmap.UriSource = _uri;

            bitmap.DecodePixelWidth = size;
            bitmap.EndInit();

            return bitmap;
        }

        private async void UnloadAsync()
        {
            Image = await Task.Factory.StartNew(Unload);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
