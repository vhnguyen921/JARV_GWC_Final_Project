using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CurlSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Third
{
    [Activity(Label = "Third", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    //class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NSFW Tagger, powered by Clarifai.com");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.WriteLine("Potentially NSFW URL: {Added for debug}");



            var bits = IntPtr.Size * 8;
            Console.WriteLine("Test curl {0} bit", bits);
            Curl.GlobalInit(CurlInitFlag.All);
            Console.WriteLine("Curl Version: {0}\n", Curl.Version);

            const string postData = "encoded_data =@/ Users / USER / tree.png";
            var postLength = postData.Length;

            Console.WriteLine("\n========== TEST 1 HttpWebRequest ============");

            var request = (HttpWebRequest)WebRequest.Create("http://api.clarifai.com/v1/tag/?url=");
            var data = Encoding.ASCII.GetBytes(postData);
            request.UserAgent = "HttpWebRequest";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Headers.Add("Authorization", "Bearer {2V76B0VAYy3bVBMwbooE2QBrb6gf89}");
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Console.WriteLine(responseString);

            // string remoteUri = Console.ReadLine();
            string test1 = "http://www.mortonarb.org/files/EVENT_Oak-Collection.jpg";

            string modelURL = "http://api.clarifai.com/v1/tag/?url=" + test1 + "&access_token=2V76B0VAYy3bVBMwbooE2QBrb6gf89";
            // make a webclient 
            WebClient myWebClient = new WebClient();
            // download & save in a databuffer)
            byte[] myDataBuffer = myWebClient.DownloadData(modelURL);
            // full data buffer -> string
            string download = Encoding.ASCII.GetString(myDataBuffer);
            // DEBUG 
            // Console.WriteLine(download);

            ClarNSFW(responseString);

        }
        public static void ClarNSFW(string download)
        {

            int iop = download.IndexOf("classes");
            int iof = download.IndexOf("docid_str");
            string strn = download.Substring(iop, iof - iop);

            //Enable for dbg 
            Console.WriteLine("Parsed Text: ");
            Console.WriteLine(" ");
            //Console.WriteLine(strn);
            Console.WriteLine(strn.Split(' ')[1]);


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
                // Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Image has been deemed SAFE FOR WORK");
                Console.WriteLine("     probability: 0 . {0} %", afterDeciTwo);
            }
            else if (iop2 > iof2)
            {
                // Console.BackgroundColor = ConsoleColor.White;
                //Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Image has been deemed NOT SAFE FOR WORK");
                Console.WriteLine("     probability: 0 . {0} %", afterDeciOne);
            }
            else
            {
                Console.WriteLine("Couldnt tell...");
            }

            int count = 1;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}


