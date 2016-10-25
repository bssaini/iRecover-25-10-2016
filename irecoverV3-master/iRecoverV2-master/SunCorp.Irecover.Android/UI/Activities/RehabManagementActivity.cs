using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using SunCorp.IRecover.ViewModels;
using SunCorp.IRecover.ViewModels.Drawer;

namespace SunCorp.Irecover.Android.UI.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppThemeNoActionBar", ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop, Name = "suncorp.irecover.android.ui.activities.RehabManagementActivity")]
    public class RehabManagementActivity : MvxAppCompatActivity
    {

        public new RehabManagementViewModel ViewModel
        {
            get { return (RehabManagementViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            SetContentView(Resource.Layout.activity_rehab_management);
            var toolbarTitle = FindViewById<TextView>(Resource.Id.toolbar_title);
            toolbarTitle.Text="Rehab management";

            ViewModel.SetOnContactRehabAdvisorAction(ContactRehabAdvisorAction);
        }

        private void ContactRehabAdvisorAction()
        {
            var intent = new Intent(Intent.ActionDial);
            intent.SetData(Uri.Parse("tel:0123456789"));
            StartActivity(intent);
        }
    }
}