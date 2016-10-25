using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using SunCorp.Irecover.Android.Interfaces;

namespace SunCorp.Irecover.Android.UI.Fragments.Drawer
{
    public abstract class BaseFragment : MvxFragment
    {
        private Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;

        public MvxCachingFragmentCompatActivity ParentActivity
        {
            get
            {
                return ((MvxCachingFragmentCompatActivity)this.Activity);
            }
        }

        protected abstract int FragmentId { get; }

        protected BaseFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentId, null);

            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            if (_toolbar != null)
            {
                ParentActivity.SetSupportActionBar(_toolbar);
                ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                var drawerLayout = (ParentActivity as INavigationActivity).GetDrawerLayout();

                _drawerToggle = new MvxActionBarDrawerToggle(
                    Activity,                               // host Activity
                    drawerLayout,  // DrawerLayout object
                    _toolbar,                               // nav drawer icon to replace 'Up' caret
                    Resource.String.drawer_open,            // "open drawer" description
                    Resource.String.drawer_close            // "close drawer" description
                );
                //_drawerToggle.DrawerOpened += (object sender, ActionBarDrawerEventArgs e) => ((INavigationActivity)Activity).HideSoftKeyboard();
                drawerLayout.AddDrawerListener(_drawerToggle);
            }

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
        }

       
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (_toolbar != null)
            {
                _drawerToggle.OnConfigurationChanged(newConfig);
            }
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            if (_toolbar != null)
            {
                _drawerToggle.SyncState();
            }
        }

       
        public Activity GetCurrentActivity()
        {
            return this.Activity;
        }

        
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}