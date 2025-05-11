using Microsoft.EntityFrameworkCore;
using ProtosInterface.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace ProtosInterface
{
    class TreeMenu
    {
        private static MenuItem elementToCopy;
        private AppDbContext _context;
        int productId;

        public MenuItem createMenu()
        {
            MenuItem root = new MenuItem() { Title = getProductName(productId) };
            if (isHasChildren(productId))
            {
                buildMenuItems(productId, ref root);
            }
            return root;
        }

        private void buildMenuItems(int productId, ref MenuItem item)
        {
            IQueryable products = _context.ProductLinks.Where(p => p.ParentProductId == productId);
            var productIdList = sqlToDictionary(products);
            foreach (KeyValuePair<int, double> id in productIdList)
            {
                //for (int i = 0; i < id.Value; i++)
                //{
                    MenuItem doughterItem = new MenuItem() { Title = $"{getProductName(id.Key)}" };
                    doughterItem.Parent = item;
                    doughterItem.itemId = id.Key;
                    item.Items.Add(doughterItem);
                    if (isHasChildren(id.Key))
                    {
                        buildMenuItems(id.Key, ref doughterItem);
                    }
                //}
            }
        }

        private bool isHasChildren(int productId)
        {
            var childs = _context.ProductLinks.Where(p => p.ParentProductId == productId);
            if (childs.Count() > 0)
            {
                return true;
            }
            return false;
        }

        private string getProductName(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return "undefined";
            }
            string name = product.Name;
            return name;
        }

        private Dictionary<int, double> sqlToDictionary(IQueryable products)
        {
            Dictionary<int, double> dbRows = new Dictionary<int, double>();
            foreach (ProductLink product in products)
            {
                dbRows.Add(product.IncludedProductId, product.Amount);
            }
            return dbRows;
        }

        public static void copyMenuItem(MenuItem item)
        {
            elementToCopy = (MenuItem)item.Clone();
        }

        public static MenuItem InsertMenuItem()
        {
            if (elementToCopy == null)
            {
                MessageBox.Show("Не выбран элемент для вставки");
                return null;
            }

            MenuItem itemToInsert = (MenuItem)elementToCopy.Clone();
            return itemToInsert;

        }

        public static string MenuItemSearch(MenuItem root, string search, string result)
        {
            foreach (var item in root.Items)
            {
                if (item.Title.ToString().ToLower().Contains(search.ToLower()))
                    result += item.Title.ToString() + "\n";
                if (item.Items.Count > 0)
                    result = MenuItemSearch(item, search, result);
            }
            return result;
        }

        public List<MenuItem> ItemOperations(MenuItem item)
        {
            List<MenuItem> result = new List<MenuItem>();
            IQueryable operations = _context.Operations.Include(o => o.OperationType).Where(o => o.ProductId == item.itemId);
            foreach (Operation operation in operations)
            {
                if (operation.OperationType != null)
                {
                    result.Add(new MenuItem() { Title = operation.OperationType.Name + "\n" });
                    result.Add(new MenuItem() { Title = operation.OperationType.Name + "\n" });
                    result.Add(new MenuItem() { Title = operation.OperationType.Name + "\n" });
                    result.Add(new MenuItem() { Title = operation.OperationType.Name + "\n" });
                    result.Add(new MenuItem() { Title = operation.OperationType.Name + "\n" });
                }
            }
            return result;
        }

        public TreeMenu(int productId)
        {
            this.productId = productId;
            _context = new AppDbContext();
        }
    }
}
