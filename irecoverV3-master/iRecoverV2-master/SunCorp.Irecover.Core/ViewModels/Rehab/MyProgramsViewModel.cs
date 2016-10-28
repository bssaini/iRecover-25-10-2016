using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.Add;
using System.Windows.Input;
using Microsoft.WindowsAzure.MobileServices;
using SunCorp.IRecover.Data;
using System.Collections.Generic;
using System;
using MvvmCross.Core.ViewModels;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SunCorp.IRecover.ViewModels.Rehab
{
    public class MyProgramsViewModel : AddListViewModel
    {
        private ICommand mRowClickedCommand;

        private MobileServiceClient client;
        private IMobileServiceTable<ProgramInfoData> infoTable;
        private bool _isBusy;

        private List<ProgramInfoData> datas;

        const string applicationURL = "http://suncorpandroid.azurewebsites.net";

        public Boolean IsBusy
        {
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

        public MyProgramsViewModel()
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

                infoTable = client.GetTable<ProgramInfoData>();

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

                foreach (ProgramInfoData data in listItems)
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
            foreach (ProgramInfoData data in datas)
            {
                if (obj.Equals(data.name, StringComparison.Ordinal))
                {
                    ShowViewModel<ViewProgramViewModel>(data);
                    break;
                }

            }
        }

        public override void AddListAction()
        {
            ShowViewModel<ViewProgramViewModel>();
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
