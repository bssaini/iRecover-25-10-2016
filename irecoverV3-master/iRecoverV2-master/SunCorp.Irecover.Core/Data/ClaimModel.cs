using System;
using MvvmCross.Core.ViewModels;

namespace SunCorp.IRecover.Data
{
    public class ClaimModel :MvxViewModel
    {
        private string mName;
        private bool mIsResolved;
        public DateTime Date;

        public string Name
        {
            get { return mName; }
            set
            {
                mName = value;
                RaisePropertyChanged(() => Name);
            }
        }
        public bool IsResolved
        {
            get { return mIsResolved; }
            set
            {
                mIsResolved = value;
                RaisePropertyChanged(() => IsResolved);
            }
        }

        public string DateFormatted => $"{Date.Day}/{Date.Month}/{Date.Year}";
    }
}
