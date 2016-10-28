using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.ViewModels.Add;

namespace SunCorp.IRecover.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private ICommand mBackCommand;

        public ICommand BackCommand
        {
            get
            {
                mBackCommand = mBackCommand ?? new MvxCommand(BackAction);
                return mBackCommand;
            }
        }

        private void BackAction()
        {
            Close(this);
        }
    }
}