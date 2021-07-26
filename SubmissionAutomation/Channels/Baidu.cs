using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SubmissionAutomation.Extensions;
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

        private WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); //等待器

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="videoPath"></param>
        /// <param name="coverPath"></param>
        /// <param name="tags"></param>
        /// <param name="title"></param>
        /// <param name="introduction"></param>
        public Baidu(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) : base(url, videoPath, coverPath, tags, title, introduction, classifyName, originalName, operateInterval)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Operate()
        {
            return base.Operate(new Func<bool>[] { ClearRegion }, null);
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

        internal bool ClearRegion()
        {
            //去除视频历史
            var removeBtns = Driver.FindElements(
                By.ClassName("remove-btn")
                );

            foreach(var removeBtn in removeBtns)
            {
                try
                {
                    removeBtn.Click();
                    wait.Until(x => x.FindElementByTagAndText("button", "确 定", true)).Click();
                }
                catch
                {

                }
            }

            //去除封面历史
            removeBtns = Driver.FindElements(
                By.ClassName("op-remove")
                );

            foreach (var removeBtn in removeBtns)
            {
                try
                {
                    removeBtn.Click();
                }
                catch
                {

                }
            }

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
                By.ClassName("updataCoverBox")
                )).FindElement(By.TagName("input"));
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
            //Thread.Sleep(1000); //等待
            IWebElement titleElement = wait.Until(wb => wb.FindElementByTagAndAttribute("textarea", "placeholder", "标题", true));
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
            IWebElement element = wait.Until(wb => wb.FindElement(
                By.CssSelector("#desc")
                ));
            element.Clear();
            element.SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            IWebElement tagElement = wait.Until(wb => wb.FindElementByTagAndAttribute("input", "placeholder", "标签", true)); //标签
            tagElement.Clear();
            IEnumerable<string> _tags = tags.Take(maxTagCount);
            foreach (string tag in _tags)
            {
                tagElement.SendKeys(tag + Keys.Enter);
                Thread.Sleep(200);
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
            Driver.ExecuteScript("document.getElementsByClassName('container')[0].click()"); //不知为啥只能用js方式
            
            IWebElement coverElement2 = wait.Until(wb => wb.FindElement(
                By.ClassName("ant-upload")
                )).FindElement(
                By.TagName("input")
                ); //获取图片上传控件
            //coverElement2.Click();
            coverElement2.SendKeys(path); //设置上传值
            Thread.Sleep(1000);
            wait.Until(wb => wb.FindElementByTagAndText("button", "确 认", true)).Click(); //点击确定

            return true;
        }

        /// <summary>
        /// 声明原创
        /// </summary>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            wait.Until(wb => wb.FindElementByTagAndText("span", "原创")).Click();

            return true;
        }
    }
}
