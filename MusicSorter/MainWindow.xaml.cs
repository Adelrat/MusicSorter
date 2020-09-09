using System;
using System.Collections.Generic;
using System.IO;
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
using System.Diagnostics;
using Microsoft.Win32;

namespace MusicSorter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BtnSimpleSort.Click += delegate { SimpleSorter(); };
            BtnMediumSort.Click += delegate { MediumSort(); };
        }

        //присваивает новое название, состоящее только из цифр
        private void SimpleSorter()
        {
            Sorter("Int");
        }

        //присваивает новое название, состоящее из цифр+старого названия
        private void MediumSort()
        {
            Sorter("Int+old");
        }

        private void Sorter(string mode)
        {
            string[] paths;
            int newNameInt;
            string newName;
            string[] pathMas;
            string newPath;
            string format;
            string oldName;
            //открываем проводник
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                //paths = new string[openFileDialog.];
                paths = openFileDialog.FileNames;
                foreach (var item in paths)
                {
                    newNameInt = Math.Abs(Guid.NewGuid().GetHashCode() % 5000); //рандомное название от 0 до 5000
                    newName = newNameInt.ToString();
                    pathMas = item.Split('\\');
                    oldName = pathMas.Last();
                    format = item.Split('.').Last();
                    pathMas = pathMas.Take(pathMas.Count() - 1).ToArray();
                    newPath = String.Join("\\", pathMas);
                    if (mode == "Int")
                    {
                        newPath += '\\' + newName + '.' + format;
                    }
                    else if (mode == "Int+old")
                    {
                        newPath += '\\' + newName + oldName+ '.' + format;
                    }
                    File.Move(item, newPath); //меняем названия 
                }
            }
        }
    }
}
