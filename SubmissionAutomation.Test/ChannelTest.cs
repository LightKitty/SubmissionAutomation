 using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using SubmissionAutomation.Channels;
using SubmissionAutomation.Consts;

namespace SubmissionAutomation.Test
{
    [TestClass]
    public class ChannelTest
    {
        ChromeOptions options = new ChromeOptions();
        ChromeDriver driver = null;

        string videoPath = @"D:\地球频道\2.videos\20210424\导出.mp4";
        string coverPath = @"D:\地球频道\2.videos\20210424\vlcsnap-2021-04-24-23h25m34s546.png";
        string[] tags = new string[] { "太空", "地球", "空间站", "夜晚", "灯光", "闪电", "卫星", "科技", "科普" };
        string title = "国际空间站直播出现大量闪电";
        string introduction = "北京时间2021年4月24日13点，国际空间站直播中出现大量闪电，此时空间站位于南美洲上空。";

        public ChannelTest()
        {
            Config.Init();
            options.AddArgument("--user-data-dir=C:/Users/Administrator/AppData/Local/Google/Chrome/User Data"); //指定用户文件夹路径
            options.AddArgument("--profile-directory=Default"); //指定用户
            driver = new ChromeDriver(@"D:\WebDriver\bin", options);
            Channel.Driver = driver;
        }

        /// <summary>
        /// 哔哩哔哩
        /// </summary>
        [TestMethod]
        public void BilibiliTest()
        {
            Channel channle = new Bilibili(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, null, null);
            channle.Operate();
            Console.ReadLine();
        }

        /// <summary>
        /// 斗鱼
        /// </summary>
        [TestMethod]
        public void DouyuTest()
        {
            Channel channle = new Douyu(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, "知识", null);
            channle.Operate();
            Console.ReadLine();
        }

        /// <summary>
        /// 西瓜
        /// </summary>
        [TestMethod]
        public void XiguaTest()
        {
            Channel channle = new Xigua(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, null, null);
            channle.Operate();
            Console.ReadLine();
        }

        /// <summary>
        /// 百度
        /// </summary>
        [TestMethod]
        public void BaiduTest()
        {
            Channel channle = new Baidu(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, null, null);
            channle.Operate();
            Console.ReadLine();
        }

        /// <summary>
        /// 网易
        /// </summary>
        [TestMethod]
        public void WangyiTest()
        {
            Channel channle = new Wangyi(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, "科普·趣闻", "原创");
            channle.Operate();
            Console.ReadLine();
        }

        /// <summary>
        /// 微博
        /// </summary>
        [TestMethod]
        public void WeiboTest()
        {
            Channel channle = new Weibo(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, null, null);
            channle.Operate();
            Console.ReadLine();
        }

        /// <summary>
        /// 知乎
        /// </summary>
        [TestMethod]
        public void ZhihuTest()
        {
            Channel channle = new Zhihu(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, null, "原创");
            channle.Operate();
            Console.ReadLine();
        }

        /// <summary>
        /// 小红书
        /// </summary>
        [TestMethod]
        public void XiaohongshuTest()
        {
            Channel channle = new Xiaohongshu(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, null, "原创");
            channle.Operate();
            Console.ReadLine();
        }
    }
}
