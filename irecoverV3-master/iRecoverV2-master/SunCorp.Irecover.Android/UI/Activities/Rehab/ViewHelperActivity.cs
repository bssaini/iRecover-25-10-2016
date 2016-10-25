using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using SunCorp.IRecover.ViewModels;
using SunCorp.IRecover.ViewModels.Rehab;

namespace SunCorp.Irecover.Android.UI.Activities.Rehab
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppThemeNoActionBar", ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop, Name = "suncorp.irecover.android.ui.activities.ViewHelperActivity")]
    public class ViewHelperActivity : MvxAppCompatActivity
    {

        public new ViewHelperViewModel ViewModel
        {
            get { return (ViewHelperViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            SetContentView(Resource.Layout.activity_view_helper);

            var toolbarTitle = FindViewById<TextView>(Resource.Id.toolbar_title);
            toolbarTitle.Text = "Helper";
        }
    }
}