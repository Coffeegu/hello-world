using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;
using System.ComponentModel;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Dictionary<string, int> unload = new Dictionary<string, int>();
        Dictionary<string, int> loaded = new Dictionary<string, int>();
        private static int id = 0;
        private static string name = "";
        private static SqlConnection con;
        private static string ConStr = "server=.;database=Test;uid=sa;pwd=GZMgzm123";
        protected void Page_Load(object sender, EventArgs e)
        {
            Calendar1.SelectionChanged += new EventHandler(this.Calendar_SelectionChanged);
            if (Convert.ToInt32(Request.QueryString["grade"])>1)
            {
                this.Button3.Visible = true;
            }
            if (string.IsNullOrEmpty(label2.Text))
            {
                //label2.Text = Session["name"].ToString();
                label2.Text = Request.QueryString["name"];
            }
        }


        private static void AddData(DateTime time, string content, SqlConnection conn, string url)
        {
            string sqlStr = "INSERT INTO [Member](reply_id,member_name,reply_content,reply_time,reply_url) VALUES('" + id + "','" + name + "','" + content + "','" + time + "','" + url + "')";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlStr;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            //conn.Close();
            //int i = Convert.ToInt32(cmd.ExecuteNonQuery());
            //Console.Write("共有" + i.ToString() + "条数据");
        }

        protected void btnSelect_Click(object sender, EventArgs e)

        {
            con = new SqlConnection("server=.;database=Test;uid=sa;pwd=GZMgzm123");
            //con.Open();
            int differenceInDays = 0;
            DateTime olddate = new DateTime();
            using (SqlCommand sc = new SqlCommand())
            {
                con.Open();
                sc.Connection = con;
                sc.CommandType = CommandType.Text;
                DateTime date = new DateTime();
                SqlDataReader sr = null;

                try
                {
                    string s = "";
                    if (!string.IsNullOrEmpty(TextBox2.Text))
                    {
                        name = TextBox2.Text;
                        s = "select UserID from [User] where UserName='" + name + "'";
                        sc.CommandText = s;
                        sr = sc.ExecuteReader();
                        if (sr.Read())
                        {
                            id = Convert.ToInt32(sr[0]);
                        }
                        else
                        {
                            Response.Write("<script language='javascript'>alert('No result!');localtion='WebForm1.aspx'</script>");
                            return;
                        }
                        sr.Close();
                        //conn.Close();
                    }

                    if (!string.IsNullOrEmpty(Message.Text))
                    {
                        string ddd = Message.Text.Replace("<br />", "");
                        if (!ddd.StartsWith("No"))
                        {
                            olddate = Convert.ToDateTime(ddd);
                            date = DateTime.Now;
                            TimeSpan tss = date - olddate;
                            differenceInDays = tss.Days;
                        }
                    }
                }
                catch (WebException we)
                {
                    con.Close();
                    Console.WriteLine(we.Message);
                }
            }


            Console.WriteLine(con.State.ToString());


            string rc = "";    //定义空字符串，用来条件查询

            if (!string.IsNullOrEmpty(this.TextBox2.Text))

            {
                if (!this.TextBox2.Text.Equals("") && !this.TextBox2.Text.Contains("%"))
                {
                    rc += "UserName like '%" + this.TextBox2.Text + "%'";
                }
            }
            DateTime odt;
            string sd = "";
            string DName = "";
            string fd = "";
            string sql = "";
            //string s12 =Request.Form["TextBox3"].Trim();
            if (DateTime.TryParse(this.TextBox3.Text, out odt))
            {
                DateTime ndt;
                if ( DateTime.TryParse(this.TextBox4.Text, out ndt))
                {
                    fd = odt.ToString("yyyy-MM-dd");
                    sd = ndt.ToString("yyyy-MM-dd");
                }
                else
                {
                    fd = odt.ToString("yyyy-MM-dd");
                    sd = DateTime.Now.ToString("yyyy-MM-dd");
                }

            }
            else if (!string.IsNullOrEmpty(Message.Text) && !Message.Text.StartsWith("No"))
            {
                if (this.RadioButton_Month.Checked)
                {
                    DName = olddate.ToString("m", CultureInfo.CreateSpecificCulture("en-US"));
                    DateTime d1 = new DateTime(olddate.Year, olddate.Month, 1);
                    fd = d1.ToString("yyyy-MM-dd");
                    DateTime d2 = d1.AddMonths(1).AddDays(-1);
                    sd = d2.ToString("yyyy-MM-dd");
                }
                else if (this.RadioButton_Week.Checked)
                {
                    DName = olddate.ToString("f", CultureInfo.CreateSpecificCulture("en-US"));
                    int w = (int)olddate.DayOfWeek;
                    fd = olddate.AddDays(Convert.ToDouble(0 - w)).ToString("yyyy-MM-dd");
                    sd = olddate.AddDays(Convert.ToDouble(6 - w)).ToString("yyyy-MM-dd");
                }
                else
                {
                    fd = olddate.ToString("yyyy-MM-dd");
                    sd = olddate.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                fd = DateTime.Now.ToString("yyyy-MM-dd");
                sd = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            //sql= selectByDate(, sd);
            sql = "select UserID,UserName from [User]";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader sdr = cmd.ExecuteReader();

            DataTable db = new DataTable();
            TimeSpan ts = DateTime.Parse(sd) - DateTime.Parse(fd);
            int[] sum;
            if (sdr.HasRows)
            {
                db.Load(sdr);
                DataColumn colItem = new DataColumn("Avg", Type.GetType("System.Int32"));
                db.Columns.Add(colItem);
                int i = 0;
                sum = new int[db.Rows.Count];
                DataRow newRow = db.NewRow();
                int k = ts.Days+1;
                while (i <= ts.Days)
                {
                    DateTime fDate;
                    if (fd != "")
                    {
                        fDate = Convert.ToDateTime(fd).AddDays(i);
                    }
                    else
                    {
                        fDate = Convert.ToDateTime(sd);
                    }

                    DName = fDate.ToString("m", CultureInfo.CreateSpecificCulture("en-US"));
                    DataColumn colItems = new DataColumn(DName, Type.GetType("System.Int32"));
                    db.Columns.Add(colItems);
                    sql = selectByDate("", fDate.ToString("yyyy-MM-dd"));
                    int rs = 0;
                    using (SqlCommand sqlcom = new SqlCommand(sql, con))
                    {
                        SqlDataReader sr = sqlcom.ExecuteReader();
                        while (sr.Read())
                        {
                            for (int j = 0; j < db.Rows.Count; j++)
                            {
                                Object[] di = db.Rows[j].ItemArray;
                                string ss = sr[1].ToString();
                                string sss = db.Rows[j].ToString();
                                if (di.Contains(sr[1].ToString()))
                                {
                                    db.Rows[j][DName] = Convert.ToInt32(sr[2]);
                                    sum[j] += Convert.ToInt32(sr[2]);
                                    if (Convert.ToInt32(sr[2]) == 0)
                                    {
                                        k--;
                                    }
                                    //db.Rows[j]["Total"] += Convert.ToInt32(sr[2]);
                                }
                            }
                            rs += Convert.ToInt32(sr[2]);
                        }
                        sr.Close();
                    }
                    newRow[DName] = rs/db.Rows.Count;
                    i++;
                }
                for(int c = 0; c < db.Rows.Count; c++)
                {
                    if (ts.Days > 0)
                    {
                        db.Rows[c]["Avg"] = sum[c] / k;
                    }
                }
                //newRow["UserID"] = "";
                newRow["UserName"] = "";
                db.Rows.Add(newRow);
                DataView dv = new DataView(db); //调用查询方法

                if (rc != "")
                {
                    dv.RowFilter = rc;
                }

                dv.Sort = "UserID ASC";

                GridView1.DataSource = dv;

                GridView1.DataBind();

                //设置列名，如果不设置，将会以数据库中对应的字段名称代替
                GridView1.HeaderRow.Cells[0].Text = "No.";

                GridView1.HeaderRow.Cells[1].Text = "Alias";

            }
            sdr.Close();

            con.Close();
        }
       

        public string selectByDate(string fdate, string ldate)
        {
            DataTable db = new DataTable();
            string sql = "select reply_id,member_name,COUNT(*),DATEPART(DAY,reply_time) from Member where reply_content like '%"+this.TextBox1.Text+"%'";
            if (fdate != "")
            {
                sql += " and DATEPART(DAY,reply_time) between '" + fdate + "' and '" + ldate + "'";
            }
            else if (ldate != "")
            {
                sql += " and DATEPART(mm,reply_time) ='" + Convert.ToInt32(ldate.Substring(5, 2)) + "' and DATEPART(DAY, reply_time) = '" + Convert.ToInt32(ldate.Substring(8, 2)) + "'";
            }

            sql += " group by member_name,reply_id,DATEPART(DAY,reply_time)";


            //SqlCommand cmd = new SqlCommand(sql, con);

            //SqlDataReader sdr = cmd.ExecuteReader();

            return sql;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            //TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            RadioButton_Month.Checked = false;
            RadioButton_Week.Checked = false;
            RadioButton_Day.Checked = false;
            Message.Text = "";
            TextBox2.Focus();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string u = TextBox2.Text;
            int i = 0;
            string sqltest = "";
            if (string.IsNullOrEmpty(this.TextBox2.Text))
            {
                Response.Write("<script language='javascript'>alert('Name must be filled in');localtion='WebForm1.aspx'</script>");
                return;
            }
            else
            {
                using (con = new SqlConnection(ConStr))
                {
                    con.Open();
                    if (AddUrl(this.TextBox2.Text) == true)
                    {
                        sqltest = "insert into [User](UserName,UserGrade,UserPassword) VALUES('" + this.TextBox2.Text + "','1','sa')";
                        SqlCommand sqlcom = new SqlCommand(sqltest, con);
                        i = sqlcom.ExecuteNonQuery();
                    }
                }
            }
            if (i <= 0)
            {
                Response.Write("<script language='javascript'>alert('ID or Name has existed');localtion='WebForm1.aspx'</script>");
                return;
            }

            Response.Redirect("WebForm1.aspx");

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TextBox2.Text))
            {
                AddUrl(this.TextBox2.Text);
            }

        }
        public bool AddUrl(string n)
        {
            unload.Add("https://social.msdn.microsoft.com/Profile/" + n + "/activity/", 0);
            string url = unload.First().Key;
            int depth = unload.First().Value;
            loaded.Add(url, depth);
            unload.Remove(url);

            Console.WriteLine("Now loading " + url);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "text/html";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0;Win64;x64)";
            //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)";

            try
            {
                string html = null;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    html = reader.ReadToEnd();
                    if (string.IsNullOrEmpty(html)||html.Contains("The resource you are looking for has been removed, had its name changed, or is temporarily unavailable."))
                    {
                        Response.Write("<script language='javascript'>alert('The html doesn't have content.');localtion='WebForm1.aspx'</script>");
                        return false;
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('You can add it normally.');localtion='WebForm1.aspx'</script>");
                        return true;
                    }
                }
            }
            catch (WebException we)
            {
                Console.Write(we.Message);
                Response.Write("<script language='javascript'>alert('The html doesn't have content.');localtion='WebForm1.aspx'</script>");
                return false;
            }
        }
        public int insertOrUpdate(string sql)
        {
            int eccf = -1;
            //SqlConnection conn = createConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandText = sql;
                eccf = cmd.ExecuteNonQuery();
                return eccf;
            }
            finally
            {
                con.Close();
            }
        }

        protected void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            Message.Text = "";

            foreach (DateTime day in Calendar1.SelectedDates)
            {
                DateTime d = day;
                Message.Text += day.Date.ToShortDateString() + "<br />";
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].FindControl("CheckItem").Visible = (e.Row.Cells[2].Text.Trim() == "0" ? true : false);
                //e.Row.Cells[2].Text = (e.Row.Cells[2].Text.Trim() == "0" ? "No" : "Yes");
                e.Row.Cells[0].FindControl("CheckItem").Visible = (this.Button3.Visible == true ? true : false);
            }
        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            int rowCount = GridView1.Rows.Count;

            string checkIDlink = "";


            for (int i = 0; i < rowCount; i++)
            {
                CheckBox tempChk = (CheckBox)GridView1.Rows[i].FindControl("CheckItem");
                HiddenField HidID = (HiddenField)GridView1.Rows[i].FindControl("HidID");
                if (tempChk.Checked == true)
                {
                    checkIDlink += HidID.Value + "|";
                }
            }


            if (String.IsNullOrEmpty(checkIDlink.Trim()))
            {
                string ErroMsg = @"<mce:script language=""javascript""><!--alert(""No Row is Selected!"")// --></mce:script>";
                DeleteResults.Text = ErroMsg;
                return;
            }
            checkIDlink = checkIDlink.Substring(0, checkIDlink.LastIndexOf("|"));


            int returnRows = DeleteUser(checkIDlink);

            //GridView1.DataSourceID = ***;
            GridView1.DataBind();
        }
        public int DeleteUser(string userIDLink)
        {
            string[] userID = userIDLink.Split(Convert.ToChar("|"));
            string[] SQLs = new string[userID.Length];
            for (int i = 0; i < userID.Length; i++)
            {
                SQLs[i] = "Delete from [User] WHERE UserID = '" + Convert.ToInt32(userID[i]) + "'";
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(ConStr))
                {
                    connection.Open();

                    SqlTransaction transaction;

                    // Start a local transaction.  
                    transaction = connection.BeginTransaction();

                    int executeCount = 0;
                    try
                    {
                        for (int i = 0; i < SQLs.Length; i++)
                        {
                            // Call the overload that takes a connection in place of the connection string
                            SqlCommand sqlCommand = connection.CreateCommand();
                            sqlCommand.CommandText = SQLs[i];
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.Transaction = transaction;
                            executeCount += sqlCommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new ArgumentNullException("The transaction is rollbacked , please check the execute SQL.");
                    }

                    transaction.Dispose();

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return executeCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }

    public class GraspJob : ISchedulerJob
    {
        public void Execute()
        {
            SqlConnection con = new SqlConnection("server=.;database=Test;uid=sa;pwd=GZMgzm123");
            con.Open();
            string sql = "select UserID,UserName from [User]";
            SqlCommand sc = new SqlCommand(sql, con);
            SqlDataReader sr = sc.ExecuteReader();
            Dictionary<string, int> unload = new Dictionary<string, int>();
            Dictionary<string, int> loaded = new Dictionary<string, int>();
            while (sr.Read())
            {
                unload.Add(sr[1].ToString(), Convert.ToInt32(sr[0]));
            }
            sr.Close();
            while (unload.Count > 0)
            {
                string name = unload.First().Key;
                string url = "https://social.msdn.microsoft.com/Profile/" + name + "/activity/";
                int id = unload.First().Value;
                try
                {
                    //Log.SaveNote(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":AutoTask is Working!");

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = "GET";
                    req.Accept = "text/html";
                    req.UserAgent = "Mozilla/5.0 (Windows NT 10.0;Win64;x64)";
                    string html = null;
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                    {
                        html = reader.ReadToEnd();
                        NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(html);
                        SqlCommand findc = new SqlCommand();
                        findc.CommandType = CommandType.Text;
                        findc.Connection = con;
                        DateTime newDateTime = DateTime.Now;
                        SqlDataReader scc;
                        NSoup.Select.Elements ele = doc.Select("div#Activities>div");
                        int i = 0;
                        foreach (NSoup.Nodes.Element ee in ele)
                        {
                            string dt = ee.Select("div.activity-date").Attr("title");
                            string content = ee.Select("div.activity-detail").Text;
                            string href = ee.Select("div.activity-detail>a").First().Attr("href");
                            //href = href.Substring(href.IndexOf("<a"), href.IndexOf("/a>"));
                            dt = dt.Substring(dt.IndexOf("Date(") + 5);
                            dt = dt.Substring(0, dt.IndexOf(")"));
                            double unixDate = Convert.ToDouble(dt);
                            DateTime start1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            DateTime date = new DateTime();
                            date = start1.AddMilliseconds(unixDate).ToLocalTime();
                            TimeSpan ts = newDateTime - date;
                            if (content.Contains("'"))
                            {
                                content = content.Replace("'", "''");
                            }

                            //string insql = "select * from [Member] where reply_content='" + content + "' and reply_time='" + date + "' and reply_id=" + id;
                            string insql = "select * from [Member] where reply_url='" + href + "'and reply_id=" + id;
                            findc.CommandText = insql;
                            scc = findc.ExecuteReader();
                            if (scc.Read())
                            {
                                scc.Close();
                                i++;
                                continue;
                                /*if (i < 3)
                                {
                                    continue;
                                }
                                else
                                {
                                    i = 0;
                                    break;
                                }*/
                            }
                            else
                            {
                                scc.Close();
                                insql = "INSERT INTO [Member](reply_id,member_name,reply_content,reply_time,reply_url) VALUES('" + id + "','" + name + "',N'" + content + "','" + date + "','" + href + "')";
                                SqlCommand ff = new SqlCommand(insql, con);
                                ff.ExecuteNonQuery();
                            }
                        }
                        unload.Remove(name);
                        loaded.Add(name, id);
                    }
                }
                catch (WebException we)
                {
                    con.Close();
                    Console.WriteLine(we.Message);
                }
            }
            sr.Close();
            con.Close();
        }
    }
}
