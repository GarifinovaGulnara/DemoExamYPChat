using DemoExamYPChat.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DemoExamYPChat
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ChatServerEntities db = new ChatServerEntities();
        public static Employee employee;
    }
}
