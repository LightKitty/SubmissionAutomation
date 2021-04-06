using LightLog;
using OpenQA.Selenium.Chrome;
using SubmissionAutomation.Channels;
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
        }

        /// <summary>
        /// 初始化谷歌浏览器
        /// </summary>
        private void InitChromeDriver()
        {
            lock(initChromeDriverLock)
            {
                if (Channel.Driver == null)
                {
                    var options = new ChromeOptions();
                    options.AddArgument("--user-data-dir=C:/Users/Yang/AppData/Local/Google/Chrome/User Data"); //置顶用户文件夹路径
                    options.AddArgument("--profile-directory=Default"); //指定用户
                    Channel.Driver = new ChromeDriver(@"D:\WebDriver\bin", options); //声明chrome驱动器
                    Channel.Driver.Manage().Window.Maximize();
                    Channel.Driver.Navigate().GoToUrl("chrome://newtab");
                }
            }
        }

        private void buttonOpenVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = "(*.mp4;*.flv)|*.mp4;*.flv|(*.*)|*.*",
                Multiselect = false
            };

            if(openFileDialog.ShowDialog() == DialogResult.OK)
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
            //try
            //{
                UpdateToolStripStatusLabel("开始发布");

                InitChromeDriver();

            Thread.Sleep(100);

                List<Channel> channels = GetPublishChannels();

                foreach (Channel channel in channels)
                {
                    channel.Operate();
                }

                UpdateToolStripStatusLabel("完成");
            //}
            //catch(Exception ex)
            //{
            //    Log.Error("发布错误", ex);
            //    UpdateToolStripStatusLabel("发布错误");
            //    MessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
                channels.Add(new Bilibili(videoPath, coverPath, tags, title, introduction, null, null));
            }

            if(checkBoxPublishDouyu.Checked)
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
