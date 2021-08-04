using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionAutomation.Consts
{
    /// <summary>
    /// 配置
    /// </summary>
    public static class Config
    {
        private const string appIniPath = "config.ini"; //应用配置文件路径

        /// <summary>
        /// 账号
        /// </summary>
        public static string Account { get; private set; }

        /// <summary>
        /// 配置初始化
        /// </summary>
        public static void Init()
        {
            using (StreamReader sr = new StreamReader(appIniPath))
            {
                string line = null;
                line = sr.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    if (!line.StartsWith("//"))
                    {
                        int index = line.IndexOf('=');
                        string key = line.Substring(0, index).Trim();
                        string value = line.Substring(index + 1).Trim();
                        switch (key)
                        {
                            case "Account":
                                Account = value;
                                break;
                            default:
                                break;
                        }
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
        }
    }
}
