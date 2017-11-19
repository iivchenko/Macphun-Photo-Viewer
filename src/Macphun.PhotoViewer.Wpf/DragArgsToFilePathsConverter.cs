// <copyright company="XATA">
//      Copyright (c) 2017 by Shogun, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <email>iivchenko@live.com</email>

using System.Windows;

namespace Macphun.PhotoViewer.Wpf
{
    public sealed class DragArgsToFilePathsConverter : IConverter
    {
        public object Convert(object value)
        {
            var args = (DragEventArgs)value;

            if (args.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return (string[])args.Data.GetData(DataFormats.FileDrop);
            }

            return new string[0];
        }
    }
}
