using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            ////注册编码提供程序
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            //Test1();


            for (int i = 0; i < 3; i++)
            {
                Task.Run(() =>
                {
                    Test2();
                });
            }
            Test2();


            Console.Read();
        }

        /// <summary>
        /// 获取当前程序的位置
        /// </summary>
        static void Test1()
        {
            //获取当前工作目录
            string dir1 = Directory.GetCurrentDirectory();
            Console.WriteLine(dir1);
        }

        static void Test2()
        {
            LogStandard.LogHelper _log = new LogStandard.LogHelper();
            // _log.WriteLine("测试内容");
            _log.Debug(new Exception("请求出错"));
        }
    }
}