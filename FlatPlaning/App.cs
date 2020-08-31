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
            Util.GetLocalisationValues(app);
            string NameOfApp = "FlatPlaning.";
            string NameOfAppResources = "FlatPlaning.Resources.";
            { // Лента
                string thisAssembyPath = Assembly.GetExecutingAssembly().Location;
                string panelName = "FlatPlaning";//Util.GetLanguageResources.GetString("groupTitle_ribbonPanel", Util.Cult);
                Autodesk.Revit.UI.RibbonPanel ribbonPanel = application.CreateRibbonPanel(panelName);
                ribbonPanel.Enabled = true;
                ribbonPanel.Visible = true;

                { // Выпадающее меню
                    string groupTitle = "Group1";// Util.GetLanguageResources.GetString("groupTitle_ribbonPanel", Util.Cult);
                    PulldownButtonData group1Data = new PulldownButtonData("PulldownGroup", groupTitle);
                    PulldownButton group1 = ribbonPanel.AddItem(group1Data) as PulldownButton;
                    group1.Image = GetEmbeddedImage(NameOfAppResources + "Group1.ico");
                    group1.LargeImage = GetEmbeddedImage(NameOfAppResources + "Group1.ico");


                    {
                        string classWallName = NameOfApp + "According";
                        string wallTitle = "According";//Util.GetLanguageResources.GetString("wallTitle_ribbonPanel", Util.Cult);
                        PushButtonData buttonWallData = new PushButtonData("Name1", wallTitle, thisAssembyPath, classWallName);
                        PushButton pushWallButton = group1.AddPushButton(buttonWallData) as PushButton;
                        pushWallButton.Image = GetEmbeddedImage(NameOfAppResources + "According.ico");
                        pushWallButton.LargeImage = GetEmbeddedImage(NameOfAppResources + "According.ico");
                    }

                    {
                        string classFroolName = NameOfApp + "Filling";
                        string floorTitle = "Filling";//Util.GetLanguageResources.GetString("floorTitle_ribbonPanel", Util.Cult);
                        PushButtonData buttonFloorData = new PushButtonData("Name2", floorTitle, thisAssembyPath, classFroolName);
                        PushButton pushFloorButton = group1.AddPushButton(buttonFloorData) as PushButton;
                        string ImageFilling = NameOfAppResources + "Filling.ico";
                        pushFloorButton.Image = GetEmbeddedImage(NameOfAppResources + "Filling.ico");
                        pushFloorButton.LargeImage = GetEmbeddedImage(NameOfAppResources + "Filling.ico");

                    }
                    group1.AddSeparator();
                    {
                        string classAbout = NameOfApp + "About";
                        string aboutTitle = "About";//Util.GetLanguageResources.GetString("aboutTitle_ribbonPanel", Util.Cult);
                        PushButtonData buttonAboutData = new PushButtonData("Name3", aboutTitle, thisAssembyPath, classAbout);
                        PushButton pushAboutButton = group1.AddPushButton(buttonAboutData) as PushButton;
                        pushAboutButton.Image = GetEmbeddedImage(NameOfAppResources + "iconParameters16.png");
                        pushAboutButton.LargeImage = GetEmbeddedImage(NameOfAppResources + "iconParameters32.png");
                    }
                }
                return Result.Succeeded;
            }
        }

        // Комманда Соответсвия параметров
        // Комманда Заполнения параметров
        // Комманда About

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
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
    class ButtonCommand
    {
        int CountNameButton = 0;
        internal void ButtonCommand(String NameButton, PulldownButton NamePull,string thisAssembyPath)
        {
            CountNameButton++;
            string classWallName = NameOfApp + "According";
            string wallTitle = "According";//Util.GetLanguageResources.GetString("wallTitle_ribbonPanel", Util.Cult);

            PushButtonData buttonWallData = new PushButtonData("Name1", wallTitle, thisAssembyPath, classWallName);
            PushButton pushWallButton = NamePull.AddPushButton(buttonWallData) as PushButton;
            pushWallButton.Image = GetEmbeddedImage(NameOfAppResources + "According.ico");
            pushWallButton.LargeImage = GetEmbeddedImage(NameOfAppResources + "According.ico");

        }
        

    }


}
