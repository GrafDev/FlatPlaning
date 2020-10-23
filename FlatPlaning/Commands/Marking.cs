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
    public class Marking : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string messageErr,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            using (Transaction tx = new Transaction(doc))
            {
                try
                {

                    tx.Start("Transaction Name");

                    Document document = doc;
                    MarkingBox markingWindow = new MarkingBox(uidoc);
                    
                    //markingWindow.InitializeComponent();
                    //List<RoomTag> roomTags = new List<RoomTag>(SelectTags(uidoc,doc).ToList());
                    //ShowMarksName(roomTags);

                    tx.Commit();


                    return Result.Succeeded;
                }

                catch (Autodesk.Revit.Exceptions.OperationCanceledException exceptionCanceled)
                {
                    messageErr = exceptionCanceled.Message;
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Cancelled;
                }
                catch (ErrorMessageException errorEx)
                {
                    // checked exception need to show in error messagebox
                    messageErr = errorEx.Message;
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Failed;
                }
                catch (Exception ex)
                {
                    // unchecked exception cause command failed
                    messageErr = "Ошибка выбора";
                    //Trace.WriteLine(ex.ToString());
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Failed;
                }

            }

        }
        private IEnumerable<RoomTag> SelectTags(UIDocument _uidoc,Document _doc)
        {
            //Create a set of selected elements ids
            ICollection<ElementId> selectedObjectsIds = _uidoc.Selection.GetElementIds();
            //Create a set of rooms
            IEnumerable<RoomTag> ModelTags = null;
            IList<RoomTag> tempList = new List<RoomTag>();
            //..все на виде

            ModelTags = from elem in new FilteredElementCollector(_doc, _doc.ActiveView.Id).OfClass(typeof(SpatialElement))
                        let roomTags = elem as RoomTag
                        select roomTags;


            // ..если ничего не выбрано
            {
                if (ModelTags.LongCount() == 0)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Ошибка выбора помещений", "Команда не выполнена, так как не было найдено ни одного помещения на данном виде. Попробуйте перейти на другой вид и выполнить команду еще раз.");

                }
                else if (ModelTags.LongCount() == 0)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Ошибка выбора помещений", "Не было выбрано ни одного помещения.");
                }
            }

            Test.Show(ModelTags.Count().ToString());
            return ModelTags;
        }
        private void ShowMarksName(List<RoomTag> roomTags)
        {
            string tempString = "";
            foreach(RoomTag roomTag in roomTags)
            {
                tempString = tempString+roomTag.Name + "\n";
            }
            Test.Show(tempString);
        }
    }

}
