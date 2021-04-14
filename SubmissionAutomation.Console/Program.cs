using OpenQA.Selenium.Chrome;
using SubmissionAutomation.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

                Thread.Sleep(100);

                //Channel bilibili = new Bilibili(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", null);
                //bilibili.Operate();

                //Channel douyu = new Douyu(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", "科学科普");
                //douyu.Operate();

                //Channel xigua = new Xigua(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321","2222","222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", null);
                //douyu.Operate();

                //Channel baidu = new Baidu(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321","2222","222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", null);
                //baidu.Operate();

                //Channel wangyi = new Wangyi(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321", "2222", "222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", "科普·趣闻", "原创");
                //wangyi.Operate();

                //Channel weibo = new Weibo(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321", "2222", "222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", "科普·趣闻", "原创");
                //weibo.Operate();

                //Zhihu weibo = new Zhihu(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321", "2222", "222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", "科普·趣闻", "原创");
                //weibo.Operate();

                //Xiaohongshu xiaohongshu = new Xiaohongshu(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321", "2222", "222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", "科普·趣闻", "原创");
                //xiaohongshu.Operate();

                //Kuaishou kuaishou = new Kuaishou(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321", "2222", "222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", "科学 天文", "原创");
                //kuaishou.Operate();

                //Douyin douyin = new Douyin(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321", "2222", "222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", "科学 天文", "原创");
                //douyin.Operate();

                Youku youku = new Youku(@"E:\地球频道\2.videos\20210402毅力号自拍\导出.mp4", @"E:\地球频道\2.videos\20200809\vlcsnap-2020-08-09-23h21m21s931.png", new string[] { "123", "321", "2222", "222999" }, "标题哈哈哈呀呀", "啦啦啦啦啊咯", "知识/文化 科普知识", "原创");
                youku.Operate();

                System.Console.ReadLine();
            }
        }
    }
}
