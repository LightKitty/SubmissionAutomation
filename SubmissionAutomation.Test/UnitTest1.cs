using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SubmissionAutomation.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SoundTest()
        {
            System.Media.SystemSounds.Asterisk.Play();
            System.Media.SystemSounds.Beep.Play();
            System.Media.SystemSounds.Exclamation.Play();
            System.Media.SystemSounds.Hand.Play();
            System.Media.SystemSounds.Question.Play();
        }
    }
}
