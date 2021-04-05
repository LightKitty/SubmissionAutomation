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
    /// 哔哩哔哩
    /// </summary>
    public class Bilibili : Channel
    {

        private const string url = "https://member.bilibili.com/platform/upload/video/frame"; //网址
        private const int maxTagCount = 10; //最大标签个数
        private const int operateInterval = 100; //默认操作间隔
        private static readonly Func<bool>[] beforeOperates = new Func<bool>[] { WaitIframeLoaded }; //预处理方法
        private static readonly Func<bool>[] afterOperates = null; //处理后方法
        private static WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); //等待器

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="videoPath"></param>
        /// <param name="coverPath"></param>
        /// <param name="tags"></param>
        /// <param name="title"></param>
        /// <param name="introduction"></param>
        /// <param name="classifyName"></param>
        public Bilibili(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName) : base(url, videoPath, coverPath, tags, title, introduction, classifyName, beforeOperates, afterOperates, operateInterval)
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
        /// 等待上传iframe加载
        /// </summary>
        /// <returns></returns>
        internal static bool WaitIframeLoaded()
        {
            wait.Until(wb => wb.FindElements(By.TagName("iframe")).Count() > 0);
            Driver.SwitchTo().Frame(1);

            return true;
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool UploadVideo(string path)
        {
            IWebElement uploadButton = wait.Until(wb => wb.FindElement(By.Id("bili-upload-btn"))); //查找上传按钮
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
            //获取图片上传控件
            IWebElement coverElement = wait.Until(wb => wb.FindElement(
                By.CssSelector("#app > div.upload-v2-container > div.upload-v2-step2-container > div.file-content-v2-container > div.normal-v2-container > div.cover-v2-container > div.cover-v2-detail-wrp > div.cover-v2-preview > input[type=file]")
                )); 
            coverElement.SendKeys(path); //设置上传值

            Thread.Sleep(100);

            //点击确定
            wait.Until(wb => wb.FindElement(
                By.CssSelector("#app > div.common-modal-container > div > div.common-modal-foot > div > div > div:nth-child(1)")
                )).Click(); 

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
                By.CssSelector("#app > div.upload-v2-container > div.upload-v2-step2-container > div.file-content-v2-container > div.normal-v2-container > div.content-title-v2-container > div.content-title-v2-input-wrp > div > div > input")
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
                By.CssSelector("#app > div.upload-v2-container > div.upload-v2-step2-container > div.file-content-v2-container > div.normal-v2-container > div.content-desc-v2-container > div.content-desc-v2-text-wrp > div > textarea")
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
                By.CssSelector("#content-tag-v2-container > div.content-tag-v2-input-wrp > div > div.input-box-v2-1-instance > input")
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
            return true;
        }
    }
}
