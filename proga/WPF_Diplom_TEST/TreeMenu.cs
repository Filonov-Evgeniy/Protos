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

            //MenuItem root = new MenuItem() { Title = "Изделие" };
            //MenuItem childItem1 = new MenuItem() { Title = "СЕ 1" };
            //childItem1.Items.Add(new MenuItem() { Title = "Деталь 1" });
            //childItem1.Items.Add(new MenuItem() { Title = "Деталь 2" });
            //root.Items.Add(childItem1);
            //root.Items.Add(new MenuItem() { Title = "СЕ 2" });
            //root.Items.Add(new MenuItem() { Title = "Мамут Рахал" });
            //root.Items.Add(new MenuItem() { Title = "Сынша Лавы" });
            //return root;
        }

        //private void openTree(int productId)
        //{
        //    MenuItem root = new MenuItem() { Title = getProductName(productId) };
        //    if (isHasChildren(productId))
        //    {
        //        buildMenuItems(productId, ref root);
        //    }

        //    return root;
        //}

        private void buildMenuItems(int productId, ref MenuItem item)
        {
            string query = $"select [Child_product_id] from [Product_Link] where [Parent_product_id] = '{productId}'";
            SqlCommand command = new SqlCommand(query, connect.GetConn());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int name = reader.GetInt32(0);
                reader.Close();
                MenuItem doughterItem = new MenuItem() { Title = getProductName(Convert.ToInt32(name)) };
                item.Items.Add(doughterItem);
                if (isHasChildren(Convert.ToInt32(name)))
                {
                    buildMenuItems(Convert.ToInt32(name), ref doughterItem);
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

        public TreeMenu(int productId)
        {
            connect.OpenConn();
            this.productId = productId;
        }

        ~TreeMenu()
        {
            connect.CloseConn();
        }
    }
}
