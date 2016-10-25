using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using SunCorp.Irecover.Android.Interfaces;
using SunCorp.IRecover.ViewModels.Drawer;

namespace SunCorp.Irecover.Android.UI.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppThemeNoActionBar", ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop, Name = "suncorp.irecover.android.ui.activities.HomeScreenActivity")]
    public class HomeScreenActivity : MvxCachingFragmentCompatActivity<HomeScreenViewModel>, INavigationActivity
    {
        public DrawerLayout DrawerLayout { get; protected set; }

        public CoordinatorLayout CoordinatorLayout { get; set; }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            SetContentView(Resource.Layout.activity_home_screen);
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            CoordinatorLayout = FindViewById<CoordinatorLayout>(Resource.Id.coordinatorLayout);

            if (bundle == null)
            {
                ViewModel.ShowMenu();
            }
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                DrawerLayout.CloseDrawers();
            }
            else
            {
                base.OnBackPressed();
            }
        }
        
        public DrawerLayout GetDrawerLayout()
        {
            return DrawerLayout;
        }

        public CoordinatorLayout GetCoordinateLayout()
        {
            return CoordinatorLayout;
        }
    }
}