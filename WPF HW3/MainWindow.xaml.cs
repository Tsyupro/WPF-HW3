using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WPF_HW3
{

    public partial class MainWindow : Window
    {
        List<MyLabelInfo> labelInfos = new List<MyLabelInfo>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Media files (*.mp3;*.wav;*.wma;*.mid;*.midi)|*.mp3;*.wav;*.wma;*.mid;*.midi|Video files (*.mp4;*.avi;*.mkv;*.wmv)|*.mp4;*.avi;*.mkv;*.wmv";
            ofd.ShowDialog();
            myMediaElement.Source = new Uri(ofd.FileName);
            myImage.Source = null;

            string extension = Path.GetExtension(ofd.FileName);
            if (ofd.FileName.EndsWith(".mp3") || ofd.FileName.EndsWith(".wav") || ofd.FileName.EndsWith(".wma")
                || ofd.FileName.EndsWith(".mid") || ofd.FileName.EndsWith(".midi"))
            {
                myImage.Visibility = Visibility.Visible;
                myImage.Source = new BitmapImage(new Uri("Images/image.jpg", UriKind.Relative));



            }
            else
            {
                myImage.Visibility = Visibility.Collapsed;
                myImage.Source = null;
            }

            StackPanel sp = myExpander.Content as StackPanel;

            MyLabelInfo info = new MyLabelInfo(Path.GetFileName(ofd.FileName), ofd.FileName);
            TextBox textBox = new TextBox();
            textBox.Text = info.Name;
            textBox.IsReadOnly = true;
            textBox.PreviewMouseLeftButtonDown += TextBox_PreviewMouseLeftButtonDown;

            labelInfos.Add(info);

            sp.Children.Add(textBox);
        }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobutton1.IsChecked)
            {
                myMediaElement.SpeedRatio = 0.5;
            }
            else if ((bool)radiobutton2.IsChecked)
            {
                myMediaElement.SpeedRatio = 1.0;
            }
            else if ((bool)radiobutton3.IsChecked)
            {
                myMediaElement.SpeedRatio = 1.5;
            }
            else if ((bool)radiobutton4.IsChecked)
            {
                myMediaElement.SpeedRatio = 1.25;
            }
            else if ((bool)radiobutton5.IsChecked)
            {
                myMediaElement.SpeedRatio = 1.75;
            }
            else if ((bool)radiobutton6.IsChecked)
            {
                myMediaElement.SpeedRatio = 2.0;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            myMediaElement.LoadedBehavior = MediaState.Manual;
            if (button.Content.ToString() == "Play")
            {
                myMediaElement.Play();
            }
            else if (button.Content.ToString() == "Pause")
            {
                myMediaElement.Pause();
            }
            else if (button.Content.ToString() == "10 Up")
            {
                myMediaElement.Position = myMediaElement.Position.Add(new TimeSpan(0, 0, 10));
            }
            else if (button.Content.ToString() == "10 Low")
            {
                myMediaElement.Position = myMediaElement.Position.Subtract(new TimeSpan(0, 0, 10));
            }
        }
        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("OK");
            myMediaElement.LoadedBehavior = MediaState.Manual;
            TextBox textBox = sender as TextBox;
            MyLabelInfo info = labelInfos.FirstOrDefault(x => x.Name == textBox.Text);
            string extension = System.IO.Path.GetExtension(info.Patch);

            if (info != null && File.Exists(info.Patch))
            {
                if (extension == ".mp3" || extension == ".wav" || extension == ".wma" || extension == ".mid" || extension == ".midi")
                {
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("Images/image.jpg", UriKind.Relative));
                }
                else
                {
                    myImage.Visibility = Visibility.Collapsed;
                    myImage.Source = null;
                }
                try
                {
                    myMediaElement.Stop();
                    myMediaElement.Source = null;
                    myMediaElement.Source = new Uri(info.Patch);
                    myMediaElement.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при відтворенні музики: {ex.Message}");
                }
            }
        }
    }
}
