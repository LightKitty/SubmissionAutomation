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
    public class Wangyi : Channel
    {

        private const string url = "https://mp.163.com/index.html#/post/video"; //网址
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
        public Wangyi(string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName) 
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

        internal bool Login()
        {
            string title = Driver.Title;
            if (title.StartsWith("登录"))
            { // 需要登录
                wait.Until(wb => wb.FindElements(By.TagName("iframe")).Count() > 0);
                Driver.SwitchTo().Frame(0);

                //登录
                wait.Until(wb => wb.FindElement(
                    By.CssSelector("#dologin")
                    //By.LinkText("登  录")
                    )).Click();
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
            IWebElement uploadButton = wait.Until(wb => wb.FindElement(
                By.CssSelector("#root > div > div.content.slide.slide-entered.post-content > div > div > div > div > div > div > div:nth-child(1) > div")
                ));
            //uploadButton.SendKeys(path); //设置上传值

            uploadButton.Click(); //点击上传按钮

            Thread.Sleep(1000); //等待系统弹窗

            if (!Helpers.OpenFileDialog.SelectFileAndOpen(path)) return false;

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
            //Thread.Sleep(1000); //等待
            IWebElement titleElement = wait.Until(wb => wb.FindElement(
                By.CssSelector("#neatui-form-title")
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
            return true;
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal override bool SetTags(string[] tags)
        {
            Driver.ExecuteScript(
                "document.querySelector('#root > div > div.content.slide.slide-entered.post-content > div > div > div > div > div:nth-child(3) > div.common-form.style-module_video-form_2zQ3d > div:nth-child(4) > div.column.column-19.ne-form-item-content.ne-form-item-col.ne-form-item-content-medium > div > div.style-module_tag-contain_2_FqU > span.ne-tag.ne-tag-medium.ne-tag-create > span').click()"
                ); //js方式设置焦点（Selenium的方式未成功）

            Thread.Sleep(100);

            IEnumerable<string> _tags = tags.Take(maxTagCount);
            foreach (string tag in _tags)
            {
                IWebElement tagElement = wait.Until(wb => wb.FindElement(
                    By.CssSelector("#root > div > div.content.slide.slide-entered.post-content > div > div > div > div > div:nth-child(3) > div.common-form.style-module_video-form_2zQ3d > div:nth-child(4) > div.column.column-19.ne-form-item-content.ne-form-item-col.ne-form-item-content-medium > div > div.style-module_tag-contain_2_FqU > span.ne-tag.ne-tag-medium.ne-tag-create > span > input")
                    )); //标签

                tagElement.SendKeys(tag + Keys.Enter);
                Thread.Sleep(100);
            }

            return true;
        }

        /// <summary>
        /// 设置分类
        /// </summary>
        /// <returns></returns>
        internal override bool SetClassify(string name)
        {
            var spans = wait.Until(wb => wb.FindElements(
                By.ClassName("ne-tag-content")
                ));
            foreach(var span in spans)
            {
                if(span.Text=="科技")
                {
                    span.Click();
                    Thread.Sleep(200);
                    break;
                }
            }

            //分类按钮
            var classifyButtons = wait.Until(wb => wb.FindElements(
                By.ClassName("ne-menu-item")
                ));

            //var classifyButtons = classifyButton1.FindElements(
            //    By.ClassName("ne-menu-item")
            //    );

            foreach (var btn in classifyButtons)
            {
                var text = btn.Text;
                if (text.StartsWith(name))
                {
                    btn.Click();
                    break;
                }
            }

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
                By.Id("cropper-input")
                )); //获取图片上传控件
            coverElement.SendKeys(path); //设置上传值
            Thread.Sleep(500);
            
            var divs = wait.Until(wb => wb.FindElements(
               By.ClassName("controller")
               ));

            foreach(var div in divs)
            {
                var btns = div.FindElements(
                    By.TagName("button")
                    );
                foreach(var btn in btns)
                {
                    if (btn.Text == "确 定")
                    {
                        btn.Click();
                        goto Jump2;
                    }
                }
            }
            
            Jump2:

            return true;
        }

        /// <summary>
        /// 原创声明
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        internal override bool OriginalStatement(string typeName)
        {
            var spans = wait.Until(wb => wb.FindElements(
                By.ClassName("ne-switch-base-label-text")
                ));
            foreach (var span in spans)
            {
                if (span.Text == typeName)
                {
                    span.Click();
                    break;
                }
            }

            return true;
        }
    }
}
