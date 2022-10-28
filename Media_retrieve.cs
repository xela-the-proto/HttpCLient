using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HypertextClient;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace HttpCLient
{
    internal class Media_retrieve
    {
        private List<string> original_list;
        private List<string> parsed_list;
        static Downloader down = new Downloader();
        static string? source = down?.Get_url;
        static HtmlWeb web = new HtmlWeb();
        static Uri uri = new Uri(down.Get_url);
        private static string confirmation = "N";
        public Media_retrieve()
        {

        }

        public void GetImages()
        {
            original_list = new List<string>();
            parsed_list = new List<string>();

            var doc = web.Load(source);
            original_list.Add(down.Get_url);

            foreach (var link in doc.DocumentNode.Descendants("img")
                         .Select(i => i.Attributes["src"]))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Thread.Sleep(200);
                original_list.Add(link.Value);
                Console.WriteLine("Got " + link.Value);
            }

            Console.WriteLine("attempting to parse lines..");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            ParseSources(original_list,parsed_list);
        }

        public void ParseSources(List<string>? original_list, List<string>? parsed_list)
        {
            original_list = new List<string>();
            parsed_list = new List<string>();
            string sources_domain = "";
            string initial_source = "";
            Console.WriteLine("Original link   " + "   Parsed link");

            for (int i = 0; i < this.original_list.Count; i++)
            {
                if (!this.original_list[i].StartsWith("https://") && !this.original_list[i].StartsWith("http://"))
                {
                    initial_source = this.original_list[i];
                    sources_domain = this.original_list[i].Insert(0,"http://" + uri.Host);
                    Console.WriteLine(initial_source + " => " + sources_domain);
                    Thread.Sleep(200);
                }
                initial_source = this.original_list[i];
                sources_domain = this.original_list[i];
                Console.WriteLine(initial_source + " => " + sources_domain);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("would you like for the program to try and download all of the files?");
            Console.WriteLine("this doesnt actually work so its more liek i needed to push something to github lol");
            Console.WriteLine("Y/N");
            confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                download_media();
            }
            else return;
        }

        public void download_media()
        {
            original_list = new List<string>();
            using (WebClient wclient = new WebClient())
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                Console.WriteLine("this. does. not. work. yet");
                Thread.Sleep(2000);
                folder.ShowDialog();
                for (int i = 0; i < this.original_list.Count; i++)
                {
                    Console.WriteLine("this. does. not. work. yet");
                    //wclient.DownloadFile(this.original_list[i],folder.SelectedPath + "file_" + i + ".png");
                }
                
            }
        }
    }
}
