﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
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
        public static TResult Until<T, TResult>(T waitObject,Func<T, TResult> condition, int maxWaitTime = 10000, int waitInterval = 500)
        {
            int waitTime = 0;
            while(true)
            {
                try
                {
                    return condition(waitObject);
                }
                catch
                {
#if DEBUG
                    Console.WriteLine("waitTime:" + waitTime);
#endif
                    if(waitTime> maxWaitTime)
                    {
                        throw;
                    }
                }
                Thread.Sleep(waitInterval);
                waitTime += waitInterval;
            }

        }
    }
}