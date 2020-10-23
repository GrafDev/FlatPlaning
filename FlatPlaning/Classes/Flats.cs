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
using System.Windows.Media;

namespace FlatPlaning
{
    class Flats
    {
        List<Flat> flats;
        

        internal Flats(List<_Room> rooms)
        {
            // группируем по номеру квартиры
            flats = new List<Flat>();
            var roomGroups = from _room in rooms
                             group _room by _room.numberFlats;
            // добавляем комнаты в квартиру
            foreach (var group in roomGroups)
            {    
                Flat flat=new Flat();
                foreach(_Room room in group)
                {

                    flat.roomsOfFlat.Add(room);
                    flat.numberFlat = room.numberFlats;
                }
                flats.Add(flat);
            }            
            
        }
        internal void ShowFlats()
        {
            string tempString = "";
            foreach (Flat flat in flats)
            {
                tempString = tempString +"Номер квартиры="+ flat.numberFlat+"\n";
                tempString = tempString +"Количество жилых комнат="+ flat.countLivingRooms.ToString()+"\n";
                tempString = tempString +"Жилая площадь="+ flat.areaFlatLiving.ToString() + "\n";
                tempString = tempString + "Общая площадь=" + flat.areaFlatCommon.ToString() + "\n";
                tempString = tempString + "Площадь квартиры=" + flat.areaFlat.ToString() + "\n";

            }
            TaskDialog.Show("ShowFlats",tempString);
        }
        class Flat
        {
            internal List<_Room> roomsOfFlat = new List<_Room>();
            internal string numberFlat { get; set; }
            internal double areaFlatCommon;
            internal double areaFlatLiving;
            internal double areaFlat;
            internal int countLivingRooms;

        }
        internal void CalculationParametersFlat()
        {

            foreach (Flat flat in flats)
            {
                double tempAreaFlatCommon = 0;
                double tempAreaFlatLiving = 0;
                double tempAreaFlat = 0;

                foreach (_Room room in flat.roomsOfFlat)
                {
                    if (room.typeRoom == "1")
                    {
                        //.. Расчет количества жилых комнат в квартире
                        flat.countLivingRooms++;
                    }
                    //.. Расчет жилой площади квартиры
                    tempAreaFlatLiving = tempAreaFlatLiving + room.areaRoomLiving;
                    //.. Расчет площади квартиры
                    tempAreaFlat = tempAreaFlat + room.areaRoom;
                    //.. Расчет общей площади квартиры
                    tempAreaFlatCommon = tempAreaFlatCommon + room.areaRoomCommon;
                }
                //.. Запись жилой площади квартиры
                flat.areaFlatLiving = tempAreaFlatLiving;
                flat.areaFlat = tempAreaFlat;
                flat.areaFlatCommon = tempAreaFlatCommon;                

            }

        }
        internal void WriteParametersToRooms()
        {


            string tempString = "";
            int countFlats = 0;
            foreach(Flat flat in flats)
            {
                countFlats++;
                double tempAreaFlatCommon = Math.Round(flat.areaFlatCommon,2);
                double tempAreaFlatLiving = Math.Round(flat.areaFlatLiving,2);
                double tempAreaFlat = Math.Round(flat.areaFlat,2);
                string tempNumberFlat = flat.numberFlat;
                int tempCountLivingRooms = flat.countLivingRooms;

                foreach (_Room room in flat.roomsOfFlat)
                {
                    string nameP = "";
                    Parameter Parameter=null;
                    nameP = dfp.currentAreaFlat;
                    Parameter = room.room.get_Parameter(Util.GetDefinition(room.room, nameP));
                    Parameter.SetValueString(tempAreaFlat.ToString());

                    nameP = dfp.currentAreaFlatCommon;
                    Parameter = room.room.get_Parameter(Util.GetDefinition(room.room, nameP));
                    Parameter.SetValueString(tempAreaFlatCommon.ToString());

                    nameP = dfp.currentAreaFlatLiving;
                    Parameter = room.room.get_Parameter(Util.GetDefinition(room.room, nameP));
                    Parameter.SetValueString(tempAreaFlatLiving.ToString());

                    nameP = dfp.currentCountRoom;
                    Parameter = room.room.get_Parameter(Util.GetDefinition(room.room, nameP));
                    Parameter.Set(tempCountLivingRooms);
                }
                tempString = tempString + tempNumberFlat + "\n";
            }
            if (dfp.dialogBox)
            {
                TaskDialog.Show("Flat Planing", $"Успешно посчитаны {countFlats.ToString()} квартир");
            }
        }
    }
}
