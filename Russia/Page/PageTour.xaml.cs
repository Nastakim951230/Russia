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

namespace Russia.Page
{
    /// <summary>
    /// Логика взаимодействия для PageTour.xaml
    /// </summary>
    public partial class PageTour 
    {
        public PageTour()
        {
            InitializeComponent();
            ListTour.ItemsSource = Base.BD.Tour.ToList();
            Sortirovka.SelectedIndex = 0;

            List<Type> type = Base.BD.Type.ToList();
            Type.Items.Add("Все типы");
            for (int i = 0; i < type.Count; i++)  // цикл для записи в выпадающий список всех пород котов из БД
            {
                Type.Items.Add(type[i].Name);
            }
            Type.SelectedIndex = 0;
        }
        void Filter()
        {
            List<Tour> TourList = new List<Tour>();  // пустой список, который далее будет заполнять элементами, удавлетворяющими условиям фильтрации, поиска и сортировки

            TourList = Base.BD.Tour.ToList();
            //Поиск
            if (!string.IsNullOrWhiteSpace(Search.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                TourList = TourList.Where(x => x.Name.ToLower().Contains(Search.Text.ToLower())).ToList();
            }

            // выбор элементов только с актуальностью
            if (cbAktyal.IsChecked == true)
            {
                TourList = TourList.Where(x => x.IsActual != false).ToList();
            }

           if(Type.SelectedIndex >0)
            {
                
                TourList =TourList.Where(x=>x.TypeOfTour.Any(y=>y.TypeId==Type.SelectedIndex)).ToList();
            }


            // сортировкаy
            switch (Sortirovka.SelectedIndex)
            {
                case 0:
                    {
                        TourList.Sort((x, y) => x.Name.CompareTo(y.Name));
                    }
                    break;
                case 1:
                    {
                        TourList.Sort((x, y) => x.Name.CompareTo(y.Name));
                        TourList.Reverse();
                    }
                    break;
            }

           

            ListTour.ItemsSource = TourList;
            if (TourList.Count == 0)
            {
                MessageBox.Show("нет записей");
                Search.Text = "";
            }

        }
        private void cbAktyal_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Sortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
    }
}
