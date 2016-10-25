using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace SunCorp.IRecover.ViewModels.New
{
    public abstract class NewItemViewModel : BaseViewModel
    {
        private ICommand mSaveCommand;
        
        public ICommand SaveCommand
        {
            get
            {
                mSaveCommand = mSaveCommand ?? new MvxCommand(SaveAction);
                return mSaveCommand;
            }
        }
        
        public abstract void SaveAction();
    }
}