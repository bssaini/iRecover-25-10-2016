using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.Data;
using SunCorp.IRecover.ViewModels.Add;

namespace SunCorp.IRecover.ViewModels
{
    public class ClaimHistoryViewModel : BaseViewModel
    {
        private ICommand mAddCommand;
        private ICommand mRowClickedCommand;
        private ObservableCollection<ClaimModel> mItems;

        public ClaimHistoryViewModel()
        {
            Items = new ObservableCollection<ClaimModel>();

            var random = new Random();
            for (var i = 5; i >=1; i--)
            {
                Items.Add(new ClaimModel()
                {
                    Name = $"Claim {i}",
                    IsResolved = random.Next(20000)%2 == 1, 
                    Date = DateTime.Now.AddDays(-5+i)
                });
            }
        }
        public ObservableCollection<ClaimModel> Items
        {
            get
            {
                return mItems;
            }
            set
            {
                mItems = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public ICommand AddCommand
        {
            get
            {
                mAddCommand = mAddCommand ?? new MvxCommand(AddListAction);
                return mAddCommand;
            }
        }


        public ICommand RowClickedCommand
        {
            get
            {
                mRowClickedCommand = mRowClickedCommand ?? new MvxCommand<ClaimModel>(RowClickedAction);
                return mRowClickedCommand;
            }
        }

        private void RowClickedAction(ClaimModel obj)
        {
            //todo:for now
            AddListAction();
        }

        public void AddListAction()
        {
            ShowViewModel<NewClaimViewModel>();
        }
    }
}