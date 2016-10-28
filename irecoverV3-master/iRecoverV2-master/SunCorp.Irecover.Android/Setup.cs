using System;
using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using SunCorp.IRecover;

namespace SunCorp.Irecover.Android
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                var list = new List<Assembly>(base.AndroidViewAssemblies)
                {
                    typeof (NavigationView).Assembly,
                    typeof (Toolbar).Assembly,
                    typeof (DrawerLayout).Assembly,
                    typeof(MvxRecyclerView).Assembly
                };
                return list;
            }
        }
        
        protected override IMvxApplication CreateApp()
        {
            return new CoreApp();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            InitDependencyInjection();
            
        }
        
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        #region private methods

        private void InitDependencyInjection()
        {
           
        }

     
        #endregion
    }
}