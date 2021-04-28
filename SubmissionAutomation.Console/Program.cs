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

                string videoPath = @"E:\地球频道\2.videos\20210424\导出.mp4";
                string coverPath = @"E:\地球频道\2.videos\20210424\vlcsnap-2021-04-24-23h25m34s546.png";
                string[] tags = new string[] { "太空", "地球", "空间站", "夜晚", "灯光", "闪电", "卫星", "科技", "科普" };
                string title = "国际空间站直播出现大量闪电";
                string introduction = "北京时间2021年4月24日13点，国际空间站直播中出现大量闪电，此时空间站位于南美洲上空。";

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
