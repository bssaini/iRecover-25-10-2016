using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.Add;
using Microsoft.WindowsAzure.MobileServices;
using SunCorp.IRecover.Data;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace SunCorp.IRecover.ViewModels.Rehab
{
    public class MyHelpersViewModel : AddListViewModel
    {
        private ICommand mRowClickedCommand;

        private MobileServiceClient client;
        private IMobileServiceTable<HelperInfoData> infoTable;
        private bool _isBusy;

        private List<HelperInfoData> datas;

        const string applicationURL = "http://suncorpandroid.azurewebsites.net";

        public Boolean IsBusy {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        public ICommand RowClickedCommand
        {
            get
            {
                mRowClickedCommand = mRowClickedCommand ?? new MvxCommand<string>(RowClickedAction);
                return mRowClickedCommand;
            }
        }

        public MyHelpersViewModel()
        {
            Items = new ObservableCollection<String>();

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

                infoTable = client.GetTable<HelperInfoData>();

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

                foreach (HelperInfoData data in listItems)
                {
                    Items.Add(data.name);
                }

                datas = listItems;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void RowClickedAction(string obj)
        {
            foreach(HelperInfoData data in datas)
            {
                if(obj.Equals(data.name, StringComparison.Ordinal))
                {
                    ShowViewModel<ViewHelperViewModel>(data);
                    break;
                }
                
            }
        }

        public override void AddListAction()
        {
            ShowViewModel<ViewHelperViewModel>();
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
