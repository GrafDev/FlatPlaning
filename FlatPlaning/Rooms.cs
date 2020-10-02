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
using FlatPlaning;

namespace FlatPlaning
{
    class Rooms
    {
        internal List<_Room> _rooms;
        internal Rooms(List<Room> rooms)
        {
            _rooms = new List<_Room>();

            // заполняем параметры помещений
            foreach (Room room in rooms)
            {
                Util.RoomState roomPlaced = Util.RoomState.Placed;
                Util.RoomState checkRoom = Util.DistinguishRoom(room);
                if (checkRoom == roomPlaced)
                {
                    // .. Проверка на наличие номера квартиры
                    string nameP = dfp.currentNumberFlat;
                    Parameter parameterRoom = room.get_Parameter(Util.GetDefinition(room, nameP));
                    if (parameterRoom.HasValue)
                    {
                        _Room temp_Room = new _Room(room);
                        _rooms.Add(temp_Room);
                    }
                }

            }

        }
    }
    internal class _Room
    {
        internal Room room;
        internal ElementId Id;
        internal string numberFlats;
        internal double area;
        internal double areaRoomLiving;
        internal double areaRoomCommon;
        internal double areaRoom;
        internal string typeRoom;


        internal double areaRoomCoefficientSet;


        internal _Room(Room room)
        {
            this.room = room;
            TakeNumberFlat();
            TakeAreaRoom();
            TakeIdRoom();
            DependenceTypeRoom();
            GetRoomTag(room);
        }




        void DependenceTypeRoom()
        {
            string nameP = dfp.currentTypeRoom;
            string temptypeRoom;
            Parameter parameterRoom = room.get_Parameter(Util.GetDefinition(room, nameP));
            temptypeRoom = parameterRoom.AsValueString();
            switch (temptypeRoom)
            {
                //...жилое
                case "1":
                    areaRoomCoefficientSet = 1;
                    areaRoomLiving = area;
                    areaRoomCommon = area * areaRoomCoefficientSet;
                    areaRoom = area;
                    typeRoom = "1";
                    break;
                //...нежилое
                case "2":
                    areaRoomCoefficientSet = 1;
                    areaRoomCommon = area * areaRoomCoefficientSet;
                    areaRoom = area;
                    typeRoom = "2";
                    break;
                //...лоджия
                case "3":
                    areaRoomCoefficientSet = 0.5;
                    areaRoomCommon = area * areaRoomCoefficientSet;
                    areaRoomLiving = 0;
                    areaRoom = 0;
                    typeRoom = "3";
                    break;
                //...балкон
                case "4":
                    areaRoomCoefficientSet = 0.3;
                    areaRoomLiving = 0;
                    areaRoomCommon = area * areaRoomCoefficientSet;
                    areaRoom = 0;
                    typeRoom = "4";
                    break;
                //...общее "5"
                default:
                    areaRoomCoefficientSet = 1;
                    areaRoomLiving = 0;
                    areaRoomCommon = 0;
                    areaRoom = 0;
                    typeRoom = "5";
                    break;
            }
            nameP = dfp.currentAreaWithCoefficient;
            Parameter Parameter = room.get_Parameter(Util.GetDefinition(room, nameP));
            Parameter.SetValueString(areaRoomCoefficientSet.ToString());

        }

        void TakeNumberFlat()
        {
            string nameP = dfp.currentNumberFlat;
            Parameter parameterRoom = room.get_Parameter(Util.GetDefinition(room, nameP));
            numberFlats = parameterRoom.AsString();
        }
        void TakeIdRoom()
        {
            Id = room.Id;
        }
        void TakeAreaRoom()
        {
            //...площадь помещения            
            string nameP = "Площадь";
            Parameter parameterRoom = room.get_Parameter(Util.GetDefinition(room, nameP));
            area = double.Parse(parameterRoom.AsValueString());
        }


        internal string ParametersOf_Room()
        {
            string tString = "";
            tString = numberFlats + "-" + "\n";
            tString = tString + typeRoom + "\n";
            tString = tString + area.ToString() + "\n";
            tString = tString + areaRoomCoefficientSet.ToString() + "\n";
            return tString;
        }

    }



}
