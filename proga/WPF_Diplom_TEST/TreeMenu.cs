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
            string query = $"select [Child_product_id] from [Product_Link] where [Parent_product_id] = '{productId}'";
            SqlCommand command = new SqlCommand(query, connect.GetConn());
            var productIdList = sqlToInt(command);
            foreach (int id in productIdList)
            {
                MenuItem doughterItem = new MenuItem() { Title = getProductName(id) };
                item.Items.Add(doughterItem);
                if (isHasChildren(id))
                {
                    buildMenuItems(id, ref doughterItem);
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

        public TreeMenu(int productId)
        {
            connect.OpenConn();
            this.productId = productId;
        }
    }
}
