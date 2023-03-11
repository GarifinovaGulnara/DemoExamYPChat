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
    /// Логика взаимодействия для FindEmployeePage.xaml
    /// </summary>
    public partial class FindEmployeePage : Page
    {
        Chatroom chroom;
        public FindEmployeePage()
        {
            InitializeComponent();
        }
        public FindEmployeePage(Chatroom chatroom)
        {
            InitializeComponent();
            chroom = chatroom;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if(chroom != null)
            {
                NavigationService.Navigate(new ChatroomPage(chroom));
            }
            else
            {
                NavigationService.Navigate(new ListChatsPage());
            }
        }

        private void SearchEmployeeTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var res = App.db.Employee.Where(x=> x.Name.Contains(SearchEmployeeTB.Text.ToLower())).ToList();
            ListEmployee.ItemsSource = res.ToList();
        }

        private void ListEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ListEmployee.SelectedItem as Employee;
            if (chroom != null)
            {
                EmployeeChatroom emChat = new EmployeeChatroom();
                {
                    emChat.Chatroom_id = chroom.ID;
                    emChat.Employee_id = item.ID;
                }
                App.db.EmployeeChatroom.Add(emChat);
                App.db.SaveChanges();
                MessageBox.Show("Employee added");
            }
            else
            {
                try
                {
                    Chatroom chatroom = new Chatroom();
                    {
                        chatroom.Topic = item.Name;
                    }
                    App.db.Chatroom.Add(chatroom);
                    App.db.SaveChanges();
                    EmployeeChatroom employeeChatroom = new EmployeeChatroom();
                    {
                        employeeChatroom.Chatroom_id = chatroom.ID;
                        employeeChatroom.Employee_id = item.ID;
                    }
                    EmployeeChatroom employeeChatroom1 = new EmployeeChatroom();
                    {
                        employeeChatroom1.Chatroom_id = chatroom.ID;
                        employeeChatroom1.Employee_id = App.employee.ID;
                    }
                    App.db.EmployeeChatroom.Add(employeeChatroom);
                    App.db.EmployeeChatroom.Add(employeeChatroom1);
                    App.db.SaveChanges();
                    NavigationService.Navigate(new ChatroomPage(chatroom));
                }
                catch(Exception ex) { MessageBox.Show($"{ex}"); }
            }
        }
    }
}
