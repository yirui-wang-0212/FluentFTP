using System;
using System.Collections.Generic;
using System.Text;

namespace FluentFTP
{
	class Program
	{

		private static bool IsIntForDouble(double d)
		{
			double eps = 1e-10;  // 精度范围
			return (d - Math.Floor(d) < eps);
		}

		public static void DownloadFile()
		{
			using (var ftp = new FtpClient("10.86.96.254", "w", "unitychina0702"))
			{
				ftp.Connect();

				// define the progress tracking callback
				Action<FtpProgress> progress = delegate (FtpProgress p) {
					if (p.Progress == 1)
					{
						// all done!
						double percentDone = p.Progress * 100;
						Console.WriteLine("-------------------- Progress: {0:g}% --------------------", percentDone);
					}
					else
					{
						// percent done = (p.Progress * 100)
						double percentDone = p.Progress * 100;
						Console.WriteLine("-------------------- Progress: {0:g}% --------------------", percentDone);
						if (IsIntForDouble(percentDone) && ((int)percentDone % 10 == 0))
						{
							Console.WriteLine("-------------------- Progress: {0:g}% --------------------", percentDone);
						}
					}
				};

				// download a file with progress tracking
				ftp.DownloadFile(@"C:\Users\w\Developer\FTPTemp\aa.txt", "/aa.txt", FtpLocalExists.Overwrite, FtpVerify.None, progress);

			}
		}

		static void Main(string[] args)
		{
			DownloadFile();
		}
	}
}
