using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using System.Globalization;
using System.Resources;
using System.Reflection.Emit;
using System.Reflection;
using Autodesk.Windows;

namespace FlatPlaning
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>

    public partial class FillingBox : Window
    {
        private Document _doc;
        private UIDocument _UIDoc;
        
        // Создаем окно выбора помещений
        public FillingBox(UIDocument UIDoc)
        {
            _doc = UIDoc.Document;
            _UIDoc = UIDoc;
            InitializeComponent();
            check_dialogbox.IsChecked = dfp.dialogBox;
            this.ShowDialog();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {  
            this.Close();
            if (check_dialogbox.IsChecked.Value)
            {
                dfp.dialogBox = true;
            }
            else
            {
                dfp.dialogBox = false;
            }
            //.. Выбираем помещения в проекте
            Rooms rooms = new Rooms(SelectRooms().ToList());
            //.. Creat virtual flats
            Flats flats = new Flats(rooms._rooms);
            flats.CalculationParametersFlat();
            flats.WriteParametersToRooms();

            //flats.ShowFlats();
        }

        // выбор помещений
        private IEnumerable<Room> SelectRooms()
        {
            //Create a set of selected elements ids
            ICollection<ElementId> selectedObjectsIds = _UIDoc.Selection.GetElementIds();
            //Create a set of rooms
            IEnumerable<Room> ModelRooms = null;
            IList<Room> tempList = new List<Room>();

            // Проверяем выбраный вариант
            //..все на виде
            if (AllRoomsOnView.IsChecked.Value)
            {
                ModelRooms = from elem in new FilteredElementCollector(_doc, _doc.ActiveView.Id).OfClass(typeof(SpatialElement))
                             let room = elem as Room
                             select room;
            }
            //..выбраные помещения
            else if (SelectedRooms.IsChecked.Value)
            {
                
                if (selectedObjectsIds.Count != 0)
                {
                    // Find all rooms in selection
                    ModelRooms = from elem in new FilteredElementCollector(_doc, selectedObjectsIds).OfClass(typeof(SpatialElement))
                                 let room = elem as Room
                                 select room;
                    tempList = ModelRooms.ToList();
                }


                if (tempList.Count == 0)
                {
                    //Create a selection filter on rooms
                    ISelectionFilter filter = new RoomSelectionFilter();

                    IList<Reference> rs = _UIDoc.Selection.PickObjects(ObjectType.Element, filter,
                        "Выберите помещения");

                    foreach (Reference r in rs)
                    {
                        tempList.Add(_doc.GetElement(r) as Room);
                    }


                    ModelRooms = tempList;
                }
            }
            //..все в проекте.
            else if (AllRooms.IsChecked.Value)
            {
                ModelRooms = from elem in new FilteredElementCollector(_doc).OfClass(typeof(SpatialElement))
                             let room = elem as Room
                             select room;
            }
           // ..если ничего не выбрано
            {
                if (ModelRooms.LongCount() == 0 && (AllRooms.IsChecked.Value||AllRoomsOnView.IsChecked.Value))
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Ошибка выбора помещений", "Команда не выполнена, так как не было найдено ни одного помещения на данном виде. Попробуйте перейти на другой вид и выполнить команду еще раз.");
                    
                }
                else if (ModelRooms.LongCount() == 0)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Ошибка выбора помещений", "Не было выбрано ни одного помещения.");
                    this.Activate();

                }
            }


            return ModelRooms;
        }        

        private string GetListNameRooms(IEnumerable<Room> Rooms)
        {
            string listRooms = "";
            foreach (Room r in Rooms)
            {
                listRooms = listRooms + "\n" + r.Name;
            }
            return listRooms;
        }
        public class RoomSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Rooms)
                {
                    return true;
                }
                return false;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }

        private void check_dialogbox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

}
