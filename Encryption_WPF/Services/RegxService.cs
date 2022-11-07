using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Encryption_WPF.Services
{
    public abstract class RegxService
    {
        public static bool CheckControl(ref TextBox tbx, int minLenght, Color defColor, string regex)
        {
            if (tbx.Text.Length >= minLenght && new Regex(regex).IsMatch(tbx.Text))
            {
                tbx.Foreground = new SolidColorBrush(defColor);
                return true;
            }
            tbx.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return false;
        }

        public static bool CheckControl(ref TextBox tbx, int minLenght, Color defColor)
        {
            if (tbx.Text.Length >= minLenght)
            {
                tbx.Foreground = new SolidColorBrush(defColor);
                return true;
            }
            tbx.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return false;
        }

        public static bool CheckCarYear(ref TextBox tbx, Color defColor)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbx.Text) && Convert.ToUInt32(tbx.Text) >= 1970 && Convert.ToUInt32(tbx.Text) <= DateTime.Now.Year)
                {
                    tbx.Foreground = new SolidColorBrush(defColor);
                    return true;
                }
            }
            catch (Exception) { }
            tbx.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return false;
        }

        public static bool CheckTypePrice(ref TextBox tbx, Color defColor)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbx.Text) && Convert.ToSingle(tbx.Text) >= 0.1)
                {
                    tbx.Foreground = new SolidColorBrush(defColor);
                    return true;
                }
            }
            catch (Exception) { }
            tbx.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return false;
        }

        public static bool CheckControl(ref PasswordBox tbx, int minLenght, Color defColor, string regex)
        {
            if (tbx.Password.Length >= minLenght && new Regex(regex).IsMatch(tbx.Password))
            {
                tbx.Foreground = new SolidColorBrush(defColor);
                return true;
            }
            tbx.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return false;
        }

        public static bool CheckControl(ref PasswordBox tbx, int minLenght, Color defColor)
        {
            if (tbx.Password.Length >= minLenght)
            {
                tbx.Foreground = new SolidColorBrush(defColor);
                return true;
            }
            tbx.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return false;
        }

    }
}
