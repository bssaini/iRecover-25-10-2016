using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.Data;
using SunCorp.IRecover.ViewModels.Add;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace SunCorp.IRecover.ViewModels
{
    public class ClaimHistoryViewModel : BaseViewModel
    {
        private ICommand mAddCommand;
        private ICommand mRowClickedCommand;
        private ObservableCollection<ClaimModel> mItems;
        private bool _isBusy;

        private MobileServiceClient client;
        private IMobileServiceTable<NewClaimInfoData> infoTable;
        private List<NewClaimInfoData> datas;

        const string applicationURL = "http://suncorpandroid.azurewebsites.net";

        public Boolean IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        public ClaimHistoryViewModel()
        {
            Items = new ObservableCollection<ClaimModel>();

            var progressHandler = new ProgressHandler();
            progressHandler.BusyStateChange += (busy) =>
            {
                IsBusy = busy;

            };

            try
            {
                client = new MobileServiceClient(
                    applicationURL,
                    progressHandler);

                infoTable = client.GetTable<NewClaimInfoData>();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            displayInfo();

        }

        private async void displayInfo()
        {
            try
            {
                var listItems = await infoTable.ToListAsync();

                var random = new Random();
                foreach (NewClaimInfoData data in listItems)
                {
                    Items.Add(new ClaimModel()
                    {
                        Name = data.name,
                        IsResolved = random.Next(20000) % 2 == 1,
                        Date = DateTime.Now
                    });
                }

                datas = listItems;

            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
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

            foreach(NewClaimInfoData data in datas)
            {
                if (obj.Name.Equals(data.name, StringComparison.Ordinal))
                {
                    ShowViewModel<NewClaimViewModel>(data);
                    break;
                }
            }

        }

        public void AddListAction()
        {
            ShowViewModel<NewClaimViewModel>();
        }

        class ProgressHandler : DelegatingHandler
        {
            int busyCount = 0;

            public event Action<bool> BusyStateChange;

            #region implemented abstract members of HttpMessageHandler

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                //assumes always executes on UI thread
                if (busyCount++ == 0 && BusyStateChange != null)
                    BusyStateChange(true);

                var response = await base.SendAsync(request, cancellationToken);

                // assumes always executes on UI thread
                if (--busyCount == 0 && BusyStateChange != null)
                {
                    BusyStateChange(false);

                }
                return response;
            }

            #endregion

        }
    }
}