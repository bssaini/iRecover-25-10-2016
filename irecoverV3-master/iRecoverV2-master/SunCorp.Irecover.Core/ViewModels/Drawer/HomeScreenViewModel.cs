using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace SunCorp.IRecover.ViewModels.Drawer
{
    public class HomeScreenViewModel : MvxViewModel
    {

        public HomeScreenViewModel() : base()
        {
        }

        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
            ShowViewModel<HomeViewModel>();
        }
    }
}