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

            string nameOfApp = "FlatPlaning";
            string nameOfTab = "GrafDev";
            string nameButtonPull = "FlatPlaning";
            {
                try
                {
                    app.CreateRibbonTab(nameOfTab);
                }
                catch
                {

                }
                var panel = new RibbonInitialize(app,nameOfTab,nameOfApp);
                
                var group1 = panel.pullDownButton(nameOfApp, nameButtonPull);
                string nameButtonFilling = "Заполнение";
                string nameClass = "Filling";
                var buttonFilling = panel.pushButton(nameOfApp, group1,nameClass, nameButtonFilling);
                string nameButtonAbout = "About";
                var buttonAbout = panel.pushButton(nameOfApp, group1,nameButtonAbout, nameButtonAbout);       
            }
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
        static ImageSource GetEmbeddedImage1(string name)// Получение иконок из сборки
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
    internal class RibbonInitialize
    {
        int CountNameButton = 0;
        Autodesk.Revit.UI.RibbonPanel ribbonPanel;
        string thisAssembyPath;
        internal RibbonInitialize(UIControlledApplication app,string nameTab, string panelName)
        {

            thisAssembyPath = Assembly.GetExecutingAssembly().Location;
            // Инициализация панели
            ribbonPanel = app.CreateRibbonPanel(nameTab, panelName);
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
        internal PushButton pushButton(string nameOfApp, PulldownButton pull, string titleButton,string nameClass)
        {
            CountNameButton++;
            string className = nameOfApp + "."+ titleButton;
            string NameButton = "Name" + CountNameButton.ToString();
            PushButtonData buttonData = new PushButtonData(NameButton, nameClass, this.thisAssembyPath, className);
            PushButton pushButton = pull.AddPushButton(buttonData) as PushButton;
            string imageName = nameOfApp + "." + "Resources" + "." +nameClass + ".ico";
            pushButton.Image = GetEmbeddedImage(imageName);
            pushButton.LargeImage = GetEmbeddedImage(imageName);
            return pushButton;
        }
        static ImageSource GetEmbeddedImage(string name)// Получение иконок из сборки
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
