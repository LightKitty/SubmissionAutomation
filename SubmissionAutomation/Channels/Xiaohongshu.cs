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
    public class Xiaohongshu : Channel
    {

        private const string url = "https://creator.xiaohongshu.com/creator/post"; //网址
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
        public Xiaohongshu(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) 
            : base(url, videoPath, coverPath, tags, title, introduction, classifyName, originalName, operateInterval)
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
            IWebElement videoInput = wait.Until(x => x.FindElement(
                By.Id("upload")
                ));

            videoInput.SendKeys(path); //设置上传值

            return true;
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal override bool WriteTitle(string title)
        {
            //标题input
            IWebElement titleInput = wait.Until(wb => wb.FindElement(
                By.Name("title")
                ));

            titleInput.SendKeys(title);

            return true;
        }

        /// <summary>
        /// 写简介
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            //标题input
            IWebElement contentInput = wait.Until(wb => wb.FindElement(
                By.Name("content")
                ));

            contentInput.SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            if(tags?.Count()>0)
            {
                //标题input 
                IWebElement display_status = wait.Until(wb => wb.FindElement(
                    By.Name("display-status")
                    ));

                display_status.Click();

                Thread.Sleep(100);

                IWebElement topic_input = wait.Until(wb => wb.FindElement(
                    By.ClassName("topic-input")
                    ));

                var tag = tags.FirstOrDefault();

                topic_input.SendKeys(tag);

                Thread.Sleep(100);

                var edit_wrapper = wait.Until(wb => wb.FindElement(
                    By.ClassName("edit-wrapper")
                    ));

                var firstOption = Wait.Until(edit_wrapper, x => x.FindElement(
                     By.ClassName("option")
                     ));

                firstOption.Click();
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
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {
            if (string.IsNullOrEmpty(path)) return true;

            //封面区域
            IWebElement video_cover_container = wait.Until(wb => wb.FindElement(
                By.ClassName("video-cover-container")
                ));

            //上传封面按钮
            var imgs = Wait.Until(video_cover_container, x => x.FindElements(
                By.TagName("img")
                ));

            imgs[2].Click();

            Thread.Sleep(1000); //等待系统弹窗

            if (!OpenFileDialog.SelectFileAndOpen(path)) return false;



            return true;
        }

        /// <summary>
        /// 原创声明
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            return true;
        }
    }
}
