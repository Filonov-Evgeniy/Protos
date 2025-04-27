using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Diplom_TEST
{
    class TreeMenu
    {
        DBConnection connect = new DBConnection();
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
            string query = $"select [Child_product_id], [Amount] from [Product_Link] where [Parent_product_id] = '{productId}'";
            SqlCommand command = new SqlCommand(query, connect.GetConn());
            var productIdList = sqlToInt(command);
            foreach (KeyValuePair<int, int> id in productIdList)
            {
                for (int i = 0; i < id.Value; i++)
                {
                    MenuItem doughterItem = new MenuItem() { Title = getProductName(id.Key) };
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

        private List<int> sqlToInt(SqlCommand command)
        {
            List<int> dbRows = new List<int>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dbRows.Add(reader.GetInt32(0));
            }
            reader.Close();
            return dbRows;
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
    }
}
