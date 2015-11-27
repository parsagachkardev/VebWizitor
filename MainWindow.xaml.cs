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
using System.IO;

namespace VebWizitor
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
        private async Task start()
        {
            
                foreach (var address in Addresses.Text.Split('\n'))
                {
                    for (int i = 0; i < int.Parse(Threads.Text); i++)
                    {
                        var a = new VizitorControl
                        {
                            DataContext = new VizitorData
                            {
                                Url = address,
                                Interval = int.Parse(Interval.Text) * 1000
                            }
                        };
                        a.Resault += A_Resault;
                        sp.Children.Add(a);
                    }
                }
            
           
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            start();
            Stop.IsEnabled = true;
        }

        private void A_Resault(object sender, EventArgs e)
        {
            logStack.Children.Add(new LogSegment { DataContext = e });
        }

        private async Task SaveLogs_Click()
        {
            SaveLogs.IsEnabled = false;
            var s = new System.Windows.Forms.SaveFileDialog {
                Filter = "Text File (*.txt)|*.txt",
                FileName = "log"
            };
            if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(s.FileName, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                foreach (var item in logStack.Children)
                {
                   await sw.WriteLineAsync(item.ToString());
                }
                sw.Close();
                fs.Close();
            }
            SaveLogs.IsEnabled = true;
        }

        private void SaveLogs_Click2(object sender, RoutedEventArgs e)
        {
            SaveLogs_Click();
        }
        private async Task stop()
        {
            sp.Children.Cast<VizitorControl>().ToList().ForEach(q => { q.t.Enabled = false; });
            sp.Children.Clear();
            sp.Children.Add(new VizitorControl { DataContext = new VizitorData { Url = "http://learnprogramming.rzb.ir", Interval = 60000 } });
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Stop.IsEnabled = false;
            stop();
            Start.IsEnabled = true;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"by Parsa Gachkar <Parsagachkar.dev@outlook.com> +989214108118
The Admin of Learnprogramming.rzb.ir","About",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sp.Children.Add(new VizitorControl { DataContext = new VizitorData { Url = "http://learnprogramming.rzb.ir", Interval = 60000 } });
        }
    }
    }
