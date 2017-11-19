// <copyright company="XATA">
//      Copyright (c) 2017 by Shogun, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <email>iivchenko@live.com</email>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Macphun.PhotoViewer.ViewModels;

namespace Macphun.PhotoViewer.Wpf.App
{
    public sealed class NavigationViewModel : INotifyPropertyChanged
    {
        private readonly ThumbnailsViewModel _thumbnailsViewModel;
        private readonly IList<Uri> _uriRepository;

        private object _selectedViewModel;

        public NavigationViewModel()
        {
            _uriRepository = new List<Uri>();
            _thumbnailsViewModel = new ThumbnailsViewModel(_uriRepository);

            SwitchToThumbnailView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object SelectedViewModel
        {
            get => _selectedViewModel;

            set
            {
                _selectedViewModel = value;
                
                OnPropertyChanged();
            }
        }

        public ICommand SwitchToThumbnailViewCommand => new RelayCommand(SwitchToThumbnailView);

        public ICommand SwitchToImageViewCommand => new DelegateCommand<BitmapImage>(SwitchToImageView);

        private void SwitchToThumbnailView()
        {
            SelectedViewModel = new NavigationThumbnailsViewModel(_thumbnailsViewModel, SwitchToImageViewCommand);
        }

        private void SwitchToImageView(BitmapImage image)
        {
            var index = _thumbnailsViewModel.Thumbnails.IndexOf(image);
            
            SelectedViewModel = new NavigationImagesViewModel(new ImagesViewModel(_uriRepository, index), SwitchToThumbnailViewCommand);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
