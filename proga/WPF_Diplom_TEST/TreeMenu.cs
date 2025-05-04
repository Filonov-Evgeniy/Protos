using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Diplom_TEST
{
    class TreeMenu
    {
        private static MenuItem elementToCopy;
        DBConnection connect = new DBConnection();
        int productId;

        public MenuItem createMenu()
        {
            MenuItem root = new MenuItem() { Title = getProductName(productId) };
            if (isHasChildren(productId))
            {
                buildMenuItems(productId, ref root);
            }

            connect.CloseConn();
            return root;
        }

        private void buildMenuItems(int productId, ref MenuItem item)
        {
            string query = $"select [Child_product_id], [Amount] from [Product_Link] where [Parent_product_id] = '{productId}'";
            SqlCommand command = new SqlCommand(query, connect.GetConn());
            var productIdList = sqlToInt(command);
            foreach (KeyValuePair<int, int> id in productIdList)
            {
                for (int i = 0; i < id.Value; i++)
                {
                    MenuItem doughterItem = new MenuItem() { Title = getProductName(id.Key) };
                    doughterItem.Parent = item;
                    doughterItem.itemId = id.Key.ToString();
                    item.Items.Add(doughterItem);
                    if (isHasChildren(id.Key))
                    {
                        buildMenuItems(id.Key, ref doughterItem);
                    }
                }
            }
        }

        private bool isHasChildren(int productId)
        {
            string query = $"select * from [Product_Link] where [Parent_product_id] = '{productId}'";
            SqlCommand command = new SqlCommand(query, connect.GetConn());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }

        private string getProductName(int productId)
        {
            string query = $"select [Название] from [Product] where [id] = '{productId}'";
            SqlCommand command = new SqlCommand(query, connect.GetConn());
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string name = reader.GetString(0).Trim();
            reader.Close();
            return name;
        }

        private Dictionary<int, int> sqlToInt(SqlCommand command)
        {
            Dictionary<int, int> dbRows = new Dictionary<int, int>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dbRows.Add(reader.GetInt32(0), reader.GetInt32(1));
            }
            reader.Close();
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

        public TreeMenu(int productId)
        {
            this.productId = productId;
            connect.OpenConn();
        }
    }
}
