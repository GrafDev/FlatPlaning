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

namespace FlatPlaning
{  
    
    internal class DataFlatPlaning
    {
        internal static Autodesk.Revit.DB.Document doc;
        // Создаем структуру данных, прикрепляем ее к данным , заполняем ее данными и получаем данные обратно 
        internal void StoreDataInProjectInfo(ProjectInfo pi, string dataToStore)
    {
        Transaction t = new Transaction(pi.Document, "tCreateAndStore");
        t.Start();
        SchemaBuilder sb =
                new SchemaBuilder(new Guid("720080CB-DA99-40DC-9415-E53F280AAAAF"));
        sb.SetReadAccessLevel(AccessLevel.Public); // разрешить кому угодно читать объект
            sb.SetWriteAccessLevel(AccessLevel.Vendor); // ограничить запись только этому поставщику
            sb.SetVendorId("ADSK"); // требуется из-за ограниченного доступа для записи
            sb.SetSchemaName("DefaultParameter");
            // создать поле для хранения string
            FieldBuilder fieldBuilder =
                sb.AddSimpleField("DefaultParameter", typeof(string));
        fieldBuilder.SetDocumentation("A stored location value representing a wiring splice in a wall.");

        Schema schema = sb.Finish(); // register the Schema object
        Entity entity = new Entity(schema); // create an entity (object) for this schema (class)
        // get the field from the schema
        Field fieldDefaultParameter = schema.GetField("DefaultParameter");
        // set the value for this entity
        entity.Set(fieldDefaultParameter, dataToStore);
        pi.SetEntity(entity); // store the entity in the element
        // get the data back from the wall
        Entity retrievedEntity = pi.GetEntity(schema);
        string retrievedData =
                retrievedEntity.Get<string>( schema.GetField("DefaultParameter"));
        t.Commit();

    }
        static internal void ReadFromFile()
        {
            // Читаем содержимое хранилища. 
            TaskDialog.Show("ReadFromFile", "Ready");



        }
        static internal void WriteToFile()
        {

            TaskDialog.Show("WriteToFile", "Ready");
            ProjectInfo pi = doc.ProjectInformation;
            DataFlatPlaning SetDefaultParameters = new DataFlatPlaning() ;
           
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Create storage");
                SetDefaultParameters.StoreDataInProjectInfo(pi, "RRRRRRRR");
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