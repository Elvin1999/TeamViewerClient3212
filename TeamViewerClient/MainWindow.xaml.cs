using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TeamViewerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Thread thread = new Thread(() =>
                {
                    App.Current.Dispatcher.BeginInvoke(() =>
                        {
                            NetworkHelper.Start(IpTxtbox.Text, int.Parse(PortTxtbox.Text));
                        });
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NetworkHelper.WriteDataToServer(NetworkHelper.ExitCommand);

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            NetworkHelper.WriteDataToServer(NetworkHelper.ExitCommand);
        }
    }
}
