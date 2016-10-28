using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.ViewModels.Rehab;

namespace SunCorp.IRecover.ViewModels
{
    public class RehabManagementViewModel : BaseViewModel
    {
        private ICommand mMyHelpersCommand;
        private ICommand mMyProgramsCommand;
        private ICommand mContactRehabAdvisorCommand;
        private Action mContactRehabAdvisorAction;

        public ICommand MyHelpersCommand
        {
            get
            {
                mMyHelpersCommand = mMyHelpersCommand ?? new MvxCommand(MyHelpersAction);
                return mMyHelpersCommand;
            }
        }

        public ICommand MyProgramsCommand
        {
            get
            {
                mMyProgramsCommand = mMyProgramsCommand ?? new MvxCommand(MyProgramsAction);
                return mMyProgramsCommand;
            }
        }

        public ICommand ContactRehabAdvisorCommand
        {
            get
            {
                mContactRehabAdvisorCommand = mContactRehabAdvisorCommand ?? new MvxCommand(SpeakToARepresentativeAction);
                return mContactRehabAdvisorCommand;
            }
        }



        public void SetOnContactRehabAdvisorAction(Action contactRehabAdvisorAction)
        {
            mContactRehabAdvisorAction = contactRehabAdvisorAction;
        }

        #region private methods
        private void MyHelpersAction()
        {
            ShowViewModel<MyHelpersViewModel>();
        }

        private void MyProgramsAction()
        {
            ShowViewModel<MyProgramsViewModel>();
        }

        private void SpeakToARepresentativeAction()
        {
            if (mContactRehabAdvisorAction == null)
            {
                throw new Exception($"The activity must implement{mContactRehabAdvisorAction}");

            }
            mContactRehabAdvisorAction();

        }
        #endregion
    }
}