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

namespace VebWizitor
{
    /// <summary>
    /// Interaction logic for LogSegment.xaml
    /// </summary>
    public partial class LogSegment : Grid
    {
        private string datetime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
        public LogSegment()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            string url =((VizitorResault)DataContext).Url;
            
            Address.Text = url;
            Dt.Text = datetime;
        }
        public override string ToString()
        {
            return datetime + "\tSucess:\t" + ((VizitorResault)DataContext).Url;
        }
    }
}
