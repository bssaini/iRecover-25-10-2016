using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace SunCorp.IRecover.ViewModels.Add
{
    public abstract class AddListViewModel : BaseViewModel
    {
        private ICommand mAddCommand;
        private ICommand mRowClickedCommand;
        private ObservableCollection<string> mItems;

        public ObservableCollection<string> Items
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
                mRowClickedCommand = mRowClickedCommand ?? new MvxCommand(RowClickedAction);
                return mRowClickedCommand;
            }
        }

        private void RowClickedAction()
        {
            //todo:for now
            AddListAction();
        }

        public abstract void AddListAction();
    }
}