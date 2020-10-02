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

namespace FlatPlaning
{

    static internal class dfp
    {

        static internal Autodesk.Revit.DB.Document doc;
        /*const  string SCHEMA_CURRENT_GUID = "720080CB-DA99-40DC-9415-E53F280A000C";
        static SchemaBuilder sb = new SchemaBuilder(new Guid(SCHEMA_CURRENT_GUID));*/
        static internal bool dialogBox=true;

        internal const string  DEFAULT_NUMBER_FLAT = "ADSK_Номер квартиры",
                      DEFAULT_TYPE_ROOM = "ADSK_Тип помещения",
                      DEFAULT_AREA_FLAT = "ADSK_Площадь квартиры",
                      DEFAULT_AREA_FLAT_COMMON = "ADSK_Площадь квартиры общая",
                      DEFAULT_AREA_FLAT_LIVE = "ADSK_Площадь квартиры жилая",
                      DEFAULT_COUNT_ROOM = "ADSK_Количество комнат",
                      DEFAULT_COEFFICIENT_ROOM = "ADSK_Коэффициент площади",
                      DEFAULT_AREA_WITH_COEFFICIENT = "ADSK_Площадь с коэффициентом",
                      DEFAULT_INDEX_ROOM = "ADSK_Индекс помещения";

        static internal string currentNumberFlat;
        static internal string currentTypeRoom;
        static internal string currentAreaFlat;
        static internal string currentAreaFlatCommon;
        static internal string currentAreaFlatLiving;
        static internal string currentCountRoom;
        static internal string currentCoefficientRoom;
        static internal string currentAreaWithCoefficient;
        static internal string currentIndexRoom;
        static internal List<string> listParameters = new List<string> 
            {
            currentNumberFlat,
            currentTypeRoom,
            currentAreaFlat,
            currentAreaFlatCommon,
            currentAreaFlatLiving,
            currentCountRoom,
            currentCoefficientRoom,
            currentAreaWithCoefficient,
            currentIndexRoom
            }; 

        static dfp()
            {
            currentNumberFlat = DEFAULT_NUMBER_FLAT;
            currentTypeRoom = DEFAULT_TYPE_ROOM;
            currentAreaFlat= DEFAULT_AREA_FLAT;
            currentAreaFlatCommon = DEFAULT_AREA_FLAT_COMMON;
            currentAreaFlatLiving= DEFAULT_AREA_FLAT_LIVE;
            currentCountRoom= DEFAULT_COUNT_ROOM;
            currentCoefficientRoom= DEFAULT_COEFFICIENT_ROOM;
            currentAreaWithCoefficient= DEFAULT_AREA_WITH_COEFFICIENT;
            currentIndexRoom= DEFAULT_INDEX_ROOM;
            }



        // Создание полей


        // Чтение параметров из хранилища
        // Проверка наличия хранилища в элементе
        // Выбираем элемент для хранения
        // Записываем хранилище в элемент

        //Сичтываем информацию из хранилища в элементе
        static internal void ReadFromFile()//TODO:
        {
            // Читаем содержимое хранилища. 
            TaskDialog.Show("ReadFromFile", "ReadFromFile");


        }


        static internal void WriteToFile()//TODO:
        {

            TaskDialog.Show("WriteToFile", "WriteToFile");


            using (Transaction t = new Transaction(doc))
            {
                t.Start("Create storage");

                t.Commit();
            }

        }


    }

}

/*internal static void SetDefaultParameters()
{

    SchemaBuilder sb = new SchemaBuilder(new Guid(schemaGuid));
    sb.AddSimpleField("DefaultNumberFlat", typeof(string));
    sb.AddSimpleField("DefaultTypeRoom", typeof(string));
    sb.AddSimpleField("DefaultAreaFlat", typeof(string));
    sb.AddSimpleField("DefaultAreaFlatCommon", typeof(string));
    sb.AddSimpleField("DefaultAreaFlatLive", typeof(string));
    sb.AddSimpleField("DefaultCountRoom", typeof(string));
    sb.AddSimpleField("DefaultCoefficientRoom", typeof(string));
    sb.AddSimpleField("DefaultAreaWithCoefficient", typeof(string));
    sb.AddSimpleField("DefaultIndexRoom", typeof(string));
    sb.SetSchemaName("StorageDefaultParametersRooms");

    sb.AddSimpleField("NumberFlat", typeof(string));
    sb.AddSimpleField("TypeRoom", typeof(string));
    sb.AddSimpleField("AreaFlat", typeof(string));
    sb.AddSimpleField("AreaFlatCommon", typeof(string));
    sb.AddSimpleField("AreaFlatLive", typeof(string));
    sb.AddSimpleField("CountRoom", typeof(string));
    sb.AddSimpleField("CoefficientRoom", typeof(string));
    sb.AddSimpleField("AreaWithCoefficient", typeof(string));
    sb.AddSimpleField("IndexRoom", typeof(string));
    sb.SetSchemaName("StorageParametersRooms");

    Schema schDefault = sb.Finish();
    Schema schCurrent = sb.Finish();

    ProjectInfo pi = doc.ProjectInformation;
    Element elem = pi as Element;

    using (Transaction t = new Transaction(doc))
    {
        Field fdNumberFlat = schDefault.GetField("DefaultNumberFlat"),
                    fdTypeRoom = schDefault.GetField("DefaultTypeRoom"),
                    fdAreaFlat = schDefault.GetField("DefaultAreaFlat"),
                    fdAreaFlatCommon = schDefault.GetField("DefaultAreaFlatCommon"),
                    fdAreaFlatLive = schDefault.GetField("DefaultAreaFlatLive"),
                    fdCountRoom = schDefault.GetField("DefaultCountRoom"),
                    fdCoefficientRoom = schDefault.GetField("DefaultCoefficientRoom"),
                    fdAreaWithCoefficient = schDefault.GetField("DefaultAreaWithCoefficient"),
                    fdIndexRoom = schDefault.GetField("DefaultIndexRoom");

        t.Start("Create storage");
        Entity ent = new Entity(schDefault);
        ent.Set<string>(fdNumberFlat, "ADSK_Номер квартиры");
        elem.SetEntity(ent);
        ent.Set<string>(fdTypeRoom, "ADSK_Тип помещения");
        elem.SetEntity(ent);
        ent.Set<string>(fdAreaFlat, "ADSK_Площадь квартиры");
        elem.SetEntity(ent);
        ent.Set<string>(fdAreaFlatCommon, "ADSK_Площадь квартиры общая");
        elem.SetEntity(ent);
        ent.Set<string>(fdAreaFlatLive, "ADSK_Площадь квартиры жилая");
        elem.SetEntity(ent);
        ent.Set<string>(fdCountRoom, "ADSK_Число комнат");
        elem.SetEntity(ent);
        ent.Set<string>(fdCoefficientRoom, "ADSK_Коэффициент площади");
        elem.SetEntity(ent);
        ent.Set<string>(fdAreaWithCoefficient, "ADSK_Площадь с коэффициентом");
        elem.SetEntity(ent);
        ent.Set<string>(fdIndexRoom, "ADSK_Индекс помещения");
        elem.SetEntity(ent);
        t.Commit();
    }

}*/