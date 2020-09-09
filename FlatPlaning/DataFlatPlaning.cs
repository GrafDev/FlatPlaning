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

namespace FlatPlaning
{

    internal class DataFlatPlaning
    {

        static internal Autodesk.Revit.DB.Document doc;
        static internal Element elem;
        static internal string schemaCurrentGuid = "720080CB-DA99-40DC-9415-E53F280A000C";
        static SchemaBuilder sb = new SchemaBuilder(new Guid(schemaCurrentGuid));

        DataFlatPlaning()
        {
            // Создание хранилища параметров
            // ..Описание хранилища
            sb.SetReadAccessLevel(AccessLevel.Public);
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
            Schema sch = sb.Finish();
            //..Выбор элемента для хранения
            ProjectInfo pi = doc.ProjectInformation;
            elem=pi as Element;
            //..занесение данных в элемент
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Create storage");
                Entity ent = new Entity(sch);
                Field nameField=sch.GetField("NumberFlat");
                ent.Set<string>(nameField, "ADSK_Номер квартиры");
                nameField = sch.GetField("TypeRoom");
                ent.Set<string>(nameField, "ADSK_Тип помещения");
                nameField = sch.GetField("AreaFlat");
                ent.Set<string>(nameField, "ADSK_Площадь квартиры");
                nameField = sch.GetField("AreaFlatCommon");
                ent.Set<string>(nameField, "ADSK_Площадь квартиры общая");
                nameField = sch.GetField("AreaFlatLive");
                ent.Set<string>(nameField, "ADSK_Площадь квартиры жилая");
                nameField = sch.GetField("CountRoom");
                ent.Set<string>(nameField, "ADSK_Число комнат");
                nameField = sch.GetField("CoefficientRoom");
                ent.Set<string>(nameField, "ADSK_Коэффициент площади");
                nameField = sch.GetField("AreaWithCoefficient");
                ent.Set<string>(nameField, "ADSK_Площадь с коэффициентом");
                nameField = sch.GetField("IndexRoom");
                ent.Set<string>(nameField, "ADSK_Индекс помещения");
                elem.SetEntity(ent);
                t.Commit();
            }
        }



        // Создание полей


        // Чтение параметров из хранилища
        // Проверка наличия хранилища в элементе







        // Выбираем элемент для хранения
        // Записываем хранилище в элемент






        //Сичтываем информацию из хранилища в элементе
        static internal void ReadFromFile()
        {
            // Читаем содержимое хранилища. 
            TaskDialog.Show("ReadFromFile", "Ready");



        }


        static internal void WriteToFile()
        {

            TaskDialog.Show("WriteToFile", "Ready");


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