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
    /// Логика взаимодействия для PageAdd_Edit_Hotel.xaml
    /// </summary>
    public partial class PageAdd_Edit_Hotel 
    {
        Hotel hotel;
        bool flagUpdate;
        public void uploadFields()
        {
            country_hotel.ItemsSource = Base.BD.Country.ToList();
            country_hotel.SelectedValuePath = "Code";
            country_hotel.DisplayMemberPath = "Name";
        }
        public PageAdd_Edit_Hotel()
        {
            InitializeComponent();
            uploadFields();
        }
        public PageAdd_Edit_Hotel(Hotel hotel)
        {
            InitializeComponent();
            uploadFields();
            flagUpdate = true;
            this.hotel = hotel;
            Name_Hotel.Text= hotel.Name;
            Kolvo_zvezd.Text = Convert.ToString(hotel.CountOfStars);
            Opisanie.Text = hotel.Description;
            List<Country> countr=Base.BD.Country.Where(x=>x.Code==hotel.CountryCode).ToList();
            string cou = "";
            foreach(Country countri in countr)
            {
                cou= countri.Name;
            }
            country_hotel.Text= cou;
        }
        
        public bool kolvo_zv(string a)
        {
            try
            {
                int kol_vo=Convert.ToInt32(a);
                if((kol_vo>0)&&(kol_vo<=5))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Количество звезд должно быть от 0 до 5", "Ошибка", MessageBoxButton.OK);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Напишите количество звезд цифрами", "Ошибка", MessageBoxButton.OK);
                return false;
            }
        }
        private void add_edit_Click(object sender, RoutedEventArgs e)
        {
           if(country_hotel.Text ==""|| Name_Hotel.Text==""|| Kolvo_zvezd.Text==""||Opisanie.Text=="")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK);
            }
           else
            {
                if(kolvo_zv(Kolvo_zvezd.Text))
                {
                    if (flagUpdate == false)
                    {
                         hotel= new Hotel();
                    }
                   
                    hotel.Name = Name_Hotel.Text;
                    hotel.CountOfStars = Convert.ToInt32(Kolvo_zvezd.Text);
                    string count = country_hotel.Text;
                    List<Country> countries = Base.BD.Country.Where(x => x.Name == count).ToList();
                    string code="";
                    foreach (Country country in countries)
                    {
                        code = country.Code;
                    }
                    hotel.CountryCode = code;
                    hotel.Description = Opisanie.Text;
                    if (flagUpdate == false)
                    {
                        Base.BD.Hotel.Add(hotel);
                    }
                    Base.BD.SaveChanges();
                    MessageBox.Show("Информация добавлена");

                    NavigateFrame.perehod.Navigate(new Page.PageHotel());
                }
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            NavigateFrame.perehod.Navigate(new Page.PageHotel());
        }
    }
}
