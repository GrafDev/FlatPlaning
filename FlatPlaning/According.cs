#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using FlatPlaning;
#endregion

namespace FlatPlaning
{
    [Transaction(TransactionMode.Manual)]
    // Комманда соответсвия параметров
    public class According : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            DataFlatPlaning.WriteToFile();

            return Result.Succeeded;
        }
    }

}
