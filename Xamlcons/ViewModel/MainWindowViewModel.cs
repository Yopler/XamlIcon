using HandyControl.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Xamlcons.Model;

namespace Xamlcons.ViewModel
{
    public class MainWindowViewModel
    {
        public ObservableCollection<GeometryItem> IconList { get; set; } = new ObservableCollection<GeometryItem>();
        public ICommand MenuSelection { get; set; }
        public ICommand Copy { get; set; }

        public MainWindowViewModel()
        {
            SetCommand();
            MenuSelectionOnChange("Basic");
        }

        private void SetCommand()
        {
            MenuSelection = new RelayCommand<string>(selection => MenuSelectionOnChange(selection), _ => true);
            Copy = new RelayCommand<GeometryItem>(icon => CopyToClipBoard(icon), _ => true);
        }

        private void CopyToClipBoard(GeometryItem icon)
        {
            string xamlGeometry = String.Format("<Geometry x:Key=\"{0}\">{1}</Geometry>", icon.Name, icon.Data.ToString());
            Clipboard.SetData(DataFormats.Text, (Object)xamlGeometry);
        }

        private void MenuSelectionOnChange(string selection)
        {
            switch (selection)
            {
                case "Basic":
                    GenerateGeometries(2, "");
                    break;
                case "HandyControl":
                    GenerateGeometries(1, "Geometry");
                    break;
                case "ModernUI":
                    GenerateGeometries(3, "ModernUI");
                    break;
                case "Windows10":
                    GenerateGeometries(5, "Windows10");
                    break;
                case "FontAwesome Solid":
                    GenerateGeometries(4, "AwesomeSolid");
                    break;
                case "FontAwesome Regular":
                    GenerateGeometries(4, "AwesomeRegular");
                    break;
                case "FontAwesome Brand":
                    GenerateGeometries(4, "AwesomeBrand");
                    break;
                default:
                    throw new ArgumentException("The choosen Menu Selection not valid");
            }
        }

        public void GenerateGeometries(int dictionnaryIdex, string filter)
        {
            IconList.Clear();

            foreach (var key in Application.Current.Resources.MergedDictionaries[dictionnaryIdex].Keys.OfType<string>().OrderBy(item => item))
            {
                if (!key.EndsWith(filter)) continue;
                IconList.Add(new GeometryItem()
                {
                    Name = key,
                    Data = ResourceHelper.GetResource<Geometry>(key),
                });
            }
        }
    }
}
