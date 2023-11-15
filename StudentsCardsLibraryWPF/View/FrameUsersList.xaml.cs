using StudentsCardsLibraryWPF.Model;
using StudentsCardsLibraryWPF.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentsCardsLibraryWPF.View
{
    /// <summary>
    /// Логика взаимодействия для FrameUsersList.xaml
    /// </summary>
    public partial class FrameUsersList : Page
    {
        public FrameUsersList()
        {
            InitializeComponent();
            DataContext = new ViewModel.UsersListLogicBlock();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    UsersListLogicBlock MVM = new UsersListLogicBlock();
        //    MVM.UsersListPreLoaded();
        //}
    }
}