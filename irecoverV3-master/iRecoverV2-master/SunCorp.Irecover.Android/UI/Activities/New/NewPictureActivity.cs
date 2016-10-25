using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using SunCorp.Irecover.Android.UI.Dialogs;
using SunCorp.IRecover.ViewModels.Add;
using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.Irecover.Android.UI.Activities.New
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppThemeNoActionBar", ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop, Name = "suncorp.irecover.android.ui.activities.NewPictureActivity")]
    public class NewPictureActivity : MvxAppCompatActivity
    {

        private readonly string[] mActionsDescriptionPicture =
        {
            "Take Photo",
            "Choose from Library",
        };
        private MediaChooserDialog mPictureChooserDialog;
        private const int PICTURE_SIZE = 500;


        public new NewPictureViewModel ViewModel
        {
            get { return (NewPictureViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            SetContentView(Resource.Layout.activity_new_picture);

            var toolbarTitle = FindViewById<TextView>(Resource.Id.toolbar_title);
            toolbarTitle.Text = "Add Picture";

            ViewModel.SetOnUploadImageAction(OnUploadImageAction);

            ViewModel.PictureSize = (int) ConvertDpToPixel(PICTURE_SIZE, BaseContext);
        }


        private void OnUploadImageAction()
        {
            var options = mActionsDescriptionPicture;

            if (mPictureChooserDialog == null)
            {
                mPictureChooserDialog = new MediaChooserDialog(options, OnPicturePositionSelected);
            }
            else
            {
                mPictureChooserDialog.ActionNames = options;
            }

             mPictureChooserDialog.Show(SupportFragmentManager, "choose_picture");
        }

        private void OnPicturePositionSelected(int position)
        {
            switch (position)
            {
                case 0:
                    ViewModel.TakePicture();
                    break;
                case 1:
                    ViewModel.ChoosePicture();
                    break;
                default:
                    ViewModel.ChoosePicture();
                    break;
            }
            mPictureChooserDialog.Dismiss();
        }

        private static int ConvertDpToPixel(int dp, Context context)
        {
            return (int)ConvertDpToPixel((float)dp, context);
        }
        private static float ConvertDpToPixel(float dp, Context context)
        {
            var resources = context.Resources;
            var metrics = resources.DisplayMetrics;
            var px = dp * (metrics.Density);
            return px;
        }
    }
}