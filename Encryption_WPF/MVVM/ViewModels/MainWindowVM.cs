using Encryption_WPF.Commands;
using Encryption_WPF.Enums;
using Encryption_WPF.MVVM.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.DesignerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Encryption_WPF.MVVM.ViewModels
{
    public class MainWindowVM
    {

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Members

        CancellationTokenSource _tokenSource = null;

        private string infoText;

        public string Data { get; set; } = string.Empty;

        public string InfoText
        {
            get { return infoText; }
            set { infoText = value; OnPropertyChanged(); }
        }

        public long StreamPosition { get; set; } = 0;

        public MainWindow Window { get; set; }

        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; OnPropertyChanged(); }
        }

        // true  => Encrypt
        // false => Decrypt
        public bool Option { get; set; } = true;

        public string Password { get; set; }

        Hash Hash = new Hash();

        public Thread EncryptThread { get; private set; }

        #endregion

        #region Commands

        public RelayCommand OpenCommand { get; set; }

        public RelayCommand OptionsCommand { get; set; }

        public RelayCommand StartCommand { get; set; }

        public RelayCommand SuspendCommand { get; set; }

        #endregion

        #region Methods

        public void RefreshPathTexbox()
        {
            Window.From.Text = FilePath;
        }


        public void OpenRun(object param)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text File (* txt)| *.txt";
            if (!(bool)ofd.ShowDialog())
                return;

            FilePath = ofd.FileName;

            RefreshPathTexbox();
        }

        public void OptionsRun(object param) => Option = !Option;

        public void StartRun(object param)
        {
            PrepareAction(true);
            Hash = new Hash(Password);
            EncryptData();
        }

        public bool StartCanRun(object param) => !string.IsNullOrWhiteSpace(Password) && Password.Length > 2 && !string.IsNullOrWhiteSpace(FilePath);

        public void PrepareAction(bool state)
        {

            Window.Start.IsEnabled = !state;
            Window.PasswordGRID.IsEnabled = !state;
        }

        public void StartEncrypt(CancellationToken token)
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                StreamPosition = 0;

                foreach (var sym in Data)
                {
                    writer.Write(Hash.Encrypt((byte)sym));
                    Window.Dispatcher.Invoke(() => Window.Progress.Value++);
                    Thread.Sleep(50);
                    StreamPosition++;

                    if (token.IsCancellationRequested)
                        token.ThrowIfCancellationRequested();

                }
                writer.Close();
            }
        }

        public void StartDecrypt()
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Append)))
            {
                Hash.Reset(Password);

                for (int i = 0; i <= StreamPosition; i++)
                {
                    writer.Write(Hash.Encrypt((byte)i));
                    Window.Dispatcher.Invoke(() => Window.Progress.Value--);
                    Thread.Sleep(50);
                }

                writer.Close();
            }
        }

        public void EncryptData()
        {
            StringBuilder builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(FilePath))
            {
                while (reader.Peek() >= 0)
                {
                    builder.Append((char)reader.Read());
                }
            }

            Data = builder.ToString();
            Window.Progress.Maximum = Data.Length;
            Window.Progress.Value = 0;

            _tokenSource = new CancellationTokenSource();
            CancellationToken token = _tokenSource.Token;

            EncryptThread = new Thread(() =>
            {
                try
                {
                    StartEncrypt(token);
                }
                catch (OperationCanceledException)
                {
                    StartDecrypt();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                finally
                {
                    _tokenSource = null;
                    Window.Dispatcher.Invoke(() =>
                    {
                        CMessageBox.Show("Finish Hash data !", CMessageTitle.Info, CMessageButton.Ok, CMessageButton.None);
                        Window.Progress.Maximum = 100;
                        Window.Progress.Value = 0;
                        FilePath = String.Empty;
                        Password = String.Empty;
                        Window.ShowPassword.Clear();
                        Window.HidePassword.Clear();
                        Window.From.Text = String.Empty;
                        PrepareAction(false);
                    });
                }
            });
            PrepareAction(true);
            EncryptThread.Start();
        }

        public void SuspendRun(object param)
        {
            try
            {
                _tokenSource?.Cancel();
            }
            catch (Exception) { }
        }

        #endregion

        public MainWindowVM()
        {
            OpenCommand = new RelayCommand(OpenRun);
            OptionsCommand = new RelayCommand(OptionsRun);
            StartCommand = new RelayCommand(StartRun, StartCanRun);
            SuspendCommand = new RelayCommand(SuspendRun);
        }
    }
}
