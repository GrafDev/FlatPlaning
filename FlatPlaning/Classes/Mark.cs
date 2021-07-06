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
using Autodesk.Windows;

namespace FlatPlaning
{
    static class Marks
    {
        internal static bool SelectedRooms = false;
        internal static bool AllRoomsOnView = true;
        internal static bool AllRooms = false;
        internal static List<RoomTag> marks;

        internal static void ShowMarks()
        {
            int i = 0;
            foreach (RoomTag roomTag in marks)
            {

                i++;
            }

            Autodesk.Revit.UI.TaskDialog.Show("MArks", i.ToString());
        }


    }

    class Mark
    {
        internal static Document _doc;
        internal static UIDocument _UIDoc;
        internal RoomTag tag;
        internal static RoomTagType roomTagType;



        private void Getinfo_RoomTag(RoomTag roomTag)
        {
            string message = "Room Tag : ";
            //get the location of the roomtag
            LocationPoint location = roomTag.Location as LocationPoint;
            XYZ point = location.Point;
            message += "\nRoomTag location: (" + point.X + ", " +
                           point.Y + ", " + point.Z + ")";

            //get the name of the roomTag
            message += "\nName: " + roomTag.Name;

            //get the room that the tag is associated with
            Room room = roomTag.Room;
            message += "\nThe number of the room is : " + room.Number;

            //get the view in which the tag is placed
            Autodesk.Revit.DB.View view = roomTag.View;
            if (null != view)
            {
                message += "\nView Name: " + view.Name;
            }
            Autodesk.Revit.UI.TaskDialog.Show("Revit", message);
        }

        private void GetRoomTags(List<Room> rooms)
        {
            List<ElementId> eids = null;
            foreach (Room room in rooms)
            {
                ElementId eid;
                eid = room.Id;
                eids.Add(eid);
            }

            RoomTag RT = null;
            foreach (ElementId eid1 in eids)
            {
                Element e = _doc.GetElement(eid1);
                if (e.Name.Contains("Tag"))
                {
                    RT = e as RoomTag;
                }
            }
            Test.Show(RT.ToString());
        }


    }

}
