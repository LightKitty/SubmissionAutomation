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

                string videoPath = @"E:\地球频道\2.videos\20210418冰岛火山\导出.mp4";
                string coverPath = @"E:\地球频道\2.videos\20210418冰岛火山\vlcsnap-2021-04-18-14h03m52s178.png";
                string[] tags = new string[] { "火山", "冰岛", "科技", "地球", "奇观" };
                string title = "近距离观察火山喷发是什么体验？";
                string introduction = "2021年3月19日至今，距冰岛首都雷克雅未克约30公里处的火山持续喷发，该火山休眠了约6000年，此次喷发暂没有造成人员伤亡，许多游客来到这里观赏难得奇景。";

                //Channel bilibili = new Bilibili(videoPath, coverPath, new string[] { "123", "321" }, title, ind, null);
                //bilibili.Operate();

                //Channel douyu = new Douyu(videoPath, coverPath, new string[] { "123", "321" }, title, ind, "科学科普");
                //douyu.Operate();

                //Channel xigua = new Xigua(videoPath, coverPath, new string[] { "123", "321","2222","222999" }, title, ind, null);
                //douyu.Operate();

                //Channel baidu = new Baidu(videoPath, coverPath, new string[] { "123", "321","2222","222999" }, title, ind, null);
                //baidu.Operate();

                Channel wangyi = new Wangyi(videoPath, coverPath, tags, title, introduction, "科普·趣闻", "原创");
                wangyi.Operate();

                //Channel weibo = new Weibo(videoPath, coverPath, tags, title, introduction, "科普·趣闻", "原创");
                //weibo.Operate();

                Zhihu zhihu = new Zhihu(videoPath, coverPath, tags, title, introduction, "科普·趣闻", "原创");
                zhihu.Operate();

                Xiaohongshu xiaohongshu = new Xiaohongshu(videoPath, coverPath, tags, title, introduction, "科普·趣闻", "原创");
                xiaohongshu.Operate();

                //Kuaishou kuaishou = new Kuaishou(videoPath, coverPath, tags, title, introduction, "科学 天文", "原创");
                //kuaishou.Operate();

                Douyin douyin = new Douyin(videoPath, coverPath, tags, title, introduction, "科学 天文", "原创");
                douyin.Operate();

                Youku youku = new Youku(videoPath, coverPath, tags, title, introduction, "知识/文化 科普知识", "原创");
                youku.Operate();

                System.Console.ReadLine();
            }
        }
    }
}
