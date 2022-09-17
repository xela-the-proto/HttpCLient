using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace HttpCLient
{
    
    public class Downloader
    {
        protected bool err = true;
        static string save_path;

        public Downloader()
        {
            err = false;
            save_path = "";
        }

        public string SavePath
        {
            get => save_path;
        }

        [STAThread] public async Task PageDownloader(string urlToGet)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage resp = await httpClient.GetAsync(urlToGet);
            try
            {
                Console.WriteLine("Starting download...");

                //if we get apositive code we save
                if (resp.IsSuccessStatusCode)
                {

                    Console.WriteLine("Got it...");
                    //get page asynchronously
                    byte[] data = await resp.Content.ReadAsByteArrayAsync();

                    save_thread.SetApartmentState(ApartmentState.STA);
                    save_thread.Start();
                    save_thread.Join();

                    FileStream fStream = File.Create(save_path);
                    await fStream.WriteAsync(data, 0, data.Length);
                    fStream.Close();
                    if (save_thread.IsAlive)
                    {
                        save_thread.Interrupt();
                    }
                }
                else throw new HttpRequestException();

                Console.WriteLine("Done!");
            }
            catch (InvalidOperationException e)
            {
                Interaction.MsgBox("Error in url!", MsgBoxStyle.Critical, "Error!");
            }
            catch (HttpRequestException e)
            {
                Interaction.MsgBox("Error in website reply! Reply from website: " + "\n\n" + resp.ReasonPhrase,
                    MsgBoxStyle.Critical, "Error!");
            }
            catch (ArgumentException e)
            {
                Interaction.MsgBox("Error in file saving!", MsgBoxStyle.Critical, "Error!");
            }

        }

        private Thread save_thread = new Thread((ThreadStart)(() =>
        {
            try
            {
                Downloader down = new Downloader();
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Html file (*.html)|*.html";
                save.FileName = "index.html";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    save_path = save.FileName;
                }
                else throw new InvalidDataException();
            }
            catch (InvalidDataException e)
            {
                Interaction.MsgBox("Path selection has been cancelled!", MsgBoxStyle.Exclamation, "Attention!");
            }

        }));
    }
}

