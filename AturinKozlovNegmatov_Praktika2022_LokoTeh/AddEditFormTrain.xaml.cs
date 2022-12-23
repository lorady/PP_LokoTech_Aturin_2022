using AturinKozlovNegmatov_Praktika2022_LokoTeh.Entities;
using Microsoft.Win32;
using System;

using System.Linq;

using System.Windows;

using System.Windows.Media.Imaging;


namespace AturinKozlovNegmatov_Praktika2022_LokoTeh
{
    /// <summary>
    /// Логика взаимодействия для AddEditFormTrain.xaml
    /// </summary>
    public partial class AddEditFormTrain : Window
    {
        public AddEditFormTrain()
        {

            InitializeComponent();
            //Подгрузка в комбобокс начальников из бд
            var nach = App.Context.Сотрудники.Where(c => c.Doljnost == 2).ToList();
            var nach1 = nach.Select(c => c.FIO).ToList();
            Nachalnik.ItemsSource = nach1.Distinct();

            //Подгрузка в комбобокс типов из бд
            var types = App.Context.Типы.Select(c => c.Тип).ToList();
            Type.ItemsSource = types.Distinct();
        }

        public Поезда _currentpoezd = null;

        public AddEditFormTrain(Поезда currentLokoteh)
        {
            InitializeComponent();

            //Подгрузка в комбобокс начальников из бд
            var nach = App.Context.Сотрудники.Where(c => c.Doljnost == 2).ToList();
            var nach1 = nach.Select(c => c.FIO).ToList();
            Nachalnik.ItemsSource = nach1.Distinct();
            //Подгрузка в комбобокс типов из бд
            var types = App.Context.Типы.Select(c => c.Тип).ToList();
            Type.ItemsSource = types.Distinct();

            _currentpoezd = currentLokoteh;
            Name.Text = _currentpoezd.Name_poezda;
            Massa.Text = _currentpoezd.Massa;
            Speed.Text = _currentpoezd.Speed;
            Condition.Text = _currentpoezd.Condition;
            Nachalnik.Text = _currentpoezd.Nachalnik.ToString();
            Type.SelectedItem = _currentpoezd.Type;
            Picture.Source = new BitmapImage(new Uri(_currentpoezd.Photo));
        }
        private string file, filename;


        private void AddEdit_Button_Click(object sender, RoutedEventArgs e)
        {
            string nachname = Nachalnik.Text;
            //Выбор айди из бд начальника, для дальнейшего добавления или редактирования
            var nach2 = App.Context.Сотрудники.Where(c => c.FIO == nachname).ToList();
            var nach3 = nach2.Select(c => c.Id_Worker).ToList();
            int idworker = Convert.ToInt32(nach3.FirstOrDefault());


            //var photography = App.Context.Поезда.Where(c => c.Nachalnik == idworker).ToList();
            //var photography2 = photography.Select(c => c.Photo).ToList();
            //file = photography2.FirstOrDefault().ToString();

            if (_currentpoezd == null)
            {
                try { 
                //Заглушка
                if (file == null || file == "")
                    file = $@"C:\Users\gagso\source\repos\AturinKozlovNegmatov_Praktika2022_LokoTeh\Images\picture.png";

                var poezd = new Поезда
                {
                    Name_poezda = Name.Text,
                    Massa = Massa.Text,
                    Speed = Speed.Text,
                    Condition = Condition.Text,
                    Nachalnik = idworker,
                    Type = Type.SelectedIndex + 1,
                    Photo = file
                };
                App.Context.Поезда.Add(poezd);
                App.Context.SaveChanges();
                this.Close();
                }
                catch
                {
                    MessageBox.Show("Вы что-то не выбрали");

                }

            }

            else
            {
                try { 
                _currentpoezd.Name_poezda = Name.Text;
                _currentpoezd.Massa = Massa.Text;
                _currentpoezd.Speed = Speed.Text;
                _currentpoezd.Condition = Condition.Text;
                _currentpoezd.Nachalnik = idworker;
                _currentpoezd.Type = Type.SelectedIndex + 1;
                _currentpoezd.Photo = file;

                App.Context.SaveChanges();
                this.Close();
                }
                catch
                {
                    MessageBox.Show("Вы что-то не выбрали");
                }
            }
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "image files (*.jpg;*.png) | *.jpg;*.png|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            file = openFileDialog.FileName;
            filename = openFileDialog.SafeFileName;
            try
            {
                Picture.Source = new BitmapImage(new Uri(file));
            }
            catch
            {
                string nachname = Nachalnik.Text;
                var nach2 = App.Context.Сотрудники.Where(c => c.FIO == nachname).ToList();
                var nach3 = nach2.Select(c => c.Id_Worker).ToList();
                int idworker = Convert.ToInt32(nach3.FirstOrDefault());
                var photography = App.Context.Поезда.Where(c => c.Nachalnik == idworker).ToList();
                var photography2 = photography.Select(c => c.Photo).ToList();
                
                //Заглушка
                if (file == null || file == "")
                    file = $@"C:\Users\gagso\source\repos\AturinKozlovNegmatov_Praktika2022_LokoTeh\Images\picture.png";
                else
                    file = photography2.FirstOrDefault().ToString();
                Picture.Source = new BitmapImage(new Uri(file));
            }
        }

        private void Cancel_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
