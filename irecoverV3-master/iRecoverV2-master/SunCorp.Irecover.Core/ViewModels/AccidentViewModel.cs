using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.ViewModels.Add;

namespace SunCorp.IRecover.ViewModels
{
    public class AccidentViewModel : BaseViewModel
    {
        #region private members

        private ICommand mGotoAddWitnessCommand;
        private ICommand mGotoAddOherDriversCommand;
        private ICommand mGotoAddPicturesCommand;
        private ICommand mGotoAddVoiceMemosCommand;
        private ICommand mGotoAddContactCommand;

        #endregion

        public ICommand GotoAddWitnessCommand
        {
            get
            {
                mGotoAddWitnessCommand = mGotoAddWitnessCommand ?? new MvxCommand(GoToAddWitnessAction);
                return mGotoAddWitnessCommand;
            }
        }

        public ICommand GotoAddOherDriversCommand
        {
            get
            {
                mGotoAddOherDriversCommand = mGotoAddOherDriversCommand ?? new MvxCommand(GoToAddOtherDriversAction);
                return mGotoAddOherDriversCommand;
            }
        }

        public ICommand GotoAddPicturesCommand
        {
            get
            {
                mGotoAddPicturesCommand = mGotoAddPicturesCommand ?? new MvxCommand(GoToAddPicturesAction);
                return mGotoAddPicturesCommand;
            }
        }

        public ICommand GotoAddVoiceMemosCommand
        {
            get
            {
                mGotoAddVoiceMemosCommand = mGotoAddVoiceMemosCommand ?? new MvxCommand(GoToAddVoiceMemosAction);
                return mGotoAddVoiceMemosCommand;
            }
        }

        public ICommand GotoAddContactCommand
        {
            get
            {
                mGotoAddContactCommand = mGotoAddContactCommand ?? new MvxCommand(GoToAddContactAction);
                return mGotoAddContactCommand;
            }
        }

        #region private methods
        private void GoToAddWitnessAction()
        {
            ShowViewModel<AddWitnessViewModel>();
        }

        private void GoToAddOtherDriversAction()
        {
            ShowViewModel<AddOtherDriversViewModel>();
        }

        private void GoToAddPicturesAction()
        {
            ShowViewModel<AddPicturesViewModel>();
        }

        private void GoToAddVoiceMemosAction()
        {
            ShowViewModel<AddVoiceMemosViewModel>();
        }
        private void GoToAddContactAction()
        {
            ShowViewModel<AddContactsViewModel>();
        }

        #endregion
    }
}