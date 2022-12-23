using System.Windows;


namespace AturinKozlovNegmatov_Praktika2022_LokoTeh
{
    /// <summary>
    /// Логика взаимодействия для RegForm.xaml
    /// </summary>
    public partial class RegForm : Window
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTxt.Text == PasswordSecondTxt.Text)
            {
                var users = new Entities.Авторизация
                {
                    Login = LoginTxt.Text,
                    Password = PasswordSecondTxt.Text
                };
                App.Context.Авторизация.Add(users);
                App.Context.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались");
                this.Close();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }

        }
    }
}
