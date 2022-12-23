using AturinKozlovNegmatov_Praktika2022_LokoTeh.Entities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace AturinKozlovNegmatov_Praktika2022_LokoTeh
{
    /// <summary>
    /// Логика взаимодействия для TrainsWindow.xaml
    /// </summary>
    public partial class TrainsWindow : Window
    {
        public TrainsWindow()
        {
            InitializeComponent();
            //Подгрузка в комбобокс типов из бд
            var types = App.Context.Типы.Select(c => c.Тип).ToList();
            types.Insert(0, "Без фильтрации");
            FilterComboBox.ItemsSource = types.Distinct();
            FilterComboBox.SelectedIndex = 0;
            SortComboBox.SelectedIndex = 0;
            var currentLoko = App.Context.Поезда.ToList();
            LviewLokoTeh.ItemsSource = currentLoko;

            var currentLokot = App.Context.Поезда.Select(c => c.Type).ToList();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditFormTrain AEF = new AddEditFormTrain();
            AEF.ShowDialog();
            Update();

        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            var currentLokoteh = (sender as Button).DataContext as Поезда;
            AddEditFormTrain addLokoth = new AddEditFormTrain(currentLokoteh);
            Visibility = Visibility.Hidden;
            addLokoth.ShowDialog();
            Update();
            Visibility = Visibility.Visible;
        }

        private void Dellete_Button_Click(object sender, RoutedEventArgs e)
        {
           var currentLokomotiv = (sender as Button).DataContext as Поезда;
            if (MessageBox.Show($"Вы уверены, что хотите удалить услугу: " + $"{currentLokomotiv.Name_poezda}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
              App.Context.Поезда.Remove(currentLokomotiv);
              App.Context.SaveChanges();
              Update();
            }
        }

        void Update()
        {
            var lokomotiv = App.Context.Поезда.ToList();

            if (FilterComboBox.SelectedIndex == 0)
                lokomotiv = App.Context.Поезда.ToList();
            if (FilterComboBox.SelectedIndex > 0)
                lokomotiv = App.Context.Поезда.Where(c => c.Type == FilterComboBox.SelectedIndex).ToList();

            if (SortComboBox.SelectedIndex == 0)
                lokomotiv = App.Context.Поезда.ToList();
            if (SortComboBox.SelectedIndex == 1)
                lokomotiv = lokomotiv.OrderBy(p => p.Name_poezda).ToList();
            if (SortComboBox.SelectedIndex == 2)
                lokomotiv = lokomotiv.OrderByDescending(p => p.Name_poezda).ToList();

            if (SortComboBox.SelectedIndex == 0 && FilterComboBox.SelectedIndex == 0)
                lokomotiv = App.Context.Поезда.ToList();
            if (SortComboBox.SelectedIndex == 1 && FilterComboBox.SelectedIndex == 0)
                lokomotiv = lokomotiv.OrderBy(p => p.Name_poezda).ToList();
            if (SortComboBox.SelectedIndex == 2 && FilterComboBox.SelectedIndex == 0)
                lokomotiv = lokomotiv.OrderByDescending(p => p.Name_poezda).ToList();

            if (SortComboBox.SelectedIndex == 0 && FilterComboBox.SelectedIndex > 0)
            {
                lokomotiv = App.Context.Поезда.ToList();
                lokomotiv = lokomotiv.Where(c => c.Type == FilterComboBox.SelectedIndex).ToList();
            }

            if (SortComboBox.SelectedIndex == 1 && FilterComboBox.SelectedIndex > 0)
            {
                lokomotiv = lokomotiv.OrderBy(p => p.Name_poezda).ToList();
                lokomotiv = lokomotiv.Where(c => c.Type == FilterComboBox.SelectedIndex).ToList();
            }

            if (SortComboBox.SelectedIndex == 2 && FilterComboBox.SelectedIndex > 0)
            {
                lokomotiv = lokomotiv.OrderByDescending(p => p.Name_poezda).ToList();
                lokomotiv = lokomotiv.Where(c => c.Type == FilterComboBox.SelectedIndex).ToList();
            }

            lokomotiv = lokomotiv.Where(p => p.Name_poezda.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LviewLokoTeh.ItemsSource = lokomotiv;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
