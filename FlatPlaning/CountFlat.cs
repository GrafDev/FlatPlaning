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
        IList<string> numberFlats;
        IList<int> typeRoom;
        IList<double> areaRoom;

        IList<Area> areaFlatCommon;
        IList<Area> areaFlatLiving;
        IList<Area> areaFlat;
        IList<Area> areaRoomCoefficient;
        internal IList<Room> SelectedRoom { get; set; }

        internal CountFlat()
        {

        }
        /*internal void GetParametersRooms()
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
        */
        internal void PrintNumberFlat()
        {
            string str = "";
            foreach (string e in numberFlats)
            {
                str = str + e + "\n";
            }
            TaskDialog.Show("Rooms", str);
        }
        internal void PrintTypeRooms()
        {
            string str = "";
            foreach (int e in typeRoom)
            {
                str = str + e.ToString() + "\n";
            }
            TaskDialog.Show("Rooms", str);
        }
        internal void PrintAreaRooms()
        {
            string str = "";
            foreach (double e in areaRoom)
            {
                str = str + e.ToString() + "\n";
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


        internal void GetParametersRooms()
        {
            foreach(Room room in SelectedRoom)
            {
               {
                    //...номер квартиры
                    Test.Show(@"...номер квартиры начало");
                    string nameP = dfp.currentNumberFlat;
                    /* Parameter parameterRoom = room.get_Parameter(GetDefinition(room, nameP));
                     Test.Show(@"...Перед присвоением ParameterValueString(parameterRoom);");
                     string temp = ParameterValueString(parameterRoom).ToString();*/
                    Test.Show(@"...Попытка ввести строку");
                    string temp = "0";
                    numberFlats.Add(temp);
                    Test.Show(@"...номер квартиры конец");
                }
                {
                    //...тип помещения
                    Test.Show(@"...тип помещения начало");
                    string nameP = dfp.currentTypeRoom;
                    Parameter parameterRoom = room.get_Parameter(GetDefinition(room, nameP));
                    typeRoom.Add(parameterRoom.AsInteger());
                    Test.Show(@"...тип помещения конец");
                }
                {
                    //...площадь помещения
                    Test.Show(@"...площадь помещения начало");
                    string nameP = "Площадь";
                    Parameter parameterRoom = room.get_Parameter(GetDefinition(room, nameP));
                    areaRoom.Add(parameterRoom.AsDouble());
                    Test.Show(@"...площадь помещения конец");
                }
            }

        }
        internal Definition GetDefinition(Element e, string parameter_name)
        {
            Test.Show(@"...Получение дефинишион начало");
            IList<Parameter> ps = e.GetParameters(parameter_name);

            int n = ps.Count;

            Debug.Assert(1 >= n,
              "expected maximum one shared parameters "
              + "named " + parameter_name);

            Definition d = (0 == n)
              ? null
              : ps[0].Definition;
            Test.Show(@"...Получение дефинишион конец");
            return d;            
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

        /// Строковое значение параметра.
        /// summary>
        public string ParameterValueString(Parameter par)
        {
            Test.Show(@"...ParameterValueString начало");

            switch (par.StorageType)
                {
                    case StorageType.Double:

                        Test.Show(@"...Double");
                        return par.AsValueString();

                        break;
                    case StorageType.Integer:
                        Test.Show(@"...Integer");
                        if (par.Definition.ParameterType == ParameterType.YesNo)
                        {
                        if (par.AsInteger() == 0)
                                return "false";
                            else
                                return "true";

                        }
                        return par.AsValueString();
                        break;
                    case StorageType.String:
                        Test.Show(@"...String");
                    string a = "-";
                    if (par.AsValueString() != "" && par.AsValueString() != null)
                    {
                        Test.Show(@"...------");
                        a = par.AsValueString().Length.ToString();
                    }
                    
                    Test.Show(a);
                        return a;

                        break;
                    case StorageType.ElementId:
                        Test.Show(@"...ElementID");
                        Document doc = par.Element.Document;
                        string znachenie = "";
                        Element el = doc.GetElement(par.AsElementId());
                        if (el != null)
                            znachenie = el.Name;
                        else
                            znachenie = par.AsValueString();
                        return znachenie;
                        break;
                    default:
                        return null;
                        break;
                }

        }


    }

}
