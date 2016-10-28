using System.Windows.Input;
using SunCorp.IRecover.ViewModels.New;
using SunCorp.IRecover.Data;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Core.ViewModels;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SunCorp.IRecover.ViewModels
{
    public class NewClaimViewModel : NewItemViewModel
    {
        #region private members

        private ICommand mSaveCommand;

        private MobileServiceClient client;
        private IMobileServiceTable<NewClaimInfoData> infoTable;

        const string applicationURL = "http://suncorpandroid.azurewebsites.net";
        #endregion

        public NewClaimViewModel()
        {
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
        }

        public void Init(NewClaimInfoData data)
        {
            if(data != null)
            {
                Name = data.name;
                AccidentType = data.accidentType;
                AccidentTime = data.accidentTime;
            }
        }

        public ICommand SaveCommand
        {
            get { mSaveCommand = mSaveCommand ?? new MvxCommand(SaveInfoToServer);
                return mSaveCommand;

            }
        }

        public string Name
        {
            get;set;
        }

        public string AccidentType
        { get; set; }

        public string AccidentTime
        { get; set; }

        public Boolean IsBusy
        { get; set; }

        public async void SaveInfoToServer()
        {
            if (client == null || string.IsNullOrEmpty(Name))
            {
                return;
            }

            var item = new NewClaimInfoData
            {
               name = Name,
               accidentType = AccidentType,
               accidentTime = AccidentTime
            };

            try
            {
                await infoTable.InsertAsync(item);
                Close(this);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

        public override void SaveAction()
        {
            Close(this);
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
                    BusyStateChange(false);

                return response;
            }

            #endregion


        }
          
    }
}