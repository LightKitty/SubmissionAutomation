using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubmissionAutomation.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class Wait
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="waitObject"></param>
        /// <param name="condition"></param>
        /// <param name="maxWaitTime"></param>
        /// <param name="waitInterval"></param>
        /// <returns></returns>
        public static TResult Until<T, TResult>(T waitObject, Func<T, TResult> condition, int maxWaitTime = 10000, int waitInterval = 100)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //var startTime = DateTime.Now;
            while(true)
            {
                try
                {
                    return condition(waitObject);
                }
                catch
                {
                    if (sw.Elapsed.TotalSeconds >= maxWaitTime)
                    {
                        throw;
                    }
                }
                Thread.Sleep(waitInterval);
            }
        }
    }
}
