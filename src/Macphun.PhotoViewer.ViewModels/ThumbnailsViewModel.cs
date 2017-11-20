// <copyright company="XATA">
//      Copyright (c) 2017 by Shogun, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <email>iivchenko@live.com</email>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Macphun.PhotoViewer.Wpf;

namespace Macphun.PhotoViewer.ViewModels
{
    public sealed class ThumbnailsViewModel
    {
        private readonly IList<Uri> _uriRepository;

        public ThumbnailsViewModel(IList<Uri> uriRepository)
        {
            if (uriRepository == null)
            {
                throw new ArgumentNullException(nameof(uriRepository));
            }

            _uriRepository = uriRepository;

            Thumbnails = new ObservableCollection<BitmapImage>();
        }

        public ObservableCollection<BitmapImage> Thumbnails { get; }

        public ICommand AddThumbnailsCommand => new DelegateCommand<IEnumerable<string>>(AddThumbnailsAsync);

        private static BitmapImage Load(Uri uri, int size)
        {
            var bitmap = new BitmapImage();

            bitmap.BeginInit();

            bitmap.UriSource = uri;

            bitmap.DecodePixelWidth = size;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        private async void AddThumbnailsAsync(IEnumerable<string> paths)
        {
            foreach (var uri in paths.Select(x => new Uri(x)))
            {
                await Task.Delay(1);

                try
                {
                    var image = await Task.Factory.StartNew(() => Load(uri, 200));
                    Thumbnails.Add(image);
                    _uriRepository.Add(uri);
                }
                catch
                {
                }
            }
        }
    }
}
