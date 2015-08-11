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
using Microsoft.Win32;


namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
           
            ResizeMode = ResizeMode.NoResize;

            InitializeComponent();
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            
            Song.Content = "Play";
            
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
                    SongPlayer.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        
       
    }
}
