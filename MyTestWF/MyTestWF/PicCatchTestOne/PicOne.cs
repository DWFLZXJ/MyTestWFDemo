using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Threading;

namespace MyTestWF.PicCatchTestOne
{
    /// <summary>
    /// 抓抓_你懂的
    /// </summary>
    public class PicOne
    {
        /// <summary>
        /// 用于下载图片的WebClient类
        /// </summary>
        private WebClient aWebClient = new WebClient();

        /// <summary>
        /// 基础网址
        /// </summary>
        private String aBaseUrl = "http://www.youmzi.com/meinv.html";

        /// <summary>
        /// 存放图片的基础路径
        /// </summary>
        private String aFileBasePath = @"F:\img\";

        /// <summary>
        /// 主界面对象
        /// </summary>
        private Form1 aMainForm = null;

        public PicOne(Form1 MainForm)
        {
            aMainForm = MainForm;
        }
        //离开作用域调用
        ~PicOne()
        {
            if (aWebClient != null)
            {
                aWebClient.Dispose();
            }
        }

        /// <summary>
        /// 开始抓抓_线程函数
        /// </summary>
        public void Start()
        {
            String iurl = aBaseUrl;
            String inextpag_url = "";
            while (iurl != "")
            {
                List<String> iPostsList = GetPostsList(iurl, out inextpag_url);
                //for (Int32 i = 0; i < iPostsList.Count; i++)
                //{
                //    SetMsg(i.ToString() + "-------------------------");
                //    string iPostid = iPostsList[i].Substring(iPostsList[i].LastIndexOf("/") + 1);
                //    if (Directory.Exists(aFileBasePath + iPostid))
                //    {
                //        SetMsg("该图的文件夹已存在，放弃抓取……");
                //        continue;
                //    }

                //    List<String> iImgsList = GetImgList(iPostsList[i], iPostid);
                   
                //}
                //string iPostid = inextpag_url.Substring(inextpag_url.LastIndexOf("/") + 1,5);
                string iPostid = "测试图";
                SavaImg(iPostsList, iPostid);
                //Thread.Sleep(3000);
                iurl = "";
                if (inextpag_url != "")
                {
                    iurl = inextpag_url;
                }
            }
        }

        /// <summary>
        /// 获取帖子列表
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="nextpagstr">返回下一页的网址，没有的话将返回""</param>
        /// <returns></returns>
        private List<String> GetPostsList(String url, out String nextpagstr)
        {
            nextpagstr = "";
            List<String> iRet = new List<string>();
            HtmlWeb iWeb = new HtmlWeb();
            SetMsg("开始获取地址列表……");

            HtmlAgilityPack.HtmlDocument iHtmlDoc = iWeb.Load(url);

            HtmlNodeCollection iNodes = iHtmlDoc.DocumentNode.SelectNodes("//img[@width='160']");
            foreach (HtmlNode iNode_a in iNodes)
            {
                if (iNode_a.Attributes["width"].Value=="160")
                {
                    iRet.Add(iNode_a.Attributes["src"].Value);
                }
            }
            HtmlNodeCollection ipag_Nodes = iHtmlDoc.DocumentNode.SelectNodes("//a[@href]");
            foreach (HtmlNode ipag_Node_a in ipag_Nodes)
            {
                if (ipag_Node_a.InnerText == "下一页")
                {
                    nextpagstr = ipag_Node_a.Attributes["href"].Value;
                }
            }

            SetMsg("  共 " + iRet.Count.ToString() + " 张图片……");
            //Thread.Sleep(3000);
            return iRet;
        }

        /// <summary>
        /// 获取图片列表
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="pathname">帖子的id，用于消息显示</param>
        /// <returns></returns>
        private List<String> GetImgList(String url, String pathname)
        {
            List<String> iRet = new List<string>();
            HtmlWeb iWeb = new HtmlWeb();
            SetMsg("开始获取" + pathname + "的图片列表……");
            HtmlAgilityPack.HtmlDocument iHtmlDoc = iWeb.Load(url);
            HtmlNodeCollection iNodes = iHtmlDoc.DocumentNode.SelectNodes("//div[@class='post-image-holder']/a");
            foreach (HtmlNode iNode_a in iNodes)
            {
                iRet.Add(iNode_a.Attributes["href"].Value);
            }
            SetMsg("  共 " + iRet.Count.ToString() + " 张……");
            SetMsg("…………………………延时5秒……………………");
            //Thread.Sleep(5000);
            return iRet;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="imgslist">图片地址列表</param>
        /// <param name="pathname">帖子的id，用于保存的文件名称和消息显示</param>
        private void SavaImg(List<String> imgslist, String pathname)
        {
            String iPath = aFileBasePath + pathname;
            if (!Directory.Exists(iPath))
            {
                Directory.CreateDirectory(iPath);
            }
            SetMsg("开始保存" + pathname + "的图片，每张延时3秒……");
            for (Int32 i = 0; i < imgslist.Count; i++)
            {
                String aFileName = iPath + @"\" + i.ToString() + ".jpg";
                aWebClient.DownloadFile(imgslist[i], aFileName);
                //Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="Msg">消息内容</param>
        private void SetMsg(String Msg)
        {
            if (aMainForm != null)
            {
                aMainForm.SetListMsg(Msg);
            }
        }
    }
}
