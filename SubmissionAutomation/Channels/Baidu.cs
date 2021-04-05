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
    /// 百度（百家号）
    /// </summary>
    public class Baidu : Channel
    {

        private const string url = "https://baijiahao.baidu.com/builder/rc/edit?type=video&app_id=1668272018575256"; //网址
        private const int maxTagCount = 5; //最大标签个数
        private const int operateInterval = 100; //默认操作间隔
        private static readonly Func<bool>[] beforeOperates = null;//预处理方法
        private static readonly Func<bool>[] afterOperates = new Func<bool>[] { OriginalStatement }; //处理后方法
        private static WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); //等待器

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="videoPath"></param>
        /// <param name="coverPath"></param>
        /// <param name="tags"></param>
        /// <param name="title"></param>
        /// <param name="introduction"></param>
        public Baidu(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName) : base(url, videoPath, coverPath, tags, title, introduction, classifyName, beforeOperates, afterOperates, operateInterval)
        {

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
            //try
            //{ //尝试删除之前的视频
            //    wait.Until(wb => wb.FindElement(
            //        By.CssSelector("#video-goods-content-wrap > span")
            //        )).Click();

            //    Thread.Sleep(100);

            //    wait.Until(wb => wb.FindElement(
            //        By.CssSelector("body > div:nth-child(29) > div > div.ant-modal-wrap > div > div.ant-modal-content > div > div > div.ant-confirm-btns > button.ant-btn.ant-btn-primary")
            //        )).Click();

            //    Thread.Sleep(100);
            //}
            //catch { }

            //查找上传按钮
            IWebElement uploadButton = wait.Until(wb => wb.FindElement(
                By.CssSelector("#root > div > div > div.mp-content > div > div > div.scale-box > div > div > div.ant-tabs-content.ant-tabs-content-no-animated > div.ant-tabs-tabpane.ant-tabs-tabpane-active > div > div > div.updataCoverBox > input[type=file]")
                ));
            uploadButton.SendKeys(path); //设置上传值

            //uploadButton.Click(); //点击上传按钮

            //Thread.Sleep(1000); //等待系统弹窗

            //if (!Helpers.OpenFileDialog.SelectFileAndOpen(path)) return false;

            Thread.Sleep(1000); //等待

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            Thread.Sleep(1000); //等待
            IWebElement titleElement = wait.Until(wb => wb.FindElement(
                By.CssSelector("#root > div > div > div.mp-content > div > div > div.scale-box > div > div > div.ant-tabs-content.ant-tabs-content-no-animated > div.ant-tabs-tabpane.ant-tabs-tabpane-active > div > div > div.video-active > form > div.grid-edit-video-content > div:nth-child(11) > div > div > div > div.client_components_titleInput > div > div.input-box > textarea")
                ));
            titleElement.Clear();
            titleElement.SendKeys(title);

            return true;
        }

        /// <summary>
        /// 写简介
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            //简介
            wait.Until(wb => wb.FindElement(
                By.CssSelector("#desc")
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
                By.CssSelector("#root > div > div > div.mp-content > div > div > div.scale-box > div > div > div.ant-tabs-content.ant-tabs-content-no-animated > div.ant-tabs-tabpane.ant-tabs-tabpane-active > div > div > div.video-active > form > div.grid-edit-video-utils > div:nth-child(3) > div.ant-form-item-control-wrapper.ant-col-xs-24.ant-col-sm-20 > div > div > div.tags-container > input")
                )); //标签
            tagElement.Clear();
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
            ////分类按钮
            //IWebElement classifyButton = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-input > div.cate-info")
            //    ));
            //classifyButton.Click();

            ////知识分类
            //IWebElement zhishiButton = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-selected > div > div.CateSelect > div.cate-scrollbar-first > div:nth-child(1) > div:nth-child(12)")
            //    )); 
            //zhishiButton.Click();

            ////输入搜索文本
            //IWebElement searchButton = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-selected > div > div.CateSelect > div.cate-search > div.search-wrap > div > input")
            //    )); 
            //searchButton.Clear();
            //searchButton.SendKeys(name);

            //Thread.Sleep(100); //等待搜索结果

            ////上传按钮
            //IWebElement resultItem = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCate > div.cate-selected > div > div.CateSelect > div.cate-search > div.cate-scrollbar-second > div:nth-child(1) > div > p")
            //    ));
            //resultItem.Click();

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
                By.CssSelector("#root > div > div > div.mp-content > div > div > div.scale-box > div > div > div.ant-tabs-content.ant-tabs-content-no-animated > div.ant-tabs-tabpane.ant-tabs-tabpane-active > div > div > div.video-active > form > div.grid-edit-video-utils > div:nth-child(1) > div.ant-form-item-control-wrapper.ant-col-xs-24.ant-col-sm-20 > div > div.client_pages_edit_cover.one > div.cover-list.cover-list-one > div > div > div > div > div.DraggableTags-tag-drag > div > div")
                )); //获取图片上传控件
            coverElement.Click();
            IWebElement coverElement2 = wait.Until(wb => wb.FindElement(
                By.XPath("/html/body/div[3]/div/div[2]/div/div[1]/div[1]/div/div/div[2]/div/div/div/div/div/div/span/div/span/input")
                )); //获取图片上传控件
            //coverElement2.Click();
            coverElement2.SendKeys(path); //设置上传值
            Thread.Sleep(500);
            wait.Until(wb => wb.FindElement(
                By.XPath("/html/body/div[3]/div/div[2]/div/div[1]/div[2]/button[2]")
                )).Click(); //点击确定

            return true;
        }

        /// <summary>
        /// 声明原创
        /// </summary>
        /// <returns></returns>
        public static bool OriginalStatement()
        {
            wait.Until(wb => wb.FindElement(
                By.CssSelector("#root > div > div > div.mp-content > div > div > div.scale-box > div > div > div.ant-tabs-content.ant-tabs-content-no-animated > div.ant-tabs-tabpane.ant-tabs-tabpane-active > div > div > div.video-active > form > div.grid-edit-video-utils > div:nth-child(5) > div.ant-form-item-control-wrapper.ant-col-xs-24.ant-col-sm-20 > div > div > div > label:nth-child(2) > span:nth-child(2) > span:nth-child(1)")
                )).Click();

            return true;
        }
    }
}
