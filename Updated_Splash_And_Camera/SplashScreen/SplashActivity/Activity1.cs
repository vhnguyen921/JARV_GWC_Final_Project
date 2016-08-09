using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Provider;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
          //CHEAT APP!!!!!!!!///**********************************************
namespace com.xamarin.sample.splashscreen
{
    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }

    [Activity(Label = "@string/ApplicationName")]
    public class Activity1 : AppCompatActivity
    {
        private ImageView imageView;
        static readonly string TAG = "X:" + typeof (Activity1).Name;


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = Resources.DisplayMetrics.WidthPixels;// _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
        }


            //if (App.bitmap != null)
            //{
            //    imageView.SetImageBitmap(App.bitmap);
            //Resources.DisplayMetrics.SetImageBitmap(App.bitmap);
            //    App.bitmap = null;
            //}

            // Dispose of the Java side bitmap.
            //GC.Collect();

            //CLARIFAI CODE HERE
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("NSFW Tagger, powered by Clarifai.com");
                Console.WriteLine(" ");
                Console.WriteLine(" ");

                Console.WriteLine("Potentially NSFW URL: {Added for debug}");
                // string remoteUri = Console.ReadLine();
                // string vikingDude = "https://i.imgur.com/lnzsgv9.jpg";
                //string nudeWoman = "https://i.imgur.com/aJXq544.jpg";

                string test1 = "http://www.mortonarb.org/files/EVENT_Oak-Collection.jpg";
                string modelURL = "http://api.clarifai.com/v1/tag/?&url=" + test1 + "&access_token=2V76B0VAYy3bVBMwbooE2QBrb6gf89";
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
                string englishTag = download.Substring(iop, iof - iop); //Our tag for the photo.

                //Enable for dbg 
                Console.WriteLine("Parsed Text: ");
                Console.WriteLine(" ");
                //Console.WriteLine(englishTag);
                Console.WriteLine(englishTag.Split(' ')[1]);


                int iof2 = download.IndexOf("nsfw");
                int iop2 = download.IndexOf("sfw");
                // dbg 
                // Console.WriteLine("{0}, {1}", iof2, iop2);


                string[] numbers = Regex.Split(download, @"\D+");

                int beforeDeciOne = int.Parse(numbers[1]);
                int beforeDeciTwo = int.Parse(numbers[3]);
                double afterDeciOne = double.Parse(numbers[2]);
                double afterDeciTwo = double.Parse(numbers[4]);

                string SpanishTag = "el Arbol"; //Translate to Spanish.

                if (iof2 > iop2)
                {
                    // Console.BackgroundColor = ConsoleColor.White;
                    //Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Image has been deemed SAFE FOR WORK");
                    Console.WriteLine("     probability: 0 . {0} %", afterDeciTwo);
                }
                else if (iop2 > iof2)
                {
                    //Console.BackgroundColor = ConsoleColor.White;
                    // Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Image has been deemed NOT SAFE FOR WORK");
                    Console.WriteLine("     probability: 0 . {0} %", afterDeciOne);
                }
                else
                {
                    Console.WriteLine("Couldnt tell...");
                }
            }
        }
    }


}
//End of Clarifai code.
protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button button = FindViewById<Button>(Resource.Id.MyButton);
                //imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                button.Click += TakeAPicture;
            }

        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));

            StartActivityForResult(intent, 0);
        }
    