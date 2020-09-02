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

namespace FlatPlaning
{
    static class FlatPlaningData
    {
        static internal void ReadFromFile()
        {
            TaskDialog.Show("ReadFromFile", "Ready");
        }
        static internal void WriteToFile()
        {
            TaskDialog.Show("WriteToFile", "Ready");
        }

    }
}
