using LightLog;
using OpenQA.Selenium.Chrome;
using SubmissionAutomation.Channels;
using SubmissionAutomation.Consts;
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

        public MainForm()
        {
            InitializeComponent();

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
            Channel.Driver = new ChromeDriver(@"D:\WebDriver\bin", options);
            Channel.Driver.Manage().Window.Maximize();
            Channel.Driver.Navigate().GoToUrl("chrome://newtab");
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
                        Log.Error($"渠道发布错误", ex);
                        textBoxLog.AppendText($"渠道发布错误," + ex.ToString() + Environment.NewLine);
                    }
                }

                UpdateToolStripStatusLabel("完成");
            }
            catch(Exception ex)
            {
                Log.Error($"发布错误", ex);
                textBoxLog.AppendText($"发布错误," + ex.ToString() + Environment.NewLine);
            }
            buttonSubmit.Enabled = true;
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
                string original = checkBoxOriginalBilibili.Checked.ToString();
                channels.Add(new Bilibili(videoPath, coverPath, tags, title, introduction, null, original));
            }

            if (checkBoxPublishDouyu.Checked)
            {
                channels.Add(new Douyu(videoPath, coverPath, tags, title, introduction, textBoxDouyuClassify.Text.Trim(), null));
            }

            if (checkBoxPublishXigua.Checked)
            {
                channels.Add(new Xigua(videoPath, coverPath, tags, title, introduction, null, null));
            }

            if (checkBoxPublishBaidu.Checked)
            {
                channels.Add(new Baidu(videoPath, coverPath, tags, title, introduction, null, null));
            }

            if (checkBoxPublishWangyi.Checked)
            {
                string originalTypeName = null;
                if (radioButtonWangyiOriginal.Checked) originalTypeName = "原创";
                else if (radioButtonWangyiReprint.Checked) originalTypeName = "转载";

                channels.Add(new Wangyi(videoPath, coverPath, tags, title, introduction, null, originalTypeName));
            }

            if (checkBoxPublishWeibo.Checked)
            {
                channels.Add(new Weibo(videoPath, coverPath, tags, title, introduction, null, null));
            }

            if (checkBoxPublishZhihu.Checked)
            {
                channels.Add(new Zhihu(videoPath, coverPath, tags, title, introduction, null, null));
            }

            if (checkBoxPublishXiaohongshu.Checked)
            {
                channels.Add(new Xiaohongshu(videoPath, coverPath, tags, title, introduction, "科普·趣闻", null));
            }

            if (checkBoxPublishKuaishou.Checked)
            {
                channels.Add(new Kuaishou(videoPath, coverPath, tags, title, introduction, "科学 天文", null));
            }

            if (checkBoxPublishDouyin.Checked)
            {
                channels.Add(new Douyin(videoPath, coverPath, tags, title, introduction, "科学 天文", null));
            }

            if (checkBoxPublishYouku.Checked)
            {
                channels.Add(new Youku(videoPath, coverPath, tags, title, introduction, "知识/文化 科普知识", "原创"));
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
    }
}
