using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Threading;
using System.Timers;
using System.Data;
using System.Data.SqlClient;
namespace WebApplication1
{

    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    DataTable team = GetTeamInfo();
                    this.ddlTeam.DataSource = team;
                    this.ddlTeam.DataTextField = "TeamName";
                    this.ddlTeam.DataValueField = "TeamID";
                    this.ddlTeam.DataBind();
                    ddlTeam.SelectedIndex = 0;
                    DataTable major = DropdownlistDB.GetMemberByTeamID("1");
                    this.ddlMember.DataSource = major;
                    this.ddlMember.DataTextField = "UserName";
                    this.ddlMember.DataValueField = "UserID";
                    this.ddlMember.DataBind();
                }
            }

        }

        public static DataTable GetTeamInfo()
        {
            string sql = "select * from [dbo].[Team] ";
            DataTable dt = OperatorDb.GetDataTable(sql);
            return dt;
        }
        public static DataTable GetMemberInfo()
        {
            string sql = "select * from [dbo].[User] ";
            DataTable dt = OperatorDb.GetDataTable(sql);
            return dt;
        }
        public static DataTable GetMemberByTeamID(string UserID)
        {
            string sql = "select * from [dbo].[User] where UserID='" + UserID + "'";
            DataTable dt = OperatorDb.GetDataTable(sql);
            return dt;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }


        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable major = DropdownlistDB.GetMemberByTeamID(this.ddlTeam.SelectedValue);
            this.ddlMember.DataSource = major;
            this.ddlMember.DataTextField = "UserName";
            this.ddlMember.DataValueField = "UserID";
            this.ddlMember.DataBind();
        }

        protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSel_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {

        }

    }
    public interface ISchedulerJob
    {
        void Execute();
    }
    public class SampleJob : ISchedulerJob
    {
        public void Execute()
        {
            //文件保存的物理路径，CSTest为虚拟目录名称，F:\Inetpub\wwwroot\CSTest为物理路径
            string p = @"C:\Users\coffeeg\Desktop\Folder1";
            //我们在虚拟目录的根目录下建立SchedulerJob文件夹，并设置权限为匿名可修改，
            //SchedulerJob.txt就是我们所写的文件
            string FILE_NAME = p + "\\SchedulerJob\\SchedulerJob.txt";
            //取得当前服务器时间，并转换成字符串
            //SimpleDateFormat sdf = new SimpleDateFormat(" yyyy年MM月dd日 ");
            string c = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //标记是否是新建文件的标量
            bool flag = false;
            //如果文件不存在，就新建该文件
            if (!File.Exists(FILE_NAME))
            {
                flag = true;
                StreamWriter sr = File.CreateText(FILE_NAME);
                sr.Close();
            }
            //向文件写入内容
            StreamWriter x = new StreamWriter(FILE_NAME, true, System.Text.Encoding.UTF8);
            if (flag) x.Write("计划任务测试开始：");
            x.Write("\r\n" + c);
            x.Close();
        }
    }
    public class SchedulerConfiguration
    {
        //时间间隔
        private int sleepInterval;
        //任务列表
        private ArrayList jobs = new ArrayList();

        public int SleepInterval { get { return sleepInterval; } }
        public ArrayList Jobs { get { return jobs; } }

        //调度配置类的构造函数
        public SchedulerConfiguration(int newSleepInterval)
        {
            sleepInterval = newSleepInterval;
        }
    }
    public class Scheduler
    {
        private SchedulerConfiguration configuration = null;

        public Scheduler(SchedulerConfiguration config)
        {
            configuration = config;
        }

        public void Start()
        {
            while (true)
            {
                //执行每一个任务
                foreach (ISchedulerJob job in configuration.Jobs)
                {
                    ThreadStart myThreadDelegate = new ThreadStart(job.Execute);
                    Thread myThread = new Thread(myThreadDelegate);
                    myThread.Start();
                    Thread.Sleep(configuration.SleepInterval);
                }
            }
        }
    }

    public class OperatorDb
    {
        private static string conStr = "server=.;database=Test;uid=sa;pwd=GZMgzm123";
        //连接字符串，Initial Catalog设置为自己的数据库
        public static DataTable GetDataTable(string sql)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            SqlDataAdapter ada = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            con.Close();
            return dt;
        }
    }

    public class DropdownlistDB
    {
        public static DataTable GetTeamInfo()
        {   
            string sql = "select * from [dbo].[Team] ";
            DataTable dt = OperatorDb.GetDataTable(sql);
            return dt;
        }
        public static DataTable GetMemberInfo()
        {
            string sql = "select * from [dbo].[User] ";
            DataTable dt = OperatorDb.GetDataTable(sql);
            return dt;
        }
        public static DataTable GetMemberByTeamID(string groupID)
        {
            string sql = "select * from [dbo].[User] where UserTeam='" + groupID + "'";
            DataTable dt = OperatorDb.GetDataTable(sql);
            return dt;
        }
    }
}
