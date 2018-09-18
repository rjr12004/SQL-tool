using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SQLqueryform
{
    public partial class Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string lastName = InputBox.Text;
            string queryString = "SELECT * FROM [MyTable] WHERE Last='" + lastName + "'";


            SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand(queryString, sqlConnection1);


            

            sqlConnection1.Open();
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            sqlConnection1.Close();

        }
        
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string firstName = firstNameBox.Text;
            string lastName = lastNameBox.Text;
            int MyTableID = 0;
            SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            string sql = "insert into [MyTable](First, Last) values(@first, @last)";

           
            
            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);

            cmd.Parameters.Add("@first", SqlDbType.NVarChar);
            cmd.Parameters["@first"].Value = firstName;
            cmd.Parameters.Add("@last", SqlDbType.NVarChar);
            cmd.Parameters["@last"].Value = lastName;
            //cmd.Parameters.Add("@MyTableID", SqlDbType.Int);


            cmd.ExecuteNonQuery();
            

        }
    }
}