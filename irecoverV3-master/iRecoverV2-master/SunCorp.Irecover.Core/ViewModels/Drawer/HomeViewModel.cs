using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.ViewModels.Add;

namespace SunCorp.IRecover.ViewModels.Drawer
{
    public class HomeViewModel
        : MvxViewModel
    {
        #region private fields

        #endregion

        #region commands

        private ICommand mGoToAccidentCommand;

        public ICommand GoToAccidentCommand
        {
            get
            {
                mGoToAccidentCommand = mGoToAccidentCommand ?? new MvxCommand(GoToAccidentAction);
                return mGoToAccidentCommand;
            }
        }

        private ICommand mGotoClaimManagementCommand;

        public ICommand GotoClaimManagementCommand
        {
            get
            {
                mGotoClaimManagementCommand = mGotoClaimManagementCommand ?? new MvxCommand(GoToClaimManagementAction);
                return mGotoClaimManagementCommand;
            }
        }

        private ICommand mGotoRehabManagementCommand;

        public ICommand GotoRehabManagementCommand
        {
            get
            {
                mGotoRehabManagementCommand = mGotoRehabManagementCommand ?? new MvxCommand(GoToRehabManagementAction);
                return mGotoRehabManagementCommand;
            }
        }

        #endregion

        #region private methods

        private void GoToClaimManagementAction()
        {
            ShowViewModel<ClaimManagementViewModel>();
        }

        private void GoToAccidentAction()
        {
            ShowViewModel<AccidentViewModel>();
        }

        private void GoToRehabManagementAction()
        {
            ShowViewModel<RehabManagementViewModel>();
        }



        #endregion
    }
}