using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
namespace MysqlMonitoringPlus
{
    public class F_MysqlMonitoring : Form
    {
        private string var_datatime = "";
        private BindingSource Bs;
        private IContainer components;
        private TextBox txt_pass;
        private Label label3;
        private TextBox txt_user;
        private Label label2;
        private TextBox txt_host;
        private Label label1;
        private Button button2;
        private GroupBox groupBox1;
        private Label txt_count;
        private Label txt_break;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 复制ToolStripMenuItem;
        private DataGridView dataGridView1;
        private Label label4;
        private Button button3;
        private TabControl tabControl2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private ToolStripMenuItem 清空ToolStripMenuItem;
        private GroupBox groupBox2;
        private Label label6;
        private Label label5;
        private Label label16;
        private TabPage tabPage1;
        private Button button4;
        private Button button5;
        private Label label18;
        private TextBox textBox1;
        private GroupBox groupBox6;
        private DataGridView dataGridView2;
        private ToolStripMenuItem 执行选中语句ToolStripMenuItem;
        private TabPage tabPage2;
        private WebBrowser webBrowser1;
        private LinkLabel linkLabel1;
        private GroupBox groupBox3;
        private Label label11;
        private GroupBox groupBox4;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel6;
        private Label label7;
        private TextBox txt_searchkey;
        public F_MysqlMonitoring()
        {
            this.InitializeComponent();
        }

