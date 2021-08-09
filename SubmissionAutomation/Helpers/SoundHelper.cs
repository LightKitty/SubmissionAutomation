using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionAutomation.Helpers
{
    /// <summary>
    /// 声音助手
    /// </summary>
    public static class SoundHelper
    {
        /// <summary>
        /// 提醒
        /// </summary>
        public static void Remind()
        {
            PlayBeep();
        }

        /// <summary>
        /// 嘟
        /// </summary>
        public static void PlayBeep()
        {
            SystemSounds.Beep.Play();
        }
    }
}
