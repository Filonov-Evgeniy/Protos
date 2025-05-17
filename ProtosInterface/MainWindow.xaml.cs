using Microsoft.EntityFrameworkCore;
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
    public MainWindow()
    {
        InitializeComponent();
        trvMenu.Items.Add(Menu_Create());
    }

    static public MenuItem Menu_Create()
    {
        TreeMenu menu = new TreeMenu(11);
        MenuItem item = menu.createMenu();
        return item;
    }

    private void SaveEdition_Click(object sender, RoutedEventArgs e)
    {
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

                string text = TreeMenu.MenuItemSearch(root, search.Text, "");

                MessageBox.Show(text);
            }
            catch { }
        }
    }

    private void trvMenu_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (trvMenu.SelectedItem != null)
        {
            OperationList.Items.Clear();
            TreeMenu menu = new TreeMenu(11);
            var selectedItem = trvMenu.SelectedItem as MenuItem;
            OperationList.Items.Add(menu.ItemOperations(selectedItem));
        }
    }
}