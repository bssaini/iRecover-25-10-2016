using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.ViewModels;
using SunCorp.IRecover.ViewModels.Drawer;

namespace SunCorp.IRecover
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {

        public AppStart()
        {
        }

        public void Start(object hint = null)
        {
            NavigateToProperScreen();
        }

        public void NavigateToProperScreen()
        {
            ShowViewModel<HomeScreenViewModel>();
            //ShowViewModel<AccidentViewModel>();
        }

        #region private methods
        
        #endregion

    }
}
