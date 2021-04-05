using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolBox;

namespace SubmissionAutomation.Helpers
{
    /// <summary>
    /// 打开对话框帮助类
    /// </summary>
    public static class OpenFileDialog
    {
        /// <summary>
        /// 选择文件并打开
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SelectFileAndOpen(string path)
        {
            IntPtr hWnd = TWindowsApi.FindWindow(null, "打开");
            if (hWnd != null && hWnd != new IntPtr(0))
            {
                uint WM_SETTEXT = 0xC;
                IntPtr textHwnd = TWindowsApi.FindWindowEx(hWnd, IntPtr.Zero, null, "文件名(&N):"); //获取文件名lable句柄
                IntPtr editor = TWindowsApi.FindWindowEx(hWnd, textHwnd, null, null); //获取文本框句柄（位于文件名lable后）
                TWindowsApi.SendMessage(editor, WM_SETTEXT, IntPtr.Zero, path);
                Thread.Sleep(100);

                IntPtr childHwnd = TWindowsApi.FindWindowEx(hWnd, IntPtr.Zero, null, "打开(&O)"); //获取按钮的句柄
                if (childHwnd != IntPtr.Zero)
                {
                    TWindowsApi.SendMessage(childHwnd, 0xF5, 0, 0); //鼠标点击的消息，对于各种消息的数值，大家还是得去API手册
                    return true; //成功
                }
            }

            return false;
        }
    }
}
