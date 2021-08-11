using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionAutomation.Models
{
    public class ChannelInitParam
    {
        public string VideoPath;

        public string CoverPath;

        public string[] Tags;

        public string Title;

        public string Introduction;

        public string ClassifyName;

        public string OriginalName;

        public int OperateInterval;

        public Action<string, Exception> HandelException = new Action<string, Exception>((message, exception) =>
        {
            throw new Exception(message, exception);
        });
    }
}
