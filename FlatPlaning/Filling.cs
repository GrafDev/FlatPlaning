#region Namespaces
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
#endregion

namespace FlatPlaning
{
    [Transaction(TransactionMode.Manual)]
    public class Filling : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            DataFlatPlaning.doc = doc;
            // ������ ����� ���������� �� ��������� �� �����
            DataFlatPlaning.ReadFromFile();
            // �������� ����� ���������� �� ������
            // ������ ������ 
            // ���������� ��������� ������� � ������ within a transaction

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}
