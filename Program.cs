using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HttpCLient;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace HypertextClient
{
    class Downloader
    {
        
        [STAThread] public static void Main()
        {
            try
            {
                string toGet = "https://www.google.com/";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("paste in the console which page you want to download");
                toGet = Console.ReadLine();

                HttpCLient.Downloader download = new HttpCLient.Downloader();
                var downTask = download.PageDownloader(toGet);

                Task dlTask = downTask;

                //waiting for end
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Holding for at least 5 seconds...");
                Thread.Sleep(TimeSpan.FromSeconds(5));

                dlTask.GetAwaiter().GetResult();
                Console.ForegroundColor = ConsoleColor.Green;
                if (!download.Err)
                {
                    Console.WriteLine("html file downloaded to " + download.SavePath + "\nquitting in 5 seconds");
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                Console.WriteLine("quitting in 5 seconds");
                Console.ForegroundColor = ConsoleColor.White;

            }
            catch (InvalidOperationException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error in url!");
                Interaction.MsgBox("Error in url!", MsgBoxStyle.Critical, "Error!");
                Console.ForegroundColor = ConsoleColor.White;
            }

            
        }
    }
}

