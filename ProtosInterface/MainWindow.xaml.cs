﻿using Microsoft.EntityFrameworkCore;
using ProtosInterface.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProtosInterface;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    List<MenuItem> itemsToAdd = new List<MenuItem>();
    List<MenuItem> productsList = new List<MenuItem>();
    dbDataLoader dbLoader = new dbDataLoader();
    int itemid = -1;
    public MainWindow()
    {
        InitializeComponent();
        productsList = dbLoader.getProductData();
        productsComboBox.ItemsSource = productsList;
        productsComboBox.DisplayMemberPath = "Title";
        productsComboBox.SelectedIndex = 0;
    }

    static public MenuItem Menu_Create(int id)
    {
        TreeMenu menu = new TreeMenu(id);
        MenuItem item = menu.createMenu();
        return item;
    }

    private void SaveEdition_Click(object sender, RoutedEventArgs e)
    {
        SaveWindow message = new SaveWindow();
        message.Owner = this;
        message.ShowDialog();
        switch (message.Result)
        {
            case SaveWindow.SaveOption.SaveAsNew:
                MessageBox.Show("сохранил");
                break;
            case SaveWindow.SaveOption.SaveChanges:
                // Логика "Сохранить изменения"
                break;
            case SaveWindow.SaveOption.Cancel:
                // Действие при отмене
                break;
        }
        AppDbContext _context = new AppDbContext();
        MenuItem item = new MenuItem();
        foreach (MenuItem menuItem in trvMenu.Items)
        {
            item = menuItem;
        }
        _context.ProductLinks
            .Where(p => p.ParentProductId == item.itemId)
            .ExecuteDelete();
        List<ProductLink> links = new List<ProductLink>();
        foreach (MenuItem product in item.Items)
        {
            Product productParent = _context.Products.Find(item.itemId);
            Product includedProduct = _context.Products.Find(product.itemId);
            links.Add(new ProductLink
            {
                ParentProductId = item.itemId,
                ParentProduct = productParent,

                IncludedProductId = product.itemId,
                IncludedProduct = includedProduct,

                Amount = product.Amount
            });
        }
        _context.ProductLinks.AddRange(links);
        _context.SaveChanges();
        MessageBox.Show("Сохранение завершено!");
    }

    private void CopyTreeItemButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = trvMenu.SelectedItem as MenuItem;
        TreeMenu.copyMenuItem(selectedItem);
    }

    private void InsertTreeItemButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = trvMenu.Items[0] as MenuItem;
        if (selectedItem != null)
        {
            var insertedItem = TreeMenu.InsertMenuItem();
            if (insertedItem.Id == selectedItem.Id)
            {
                MessageBox.Show("Нельзя копировать элемент в себя");
            }
            else
            {
                if (insertedItem != null)
                {
                    insertedItem.Parent = selectedItem;
                    selectedItem.Items.Add(insertedItem);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите элемент для вставки");
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

    private void SearchText_Changed(object sender, TextChangedEventArgs e)
    {
        //TextBox search = sender as TextBox;
        //if (search.Text.Length > 2)
        //{
        //    try
        //    {
        //        var root = trvMenu.Items[0] as MenuItem;

        //        string text = TreeMenu.MenuItemSearch(root, search.Text, "");

        //        MessageBox.Show(text);
        //    }
        //    catch { }
        //}
    }

    private TreeViewItem GetNextTreeViewItem(TreeViewItem item)
    {
        TreeViewItem nextItem = null;
        item.IsExpanded = true;
        item.UpdateLayout();

        if (item != null)
        {
            if (item.Items.Count > 0)
            {
                nextItem = (TreeViewItem)trvMenu.ItemContainerGenerator.ContainerFromIndex(0);
            }
            else
            {
                TreeViewItem parentItem = GetParentTreeViewItem(item);
                if (parentItem != null)
                {
                    int index = parentItem.ItemContainerGenerator.IndexFromContainer(item);
                    if (index < parentItem.Items.Count - 1)
                    {
                        nextItem = (TreeViewItem)parentItem.ItemContainerGenerator.ContainerFromIndex(index + 1);
                    }
                }
            }
        }

        return nextItem;
    }

    private TreeViewItem GetParentTreeViewItem(TreeViewItem item)
    {
        return item?.Parent as TreeViewItem;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //TreeViewItem nextItem = GetNextTreeViewItem();

        //if (nextItem != null)
        //{
        //    nextItem.IsSelected = true;
        //    nextItem.Focus();
        //}
        TextBox search = SearchTreeItem;
        if (search.Text.Length > 0 && !search.Text.Contains("Поиск"))
        {
            try
            {
                var root = trvMenu.Items[0] as MenuItem;

                string text = TreeMenu.MenuItemSearch(root, search.Text, "", "");

                MessageBox.Show(text);
            }
            catch { }
        }
    }

    private void trvMenu_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (trvMenu.SelectedItem != null)
        {
            OperationList.ItemsSource = null;
            var selectedItem = trvMenu.SelectedItem as MenuItem;
            this.FillListItems("Operation", selectedItem.itemId);
            this.FillListItems("Equipment", (OperationList.Items[0] as MenuItem).Id);          
        }
    }

    private void productsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        MenuItem item = productsComboBox.SelectedItem as MenuItem;
        trvMenu.Items.Clear();
        trvMenu.Items.Add(Menu_Create(item.Id));
        FullName.Text = ((MenuItem)productsComboBox.SelectedItem).Title.ToString() + " полное название";
        itemid = item.Id;
        this.FillListItems("Operation", (trvMenu.Items[0] as MenuItem).itemId);
        this.FillListItems("Equipment", (OperationList.Items[0] as MenuItem).Id);
    }

    private void OperationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(OperationList.SelectedItem != null)
        {
            EquipmentList.ItemsSource = null;
            var selectedItem = OperationList.SelectedItem as MenuItem;
            this.FillListItems("Equipment", selectedItem.Id);            
        }
    }

    private void FillListItems(string type, int request)
    {
        ListFill list = new ListFill();
        switch (type)
        {
            case "Operation":
                OperationList.ItemsSource = list.ItemOperations(request);
                OperationList.DisplayMemberPath = "Title";
                break;
            case "Equipment":
                EquipmentList.ItemsSource = list.OperationEquipment(request);
                EquipmentList.DisplayMemberPath = "Title";
                break;
        }
    }
}