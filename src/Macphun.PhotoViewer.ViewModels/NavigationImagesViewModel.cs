// <copyright company="XATA">
//      Copyright (c) 2017 by Shogun, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <email>iivchenko@live.com</email>

using System;
using System.Windows.Input;

namespace Macphun.PhotoViewer.ViewModels
{
    public sealed class NavigationImagesViewModel
    {
        public NavigationImagesViewModel(ImagesViewModel viewModel, ICommand switchToThumbnailsViewCommand)
        {
            if (switchToThumbnailsViewCommand == null)
            {
                throw new ArgumentNullException(nameof(switchToThumbnailsViewCommand));
            }

            SwitchToThumbnailsViewCommand = switchToThumbnailsViewCommand;

            ViewModel = viewModel;
        }

        public ImagesViewModel ViewModel { get; }

        public ICommand SwitchToThumbnailsViewCommand { get; }
    }
}
