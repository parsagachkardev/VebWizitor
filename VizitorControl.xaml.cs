using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
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
    public struct VizitorData
    {
        public string Url;
        public int Interval;
    }
    public class VizitorResault: EventArgs
    {
        
        internal string Url;
    }
    /// <summary>
    /// Interaction logic for VizitorControl.xaml
    /// </summary>
    public partial class VizitorControl : Grid
    {
        internal Timer t = new Timer();
        public event EventHandler Resault;
        public VizitorControl()
        {
            InitializeComponent();
        }
        System.Windows.Forms.WebBrowser wb;
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
            t.Interval = ((VizitorData)DataContext).Interval;
            t.Tick += T_Tick;
            t.Enabled = true;
            System.Windows.Forms.Integration.WindowsFormsHost h = new System.Windows.Forms.Integration.WindowsFormsHost();
            wb = new System.Windows.Forms.WebBrowser();
            h.Child = wb;
            host.Children.Add(h);
            wb.ScriptErrorsSuppressed = true;
            wb.DocumentCompleted += Wb_DocumentCompleted; ;
            wb.Navigate(((VizitorData)DataContext).Url);
        }

        private void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (Resault == null) return;
            Resault.Invoke(this, new VizitorResault { Url = ((VizitorData)DataContext).Url });
        }

        private void Wb_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (Resault == null) return;
            Resault.Invoke(this, new VizitorResault { Url= ((VizitorData)DataContext).Url} );
        }

        private void T_Tick(object sender, EventArgs e)
        {
            wb.Navigate(((VizitorData)DataContext).Url);
        }
    }
}
