using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using SubmissionAutomation.Channels;

namespace SubmissionAutomation.Test
{
    [TestClass]
    public class ChannelTest
    {
        ChromeOptions options = new ChromeOptions();
        ChromeDriver driver = null;

        string videoPath = @"E:\地球频道\2.videos\20210424\导出.mp4";
        string coverPath = @"E:\地球频道\2.videos\20210424\vlcsnap-2021-04-24-23h25m34s546.png";
        string[] tags = new string[] { "太空", "地球", "空间站", "夜晚", "灯光", "闪电", "卫星", "科技", "科普" };
        string title = "国际空间站直播出现大量闪电";
        string introduction = "北京时间2021年4月24日13点，国际空间站直播中出现大量闪电，此时空间站位于南美洲上空。";

        public ChannelTest()
        {
            options.AddArgument("--user-data-dir=C:/Users/Yang/AppData/Local/Google/Chrome/User Data"); //指定用户文件夹路径
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
            Channel bilibili = new Bilibili(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, null, null);
            bilibili.Operate();
            Console.ReadLine();
        }

        /// <summary>
        /// 斗鱼
        /// </summary>
        [TestMethod]
        public void DouyuTest()
        {
            Channel bilibili = new Douyu(videoPath, coverPath, new string[] { "123", "321" }, title, introduction, "科学科普", null);
            bilibili.Operate();
            Console.ReadLine();
        }
    }
}
