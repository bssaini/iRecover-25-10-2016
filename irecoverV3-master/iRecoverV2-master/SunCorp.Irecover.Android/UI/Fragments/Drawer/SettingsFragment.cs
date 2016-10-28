using Android.Runtime;
using MvvmCross.Droid.Shared.Attributes;
using SunCorp.IRecover.ViewModels.Drawer;

namespace SunCorp.Irecover.Android.UI.Fragments.Drawer

{
    [MvxFragment(typeof(HomeScreenViewModel), Resource.Id.content_frame)]
    [Register("suncorp.irecover.android.ui.fragments.drawer.SettingsFragment")]
    public class SettingsFragment : BaseFragment<SettingsViewModel>
    {
        protected override int FragmentId
        {
            get
            {
					return Resource.Layout.activity_home_screen;
            }
        } 
    }
}