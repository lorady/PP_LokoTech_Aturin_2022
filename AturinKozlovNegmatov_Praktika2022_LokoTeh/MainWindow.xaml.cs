using System.Linq;
using System.Windows;


namespace AturinKozlovNegmatov_Praktika2022_LokoTeh
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTxt.Text;
            string password = PasswordTxt.Text;
            var Users = App.Context.Авторизация.ToList();
            int login_count = Users.Where(c => c.Login == login).Count();
            int pass_count = Users.Where(c => c.Password == password).Count();

            if (login_count > 0 && pass_count > 0)
            {
                MessageBox.Show("Вы авторизованы");
                login_count = 0;
                pass_count = 0;
                TrainsWindow CP = new TrainsWindow();
                CP.Show();
                this.Close();

            }
            else if (login_count == 0 || pass_count == 0)
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegForm RF = new RegForm();
            RF.ShowDialog();

        }
    }
}
