using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace MyTestWF
{
    public partial class Form1 : Form
    {
        private string info = null;
        /// <summary>
        /// 委托,用于跨线程设置消息
        /// </summary>
        /// <param name="Msg">消息内容</param>
        public delegate void _SetListMsg(String Msg);

        /// <summary>
        /// 线程对象
        /// </summary>
        Thread aThread = null;
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="Msg">消息内容</param>
        public void SetListMsg(String Msg)
        {
            if (listMsg.InvokeRequired)
            {
                listMsg.Invoke(new _SetListMsg(SetListMsg), new object[] { Msg });
            }
            else
            {
                listMsg.Items.Add(Msg);
                listMsg.SelectedIndex = listMsg.Items.Count - 1;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (aThread != null)
            {
                aThread.Abort();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
           
            //HttpWebUtil ht = new HttpWebUtil();
            ////ht.downloadFile(Url, @"F:\MyDemoCode\MyTestPic\test.txt");
            //ht.GetImg(Url, "UTF-8", out Msg);
            //MessageBox.Show("下载图片成功！"+Msg);
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(@"F:\MyDemoCode\MyTestPic\test.txt", true, Encoding.UTF8);
            //sw.WriteLine(Msg);
            //sw.Close();
           //揭秘字符串

            //MessageBox.Show("解密后的字符串！" + JieMi.JieMiValue(Url));
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        //按钮事件
        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result =

               System.Windows.Forms.MessageBox.Show(

                       "确认要进行归档吗？",

                       "确认",

                       MessageBoxButtons.OKCancel,

                       MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //确认处理 
                //桌面路径
                string srcdir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string desdir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //如果text有值就用text中的值
                if (textBox2.Text != "")
                {
                    srcdir = textBox2.Text;
                }
                if (textBox3.Text != "")
                {
                    desdir = textBox3.Text;
                }

                //获取源文件夹的完整路径
                // string srcdir = textBox2.Text;
                //遍历
                DirectoryInfo TheFolder = new DirectoryInfo(srcdir);
                List<string> srcfiles = new List<string>();

                //遍历文件夹
                //foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())

                //遍历文件
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {

                    string Extension = Path.GetExtension(NextFile.Name);

                    if (Directory.Exists(desdir + @"\" + Extension) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(desdir + @"\" + Extension);
                    }
                    //判断文件是否存在
                    if (!File.Exists(desdir + @"\" + Extension + @"\" + NextFile.Name))
                    {
                        DesktopClean.FileOperateProxy.MoveFile(NextFile.FullName, desdir + @"\" + Extension, true, true, true, ref info);
                    }
                }
            }
            else
            {
                return;
                //取消处理 
            } 


            //DesktopClean.FileOperateProxy.MoveFile();

           //textBox2.Text = "桌面的路径是" + dirDesk;

          //  DirectoryInfo theForlder = new DirectoryInfo();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnjiemi_Click(object sender, EventArgs e)
        {
            //HttpWebUtil ht = new HttpWebUtil();
            ////ht.downloadFile(Url, @"F:\MyDemoCode\MyTestPic\test.txt");
            //ht.GetImg(Url, "UTF-8", out Msg);
            //MessageBox.Show("下载图片成功！"+Msg);
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(@"F:\MyDemoCode\MyTestPic\test.txt", true, Encoding.UTF8);
            //sw.WriteLine(Msg);
            //sw.Close();
            //揭秘字符串

            txtmingwen.Text = JieMi.JieMiValue(txtmiwen.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtmiwen.Text = JieMi.JiaMiValue(txtmingwen.Text);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            PicCatchTestOne.PicOne PCO = new PicCatchTestOne.PicOne(this);
            aThread = new Thread(new ThreadStart(PCO.Start));
            aThread.Start();
           

            //WebClient aWebClient = new WebClient();

            //aWebClient.DownloadFile("http://img.wm321.com/uploads/allimg/c161130/14P515FL2050-1042617.jpg", @"F:\MyDemoCode\MyTestPic\test.jpg");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        //一键导入张家港中学生考试数据 


        private void button4_Click(object sender, EventArgs e)
        {
            MyTestWF.ZJGStudentCore.CoreCatcher cc = new ZJGStudentCore.CoreCatcher();
            
            textBox3.Text = cc.AnalysHtml();
        }
    }
}
