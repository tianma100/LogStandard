using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogStandard
{
    /// <summary>
    /// 日志记录操作
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 操作文件对象
        /// </summary>
        public LocalPath _File = null;

        /// <summary>
        /// 默认处理 
        /// </summary>
        public LogHelper()
        {
            _File = new LocalPath();
        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="FileName">文件名，不含扩展名</param>
        public LogHelper(string FileName)
        {
            _File = new LocalPath(FileName);
        }


        private static object obj = new object();
        /// <summary>
        /// 追加一样内容，时间+msg
        /// </summary>
        /// <param name="msg">内容</param>
        public void WriteLine(string msg, Encoding encode = null)
        {
            if (encode == null)
                encode = Encoding.UTF8;
            //启用锁定，多线程问题
            lock (obj)
            {
                using (FileStream fs = new FileStream(_File.GetFullName(), FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    StreamWriter sw = new StreamWriter(fs, encode);
                    sw.Write(DateTime.Now.ToString("****>>yyyy/MM/dd HH:mm:ss ："));
                    sw.WriteLine(msg);
                    sw.Flush();
                    sw.Dispose();
                    fs.Dispose();
                }
            }
        }

        ///// <summary>
        ///// 向文件中最佳一行内容，指定格式
        ///// 时间+content
        ///// </summary>
        ///// <param name="content">写入内容</param>
        //public void WriteLine(string content, Encoding encoding = null)
        //{
        //    //使用FileStream 写入
        //    if (encoding == null)
        //        encoding = Encoding.UTF8;
        //    byte[] bytes = encoding.GetBytes(content);
        //    using (FileStream fs = new FileStream(_File.GetFullName(), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
        //    {
        //        //移动到文件最后位置
        //        fs.Seek(0, SeekOrigin.End);
        //        fs.Write(bytes, 0, bytes.Length);
        //    }
        //}


        /// <summary>
        /// 追加 一行内容，Unicode编码格式
        /// </summary>
        /// <param name="msg"></param>
        public void WriteLineUnicode(string msg)
        {
            WriteLine(msg, Encoding.Unicode);
        }
        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="ex"></param>
        public void Debug(Exception ex, Encoding encode = null)
        {
            StringBuilder builder = new StringBuilder();
            Exception innerException = ex;
            // builder.AppendLine($"****Debug:{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ms")}");
            builder.AppendLine();
            builder.AppendLine("****异常内容");
            while (innerException != null)
            {
                builder.Append(innerException.Message);
                builder.AppendLine();
                innerException = innerException.InnerException;
            }
            builder.Append("*****跟踪：")
                .Append(ex.StackTrace)
                ;
            WriteLine(builder.ToString(), encode);
        }

    }
}
