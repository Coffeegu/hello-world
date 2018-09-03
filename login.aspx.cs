using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace WebApplication1
{
    public partial class login : System.Web.UI.Page
    {
        private static string ConStr = "server=.;database=Test;uid=sa;pwd=GZMgzm123";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void allow_Click(object sender, EventArgs e)
        {
            if (labuser.Text == "" || labpwd.Text == "")
            {
                Label3.Text = "User name and password cannot be empty!";
            }
            else
            {

                SqlConnection con = new SqlConnection(ConStr);

                con.Open();

                string strSql = "select UserPassword,UserGrade from [User] where UserName='" + labuser.Text + "'";

                SqlCommand cmd = new SqlCommand(strSql, con);

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter(strSql, con);

                da.Fill(ds, "mytable");

                try

                {

                    if (labpwd.Text == ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim())

                    {

                        string curuser = labuser.Text;

                        Label3.Text = "Login successfully!";

                        /*Session["name"] = curuser;

                        Session["grade"] = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        */

                        Response.Redirect("WebForm1.aspx?name="+curuser.Trim()+"&grade="+ ds.Tables[0].Rows[0].ItemArray[1].ToString());

                    }

                    else

                    {

                        Label3.Text = "User name or password error!";

                    }

                }

                catch

                {

                    Label3.Text = "Sorry!The username you entered does not exist!";

                }

                con.Close();
            }
        }

        protected void abov_Click(object sender, EventArgs e)
        {

            Response.Redirect("Register.aspx");
            /*if ((labuser.Text == "") || (labpwd.Text == ""))

            {

                Label3.Text = "User name or password cannot be empty";

            }

            else

            {

                try

                {

                    SqlConnection con = new SqlConnection(ConStr);

                    con.Open();

                    string strsql = "insert into userinfo values('" + labuser.Text + "','" + labpwd.Text + "')";

                    SqlCommand cmd = new SqlCommand(strsql, con);

                    cmd.ExecuteNonQuery();

                    con.Close();

                    labuser.Text = "";

                    labpwd.Text = "";

                    Label3.Text = "Register sucessfully, Welcome to login.";

                }

                catch

                {

                    Label3.Text = "用户名已存在!";

                }

            }*/
        }
    }
}
