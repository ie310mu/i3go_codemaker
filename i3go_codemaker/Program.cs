﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IE310.Tools.CodeMaker
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            //Application.Run(new FormTest());
        }
    }
}
