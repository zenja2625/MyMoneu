using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MyMoney.ViewModels;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyMoney.Views
{
    /// <summary>
    /// Логика взаимодействия для Purse.xaml
    /// </summary>
    public partial class Purse : UserControl
    {
        public Purse()
        {
            InitializeComponent();
            DataContext = new PurseViewModel();
        }
    }
}
