#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.IO;
using System.Runtime.CompilerServices;
using Autodesk.Revit.Creation;
using System.Windows.Media;
using Autodesk.Windows;
#endregion

namespace FlatPlaning
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            UIControlledApplication app = application;
            //Util.GetLocalisationValues(app);
            string NameOfApp = "FlatPlaning";
            { // �����
                RibbonInitialize panel = new RibbonInitialize(app, NameOfApp);
                // ���������� ����
                string nameButtonPull = "FlatPlaning";
                PulldownButton group1 = panel.pullDownButton(NameOfApp, nameButtonPull);
                // �������� ����������� ����������
                string nameButtonAccording = "According";
                PushButton buttonAccording = panel.pushButton(NameOfApp, group1, nameButtonAccording);
                // �������� ���������� ����������
                string nameButtonFilling = "Filling";
                PushButton buttonFilling = panel.pushButton(NameOfApp, group1, nameButtonFilling);
                //---------
                group1.AddSeparator();
                // �������� About
                string nameButtonAbout = "About";
                PushButton buttonAbout = panel.pushButton(NameOfApp, group1, nameButtonAbout);        
            }
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
    internal class RibbonInitialize
    {
        int CountNameButton = 0;
        Autodesk.Revit.UI.RibbonPanel ribbonPanel;
        string thisAssembyPath;
        internal RibbonInitialize(UIControlledApplication app, string panelName)
        {

            thisAssembyPath = Assembly.GetExecutingAssembly().Location;
            // ������������� ������
            ribbonPanel = app.CreateRibbonPanel(panelName);
            ribbonPanel.Enabled = true;
            ribbonPanel.Visible = true;
        }
        internal PulldownButton pullDownButton(string nameOfApp, string titleButton)
        {
            CountNameButton++;
            string groupName = "Group" + CountNameButton;
            PulldownButtonData groupData = new PulldownButtonData(groupName, titleButton);
            PulldownButton group = ribbonPanel.AddItem(groupData) as PulldownButton;
            string imageName = nameOfApp + "." + "Resources" + "." + titleButton +groupName+".ico";
            group.Image = GetEmbeddedImage(imageName);
            group.LargeImage = GetEmbeddedImage(imageName);
            return group;

        }
        internal PushButton pushButton(string nameOfApp, PulldownButton pull, string titleButton)
        {
            CountNameButton++;
            string className = nameOfApp + "." + titleButton;
            string NameButton = "Name" + CountNameButton.ToString();
            PushButtonData buttonData = new PushButtonData(NameButton, titleButton, this.thisAssembyPath, className);
            PushButton pushButton = pull.AddPushButton(buttonData) as PushButton;
            string imageName = nameOfApp + "." + "Resources" + "." +titleButton + ".ico";
            pushButton.Image = GetEmbeddedImage(imageName);
            pushButton.LargeImage = GetEmbeddedImage(imageName);
            return pushButton;
        }
        static ImageSource GetEmbeddedImage(string name)// ��������� ������ �� ������
        {
            try
            {
                Assembly a = Assembly.GetExecutingAssembly();
                Stream s = a.GetManifestResourceStream(name);
                return BitmapFrame.Create(s);
            }
            catch
            {
                return null;
            }
        }


    }


}
