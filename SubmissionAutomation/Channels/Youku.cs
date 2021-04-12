using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SubmissionAutomation.Extensions;
using SubmissionAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubmissionAutomation.Channels
{
    /// <summary>
    /// 优酷
    /// </summary>
    public class Youku : Channel
    {

        private const string url = "https://mp.youku.com/v2/mp/upload_home"; //网址
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
        public Youku(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) : base(url, videoPath, coverPath, tags, title, introduction, classifyName, originalName, operateInterval)
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
            IWebElement uploadCenter = wait.Until(wb => wb.FindElement(
                By.Id("uploadCenter")
                ));

            Thread.Sleep(1000);
            var btns = Wait.Until(uploadCenter, x => x.FindElements(
                 By.ClassName("ant-btn")
                 ));

            var btn = btns.FindElementBText("上传视频");
            btn.Click();

            Thread.Sleep(1000);

            if (!OpenFileDialog.SelectFileAndOpen(path)) return false;

            Thread.Sleep(1000);

            return true;
        }

        /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {
            //IWebElement coverElement = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#Content > div > div.HomePage-container > div > div.SingleUpload > div.EditVideo > div.EditVideo-videoCover.use-video-auto-cover > div.cover-content > div > form > input[type=file]")
            //    )); //获取图片上传控件
            //coverElement.SendKeys(path); //设置上传值
            //Thread.Sleep(500);
            //wait.Until(wb => wb.FindElement(
            //    By.CssSelector("body > div:nth-child(15) > div.shark-Modal-wrapper.shark-Modal-wrapper--center.VideoDialog > div > div > div > div.shark-Modal-body > div > div.upload-cover-mod-footer > button:nth-child(2)")
            //    )).Click(); //点击确定

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            IWebElement titleInput = wait.Until(wb => wb.FindElement(
                By.Id("title")
                ));
            titleInput.Clear();
            titleInput.SendKeys(title);

            return true;
        }

        /// <summary>
        /// 写描述
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            var tempEle = wait.Until(wb => wb.FindElement(
                By.ClassName("ant-form-item-control-input-content")
                ));

            //简介
            var textarea = Wait.Until(tempEle, wb => wb.FindElement(
                By.TagName("textarea")
                ));
            textarea.SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            IWebElement inputContent = wait.Until(wb => wb.FindElement(
                By.ClassName("ant-form-item-control-input-content")
                )); //标签

            IWebElement tagInput = wait.Until(wb => wb.FindElement(
                By.TagName("input")
                ));

            IEnumerable<string> _tags = tags.Take(maxTagCount);
            foreach (string tag in _tags)
            {
                tagInput.SendKeys(tag + Keys.Enter);
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
