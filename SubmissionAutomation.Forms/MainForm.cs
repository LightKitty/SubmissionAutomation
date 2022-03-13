using LightLog;
using OpenQA.Selenium.Chrome;
using SubmissionAutomation.Channels;
using SubmissionAutomation.Consts;
using SubmissionAutomation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubmissionAutomation.Forms
{
    public partial class MainForm : Form
    {
        private static readonly object initChromeDriverLock = new object();

        CheckBox[] checkBoxes = null;

        public MainForm()
        {
            InitializeComponent();

            checkBoxes = new CheckBox[]
            {
                checkBoxPublishBilibili,
                checkBoxPublishDouyu,
                checkBoxPublishXigua,
                checkBoxPublishBaidu,
                checkBoxPublishWangyi,
                checkBoxPublishWeibo,
                checkBoxPublishZhihu,
                checkBoxPublishXiaohongshu,
                checkBoxPublishKuaishou,
                checkBoxPublishDouyin,
                checkBoxPublishYouku
            };

            InitChromeDriver();
        }

        /// <summary>
        /// 初始化谷歌浏览器
        /// </summary>
        private void InitChromeDriver()
        {
            Config.Init();
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--user-data-dir=C:/Users/Administrator/AppData/Local/Google/Chrome/User Data"); //指定用户文件夹路径
            options.AddArgument("--profile-directory=Default"); //指定用户
            var driver = new ChromeDriver(@"D:\WebDriver\bin", options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("chrome://newtab");
            Context.Driver = driver;
            Channel.Driver = driver;
        }

        private void buttonOpenVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = "(*.mp4;*.flv)|*.mp4;*.flv|(*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxVideoPath.Text = openFileDialog.FileName;
            }
        }

        private void buttonOpenCover_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = "(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|(*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxCoverPath.Text = openFileDialog.FileName;
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            buttonSubmit.Enabled = false;
            Task.Run(() =>
            {
                try
                {
                    UpdateToolStripStatusLabel("开始发布");

                    Thread.Sleep(100);

                    List<Channel> channels = GetPublishChannels();

                    foreach (Channel channel in channels)
                    {
                        try
                        {
                            channel.Operate();
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"{channel.Name}发布错误", ex);
                            ShowErrorMessageInvoke($"{channel.Name}发布错误", ex);
                        }
                    }

                    UpdateToolStripStatusLabel("完成");
                }
                catch (Exception ex)
                {
                    Log.Error($"发布错误", ex);
                    ShowErrorMessageInvoke($"发布错误", ex);
                }
                finally
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        buttonSubmit.Enabled = true;
                    }));
                }
            });
        }

        public void ShowErrorMessageInvoke(string message, Exception ex)
        {
            this.BeginInvoke(new Action(() =>
            {
                textBoxLog.AppendText(DateTime.Now.ToString() + Environment.NewLine);
                if (message!=null) textBoxLog.AppendText(message + Environment.NewLine);
                if(ex != null) textBoxLog.AppendText(ex.Message + Environment.NewLine);
            }));
        }

        private List<Channel> GetPublishChannels()
        {
            var channels = new List<Channel>();

            string videoPath = textBoxVideoPath.Text.Trim();
            string coverPath = textBoxCoverPath.Text.Trim();
            string title = textBoxTitle.Text.Trim();
            string[] tags = textBoxTags.Text.Trim().Split(' ');
            string introduction = textBoxIntroduction.Text.Trim();

            if (checkBoxPublishBilibili.Checked)
            { //bilibili
                channels.Add(new Bilibili(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = null,
                    OriginalName = checkBoxOriginalBilibili.Checked.ToString(),
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishDouyu.Checked)
            { //斗鱼
                channels.Add(new Douyu(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = textBoxDouyuClassify.Text.Trim(),
                    OriginalName = null,
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishXigua.Checked)
            { //西瓜
                string originalTypeName = null;
                if (radioButtonXiguaOriginal.Checked) originalTypeName = "原创";
                else if (radioButtonXiguaReprint.Checked) originalTypeName = "转载";

                channels.Add(new Xigua(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = null,
                    OriginalName = originalTypeName,
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishBaidu.Checked)
            { //百度
                string originalTypeName = null;
                if (radioButtonBaiduOriginal.Checked) originalTypeName = radioButtonBaiduOriginal.Text;
                else if (radioButtonBaiduReprint.Checked) originalTypeName = radioButtonBaiduReprint.Text;
                channels.Add(new Baidu(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = null,
                    OriginalName = null,
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishWangyi.Checked)
            { //网易
                string originalTypeName = null;
                if (radioButtonWangyiOriginal.Checked) originalTypeName = "原创";
                else if (radioButtonWangyiReprint.Checked) originalTypeName = "转载";

                channels.Add(new Wangyi(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = "科普·趣闻",
                    OriginalName = originalTypeName,
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishWeibo.Checked)
            { //微博
                channels.Add(new Weibo(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = null,
                    OriginalName = null,
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishZhihu.Checked)
            { //知乎
                channels.Add(new Zhihu(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = null,
                    OriginalName = "原创",
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishXiaohongshu.Checked)
            { //小红书
                channels.Add(new Xiaohongshu(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = "科普·趣闻",
                    OriginalName = null,
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishKuaishou.Checked)
            { //快手
                channels.Add(new Kuaishou(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = "科学 天文",
                    OriginalName = null,
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishDouyin.Checked)
            { //抖音
                channels.Add(new Douyin(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    OriginalName = null,
                    ClassifyName = null,
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            if (checkBoxPublishYouku.Checked)
            { //优酷
                channels.Add(new Youku(new ChannelInitParam
                {
                    VideoPath = videoPath,
                    CoverPath = coverPath,
                    Tags = tags,
                    Title = title,
                    Introduction = introduction,
                    ClassifyName = "知识/文化 科普知识",
                    OriginalName = "原创",
                    HandelException = ShowErrorMessageInvoke
                }));
            }

            return channels;
        }

        private void UpdateToolStripStatusLabel(string text)
        {
            toolStripStatusLabel.Text = text;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Channel.Driver?.Dispose();
        }

        private void checkBoxPublishAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in checkBoxes)
            {
                checkBox.Checked = checkBoxPublishAll.Checked;
            }
        }
    }
}
