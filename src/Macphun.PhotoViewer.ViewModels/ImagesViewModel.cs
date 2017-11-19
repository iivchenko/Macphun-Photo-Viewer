// <copyright company="XATA">
//      Copyright (c) 2017 by Shogun, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <email>iivchenko@live.com</email>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Macphun.PhotoViewer.Wpf;

namespace Macphun.PhotoViewer.ViewModels
{
    public sealed class ImagesViewModel : INotifyPropertyChanged
    {
        private readonly IList<Uri> _uris;
        private BitmapImage _image;

        private int _currentIndex;

        public ImagesViewModel(IList<Uri> uris, int selected)
        {
            if (uris == null)
            {
                throw new ArgumentNullException(nameof(uris));
            }

            _uris = uris;

            _currentIndex = selected;
            _image = Load(_uris[_currentIndex]);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BitmapImage Image
        {
            get => _image;

            set
            {
                _image = value;
                
                OnPropertyChanged();
            }
        }

        public ICommand NextImageCommand => new RelayCommand(Next);

        public ICommand PreviousImageCommand => new RelayCommand(Previous);

        private static BitmapImage Load(Uri uri)
        {
            var bitmap = new BitmapImage();

            bitmap.BeginInit();

            bitmap.UriSource = uri;

            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        private static Task<BitmapImage> LoadAsync(Uri uri)
        {
            return Task.Factory.StartNew(() => Load(uri));
        }

        private async void Next()
        {
            if (_currentIndex + 1 <= _uris.Count - 1)
            {
                _currentIndex++;

                var image = await LoadAsync(_uris[_currentIndex]);

                Image = image;
            }
        }

        private async void Previous()
        {
            if (_currentIndex - 1 >= 0)
            {
                _currentIndex--;

                var image = await LoadAsync(_uris[_currentIndex]);

                Image = image;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
