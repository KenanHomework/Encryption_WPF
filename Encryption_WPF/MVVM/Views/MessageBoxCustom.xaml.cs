using Encryption_WPF.Services;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Encryption_WPF.Enums;

namespace Encryption_WPF.MVVM.Views
{
    public partial class CMessageBox : Window
    {
        public CMessageBox()
        {
            InitializeComponent();
            DataContext = this;
        }
        static CMessageBox cMessageBox;
        static DialogResult result = Encryption_WPF.Enums.DialogResult.No;
        public CMessageTitle messageTitle { get; set; }

        public string GetTitle(CMessageTitle value)
        {
            return Enum.GetName(typeof(CMessageTitle), value);
        }
        public string GetButtonText(CMessageButton value)
        {
            return Enum.GetName(typeof(CMessageButton), value);
        }

        public static DialogResult Show(string message, CMessageTitle title, CMessageButton okButton, CMessageButton noButton)
        {
            cMessageBox = new CMessageBox();
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(okButton);
            cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            if (noButton == CMessageButton.None)
                cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
            else
                cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);

            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(title);

            //icon
            switch (title)
            {
                case CMessageTitle.Error:
                    cMessageBox.msgLogo.Kind = PackIconKind.Error;
                    SoundService.Error();
                    cMessageBox.msgLogo.Foreground = Brushes.DarkRed;
                    break;
                case CMessageTitle.Info:
                    SoundService.Notification();
                    cMessageBox.msgLogo.Kind = PackIconKind.InfoCircle;
                    cMessageBox.msgLogo.Foreground = Brushes.Blue;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Warning:
                    SoundService.Error();
                    cMessageBox.msgLogo.Kind = PackIconKind.Warning;
                    cMessageBox.msgLogo.Foreground = Brushes.Yellow;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Confirm:
                    SoundService.Notification();
                    cMessageBox.msgLogo.Kind = PackIconKind.QuestionMark;
                    cMessageBox.msgLogo.Foreground = Brushes.Gray;
                    break;
            }
            cMessageBox.ShowDialog();
            return result;
        }

        private void BntOk_Click(object sender, RoutedEventArgs e)
        {
            result = Encryption_WPF.Enums.DialogResult.Yes;
            Border border = new Border();

            cMessageBox.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = Encryption_WPF.Enums.DialogResult.No;
            cMessageBox.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growAnimation.From = 0;
            growAnimation.To = 1;
            storyboard.Children.Add(growAnimation);

            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, this);

            storyboard.Begin();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Closing -= Window_Closing;
            e.Cancel = true;
            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growAnimation.From = 1;
            growAnimation.To = 0;
            storyboard.Children.Add(growAnimation);

            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, this);
            growAnimation.Completed += GrowAnimation_Completed;

            storyboard.Begin();
        }

        private void GrowAnimation_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
