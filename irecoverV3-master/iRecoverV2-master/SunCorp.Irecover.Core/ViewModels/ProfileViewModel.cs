using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.ViewModels.Add;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SunCorp.IRecover.Data;
using System.Diagnostics;

namespace SunCorp.IRecover.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        #region private members

        private ICommand mSaveAccountInfo;

        private string _profileName = "";
        private string _carRegistration = "";
        private string _contactInfo = "";
        private string _age = "";
        private string _sex = "";
        private string _address = "";
        private string _policyNumber = "";
        private Boolean _isBusy = false;

        private MobileServiceClient client;
        private IMobileServiceTable<AccountInfoData> infoTable;

        const string applicationURL = "http://suncorpandroid.azurewebsites.net";
        #endregion

        public ICommand SaveCommand
        {
            get
            {
                mSaveAccountInfo = mSaveAccountInfo ?? new MvxCommand(SaveInfoToServer);
                return mSaveAccountInfo;
            }
        }

        public string ProfileName
        {
            get { return _profileName; }
            set { _profileName = value; }

        }

        public string CarRegistration
        {
            get { return _carRegistration; }
            set { _carRegistration = value; }
        }

        public string ContactInfo
        {
            get { return _contactInfo; }
            set { _contactInfo = value; }
        }

        public string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value;  }
        }

        public string PolicyNumber
        {
            get { return _policyNumber; }
            set { _policyNumber = value; }
        }

        public Boolean IsBusy
            {
                get { return _isBusy; }
                set { _isBusy = value; }
            }

        

        #region private methods
        public async void SaveInfoToServer()
        {
           if(client == null || string.IsNullOrEmpty(ProfileName))
            {
                return;
            }

            var item = new AccountInfoData
            {
                profileName = ProfileName,
                carRegistration = CarRegistration,
                contactInfo = ContactInfo,
                age = Age,
                sex = Sex,
                address = Address,
                policyNumber = PolicyNumber
            };

            try
            {
                await infoTable.InsertAsync(item);
                Close(this);

            } catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

        #endregion


        public ProfileViewModel()
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

                infoTable = client.GetTable<AccountInfoData>();

            } catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            

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