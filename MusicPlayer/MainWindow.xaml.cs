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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;


namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        string currFile;
        bool draging;
        public MainWindow()
        {
           
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if ((SongPlayer.Source != null) && (SongPlayer.NaturalDuration.HasTimeSpan) )
            {
                progBar.Minimum = 0;
                progBar.Maximum = SongPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                progBar.Value = SongPlayer.Position.TotalSeconds;
            }
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Play.Content=="Play") { 
                
              
                SongPlayer.Play();
                Play.Content = "Pause";
            }
            else
            {
                Play.Content = "Play";
                SongPlayer.Pause();
            }
            
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog File = new OpenFileDialog();

           

            if (File.ShowDialog() != null)
            {
                try
                {
                    Song.Content = File.SafeFileName;
                    SongPlayer.Source = new Uri(File.FileName);
                    currFile= File.FileName;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void DragStarted(object sender, DragStartedEventArgs e)
        {
            draging= true;
        }

        private void DragEnd(object sender, DragCompletedEventArgs e)
        {
            draging = false;
            SongPlayer.Position = TimeSpan.FromSeconds(progBar.Value);
        }

        private void valChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Curr.Content = TimeSpan.FromSeconds(progBar.Value).ToString(@"hh\:mm\:ss");
            Total.Content = TimeSpan.FromSeconds(progBar.Maximum).ToString(@"hh\:mm\:ss");
        }

        
       
    }
}
