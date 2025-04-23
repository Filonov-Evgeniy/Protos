using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Diplom_TEST
{
    class TreeMenu
    {
        public MenuItem createMenu()
        {
            MenuItem root = new MenuItem() { Title = "Изделие" };
            MenuItem childItem1 = new MenuItem() { Title = "СЕ 1" };
            childItem1.Items.Add(new MenuItem() { Title = "Деталь 1" });
            childItem1.Items.Add(new MenuItem() { Title = "Деталь 2" });
            root.Items.Add(childItem1);
            root.Items.Add(new MenuItem() { Title = "СЕ 2" });
            root.Items.Add(new MenuItem() { Title = "Мамут Рахал" });
            root.Items.Add(new MenuItem() { Title = "Сынша Лавы" });
            return root;
        }
    }
}
