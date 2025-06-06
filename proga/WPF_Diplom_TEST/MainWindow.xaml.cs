﻿using System;
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
        List<MenuItem> itemsToAdd = new List<MenuItem>();
        public MainWindow()
        {
            InitializeComponent();
            trvMenu.Items.Add(Menu_Create());
        }

        static public MenuItem Menu_Create()
        {
            TreeMenu menu = new TreeMenu(1);
            MenuItem item = menu.createMenu();
            return item;
        }

        private void CopyTreeItemButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = trvMenu.SelectedItem as MenuItem;
            TreeMenu.copyMenuItem(selectedItem);
        }

        private void InsertTreeItemButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = trvMenu.SelectedItem as MenuItem;
            var insertedItem = TreeMenu.InsertMenuItem();
            if (insertedItem != null)
            {
                insertedItem.Parent = selectedItem;
                selectedItem.Items.Add(insertedItem);
            }
        }

        private void AddNewTreeItemButton_Click(object sender, RoutedEventArgs e)
        {
            ItemList list = new ItemList(itemsToAdd);
            if (list.ShowDialog() == true)
            {
                var selectedItem = trvMenu.SelectedItem as MenuItem;
                foreach (MenuItem item in itemsToAdd)
                {
                    selectedItem.Items.Add(item);
                }
                MessageBox.Show("Элементы добавлены");
            }
        }

        private void DeleteTreeItemButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = trvMenu.SelectedItem as MenuItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Не выбран элемент для удаления");
            }
            else
            {
                selectedItem.Parent.Items.Remove(selectedItem);
            }
        }

        private void SearchText_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = SearchTreeItem;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Поиск";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void SearchText_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = SearchTreeItem;
            if (textBox.Text == "Поиск")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }
    } 
}
