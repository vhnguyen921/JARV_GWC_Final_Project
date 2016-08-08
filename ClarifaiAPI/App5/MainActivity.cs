using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;

namespace App5
{
    [Activity(Label = "App5", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        //Program p = new Program();


        protected override void OnCreate(Bundle bundle)
        {
            Console.WriteLine("NSFW Tagger, powered by Clarifai.com");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.WriteLine("Potentially NSFW URL: {Added for debug}");
            // string remoteUri = Console.ReadLine();
           // Console.WriteLine("hello");

            //Uri uri = new Uri("C:\\Users\\Girls Who Code\\Desktop\\lango\\tree.png");
            //var converted = uri.AbsoluteUri;
            //string stringUri = uri.ToString();
            // Console.WriteLine(stringUri);


            var x = (HttpWebRequest)WebRequest.Create(http://api.clarifai.com/v1/tag/?&url);
             x.method = "post";

             x.Headers.Add("Authorization", "Bearer {2V76B0VAYy3bVBMwbooE2QBrb6gf89}";
             x.data (encoded_data =@/ Users / USER / tree.png);
           

            //string test1 = "http://www.mortonarb.org/files/EVENT_Oak-Collection.jpg";
            string modelURL = "http://api.clarifai.com/v1/tag/?&url=" + stringUri + "&access_token=2V76B0VAYy3bVBMwbooE2QBrb6gf8";
            // make a webclient 
            WebClient myWebClient = new WebClient();
            // download & save in a databuffer)
            byte[] myDataBuffer = myWebClient.DownloadData(modelURL);
            // full data buffer -> string
            string download = Encoding.ASCII.GetString(myDataBuffer);
            // DEBUG 
            // Console.WriteLine(download);

            ClarNSFW(download);

        }
        public static void ClarNSFW(string download)
        {

            int iop = download.IndexOf("classes");
            int iof = download.IndexOf("docid_str");
            string strn = download.Substring(iop, iof - iop);

            //Enable for dbg 
            System.Diagnostics.Debug.Write("Parsed Text: ");
            System.Diagnostics.Debug.Write(" ");
            System.Diagnostics.Debug.Write(strn.Split(' ')[1]);
            //(strn.Split(" ")[1]);

            //Console.WriteLine(strn.Split(" ")[1]);

            // Console.WriteLine("Parsed Text: ");
            //Console.WriteLineDebug.Write(" ");
            //Console.WriteLine(strn);

            int iof2 = download.IndexOf("nsfw");
            int iop2 = download.IndexOf("sfw");
            // dbg 
            // Console.WriteLine("{0}, {1}", iof2, iop2);


            string[] numbers = Regex.Split(download, @"\D+");

            int beforeDeciOne = int.Parse(numbers[1]);
            int beforeDeciTwo = int.Parse(numbers[3]);
            double afterDeciOne = double.Parse(numbers[2]);
            double afterDeciTwo = double.Parse(numbers[4]);

            if (iof2 > iop2)
            {
                //Console.BackgroundColor = ConsoleColor.White;
                //Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Image has been deemed SAFE FOR WORK");
                Console.WriteLine("     probability: 0 . {0} %", afterDeciTwo);
            }
            else if (iop2 > iof2)
            {
                //Console.BackgroundColor = ConsoleColor.White;
                //Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Image has been deemed NOT SAFE FOR WORK");
                Console.WriteLine("     probability: 0 . {0} %", afterDeciOne);
            }
            //  else
            //   {
            // Console.WriteLine("Couldnt tell...");
            // }
        }
    }
}


