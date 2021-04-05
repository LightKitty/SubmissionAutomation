using OpenQA.Selenium.Chrome;
using SubmissionAutomation.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionAutomation.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ChromeOptions();
            options.AddArgument("--user-data-dir=C:/Users/Yang/AppData/Local/Google/Chrome/User Data"); //置顶用户文件夹路径
            options.AddArgument("--profile-directory=Default"); //指定用户
            using (var driver = new ChromeDriver(@"D:\WebDriver\bin", options)) //声明chrome驱动器
            {
                Channel.Driver = driver;

                //Channel bilibili = new Bilibili(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321" }, "title222", "啦啦啦啦啊咯", null);
                //bilibili.Operate();

                Channel douyu = new Douyu(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321" }, "title222", "啦啦啦啦啊咯", "科学科普");
                douyu.Operate();

                System.Console.ReadLine();
            }
        }
    }
}
