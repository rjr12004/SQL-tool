using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace SQLqueryform
{
    
    public partial class Form : System.Web.UI.Page
    {
        public void RegisterDOMReadyScript(string key, string script)
        {
            string enclosed = EncloseOnDOMReadyEvent(script);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), key, enclosed, true);
        }

        private string EncloseOnDOMReadyEvent(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("function r(f){/in/.test(document.readyState)?setTimeout('r('+f+')',9):f()} r(function(){")
                .Append(str)
                .Append("});");
            return sb.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string lastName = InputBox.Text;
            if (string.IsNullOrEmpty(lastName))
            {
                RegisterDOMReadyScript("alert message", "alert('Invalid Last Name');");
            }
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
        
        

        protected void Button2_Click(object sender, EventArgs e)
        {
            string firstName = firstNameBox.Text;
            string lastName = lastNameBox.Text;
            
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
            sqlConnection1.Close();

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            string sql = "SELECT * FROM [MyTable]";

            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            sqlConnection1.Close();
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            string searchStr = TextBox1.Text;

                      
            int searchInt = Int32.Parse(searchStr);
            

            string sql = "SELECT First,Last FROM [MyTable] WHERE MytableID='" + searchInt + "'";

            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    TextBox2.Text = reader["First"].ToString();
                    TextBox3.Text = reader["Last"].ToString();
                }
            }
            sqlConnection1.Close();

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            //get ID and data to update from textboxes
            string first = TextBox2.Text;
            string last = TextBox3.Text;
            int myTableID = Convert.ToInt32(TextBox1.Text);

            //string holding SQL update command
            string sql = "UPDATE [MyTable] SET First = @first, Last = @last WHERE MyTableID = @MyTableID";

            //open SQL Connection and initialize SqlCommand
            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);

            //set parameters for command
            cmd.Parameters.Add("@first", SqlDbType.NVarChar);
            cmd.Parameters["@first"].Value = first;
            cmd.Parameters.Add("@last", SqlDbType.NVarChar);
            cmd.Parameters["@last"].Value = last;
            cmd.Parameters.Add("@MyTableID", SqlDbType.Int);
            cmd.Parameters["@MyTableID"].Value = myTableID;

            //execute update command
            cmd.ExecuteNonQuery();

            sqlConnection1.Close();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
