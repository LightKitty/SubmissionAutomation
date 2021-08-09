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

namespace SubmissionAutomation.Channels
{
    /// <summary>
    /// 抖音
    /// </summary>
    public class Douyin : Channel
    {

        private const string url = "https://creator.douyin.com/content/upload"; //网址
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
        public Douyin(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) 
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
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            var flag = Wait.Until(Driver, x => x.FindElementByTagAndText("div", "登录帐号"), 3000, 100, false);
            if (flag != null)
            {
                IWebElement okBtn = Wait.Until(Driver, x => x.FindElementByTagAndText("button", "确认"));
                okBtn.Click();

                IWebElement phoneBtn = Wait.Until(Driver, x => x.FindInnermostElementByTagAndText("div", "手机号登录"));
                phoneBtn.Click();

                IWebElement phoneNumberInput = Wait.Until(Driver, x => x.FindElementByTagAndAttribute("input", "placeholder", "请输入手机号"));

                var agreementButton = Wait.Until(Driver, x => x.FindElement(By.ClassName("agreement"))).GetChildern().First();
                agreementButton.Click();

                phoneNumberInput.SendKeys(Config.Account);
                var sendButtom = wait.Until(x => x.FindElementByTagAndText("span", "发送验证码"));
                sendButtom.Click();

                SoundHelper.Remind();

                var publishBtn = Wait.Until(Driver, x => x.FindElementByTagAndText("button", "发布视频", true), 600000);
                publishBtn.Click();
            }

            return true;
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
                //By.ClassName("el-upload-dragger")
                By.Name("upload-btn")
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
            return true;
        }

        /// <summary>
        /// 写简介
        /// </summary>
        /// <param name="introduction"></param>
        /// <returns></returns>
        internal override bool WriteIntroduction(string introduction)
        {
            IWebElement element = wait.Until(wb => wb.FindElement(
                By.ClassName("notranslate")
                ));

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
            if(tags?.Count()>0)
            {
                IWebElement element = wait.Until(wb => wb.FindElement(
                By.ClassName("notranslate")
                ));

                var _tag = tags.Take(maxTagCount);
                foreach(string tag in _tag)
                {
                    element.SendKeys("#" + tag);
                    Thread.Sleep(500);
                    element.SendKeys(Keys.Enter);
                    Thread.Sleep(100);
                }
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