        public MySqlConnection func_getmysqlcon()
        {
            return new MySqlConnection(string.Concat(new string[]
			{
				"server=",
				this.txt_host.Text,
				";user id=",
				this.txt_user.Text,
				";password=",
				this.txt_pass.Text,
				";database=mysql"
			}));
        }
        public int func_getmysqlcom(string M_str_sqlstr)
        {
            MySqlConnection mySqlConnection = this.func_getmysqlcon();
            mySqlConnection.Open();
            MySqlCommand expr_16 = new MySqlCommand(M_str_sqlstr, mySqlConnection);
            int result = expr_16.ExecuteNonQuery();
            expr_16.Dispose();
            mySqlConnection.Close();
            mySqlConnection.Dispose();
            return result;
        }
        public DataSet func_getmysqlread(string M_str_sqlstr)
        {
            MySqlConnection mySqlConnection = this.func_getmysqlcon();
            mySqlConnection.Open();
            DataAdapter arg_1B_0 = new MySqlDataAdapter(M_str_sqlstr, mySqlConnection);
            DataSet dataSet = new DataSet();
            arg_1B_0.Fill(dataSet);
            return dataSet;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.func_getmysqlcom("set global general_log=on;SET GLOBAL log_output='table';");
                string m_str_sqlstr = "select event_time,argument from mysql.general_log where command_type='Query' and argument not like 'set global general_log=on;SET GLOBAL log_output%' and argument not like 'select event_time,argument from%' and event_time>'" + this.var_datatime + "'";
                DataView dataSource = new DataView(this.func_getmysqlread(m_str_sqlstr).Tables[0]);
                this.Bs = new BindingSource();
                this.Bs.DataSource = dataSource;
                this.dataGridView1.DataSource = this.Bs;
                this.txt_count.Text = "数据行数：" + this.Bs.Count;
                this.dataGridView1.Columns[0].HeaderText = "SQL语句查询时间";
                this.dataGridView1.Columns[1].HeaderText = "查询所执行SQL语句";
                this.dataGridView1.Columns[0].Width = 150;
                this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception)
            {
                MessageBox.Show("数据库链接失败，请检查连接主机/账户/密码/dll是否存在/以及确认mysql版本是否是在5.1.6以上", "温馨提示");
            }
        }
        private void F_MysqlMonitoring_Load(object sender, EventArgs e)
        {
            tabControl2.SelectedIndex = 3;
            this.var_datatime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.txt_break.Text = "时间：" + this.var_datatime;
        }
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                Clipboard.SetDataObject(this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            }
        }
        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            this.var_datatime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
        }

        private void 执行选中语句ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                this.textBox1.Text = this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                tabControl2.SelectedIndex = 1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void txt_searchkey_TextChanged(object sender, EventArgs e)
        {
            if (this.Bs != null)
            {
                this.Bs.RemoveFilter();
                if (this.txt_searchkey.Text != "")
                {
                    this.Bs.Filter = "argument like '%" + this.txt_searchkey.Text.Replace("'", "\\'") + "%'";
                    this.txt_count.Text = "行数：" + this.Bs.Count;
                    return;
                }
                this.txt_count.Text = "行数：" + this.Bs.Count;
            }
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            this.var_datatime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }





        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.Bs != null)
            {
                this.Bs.RemoveFilter();
                if (this.txt_searchkey.Text != "")
                {
                    this.Bs.Filter = "argument like '%" + this.txt_searchkey.Text.Replace("'", "\\'") + "%'";
                    this.txt_count.Text = "行数：" + this.Bs.Count;
                    return;
                }
                this.txt_count.Text = "行数：" + this.Bs.Count;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            try
            {
                string m_str_sqlstr = textBox1.Text;
                DataView dataSource = new DataView(this.func_getmysqlread(m_str_sqlstr).Tables[0]);
                this.Bs = new BindingSource();
                this.Bs.DataSource = dataSource;
                this.dataGridView2.DataSource = this.Bs;
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("SQL语句有误,请查看SQL语句之后在执行", "系统提示");
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("所执行的SQL语句有误，该SQL语句中所查询的数据库不存在,请在表前加一个数据库。比如：SELECT * FROM 数据库.表名", "系统提示");
            }
            catch (System.IndexOutOfRangeException)
            {
                MessageBox.Show("无法找到操作的表,因此无法操作。", "系统提示");
            }


        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://doc.mysql.cn/mysql5/refman-5.1-zh.html-chapter");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.226safe.com");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cnseay.com");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start("http://www.00com.org");

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.nosafe.org");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.getshell.net");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_MysqlMonitoring));
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_host = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.执行选中语句ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_count = new System.Windows.Forms.Label();
            this.txt_break = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_searchkey = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(408, 5);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(137, 21);
            this.txt_pass.TabIndex = 7;
            this.txt_pass.Text = "root";
            this.txt_pass.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "密码：";
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(238, 5);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(128, 21);
            this.txt_user.TabIndex = 8;
            this.txt_user.Text = "root";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "用户：";
            // 
            // txt_host
            // 
            this.txt_host.Location = new System.Drawing.Point(49, 5);
            this.txt_host.Name = "txt_host";
            this.txt_host.Size = new System.Drawing.Size(146, 21);
            this.txt_host.TabIndex = 9;
            this.txt_host.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "主机：";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(664, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(157, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "连接数据库&&查看执行语句";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(6, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1089, 558);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "执行过程";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(1072, 538);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem,
            this.清空ToolStripMenuItem,
            this.执行选中语句ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 70);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("复制ToolStripMenuItem.Image")));
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.复制ToolStripMenuItem.Text = "复 制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("清空ToolStripMenuItem.Image")));
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清空ToolStripMenuItem.Text = "清 空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // 执行选中语句ToolStripMenuItem
            // 
            this.执行选中语句ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("执行选中语句ToolStripMenuItem.Image")));
            this.执行选中语句ToolStripMenuItem.Name = "执行选中语句ToolStripMenuItem";
            this.执行选中语句ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.执行选中语句ToolStripMenuItem.Text = "执行选中语句";
            this.执行选中语句ToolStripMenuItem.Click += new System.EventHandler(this.执行选中语句ToolStripMenuItem_Click);
            // 
            // txt_count
            // 
            this.txt_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_count.AutoSize = true;
            this.txt_count.Location = new System.Drawing.Point(177, 609);
            this.txt_count.Name = "txt_count";
            this.txt_count.Size = new System.Drawing.Size(47, 12);
            this.txt_count.TabIndex = 12;
            this.txt_count.Text = "行数：0";
            // 
            // txt_break
            // 
            this.txt_break.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_break.AutoSize = true;
            this.txt_break.Location = new System.Drawing.Point(6, 609);
            this.txt_break.Name = "txt_break";
            this.txt_break.Size = new System.Drawing.Size(41, 12);
            this.txt_break.TabIndex = 13;
            this.txt_break.Text = "时间：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(831, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "搜索：";
            // 
            // txt_searchkey
            // 
            this.txt_searchkey.Location = new System.Drawing.Point(875, 8);
            this.txt_searchkey.Name = "txt_searchkey";
            this.txt_searchkey.Size = new System.Drawing.Size(220, 21);
            this.txt_searchkey.TabIndex = 14;
            this.txt_searchkey.TextChanged += new System.EventHandler(this.txt_searchkey_TextChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(551, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "清空执行内容";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(9, 2);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1111, 657);
            this.tabControl2.TabIndex = 17;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txt_searchkey);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txt_break);
            this.tabPage3.Controls.Add(this.txt_count);
            this.tabPage3.Controls.Add(this.txt_host);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.txt_user);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.txt_pass);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1103, 631);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "主要功能";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1103, 631);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "SQL语句执行";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(875, 9);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "清空执行内容";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(975, 9);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(118, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "执行本条SQL语句";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(4, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(95, 12);
            this.label18.TabIndex = 16;
            this.label18.Text = "请输入SQL语句：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(755, 21);
            this.textBox1.TabIndex = 15;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dataGridView2);
            this.groupBox6.Location = new System.Drawing.Point(3, 33);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1092, 595);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "执行过程";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView2.Location = new System.Drawing.Point(3, 17);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.ShowCellErrors = false;
            this.dataGridView2.ShowRowErrors = false;
            this.dataGridView2.Size = new System.Drawing.Size(1083, 572);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.webBrowser1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1103, 631);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "MYSQL在线手册";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(7, 613);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(203, 12);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "MYSQL手册官方地址（可在此处下载）";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1103, 605);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://doc.mysql.cn/mysql5/refman-5.1-zh.html-chapter/", System.UriKind.Absolute);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1103, 631);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "关于软件";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.linkLabel6);
            this.groupBox4.Controls.Add(this.linkLabel5);
            this.groupBox4.Controls.Add(this.linkLabel4);
            this.groupBox4.Controls.Add(this.linkLabel3);
            this.groupBox4.Controls.Add(this.linkLabel2);
            this.groupBox4.Location = new System.Drawing.Point(6, 332);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1064, 280);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "链接";
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabel6.Location = new System.Drawing.Point(283, 33);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(89, 12);
            this.linkLabel6.TabIndex = 6;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "北风渗透实验室";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabel5.Location = new System.Drawing.Point(392, 33);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(41, 12);
            this.linkLabel5.TabIndex = 5;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "收集家";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabel4.Location = new System.Drawing.Point(176, 33);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(89, 12);
            this.linkLabel4.TabIndex = 4;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "非安全研究社区";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabel3.Location = new System.Drawing.Point(98, 33);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(53, 12);
            this.linkLabel3.TabIndex = 3;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Seay博客";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabel2.Location = new System.Drawing.Point(8, 33);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(71, 12);
            this.linkLabel2.TabIndex = 2;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "226安全团队";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(520, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(550, 280);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "关于软件";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(80, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(344, 19);
            this.label7.TabIndex = 1;
            this.label7.Text = "QQ:177705712 欢迎各位大佬跟我一起探讨下人生！";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.DarkGreen;
            this.label11.Location = new System.Drawing.Point(80, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(387, 171);
            this.label11.TabIndex = 0;
            this.label11.Text = "大家好，我是来自 226SaFe Team 的成员：Poacher。\r\n\r\n在这里声明下这个软件的问题。\r\n\r\n这个软件是从Seay(法师)的源代码审计工具里面拿" +
    "出来的。\r\n\r\n我本人并不是原创。只是在这软件的原来的基础上进行修改。\r\n\r\n开源是一种文化,一种精神。";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 280);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "关于软件";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.DarkRed;
            this.label16.Location = new System.Drawing.Point(20, 114);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(413, 133);
            this.label16.TabIndex = 8;
            this.label16.Text = "特别温馨提示：\r\n\r\n请注意所连接的MYSQL主机,相对应相对应的链接权限是否开启。\r\n\r\n如果localhost无法连接，请自行查看该主机的MYSQL配置中.\r" +
    "\n\r\n所允许链接的主机地址！";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(33, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(400, 19);
            this.label6.TabIndex = 1;
            this.label6.Text = "这款工具可以更好的帮助你看Mysql数据库所执行的SQL语句！";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.DarkRed;
            this.label5.Location = new System.Drawing.Point(70, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(310, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "这是一款MYSQL数据库SQL语句执行监视工具。";
            // 
            // F_MysqlMonitoring
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1120, 659);
            this.Controls.Add(this.tabControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_MysqlMonitoring";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MYSQL执行语句监视工具修改版       By:Poacher";
            this.Load += new System.EventHandler(this.F_MysqlMonitoring_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

    }
}
