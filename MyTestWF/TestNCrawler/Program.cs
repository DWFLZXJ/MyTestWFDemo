using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCrawler;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;

namespace TestNCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri("http://www.csdn.net/");
            Crawler c = new Crawler(uri, new HtmlDocumentProcessor(), new DumperStep());
            c.MaximumThreadCount = 30;//线程数量  
            c.MaximumCrawlDepth = 2;//爬行深度  
            c.Crawl();//开始爬行  

        }


        public class DumperStep : IPipelineStep
        {
            public void Process(Crawler crawler, PropertyBag propertyBag)
            {
                Console.WriteLine(propertyBag.Step.Uri);
               

            }
        }  
    }
}
