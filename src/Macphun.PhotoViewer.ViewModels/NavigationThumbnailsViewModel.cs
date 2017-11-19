// <copyright company="XATA">
//      Copyright (c) 2017 by Shogun, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <email>iivchenko@live.com</email>

using System;
using System.Windows.Input;

namespace Macphun.PhotoViewer.ViewModels
{
    public sealed class NavigationThumbnailsViewModel
    {
        public NavigationThumbnailsViewModel(ThumbnailsViewModel viewModel, ICommand switchToImagesViewCommand)
        {
            if (switchToImagesViewCommand == null)
            {
                throw new ArgumentNullException(nameof(switchToImagesViewCommand));
            }

            SwitchToImagesViewCommand = switchToImagesViewCommand;

            ViewModel = viewModel;
        }

        public ThumbnailsViewModel ViewModel { get; }

        public ICommand SwitchToImagesViewCommand { get; }
    }
}
