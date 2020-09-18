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
    class CountFlat
    {
        List<string> numberFlats;
        Area[] areaFlatCommon;
        Area[] areaFlatLiving;
        Area[] areaFlat;
        int[] coefficientRoom;
        Area[] areaRoomCoefficient;
        internal List<Room> SelectedRoom { get; set; }

        internal CountFlat()
        {

        }
        internal void GetParanetersRooms()
        {
            string str = "+";
            foreach (Room room in SelectedRoom)
            {
                foreach (string nameParameter in dfp.listParameters)
                {
                    string numberF = "-";
                    Parameter parameterRoom = room.get_Parameter(GetDefinition(room, nameParameter));
                    //numberF = ShowValueParameterInformation(parameterRoom).ToString();
                    numberF = parameterRoom.GUID.ToString();
                    str = str + numberF;

                }

            }
            TaskDialog.Show("оК", str);

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
        internal void PrintAreaRooms()
        {
            string str = "";
            foreach (Room e in SelectedRoom)
            {
                str = str + e.Area.ToString() + "\n";
            }
            TaskDialog.Show("Rooms", str);
        }
        internal void PrintNameRooms()
        {
            string str = "";
            foreach (Room e in SelectedRoom)
            {
                str = str + e.Name.ToString() + "\n";
            }
            TaskDialog.Show("Rooms", str);
        }
        internal void PrintNumberRooms()
        {
            string str = "";
            foreach (string e in numberFlats)
            {
                str = str + e + "\n";
            }
            TaskDialog.Show("Rooms", str);
        }
        String ShowValueParameterInformation(Parameter attribute)
        {
            string paramValue = null;
            switch (attribute.StorageType)
            {
                case StorageType.Integer:
                    if (ParameterType.YesNo
                                        == attribute.Definition.ParameterType)
                    {
                        paramValue = null;
                    }
                    else
                    {
                        paramValue = attribute.AsValueString();
                    }
                    break;
                case StorageType.Double:
                    paramValue = attribute.AsValueString();
                    break;
                default:
                    paramValue = null;
                    break;
            }

            return paramValue;
        }


    }

}
