using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LogStandard
{
    /// <summary>
    /// 日志保存目录获取
    /// </summary>
    public class LocalPath
    {

        /// <summary>
        /// 当前日志文件名
        /// </summary>
        public string FileName { get; set; } = "log";
        /// <summary>
        /// 获取当前程序的运行目录，结束没有反斜杠
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return Directory.GetCurrentDirectory();
        }


        public LocalPath() { }
        /// <summary>
        /// 指定文件名
        /// </summary>
        /// <param name="FileName"></param>
        public LocalPath(string FileName)
        {
            this.FileName = FileName;
        }

        /// <summary>
        /// 获取绝对位置
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            string filename = GetPath() + $"\\{this.FileName}.txt";

            //判断文件是否存在 
            if (File.Exists(filename) == false)
            {
                File.Create(filename).Dispose();
            }

            return filename;
        }

    }
}
