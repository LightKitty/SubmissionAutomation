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
    /// 百度（百家号）
    /// </summary>
    public class Zhihu : Channel
    {

        private const string url = "https://www.zhihu.com/creator/video-upload"; //网址
        private const int maxTagCount = 3; //最大标签个数
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
        /// <param name="classifyName"></param>
        /// <param name="originalName"></param>
        public Zhihu(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) 
            : base(url, videoPath, coverPath, tags, title, introduction, classifyName, originalName, operateInterval)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Operate()
        {
            return base.Operate(null, new Func<bool>[] { CompleteBtnClick });
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
            IWebElement videoInput = wait.Until(x => x.FindElement(
                By.ClassName("VideoUploadButton-fileInput")
                ));

            videoInput.SendKeys(path); //设置上传值

            Thread.Sleep(1000); //等待跳转

            //if (!Helpers.OpenFileDialog.SelectFileAndOpen(path)) return false;

            //Thread.Sleep(1000); //等待

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            //发布区域
            IWebElement ZVideoUploader_form = wait.Until(wb => wb.FindElement(
                By.ClassName("ZVideoUploader-form")
                ));

            var inputs = ZVideoUploader_form.FindElements(
                By.TagName("input")
                );

            var input = inputs.FindElementByAttribute("placeholder", "输入视频标题");

            input.SendKeys(title);

            return true;
        }

        /// <summary>
        /// 写简介
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            //发布区域
            IWebElement ZVideoUploader_form = wait.Until(wb => wb.FindElement(
                By.ClassName("ZVideoUploader-form")
                ));

            var textareas = ZVideoUploader_form.FindElements(
                By.TagName("textarea")
                );

            var textarea = textareas.FindElementByAttribute("placeholder", "填写视频简介，让更多人找到你的视频");

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
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {

            return true;
        }

        /// <summary>
        /// 原创声明
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            //if (string.IsNullOrEmpty(typeName)) return true;

            //var input = wait.Until(wb => wb.FindElement(
            //    By.Id("spe1")
            //    ));
            //input.Click();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CompleteBtnClick()
        {
            

            return false;
        }
    }
}
