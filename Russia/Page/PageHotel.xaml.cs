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
    /// Логика взаимодействия для PageHotel.xaml
    /// </summary>
    public partial class PageHotel 
    {
        Strelki sp = new Strelki();
        List<Hotel> HotelFilter = new List<Hotel>();
        public PageHotel()
        {
            InitializeComponent();
            TableHotel.ItemsSource = Base.BD.Hotel.ToList();
            HotelFilter = Base.BD.Hotel.ToList();
            sp.CountPage = Base.BD.Hotel.ToList().Count();

            DataContext = sp;
        }

        private void btnTour_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Kol_vo_Tour_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<HotelOfTour> hotelOfTours = Base.BD.HotelOfTour.Where(x => x.HotelId == index).ToList();
            int a = 0;
            if(hotelOfTours.Count==0)
            {
                tb.Text ="0";
            }
           else
            {
                tb.Text = Convert.ToString(hotelOfTours.Count);
            }
        }

        private void btnRedactirovanie_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    sp.CurrentPage--;
                    break;
                case "next":
                    sp.CurrentPage++;
                    break;
                case "prev1":
                    sp.CurrentPage = 1;
                    break;
                case "next1":
                    {
                        List<Hotel> fl = Base.BD.Hotel.ToList();
                        int a = fl.Count;
                        if (kolvo_zapice.Text != "")
                        {
                            int b = Convert.ToInt32(kolvo_zapice.Text);

                            if (a % b == 0)
                            {
                                sp.CurrentPage = a / b;
                            }
                            else
                            {
                                sp.CurrentPage = a / b + 1;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите количество записей").ToString();
                        }

                    }
                    break;
                default:
                    sp.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            TableHotel.ItemsSource = HotelFilter.Skip(sp.CurrentPage * sp.CountPage - sp.CountPage).Take(sp.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
            // Skip(pc.CurrentPage* pc.CountPage - pc.CountPage) - сколько пропускаем записей
            // Take(pc.CountPage) - сколько записей отображаем на странице
        }

        private void kolvo_zapice_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sp.CountPage = Convert.ToInt32(kolvo_zapice.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                sp.CountPage = 10; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            sp.Countlist = HotelFilter.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            TableHotel.ItemsSource = HotelFilter.Skip(0).Take(sp.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            sp.CurrentPage = 1; // текущая страница - это страница 1
        }
    }
}
