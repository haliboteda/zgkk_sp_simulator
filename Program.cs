using System;
using System.Windows.Forms;

namespace zgkk_sp_simulator
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// <summary>
        /// 命令行参数（都可选，给自动化测试用，人手用照旧在界面上点）：
        ///   &lt;COMn&gt;               启动就选中这个口并自动点连接
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(args));
        }
    }
}
