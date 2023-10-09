using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace StudentsCardsLibraryWPF
{
    /// <summary>
    /// Логика взаимодействия для PickFilterMethod.xaml
    /// </summary>
    public partial class PickFilterMethod : Window
    {
        public PickFilterMethod()
        {
            InitializeComponent();

        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow OpenMainMenuWindow = new MainWindow();
            OpenMainMenuWindow.Show();
            this.Close();
        }

        private void Method1_Click(object sender, RoutedEventArgs e)
        {
            string UserAppConfigPath = Directory.GetCurrentDirectory();
            UserAppConfigPath = UserAppConfigPath.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.FilterMethod = 1;
            UConfig.UpdateAppConfig(Config);
            ViewUser OpenViewUserWindow = new ViewUser();
            OpenViewUserWindow.Show();
            this.Close();
        }

        private void Method2_Click(object sender, RoutedEventArgs e)
        {
            string UserAppConfigPath = Directory.GetCurrentDirectory();
            UserAppConfigPath = UserAppConfigPath.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.FilterMethod = 2;
            UConfig.UpdateAppConfig(Config);
            ViewUser OpenViewUserWindow = new ViewUser();
            OpenViewUserWindow.Show();
            this.Close();
        }

        private void Method3_Click(object sender, RoutedEventArgs e)
        {
            string UserAppConfigPath = Directory.GetCurrentDirectory();
            UserAppConfigPath = UserAppConfigPath.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.FilterMethod = 3;
            UConfig.UpdateAppConfig(Config);
            ViewUser OpenViewUserWindow = new ViewUser();
            OpenViewUserWindow.Show();
            this.Close();
        }

        private void Method4_Click(object sender, RoutedEventArgs e)
        {
            string UserAppConfigPath = Directory.GetCurrentDirectory();
            UserAppConfigPath = UserAppConfigPath.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.FilterMethod = 4;
            UConfig.UpdateAppConfig(Config);
            ViewUser OpenViewUserWindow = new ViewUser();
            OpenViewUserWindow.Show();
            this.Close();
        }

        private void Method5_Click(object sender, RoutedEventArgs e)
        {
            string UserAppConfigPath = Directory.GetCurrentDirectory();
            UserAppConfigPath = UserAppConfigPath.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.FilterMethod = 5;
            UConfig.UpdateAppConfig(Config);
            ViewUser OpenViewUserWindow = new ViewUser();
            OpenViewUserWindow.Show();
            this.Close();
        }
    }
}
