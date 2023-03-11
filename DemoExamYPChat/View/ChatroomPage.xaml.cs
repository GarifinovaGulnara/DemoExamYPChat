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
    /// Логика взаимодействия для ChatroomPage.xaml
    /// </summary>
    public partial class ChatroomPage : Page
    {
        public Chatroom chroom;
        public ChatroomPage(Chatroom chatroom)
        {
            InitializeComponent();
            chroom = chatroom;
            TopicChatTB.Text = chroom.Topic;
            ListUsersInChat.ItemsSource = App.db.EmployeeChatroom.Where(x => x.Chatroom_id == chroom.ID).ToList();
            ListMessage.ItemsSource = App.db.ChatMessage.Where(x=> x.Chatroom_id == chroom.ID).ToList();
            
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FindEmployeePage(chroom));
        }

        private void ChangeTopicBtn_Click(object sender, RoutedEventArgs e)
        {
            TopicChatTB.IsEnabled = true;
        }

        private void LeaveChatroomBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = App.db.EmployeeChatroom.Where(x => x.Employee_id == App.employee.ID && x.Chatroom_id == chroom.ID).FirstOrDefault();
            App.db.EmployeeChatroom.Remove(item);
            App.db.SaveChanges();
            MessageBox.Show("You left the chat");
            NavigationService.Navigate(new ListChatsPage());
        }

        private void SendMesBtn_Click(object sender, RoutedEventArgs e)
        {
            ChatMessage chatMessage = new ChatMessage();
            {
                chatMessage.Sender_id = App.employee.ID;
                chatMessage.Chatroom_id = chroom.ID;
                chatMessage.Date = DateTime.Now;
                chatMessage.Message = TextMessage.Text;
            }
            App.db.ChatMessage.Add(chatMessage);
            App.db.SaveChanges();
            ListMessage.ItemsSource = App.db.ChatMessage.Where(x => x.Chatroom_id == chroom.ID).ToList();
            TextMessage.Text = "";
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListChatsPage());
        }

        private void TopicChatTB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && (TopicChatTB.Text != "" || TopicChatTB.Text != " "))
            {
                chroom.Topic = TopicChatTB.Text;
                App.db.SaveChanges();
                TopicChatTB.IsEnabled = false;
            }
        }
    }
}
