using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubmissionAutomation.Channels
{
    /// <summary>
    /// 斗鱼
    /// </summary>
    public class Douyu : Channel
    {

        private const string url = "https://v.douyu.com/member/mycreate/center#/uploadVideo"; //网址
        private const int maxTagCount = 10; //最大标签个数
        private const int operateInterval = 100; //默认操作间隔

        private WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); //等待器

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="videoPath"></param>
        /// <param name="coverPath"></param>
        /// <param name="tags"></param>
        /// <param name="title"></param>
        /// <param name="introduction"></param>
        public Douyu(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) : base(url, videoPath, coverPath, tags, title, introduction, classifyName, originalName, operateInterval)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Operate()
        {
            return base.Operate(null, null);
        }

        /// <summary>
        /// 跳转到网址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal override bool GoTo(string url)
        {
            Driver.SwitchTo().NewWindow(WindowType.Tab);
            Driver.Navigate().GoToUrl(url);

            return true;
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool UploadVideo(string path)
        {
            //查找上传按钮
            IWebElement uploadButton = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.UploadArea > div.UploadArea-DragArea > div.UploadArea-bigUploadBtn")
                ));
            uploadButton.Click(); //点击上传按钮

            Thread.Sleep(1000); //等待系统弹窗

            if (!Helpers.OpenFileDialog.SelectFileAndOpen(path)) return false;

            Thread.Sleep(1000); //等待

            return true;
        }

        /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {
            IWebElement coverElement = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCover.use-video-auto-cover > div.cover-content > div > form > input[type=file]")
                )); //获取图片上传控件
            coverElement.SendKeys(path); //设置上传值
            Thread.Sleep(500);
            wait.Until(wb => wb.FindElement(
                By.CssSelector("body > div:nth-child(15) > div.shark-Modal-wrapper.shark-Modal-wrapper--center.VideoDialog > div > div > div > div.shark-Modal-body > div > div.upload-cover-mod-footer > button:nth-child(2)")
                )).Click(); //点击确定

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            IWebElement titleElement = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoTitle > div.video-title-content > span > input")
                ));
            titleElement.Clear();
            titleElement.SendKeys(title);

            return true;
        }

        /// <summary>
        /// 写描述
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            //简介
            wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoDetail > div.detail-content > textarea")
                ))
                .SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            IWebElement tagElement = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoTags > div.postvideo-tags > div > input[type=text]")
                )); //标签

            IEnumerable<string> _tags = tags.Take(maxTagCount);
            foreach (string tag in _tags)
            {
                tagElement.SendKeys(tag + Keys.Enter);
                Thread.Sleep(300);
            }

            return true;
        }

        /// <summary>
        /// 设置分类
        /// </summary>
        /// <returns></returns>
        internal override bool SetClassify(string name)
        {
            //分类按钮
            IWebElement classifyButton = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-input > div.cate-info")
                ));
            classifyButton.Click();

            //知识分类
            IWebElement zhishiButton = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-selected > div > div.CateSelect > div.cate-scrollbar-first > div:nth-child(1) > div:nth-child(12)")
                )); 
            zhishiButton.Click();

            //输入搜索文本
            IWebElement searchButton = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-selected > div > div.CateSelect > div.cate-search > div.search-wrap > div > input")
                )); 
            searchButton.Clear();
            searchButton.SendKeys(name);

            Thread.Sleep(100); //等待搜索结果

            //上传按钮
            IWebElement resultItem = wait.Until(wb => wb.FindElement(
                By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-selected > div > div.CateSelect > div.cate-search > div.cate-scrollbar-second > div:nth-child(1) > div > p")
                ));
            resultItem.Click();

            return true;
        }

        /// <summary>
        /// 声明原创
        /// </summary>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            return true;
        }
    }
}
