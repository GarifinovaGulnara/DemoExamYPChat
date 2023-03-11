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

namespace DemoExamYPChat.View
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = App.db.Employee.Where(x => x.Username == UsernameTB.Text && x.Password == PassTB.Password).FirstOrDefault();
            if (user != null)
            {
                App.employee = user;
                MessageBox.Show($"Hello, {user.Username}");
                NavigationService.Navigate(new ListChatsPage());
            }
            else
            {
                MessageBox.Show("Invalid Username and/or Password");
            }
        }
    }
}
