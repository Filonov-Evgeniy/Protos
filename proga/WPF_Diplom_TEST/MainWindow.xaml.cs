using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF_Diplom_TEST
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            trvMenu.Items.Add(Menu_Create());
    }

        static public MenuItem Menu_Create()
    {
            MenuItem root = new MenuItem() { Title = "Изделие" };
            MenuItem childItem1 = new MenuItem() { Title = "СЕ 1" };
            childItem1.Items.Add(new MenuItem() { Title = "Деталь 1" });
            childItem1.Items.Add(new MenuItem() { Title = "Деталь 2" });
            root.Items.Add(childItem1);
            root.Items.Add(new MenuItem() { Title = "СЕ 2" });
            return root;
        }
    }
}
