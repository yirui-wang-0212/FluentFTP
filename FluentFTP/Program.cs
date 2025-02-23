﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FluentFTP
{
	class Program
	{

		// private static bool IsIntForDouble(double d)
		// {
		// 	double eps = 1e-10;  // 精度范围
		// 	return (d - Math.Floor(d) < eps);
		// }

		public static void DownloadFile()
		{
			using (var ftp = new FtpClient("10.86.96.254", "w", "unitychina0702"))
			{
				ftp.Connect();

				// define the progress tracking callback
				//Action<FtpProgress> progress = delegate (FtpProgress p) {
				//	if (p.Progress == 1)
				//	{
				//		// all done!
				//		double percentDone = p.Progress * 100;
				//		Console.WriteLine("-------------------- Progress: {0:g}% --------------------", percentDone);
				//	}
				//	else
				//	{
				//		// percent done = (p.Progress * 100)
				//		double percentDone = p.Progress * 100;
				//		Console.WriteLine("-------------------- Progress: {0:g}% --------------------", percentDone);
				//		if (IsIntForDouble(percentDone) && ((int)percentDone % 10 == 0))
				//		{
				//			Console.WriteLine("-------------------- Progress: {0:g}% --------------------", percentDone);
				//		}
				//	}
				//};

				// define the progress tracking callback
				Action<FtpProgress> progress = delegate (FtpProgress p) {
					if (p.Progress == 1)
					{
						// all done!
					}
					else
					{
						// percent done = (p.Progress * 100)
					}
				};

				// download a file with progress tracking
				ftp.DownloadFile(@"C:\Users\w\Developer\FTPTemp\UnitySetup64-2020.3.6f1.exe", "/UnitySetup64-2020.3.6f1.exe", FtpLocalExists.Overwrite, FtpVerify.None, progress);
			}
		}

		public static void RunInstaller()
		{
			// https://docs.unity3d.com/Manual/InstallingUnity.html
			string str = @"C:\Users\w\Developer\FTPTemp\UnitySetup64-2020.3.6f1.exe" + " /S /D=" + @"C:\Users\w\Developer\UnityInstallTest\Unity";

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(str + "&exit");

            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            //StreamReader reader = p.StandardOutput;
            //string line=reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    str += line + "  ";
            //    line = reader.ReadLine();
            //}

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();


            Console.WriteLine(output);
		}

		static void Main(string[] args)
		{
			DownloadFile();
			RunInstaller();
		}
	}
}
