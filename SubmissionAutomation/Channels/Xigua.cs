using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SubmissionAutomation.Consts;
using SubmissionAutomation.Extensions;
using SubmissionAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static OpenQA.Selenium.RelativeBy;

namespace SubmissionAutomation.Channels
{
    /// <summary>
    /// 西瓜视频
    /// </summary>
    public class Xigua : Channel
    {

        private const string url = "https://mp.toutiao.com/profile_v4/xigua/upload-video"; //网址
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
        /// <param name="classifyName"></param>
        public Xigua(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) 
            : base(url, videoPath, coverPath, tags, title, introduction, classifyName, originalName, operateInterval)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Operate()
        {
            return base.Operate(new Func<bool>[] { Login }, null);
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
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            var input = Wait.Until(Driver, wb => wb.FindElementByTagAndAttribute("input", "placeholder", "手机号"), 3000, 500, false);
            if (input != null)
            {
                input.SendKeys(Config.Account);
                Wait.Until(Driver, wb => wb.FindElementByTagAndText("span", "获取验证码")).Click();
                SoundHelper.Remind();
                Wait.Until(Driver, x => x.FindElement(By.ClassName("byte-upload-trigger-tip")), 600000, 500);
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
            //查找上传按钮
            //IWebElement uploadButton = wait.Until(wb => wb.FindElement(
            //    By.CssSelector("#root > div > div.byte-tabs.byte-tabs-horizontal.byte-tabs-line.byte-tabs-size-default.xigua-upload-manage > div.byte-tabs-content.byte-tabs-content-horizontal > div > div.byte-tabs-content-item.byte-tabs-content-item-active > div > div > div > div.upload-video-trigger")
            //    ));

            IWebElement uploadButton = Wait.Until(Driver, x => x.FindElement(By.ClassName("byte-upload-trigger-tip")));
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
                By.CssSelector("#root > div > div > div.byte-tabs-content.byte-tabs-content-horizontal > div > div.byte-tabs-content-item.byte-tabs-content-item-active > div > div > div > div.video-list-content > div > div.video-from-container > div.video-form-bone.video-form-basic > div.video-form-wrapper > div.video-form-item.form-item-poster > div.video-form-item-wrapper > div.video-form-item-control > div > div > div")
                )); //获取图片上传控件
            coverElement.Click(); //点击上传按钮

            //点击本地上传
            wait.Until(wb => wb.FindElement(
                By.CssSelector("body > div.Dialog-container > div > div.m-content > div > div.body.undefined > ul > li:nth-child(2)")
                )).Click();

            //点击上传区域
            wait.Until(wb => wb.FindElement(
                By.CssSelector("body > div.Dialog-container > div > div.m-content > div > div.body.undefined > div > div > div")
                )).Click();

            Thread.Sleep(1000); //等待系统弹窗

            if (!Helpers.OpenFileDialog.SelectFileAndOpen(path)) return false;
            Thread.Sleep(500);
            
            try
            { //可能不用点这个按钮
              //点击完成裁剪
                IWebElement _we = wait.Until(wb => wb.FindElement(
                    By.CssSelector("#tc-ie-base-content > div.tc-ie-base > div.base-content-wrap > div.base-content > div > div.clip-btn-wrap > div > div")
                    ));
                _we.Click();
                Thread.Sleep(5000); //等待裁剪完成
            }
            catch { }

            //点击确定
            IWebElement okBtn1 = wait.Until(wb => wb.FindElement(
                By.CssSelector("#tc-ie-base-content > div.tc-ie-base > div.base-content-wrap > div.footer-btns > div.btns > button.btn-l.btn-sure.ml16"))
                );

            Wait.UntilTrue(okBtn1, x => x.GetCssValue("opacity") == "1", 20000);
            okBtn1.Click();

            //while (true)
            //{
            //    string opacity = okBtn1.GetCssValue("opacity");
            //    if (opacity == "1")
            //    {
            //        okBtn1.Click();
            //        break;
            //    }
            //    Thread.Sleep(100);
            //}

            Thread.Sleep(100);

            //点击确定
            IWebElement okBtn2 = wait.Until(wb => wb.FindElementByClassAndText("m-button", "确定"));
            okBtn2.Click();

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
                By.CssSelector("#root > div > div > div.byte-tabs-content.byte-tabs-content-horizontal > div > div.byte-tabs-content-item.byte-tabs-content-item-active > div > div > div > div.video-list-content > div > div.video-from-container > div.video-form-bone.video-form-basic > div.video-form-wrapper > div.video-form-item.form-item-title > div.video-form-item-wrapper > div.video-form-item-control > div > div > input")
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
                By.CssSelector("#root > div > div > div.byte-tabs-content.byte-tabs-content-horizontal > div > div.byte-tabs-content-item.byte-tabs-content-item-active > div > div > div > div.video-list-content > div > div.video-from-container > div.video-form-bone.video-form-basic > div.video-form-wrapper > div.video-form-item.form-item-abstract > div.video-form-item-wrapper > div > div > textarea")
                ))
                .SendKeys(introduction);

            //IWebElement tempElement = wait.Until(x => x.FindElementByTagAndText("span", "简介", true));
            //IWebElement incElement = Driver.FindElement(
            //    WithTagName("input")
            //    .RightOf(tempElement)
            //    );
            //incElement.SendKeys(introduction);

            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            Driver.ExecuteScript("document.querySelector('#root > div > div > div.byte-tabs-content.byte-tabs-content-horizontal > div > div.byte-tabs-content-item.byte-tabs-content-item-active > div > div > div > div.video-list-content > div > div.video-from-container > div.video-form-bone.video-form-advanced > div.video-form-wrapper > div.video-form-item.form-item-video-tag > div.video-form-item-wrapper > div > div').click()"); //js方式设置焦点（Selenium的方式未成功）

            Thread.Sleep(200);

            IWebElement tagElement = wait.Until(wb => wb.FindElement(
                By.CssSelector("#root > div > div > div.byte-tabs-content.byte-tabs-content-horizontal > div > div.byte-tabs-content-item.byte-tabs-content-item-active > div > div > div > div.video-list-content > div > div.video-from-container > div.video-form-bone.video-form-advanced > div.video-form-wrapper > div.video-form-item.form-item-video-tag > div.video-form-item-wrapper > div > div")
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
