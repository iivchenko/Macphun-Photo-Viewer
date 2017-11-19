// <copyright company="XATA">
//      Copyright (c) 2017 by Shogun, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <email>iivchenko@live.com</email>

using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Macphun.PhotoViewer.Wpf
{
    public sealed class EventToCommandAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(EventToCommandAction), null);

        public static readonly DependencyProperty ParametersConverterProperty = DependencyProperty.Register(
            "ParametersConverter", typeof(IConverter), typeof(EventToCommandAction), null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);

            set => SetValue(CommandProperty, value);
        }

        public IConverter ParametersConverter
        {
            get => (IConverter)GetValue(ParametersConverterProperty);

            set => SetValue(ParametersConverterProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            if (Command?.CanExecute(ParametersConverter?.Convert(parameter)) == true)
            {
                Command.Execute(ParametersConverter?.Convert(parameter));
            }
        }
    }
}
