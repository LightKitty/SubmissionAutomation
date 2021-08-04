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
        /// <param name="maxWaitTime">ms</param>
        /// <param name="waitInterval"></param>
        /// <param name="isThrowException"></param>
        /// <param name="notDefault">不获取默认值结果</param>
        /// <returns></returns>
        public static TResult Until<T, TResult>(T waitObject, Func<T, TResult> condition, int maxWaitTime = 10000, int waitInterval = 500, bool isThrowException = true, bool notDefault = true)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while(true)
            {
                try
                {
                    var result = condition(waitObject);
                    if (!notDefault || !(result.Equals(default(TResult)))) //可获取默认值 || 不是默认值
                        return result;
                }
                catch
                {
                    if (sw.Elapsed.TotalMilliseconds >= maxWaitTime)
                    {
                        if (isThrowException) throw;
                        else
                        {
                            return default(TResult);
                        }
                    }
                }
                Thread.Sleep(waitInterval);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="waitObject"></param>
        /// <param name="condition"></param>
        /// <param name="maxWaitTime">ms</param>
        /// <param name="waitInterval"></param>
        /// <param name="isThrowException"></param>
        /// <returns></returns>
        public static bool UntilTrue<T>(T waitObject, Func<T, bool> condition, int maxWaitTime = 10000, int waitInterval = 500, bool isThrowException = true)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                try
                {
                    if(condition(waitObject))
                    {
                        return true;
                    }
                }
                catch
                {
                    if (sw.Elapsed.TotalMilliseconds >= maxWaitTime)
                    {
                        if (isThrowException) throw new TimeoutException("Method UntilTrue Timeout");
                        return false;
                    }
                }
                Thread.Sleep(waitInterval);
            }
        }
    }
}
