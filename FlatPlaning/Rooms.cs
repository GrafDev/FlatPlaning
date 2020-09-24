using System;
using System.Linq;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using System.Globalization;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.ExtensibleStorage;
using System.Data;
using System.Windows.Markup;
using Autodesk.Revit.DB.Analysis;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace FlatPlaning
{
    class Rooms
    {
        internal List<_Room> _rooms = new List<_Room>();
        internal Rooms(List<Room> rooms)
        {
            // заполняем параметры помещений
            foreach (Room room in rooms)
            {
                _Room flatTemp = new _Room();
                flatTemp.room = room;
                _rooms.Add(flatTemp);
            }
        }
    }
    internal class _Room
    {
        internal Room room;
        internal string numberFlats;
        internal string typeRoom;

        internal double areaRoom;
        internal double coeffRoom;

        internal Area areaFlatCommon;
        internal Area areaFlatLiving;
        internal Area areaFlat;
        internal Area areaRoomCoefficient;
        internal int countLivingRooms;

        internal _Room()
        {
            TakeNumberFlat();
            TakeTypeRoom();
            TakeCoeffRoom();
            TakeAreaRoom();
        }

        internal Definition GetDefinition(Element e, string parameter_name)
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

        void TakeTypeRoom()
        {

            string nameP = dfp.currentTypeRoom;
            Parameter parameterRoom = room.get_Parameter(GetDefinition(room, nameP));
            typeRoom = parameterRoom.AsString();

        }
        void TakeCoeffRoom()
        {
            switch (typeRoom)
            {
                case "5":
                    coeffRoom = 0;
                    break;
                case "3":
                    coeffRoom = 0.5;
                    break;
                case "4":
                    coeffRoom = 0.3;
                    break;
                default:
                    coeffRoom = 1;
                    break;
            }

        }
        void TakeNumberFlat()
        {
            string nameP = dfp.currentNumberFlat;
            Parameter parameterRoom = room.get_Parameter(GetDefinition(room, nameP));
            numberFlats = parameterRoom.AsString();
        }
        void TakeAreaRoom()
        {
            //...площадь помещения
            string nameP = "Площадь";
            Parameter parameterRoom = room.get_Parameter(GetDefinition(room, nameP));
            areaRoom = parameterRoom.AsDouble();
        }

    }



}
