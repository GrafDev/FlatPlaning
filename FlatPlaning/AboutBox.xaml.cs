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
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using System.Globalization;
using System.Resources;
using System.Reflection.Emit;
using System.Reflection;

namespace FlatPlaning
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>

    public partial class AboutBox: Window
    {
        private Document _doc;
        private UIDocument _UIDoc;
        public AboutBox(UIDocument UIDoc )
        {
            _doc = UIDoc.Document;
            _UIDoc = UIDoc;
            InitializeComponent();
            this.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            dfp.dialogBox = check_dialogbox.IsChecked.Value;
        }

        private void check_dialogbox_Checked(object sender, RoutedEventArgs e)
        {
            check_dialogbox.IsChecked=dfp.dialogBox;
        }
    }
}
