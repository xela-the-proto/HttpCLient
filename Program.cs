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
            string toGet = "https://www.google.com/";
            Console.WriteLine("paste in the console which page you want to download");
            toGet = Console.ReadLine();
            
            HttpCLient.Downloader download = new HttpCLient.Downloader();
            var downTask = download.PageDownloader(toGet);
            
            Task dlTask = downTask;
            
            //waiting for end
            Console.WriteLine("Holding for at least 5 seconds...");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            
            dlTask.GetAwaiter().GetResult();

            Console.WriteLine("html file downloaded to" + download.SavePath + "\nquitting in 5 seconds");
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
    }
}

