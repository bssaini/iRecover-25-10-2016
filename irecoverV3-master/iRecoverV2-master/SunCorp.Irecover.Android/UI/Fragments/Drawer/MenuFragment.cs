using System;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using SunCorp.Irecover.Android.Interfaces;
using SunCorp.IRecover.ViewModels.Drawer;

namespace SunCorp.Irecover.Android.UI.Fragments.Drawer
{
    [MvxFragment(typeof(HomeScreenViewModel), Resource.Id.navigation_frame)]
    [Register("suncorp.irecover.android.ui.fragments.drawer.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {

        #region private fields

        private const int NAVIGATION_DELAY = 300;

        private NavigationView navigationView;
        private IMenuItem previousMenuItem;

        #endregion

        #region public methods

        public MenuFragment()
        {
            
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            item.SetCheckable(true);
            item.SetChecked(true);
            if (previousMenuItem != null)
            {
                previousMenuItem.SetChecked(false);
            }
            previousMenuItem = item;

            Navigate(item.ItemId);

            return true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_navigation, null);

            navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            navigationView.SetNavigationItemSelectedListener(this);

            return view;
        }

        #endregion

        #region private methods

       

        private void Navigate(int itemId)
        {
            ((INavigationActivity)Activity).GetDrawerLayout().CloseDrawers();

            switch (itemId)
            {
                case Resource.Id.nav_claim:
                    new Handler(Looper.MainLooper).PostDelayed(() =>
                    {
                        ViewModel.GoToNewClaim();
                    }, NAVIGATION_DELAY);
                    break;

                case Resource.Id.nav_account:
                    new Handler(Looper.MainLooper).PostDelayed(() =>
                    {
                        ViewModel.GoToProfile();
                    }, NAVIGATION_DELAY);
                    break;

                case Resource.Id.nav_contact:
                    new Handler(Looper.MainLooper).PostDelayed(() =>
                    {
                        ViewModel.GoToContactUs();
                    }, NAVIGATION_DELAY);
                    break;
                case Resource.Id.nav_history:
                    new Handler(Looper.MainLooper).PostDelayed(() =>
                    {
                        ViewModel.GoToClaimHistory();
                    }, NAVIGATION_DELAY);
                    break;
                case Resource.Id.nav_settings:
                    new Handler(Looper.MainLooper).PostDelayed(() =>
                    {
                        ViewModel.GoToSettings();
                    }, NAVIGATION_DELAY);
                    break;
            }
        }
        
        #endregion
    }
}