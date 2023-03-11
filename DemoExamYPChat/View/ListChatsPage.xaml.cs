using DemoExamYPChat.DB;
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
    /// Логика взаимодействия для ListChatsPage.xaml
    /// </summary>
    public partial class ListChatsPage : Page
    {
        public ListChatsPage()
        {
            InitializeComponent();
            ListChats.ItemsSource = App.db.EmployeeChatroom.Where(x=>x.Employee_id == App.employee.ID).ToList();
            NameUserTB.Text = App.employee.Name;
        }

        private void CloseAppBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void EmpFinderBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FindEmployeePage());
        }

        private void ListChats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var idChat = ListChats.SelectedItem as EmployeeChatroom;
                var item = App.db.Chatroom.Where(x => x.ID == idChat.Chatroom_id).FirstOrDefault();
                NavigationService.Navigate(new ChatroomPage(item));
            }
            catch(Exception ex) { MessageBox.Show($"{ex}"); }
        }
    }
}
