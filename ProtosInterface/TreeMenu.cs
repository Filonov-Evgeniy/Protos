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
        private dbDataLoader loader;
        int productId;

        public MenuItem createMenu()
        {
            MenuItem root = new MenuItem() 
            { 
                Title = loader.getProductName(productId),
                itemId = productId
            };
            if (loader.isHasChildren(productId))
            {
                loader.buildMenuItems(productId, ref root);
            }
            return root;
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

        public string ItemOperations(MenuItem item)
        {
            string result = "";
            IQueryable operations = _context.Operations.Include(o => o.OperationType).Where(o => o.ProductId == item.itemId);
            foreach (Operation operation in operations)
            {
                if (operation.OperationType != null)
                {
                    result += operation.OperationType.Name + "\n";
                }
            }
            return result;
        }

        public TreeMenu(int productId)
        {
            this.productId = productId;
            _context = new AppDbContext();
            loader = new dbDataLoader();
        }
    }
}
