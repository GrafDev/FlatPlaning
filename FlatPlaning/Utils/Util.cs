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
using System.Diagnostics;
using Autodesk.Revit.DB.Architecture;

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
        public static Definition GetDefinition(Element e, string parameter_name)
        {
            IList<Parameter> ps = e.GetParameters(parameter_name);

            int n = ps.Count;

            Debug.Assert(1 >= n,
              "expected maximum one shared parameters "
              + "named " + parameter_name);

            Definition d = (0 == n)
              ? null
              : ps[0].Definition;
            return d;
        }
        public enum RoomState
        {
            Unknown,
            Placed,
            NotPlaced,
            NotEnclosed,
            Redundant
        }

        /// <summary>
        /// Определение не размещенных, избыточных и не окруженных помещений
        /// </summary>
        public static RoomState DistinguishRoom(Room room)
        {
            RoomState res = RoomState.Unknown;

            if (room.Area > 0)
            {
                // Если есть площадь, то помещение нормальное

                res = RoomState.Placed;
            }
            else if (null == room.Location)
            {
                //Нет площади и расположения – не размещено

                res = RoomState.NotPlaced;
            }
            else
            {
                // Если сюда попали, то либо избыточное, либо не окружено
               // Test.Show("Избыточное");
                SpatialElementBoundaryOptions opt
                  = new SpatialElementBoundaryOptions();

                IList<IList<BoundarySegment>> segs
                  = room.GetBoundarySegments(opt);

                res = (null == segs || segs.Count == 0)
                  ? RoomState.NotEnclosed
                  : RoomState.Redundant;
            }
            return res;
        }

    }
    internal static class Test
    {
        static int count = 0;
         internal static void  Show(string s)
        {
            count++;
            Autodesk.Revit.UI.TaskDialog.Show(count.ToString(), s);
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
