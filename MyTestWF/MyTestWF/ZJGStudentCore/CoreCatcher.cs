using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace MyTestWF.ZJGStudentCore
{
    public class CoreCatcher
    {

        public string LoadFile(string FilePath)
        {
            return "";
        }

        public string AnalysHtml()
        {
            string strback = "";
            FileStream fs = new FileStream(@"F:\student.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
            string cont = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            int f = -1;

          DataView dv=  MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, "select personcode from student ").Tables[0].DefaultView;


            //解析中文 因为下面解析的中文乱码 只好出此下策
         string[] sArray = Regex.Split(cont, "</td><td>", RegexOptions.IgnoreCase);

            foreach (string singlestr in sArray)
            {

                if (Regex.IsMatch(singlestr, "[\u4e00-\u9fa5]"))
                {
                    f++;

                    MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, "update student set name='"+ singlestr + "' where personcode='"+ Convert.ToString(dv[f]["personcode"]) + "' ");
                }
            }

            CQ dom = CQ.Create(cont) ;

          var students=  dom.Select("tr");

            List<string> strs = new List<string>();

            foreach (var student in students)
            {
                CQ domdetails = CQ.Create(student);
                var details = domdetails.Select("td");

                foreach (var detail in details)
                {
                    

                    strs.Add(detail.InnerText);


                }


                //MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, "insert into student (personcode,totalgrade,chinese,math,english,physical,chemistry,history,politics,pe,zg) values('" + strs[0] + "', '" + strs[2] + "', '" + strs[3] + "', '" + strs[4] + "', '" + strs[5] + "', '" + strs[6] + "', '" + strs[7] + "', '" + strs[8] + "', '" + strs[9] + "', '" + strs[10] + "', '" + strs[11] + "')");

                strback = strs[1];

                strs.Clear();

              }

                return strback;


        }
    }
}
