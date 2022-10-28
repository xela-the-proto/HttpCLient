using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Security.Principal;
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
                string confirmation = "N";
                string toGet = "https://www.google.com/";
                Console.ForegroundColor = ConsoleColor.Green;
                if (!IsAdministrator())
                {
                    throw new AccessViolationException();
                }

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
                    Console.WriteLine("html file downloaded to " + download.SavePath);
                    Thread.Sleep(TimeSpan.FromSeconds(2));

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Would you like for the program to try and retrieve media from the site?\n");
                    Console.WriteLine("(EXPERIMENTAL I TAKE NO RESPONSABILITY)");
                    Console.WriteLine("Y/N");
                    confirmation = Console.ReadLine();
                    Media_retrieve retrieve = new Media_retrieve();
                    if (confirmation.ToUpper() == "Y")
                    {
                        retrieve.GetImages();
                    }
                    else if (confirmation.ToUpper() == "N")
                    {
                        Console.WriteLine("Quitting in 5 seconds");
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                    }
                    else throw new ArgumentException();

                }

                Console.WriteLine("quitting in 5 seconds");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.ForegroundColor = ConsoleColor.White;

            }
            catch (InvalidOperationException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error in url!");
                Interaction.MsgBox("Error in url!", MsgBoxStyle.Critical, "Error!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (ArgumentException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error in string recognition aborting!");
                Interaction.MsgBox("Error in string recognition aborting!", MsgBoxStyle.Critical, "Error!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (AccessViolationException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error cannot run application without admin!");
                Interaction.MsgBox("Error cannot run application without admin!", MsgBoxStyle.Critical, "Error!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            

            
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}

