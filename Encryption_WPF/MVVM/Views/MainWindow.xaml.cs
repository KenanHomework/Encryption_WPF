using Encryption_WPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Encryption_WPF.Enums;
using Encryption_WPF.Services;

namespace Encryption_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Container.GetInstance<MainWindowVM>().Window = this;
            DataContext = App.Container.GetInstance<MainWindowVM>();
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Content.ToString())
                {
                    case "_":
                        this.WindowState = WindowState.Minimized;
                        break;
                    case "X":
                        Application.Current.Shutdown();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void PasswordEye_Click(object sender, RoutedEventArgs e)
        {
            HidePassword.Visibility = PasswordEye.IsChecked == true ? Visibility.Hidden : Visibility.Visible;
            ShowPassword.Visibility = PasswordEye.IsChecked == false ? Visibility.Hidden : Visibility.Visible;
            if (PasswordEye.IsChecked == true) ShowPassword.Text = HidePassword.Password;
            else HidePassword.Password = ShowPassword.Text;
        }

        private void HidePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegxService.CheckControl(ref HidePassword, 3, Color.FromRgb(237, 236, 239));
            App.Container.GetInstance<MainWindowVM>().Password = HidePassword.Password;
        }

        private void ShowPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegxService.CheckControl(ref ShowPassword, 3, Color.FromRgb(237, 236, 239));
            App.Container.GetInstance<MainWindowVM>().Password = ShowPassword.Text;
        }

    }
}
