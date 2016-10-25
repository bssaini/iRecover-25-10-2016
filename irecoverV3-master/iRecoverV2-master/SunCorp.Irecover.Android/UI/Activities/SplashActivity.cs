using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Views;

namespace SunCorp.Irecover.Android.UI.Activities
{
    [Activity(Label = "@string/app_name", MainLauncher = true,
         Theme = "@style/Theme.Splash",
         ScreenOrientation = ScreenOrientation.Portrait,
         Icon = "@mipmap/ic_launcher",
         NoHistory = true)]
    public class SplashActivity : MvxSplashScreenActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(Resource.Style.Theme_Splash);
            base.OnCreate(bundle);
        }

        

        protected override void RequestWindowFeatures()
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
        }

        #region private methods

     

        #endregion

    }
}