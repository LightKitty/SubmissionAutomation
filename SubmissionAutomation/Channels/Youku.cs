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
            return base.Operate(new Func<bool>[] { Login }, null);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            var iframe = Wait.Until(Driver, wb => wb.FindElement(By.Id("alibaba-login-box")), 3000, 500, false);
            if (iframe != null)
            {
                //string originalWindow = Driver.CurrentWindowHandle;
                Driver.SwitchTo().Frame(iframe);
                var phoneNumberInput = Wait.Until(Driver, x => x.FindElementByTagAndAttribute("input", "placeholder", "请输入手机号码"));
                phoneNumberInput.SendKeys(Config.Account);
                var sendBtn = wait.Until(x => x.FindElement(By.ClassName("send-btn")));
                sendBtn.Click();
                SoundHelper.Remind();

                //Driver.SwitchTo().Window(originalWindow);
                Driver.SwitchTo().DefaultContent();
                var publishButton = Wait.Until(Driver, x => x.FindInnermostElementByTagAndText("span", "发布视频"), 60000);
                publishButton.Click();
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
            DateTime startTime = DateTime.Now;
            while(true)
            {
                try
                {
                    //查找上传按钮
                    var btns = wait.Until(x => x.FindElements(
                         By.ClassName("ant-btn")
                         ));

                    var btn = btns.FindElementByText("上传视频");
                    btn.Click();

                    Thread.Sleep(1000);

                    if (!OpenFileDialog.SelectFileAndOpen(path)) return false;

                    Thread.Sleep(1000);

                    return true;
                }
                catch
                {
                    if ((DateTime.Now - startTime).TotalMilliseconds > 10000)
                        throw;

                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal override bool SetCover(string path)
        {
            var upload = Wait.Until(Driver, x => x.FindInnermostElementByTagAndText("div", "上传"));
            upload.Click();
            Thread.Sleep(1000);
            OpenFileDialog.SelectFileAndOpen(path);
            Thread.Sleep(1000);

            var okBtn = Wait.Until(Driver, x => x.FindInnermostElementByTagAndText("button", "保 存"));
            okBtn.Click();

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

            //清理文本
            int textLen = titleInput.GetAttribute("value").Length;
            for (int i = 0; i < textLen; i++)
            {
                titleInput.SendKeys(Keys.Backspace);
            }
            
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
            //简介
            var textareas = wait.Until(wb => wb.FindElements(
                By.TagName("textarea")
                ));

            IWebElement textarea = Wait.Until(textareas, x => x.FindElementByAttribute("placeholder", "请输入视频简介"));
            textarea.Clear();
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
            var inputs = wait.Until(wb => wb.FindElements(
                By.TagName("label")
                ));

            IWebElement tempElement = Wait.Until(inputs, x => x.FindElementByAttribute("for", "tags"));

            IWebElement tagInput = Driver.FindElement(
                WithTagName("input")
                .RightOf(tempElement)
                );

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
            if(!string.IsNullOrEmpty(name))
            {
                string[] classes = name.Split(' ');
                if (classes.Length == 2)
                {
                    IWebElement span = wait.Until(wb => wb.FindElement(
                        By.ClassName("ant-cascader-picker-label")
                        ));
                    span.Click();
                    Thread.Sleep(100);
                    var items = wait.Until(wb => wb.FindElements(
                    By.ClassName("ant-cascader-menu-item")
                    ));
                    var item = items.FindElementByText(classes[0]);
                    item.Click();
                    Thread.Sleep(100);
                    items = wait.Until(wb => wb.FindElements(
                    By.ClassName("ant-cascader-menu-item")
                    ));
                    item = items.FindElementByTextStart(classes[1]);
                    item.Click();
                }
                else
                {
                    return false;
                }
            }

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
