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
    class Flats
    {
        List<Flat> flats = new List<Flat>();
        

        internal Flats(List<_Room> rooms)
        {
            // группируем по номеру квартиры
            var roomGroups = from _room in rooms
                             group _room by _room.numberFlats;
            // добавляем комнаты в квартиру
            foreach (var group in roomGroups)
            {
                Flat flat=new Flat();
                foreach(_Room room in group)
                {
                    flat.flat.Add(room);
                    flat.numberFlat = room.numberFlats;
                }                
                flats.Add(flat);
            }
            
        }
        class Flat
        {
            internal List<_Room> flat = new List<_Room>();
            internal string numberFlat { get; set; }

        }


        internal void WriteParametersToRooms()
        {
            string tempString = "";
            foreach(Flat flat in flats)
            {
                tempString = tempString + flat.numberFlat + "\n";
            }
            TaskDialog.Show("flats", tempString);
        }
    }
}
