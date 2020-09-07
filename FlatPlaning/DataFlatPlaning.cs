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

namespace FlatPlaning
{

    internal class DataFlatPlaning
    {

        internal static Autodesk.Revit.DB.Document doc;
        static int nextNamberGuid = 0x0001;
        internal static string schemaGuid = "720080CB-DA99-40DC-9415-E53F280A" + nextNamberGuid.ToString();
        SchemaBuilder sb;
        // Приращение Guid
        internal DataFlatPlaning(string schemaName)
        {
            string tempSchemaGuid = schemaGuid;
            int lastLetter = schemaGuid.Length - 5;
            schemaGuid = schemaGuid.Substring(0, lastLetter);
            sb=new SchemaBuilder(new Guid(schemaGuid));
            nextNamberGuid++;
        }

        // Создаем описание хранилища
        internal Schema SetDataFlatPlaning()
        {
            sb.SetSchemaName("StorageDefaultParametersRooms");
            Schema sch = sb.Finish();
            return sch;
        }



        // Выбираем элемент для хранения
        // Записываем хранилище в элемент






        //Сичтываем информацию из хранилища в элементе
        static internal void ReadFromFile()
        {
            // Читаем содержимое хранилища. 
            TaskDialog.Show("ReadFromFile", "Ready");



        }

        // Проверяем наличие хранилища в элементе
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