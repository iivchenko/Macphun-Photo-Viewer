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

            Thumbnails = new ObservableCollection<ThumbnailViewModel>();
        }

        public ObservableCollection<ThumbnailViewModel> Thumbnails { get; }

        public ICommand AddThumbnailsCommand => new DelegateCommand<IEnumerable<string>>(AddThumbnailsAsync);

        private IEnumerable<ThumbnailViewModel> AddThumbnails(IEnumerable<string> paths)
        {
            var uris = paths.Select(x => new Uri(x)).ToList();

            foreach (var uri in uris)
            {
                _uriRepository.Add(uri);
            }

            return uris.Select(x => new ThumbnailViewModel(x));
        }

        private async void AddThumbnailsAsync(IEnumerable<string> paths)
        {
            var thumbnails = await Task.Factory.StartNew(() => AddThumbnails(paths));

            foreach (var thumbnail in thumbnails)
            {
                Thumbnails.Add(thumbnail);
            }
        }
    }
}
