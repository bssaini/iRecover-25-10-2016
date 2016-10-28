using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.Data;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SunCorp.IRecover.ViewModels.New
{
    public class NewOtherDriverViewModel : NewItemViewModel
    {
        private ICommand mSaveCommand;

        private MobileServiceClient client;
        private IMobileServiceTable<AccidentOtherDriver> infoTable;

        const string applicationURL = "http://suncorpandroid.azurewebsites.net";

        public string Name { get; set; }

        public string CarRegistration { get; set; }

        public string ContactInfo { get; set; }

        public string AdditionalInfo { get; set; }

        public Boolean IsBusy { get; set; }

        public ICommand SaveCommand
        {
            get
            {
                mSaveCommand = mSaveCommand ?? new MvxCommand(SaveInfoToServer);
                return mSaveCommand;
            }
        }

        public NewOtherDriverViewModel()
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

                infoTable = client.GetTable<AccidentOtherDriver>();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async void SaveInfoToServer()
        {
            if (client == null || string.IsNullOrEmpty(Name))
            {
                return;
            }

            var item = new AccidentOtherDriver
            {
                name = Name,
                carRegistration = CarRegistration,
                contactInfo = ContactInfo,
                additionalInfo = AdditionalInfo
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
                {
                    BusyStateChange(false);

                }
                return response;
            }

            #endregion

        }

    }
}
