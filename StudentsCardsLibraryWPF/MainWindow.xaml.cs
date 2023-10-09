using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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

namespace StudentsCardsLibraryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            string UserLibraryPath = Directory.GetCurrentDirectory();
            UserLibraryPath = UserLibraryPath.Replace("\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
        }


        private void PickFilterMethod_Click(object sender, RoutedEventArgs e)
        {
            PickFilterMethod OpenPickFilterMethodWindow = new PickFilterMethod();
            OpenPickFilterMethodWindow.Show();
            this.Close();
        }
        public void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUser OpenCreateUserWindow = new CreateUser();
            OpenCreateUserWindow.Show();
            this.Close();
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
