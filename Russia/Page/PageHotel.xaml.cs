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
            NavigateFrame.perehod.Navigate(new Page.PageTour());
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigateFrame.perehod.Navigate(new Page.PageAdd_Edit_Hotel());
        }

        private void btnRedactirovanie_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;  
            int index = Convert.ToInt32(btn.Uid);   
            Hotel hotel = Base.BD.Hotel.FirstOrDefault(x => x.Id == index);
            NavigateFrame.perehod.Navigate(new Page.PageAdd_Edit_Hotel(hotel));
        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Hotel hotel = Base.BD.Hotel.FirstOrDefault(x => x.Id == index);
           List<HotelOfTour> hotelOftour = Base.BD.HotelOfTour.Where(x => x.HotelId == hotel.Id).ToList();

          if(hotelOftour.Count > 0)
            {
                int kolHO = hotelOftour.Count;
                int kol = 0;
                foreach(HotelOfTour hotelOfTour in hotelOftour)
                {
                    kol++;
                    Tour hotelList = Base.BD.Tour.FirstOrDefault(x=>x.Id==hotelOfTour.TourId);
                    if (hotelList.IsActual == true)
                    {
                        MessageBox.Show("Данный отель не может быть удален, так как находиться в списке актуальных для туров");
                        break;
                    }
                    else
                    {
                        if (kol == kolHO)
                        {
                            Base.BD.Hotel.Remove(hotel);
                            Base.BD.SaveChanges();
                            NavigateFrame.perehod.Navigate(new Page.PageHotel());
                        }
                    }
                }
              
            }
            else
            {
                Base.BD.Hotel.Remove(hotel);
                Base.BD.SaveChanges();
                NavigateFrame.perehod.Navigate(new Page.PageHotel());
            }
        }
    }
}
