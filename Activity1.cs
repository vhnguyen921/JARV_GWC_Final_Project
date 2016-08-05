using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;

namespace $safeprojectname$
{
    [Activity(Label = "@string/ApplicationName")]
    public class Activity1 : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof (Activity1).Name;
        Button _button;
        int _clickCount;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //_button = FindViewById<Button>(Resource.Id.MyButton);

            _button.Click += (sender, args) =>
                             {
                                 string message = string.Format("You clicked {0} times.", ++_clickCount);
                                 _button.Text = message;
                                 Log.Debug(TAG, message);
                             };

            Log.Debug(TAG, "Activity1 is loaded.");
        }
    }
}