using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace HttpCLient
{
    public class Downloader
    {
        protected static string location = "index.html";
        protected bool err = true;
        public Downloader()
        {
        }
        
        
        public async Task PageDownloader(string urlToGet)
        {
            try
            {
                Console.WriteLine("Starting download...");
                using (HttpClient httpClient = new HttpClient())
                {
                    //get page asynchronously
                    HttpResponseMessage resp = await httpClient.GetAsync(urlToGet);

                    //if we get apositive code we save
                    if (resp.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Got it...");
                        byte[] data = await resp.Content.ReadAsByteArrayAsync();

                        // Save it to a file
                        FileStream fStream = File.Create(location);
                        await fStream.WriteAsync(data, 0, data.Length);
                        fStream.Close();

                    }
                    Console.WriteLine("Done!");
                }
            }
            catch (InvalidOperationException e)
            {
                Interaction.MsgBox("Error while resolving url!", MsgBoxStyle.Critical, "Error!");
            }

        }
    }
}

