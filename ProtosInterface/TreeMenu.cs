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
using System.Windows.Controls;
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
            if (item != null)
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

        public static string MenuItemSearch(MenuItem root, string search, string currentPath, string result)
        {
            currentPath += root.Title + " -> ";
           
            foreach (var item in root.Items)
            {
                string path = currentPath;

                if (item.Title.ToLower().Contains(search.ToLower()))
                {
                    path += item.Title + "\n";
                    result += path;
                }

                if (item.Items.Count > 0)
                {
                    result = MenuItemSearch(item, search, path, result);
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
