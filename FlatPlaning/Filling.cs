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
                    //FloorsSetup floorsFinishesSetup = new FloorsSetup();
                    //Load the selection form
                    FillingBox fillingWindow = new FillingBox(uidoc);
                    fillingWindow.InitializeComponent();

                    dfp.PrintAreaRooms();
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
                    messageErr = Util.GetLanguageResources.GetString("Error") + ex.Message;
                    //Trace.WriteLine(ex.ToString());
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Failed;
                }

            }

        }
    }

}
