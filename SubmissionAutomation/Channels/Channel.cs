using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubmissionAutomation.Channels
{
    /// <summary>
    /// 渠道
    /// </summary>
    public abstract class Channel
    {
        /// <summary>
        /// 浏览器驱动
        /// </summary>
        public static ChromeDriver Driver { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        public string VideoPath { get; private set; }

        /// <summary>
        /// 封面地址
        /// </summary>
        public string CoverPath { get; private set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags { get; private set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; private set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassifyName { get; private set; }

        /// <summary>
        /// 来源类型（原创/转载）
        /// </summary>
        public string OriginalName { get; private set; }

        /// <summary>
        /// 操作间隔
        /// </summary>
        public int OperateInterval { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Channel(string url, string videoPath, string coverPath, string[] tags, string title, string introduction, string classifyName, string originalName, int operateInterval)
        {
            Url = url;
            VideoPath = videoPath;
            CoverPath = coverPath;
            Tags = tags;
            Title = title;
            Introduction = introduction;
            ClassifyName = classifyName;
            OriginalName = originalName;
            OperateInterval = operateInterval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool Operate();

        /// <summary>
        /// 投递
        /// </summary>
        /// <returns></returns>
        internal bool Operate(Func<bool>[] beforeOperates, Func<bool>[] afterOperates)
        {
            if (!GoTo(Url))
                return false;

            Thread.Sleep(OperateInterval);

            if (!BeforeOperate(beforeOperates))
                return false;

            Thread.Sleep(OperateInterval);

            if (!UploadVideo(VideoPath))
                return false;

            Thread.Sleep(OperateInterval);

            if (!WriteTitle(Title))
                return false;

            Thread.Sleep(OperateInterval);

            if (!WriteIntroduction(Introduction))
                return false;

            Thread.Sleep(OperateInterval);

            if (!OriginalStatement(OriginalName))
                return false;

            Thread.Sleep(OperateInterval);

            if (!SetTags(Tags))
                return false;

            Thread.Sleep(OperateInterval);

            if (!SetClassify(ClassifyName))
                return false;

            Thread.Sleep(OperateInterval);

            if (!SetCover(CoverPath))
                return false;

            Thread.Sleep(OperateInterval);

            if (!AfterOperate(afterOperates))
                return false;

            Thread.Sleep(OperateInterval);

            return true;
        }

        /// <summary>
        /// 跳转到网址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal abstract bool GoTo(string url);

        /// <summary>
        ///  前面附加操作
        /// </summary>
        /// <param name="oerates"></param>
        /// <returns></returns>
        internal bool BeforeOperate(params Func<bool>[] oerates)
        {
            if (oerates == null) return true;
            foreach (var operate in oerates)
            {
                if (!operate()) return false;
                Thread.Sleep(OperateInterval);
            }
            return true;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        internal abstract bool UploadVideo(string path);

        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        internal abstract bool SetCover(string path);

        /// <summary>
        /// 写标题
        /// </summary>
        /// <returns></returns>
        internal abstract bool WriteTitle(string title);

        /// <summary>
        /// 写简介
        /// </summary>
        /// <returns></returns>
        internal abstract bool WriteIntroduction(string introduction);

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        internal abstract bool SetTags(string[] tags);

        /// <summary>
        /// 设置分类
        /// </summary>
        /// <returns></returns>
        internal abstract bool SetClassify(string name);

        /// <summary>
        /// 原创声明
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        internal abstract bool OriginalStatement(string typeName);

        /// <summary>
        ///  后面附加操作
        /// </summary>
        /// <param name="oerates"></param>
        /// <returns></returns>
        internal bool AfterOperate(params Func<bool>[] oerates)
        {
            if (oerates == null) return true;
            foreach(var operate in oerates)
            {
                if (!operate()) return false;
                Thread.Sleep(OperateInterval);
            }
            return true;
        }
    }
}
