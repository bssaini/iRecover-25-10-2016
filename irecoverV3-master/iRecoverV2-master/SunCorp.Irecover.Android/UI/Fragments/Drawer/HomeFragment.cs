using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using SunCorp.IRecover.ViewModels.Drawer;

namespace SunCorp.Irecover.Android.UI.Fragments.Drawer

{
	[MvxFragment(typeof (HomeScreenViewModel), Resource.Id.content_frame)]
	[Register("suncorp.irecover.android.ui.fragments.drawer.HomeFragment")]
	public class HomeFragment : BaseFragment<HomeViewModel>
	{
		protected override int FragmentId
		{
			get { return Resource.Layout.activity_main; }
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var rootView = base.OnCreateView(inflater, container, savedInstanceState);
            SetTitle(GetString(Resource.String.app_name), rootView);
            
			return rootView;
		}

		private void SetTitle(string title, View rootView)
		{
			var titleView = rootView.FindViewById<TextView>(Resource.Id.toolbar_title);
            titleView.SetAllCaps(false);
			titleView.Text = title;
		}
	}
}