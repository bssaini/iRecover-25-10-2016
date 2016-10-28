using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;

namespace SunCorp.Irecover.Android.UI.Dialogs
{
    public class MediaChooserDialog : MvxDialogFragment
    {
        private readonly Action<int> mOnItemSelectedAction;

        public string[] ActionNames { get; set; }

        public MediaChooserDialog(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {

        }

        public MediaChooserDialog(string[] actionsDescription, Action<int> onItemSelectedAction)
        {
            mOnItemSelectedAction = onItemSelectedAction;
            ActionNames = actionsDescription;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_video_picture_chooser, container);

            Dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);

            Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.White));

            var actionsAdapter = new CustomArrayAdapter(Activity, global::Android.Resource.Layout.SimpleListItem1, ActionNames);
            var actions = view.FindViewById<ListView>(Resource.Id.lv_dialog_video_picture_actions);

            actions.Adapter = actionsAdapter;
            actions.ItemClick += actions_ItemClick;

            return view;
        }

        void actions_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            mOnItemSelectedAction(e.Position);
        }
    }

    public class CustomArrayAdapter : ArrayAdapter<string>
    {
        public CustomArrayAdapter(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public CustomArrayAdapter(Context context, int textViewResourceId) : base(context, textViewResourceId)
        {
        }

        public CustomArrayAdapter(Context context, int resource, int textViewResourceId) : base(context, resource, textViewResourceId)
        {
        }

        public CustomArrayAdapter(Context context, int textViewResourceId, string[] objects) : base(context, textViewResourceId, objects)
        {
        }

        public CustomArrayAdapter(Context context, int resource, int textViewResourceId, string[] objects) : base(context, resource, textViewResourceId, objects)
        {
        }

        public CustomArrayAdapter(Context context, int textViewResourceId, IList<string> objects) : base(context, textViewResourceId, objects)
        {
        }

        public CustomArrayAdapter(Context context, int resource, int textViewResourceId, IList<string> objects) : base(context, resource, textViewResourceId, objects)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);
            var text = (TextView)view.FindViewById(global::Android.Resource.Id.Text1);
            if (text == null)
            {
                return view;
            }
            text.SetTextColor(Color.Black);
            return view;
        }
    }
}