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

    public partial class MarkingBox : Window
    {
        private Document _doc;
        private UIDocument _UIDoc;

        private IEnumerable<WallType> _wallTypes;
        private IList<RoomTagType> _roomTagTypes;

        // Создаем окно выбора типов марок
        public MarkingBox(UIDocument UIDoc)
        {

            _doc = UIDoc.Document;
            Mark._doc = _doc;
            Mark._UIDoc = UIDoc;
            InitializeComponent();

            // Выбор типа Марки комнаты
            RoomTagFilter roomTagFilter = new RoomTagFilter();
            var _roomTagTypes = from elem in new FilteredElementCollector(_doc)
                       .OfCategory(BuiltInCategory.OST_RoomTags)
                       .ToList()
                                let type = elem as RoomTagType
                                where type != null
                                orderby type.Name
                                select type;

            // Bind ArrayList with the ListBox
            RoomTagListBox.ItemsSource = _roomTagTypes;
            RoomTagListBox.SelectedItem = RoomTagListBox.Items[0];
            // Обнаружение помещений для вытаскивания парметров
            //IList<Element> roomList = new FilteredElementCollector(_doc).OfCategory(BuiltInCategory.OST_RoomTags).ToList();
            this.ShowDialog();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            this.Close();
            Mark.roomTagType = RoomTagListBox.SelectedItem as RoomTagType;
            Marks.marks= SelectTags().ToList();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // выбор помещений
        private IEnumerable<RoomTag> SelectTags()
        {
            //Create a set of selected elements ids
            ICollection<ElementId> selectedObjectsIds = _UIDoc.Selection.GetElementIds();
            //Create a set of rooms
            IEnumerable<RoomTag> ModelTags = null;
            IList<RoomTag> tempList = new List<RoomTag>();

            // Проверяем выбраный вариант
            //..все на виде
            if (AllMarksOnView.IsChecked.Value)
            {
                ModelTags = from elem in new FilteredElementCollector(_doc, _doc.ActiveView.Id).OfClass(typeof(SpatialElement))
                             let room = elem as RoomTag
                             select room;
            }
            //..выбраные марки
            else if (SelectedMarks.IsChecked.Value)
            {
                
                if (selectedObjectsIds.Count != 0)
                {
                    // Find all rooms in selection
                    ModelTags = from elem in new FilteredElementCollector(_doc, selectedObjectsIds).OfClass(typeof(SpatialElement))
                                 let room = elem as RoomTag
                                 select room;
                    tempList = ModelTags.ToList();
                }


                if (tempList.Count == 0)
                {
                    //Create a selection filter on rooms
                    ISelectionFilter filter = new MarksSelectionFilter();

                    IList<Reference> rs = _UIDoc.Selection.PickObjects(ObjectType.Element, filter,
                        "Выберите марки");
                    
                    foreach (Reference r in rs)
                    {
                        tempList.Add(_doc.GetElement(r) as RoomTag);
                    }

                    
                    ModelTags = tempList;
                }


            }

           // ..если ничего не выбрано
            {
                if (ModelTags.LongCount() == 0 && (AllMarksOnView.IsChecked.Value))
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Ошибка выбора марок", "Команда не выполнена, так как не было найдено ни одной марки на данном виде. Попробуйте перейти на другой вид и выполнить команду еще раз.");
                    
                }
                else if (ModelTags.LongCount() == 0)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Ошибка выбора марок", "Не было выбрано ни одной марки.");
                    this.Activate();
                }
            }

            

            return ModelTags;
        }       
        
        public class MarksSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_RoomTags)
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

        private void SelectedMarks_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void MakeListBox(Document _doc)
        {
            var _roomTagTypes = from elem in new FilteredElementCollector(_doc)
                                   .OfCategory(BuiltInCategory.OST_RoomTags)
                                   .ToList()
                                let type = elem as RoomTagType
                                where type != null
                                orderby type.Name
                                select type;


        }

    }


}
