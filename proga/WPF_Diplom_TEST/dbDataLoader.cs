using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Diplom_TEST
{
    class dbDataLoader
    {
        DBConnection connect = new DBConnection();

        public dbDataLoader()
        {
            connect.OpenConn();
        }

        public List<MenuItem> getProductData()
        {
            string query = $"select [Id], [Название] from [Product]";
            SqlCommand command = new SqlCommand(query, connect.GetConn());
            SqlDataReader reader = command.ExecuteReader();

            List<MenuItem> items = new List<MenuItem>();
            while (reader.Read())
            {
                string id = reader.GetInt32(0).ToString();
                string name = reader.GetString(1);
                items.Add(new MenuItem(id, name));
            }
            reader.Close();

            for (int i = 0; i < items.Count; i++)
            {
                if (isHasChildren(items[i].itemId))
                {
                    MenuItem item = items[i];
                    buildMenuItems(items[i].itemId, ref item);
                    items[i] = item;
                }
            }

            return items;
        }

        private void buildMenuItems(object productId, ref MenuItem item)
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

        private bool isHasChildren(object productId)
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
    }
}
