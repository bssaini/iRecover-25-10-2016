using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using SunCorp.IRecover.ViewModels.Add;
using SunCorp.IRecover.ViewModels.Rehab;

namespace SunCorp.Irecover.Android.UI.Activities.Rehab
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppThemeNoActionBar", ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop, Name = "suncorp.irecover.android.ui.activities.MyProgramsActivity")]
    public class MyProgramsActivity : MvxAppCompatActivity
    {

        public new MyProgramsViewModel ViewModel
        {
            get { return (MyProgramsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            SetContentView(Resource.Layout.activity_list);

            var toolbarTitle = FindViewById<TextView>(Resource.Id.toolbar_title);
            toolbarTitle.Text = "My Programs";
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.activity_list);
        }
    }
}