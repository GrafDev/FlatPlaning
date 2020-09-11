using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.RevitAddIns;
using System.Windows.Data;

namespace FlatPlaning
{
    class Util
    {
        // Define cultureInfo
        public static ResourceManager GetLanguageResources;   // declare Resource manager to access to specific cultureinfo
        public static CultureInfo Cult;        // declare culture info

        public static void GetLocalisationValues(UIControlledApplication application)
        {
            string lang = application.ControlledApplication.Language.ToString();
            if (lang == "Russian")
            {
                // Create the culture for russian
                Util.Cult = CultureInfo.CreateSpecificCulture("ru");
                Util.GetLanguageResources = new System.Resources.ResourceManager("FlatPlaning.Resources.rus", System.Reflection.Assembly.GetExecutingAssembly());
                // TODO: Необходимо сделать файл ресурсов
            }
            else
            {
                // Create the culture for english
                Util.Cult = CultureInfo.CreateSpecificCulture("en");
                Util.GetLanguageResources = new System.Resources.ResourceManager("FlatPlaning.Resources.eng", System.Reflection.Assembly.GetExecutingAssembly());
            }
        }

        public static double? GetFromString(string heightValueString, Units units)
        {
            // Check the string value
            double lenght;
            if (Autodesk.Revit.DB.UnitFormatUtils.TryParse(units, UnitType.UT_Length, heightValueString, out lenght))
            {
                return lenght;
            }
            else
            {
                return null;
            }
        }



        public static void ExtractRessource(string resourceName, string path)
        {
            using (Stream input = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (Stream output = File.Create(path))
            {

                // Insert null checking here for production
                byte[] buffer = new byte[8192];

                int bytesRead;
                while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, bytesRead);
                }

            }
        }
    }
    public class ErrorMessageException : ApplicationException
    {
        /// <summary>
        /// constructor entirely using baseclass'
        /// </summary>
        public ErrorMessageException()
            : base()
        {
        }

        /// <summary>
        /// constructor entirely using baseclass'
        /// </summary>
        /// <param name="message">error message</param>
        public ErrorMessageException(String message)
            : base(message)
        {
        }
    }

}
