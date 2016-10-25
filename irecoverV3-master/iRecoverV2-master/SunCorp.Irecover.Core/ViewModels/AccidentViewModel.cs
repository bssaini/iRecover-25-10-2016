using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.ViewModels.Add;
using System;
using Microsoft.WindowsAzure.MobileServices;
using SunCorp.IRecover.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SunCorp.IRecover.ViewModels
{
    public class AccidentViewModel : BaseViewModel
    {
        #region private members

        private string _accidentType = "";
        private string _accidentTime = "";
        private string _accidentDetail = "";
        private string _accidentNote = "";
        private Boolean _isBusy = false;

        private ICommand mGotoAddWitnessCommand;
        private ICommand mGotoAddOherDriversCommand;
        private ICommand mGotoAddPicturesCommand;
        private ICommand mGotoAddVoiceMemosCommand;
        private ICommand mGotoAddContactCommand;
        private ICommand mSaveCommand;

        private MobileServiceClient client;
        private IMobileServiceTable<AccidentInfoData> infoTable;

        const string applicationURL = "http://suncorpandroid.azurewebsites.net";
        #endregion

        public string AccidentType
        {
            get { return _accidentType; }
            set { _accidentType = value; }
        }

        public string AccidentTime
        {
            get { return _accidentTime; }
            set { _accidentTime = value; }
        }

        public string AccidentDetail
        {
            get { return _accidentDetail; }
            set { _accidentDetail = value; }
        }

        public string AccidentNote
        {
            get { return _accidentNote; }
            set { _accidentNote = value; }
        }

        public Boolean IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; }
        }

        public ICommand SaveCommand
        {
            get
            {
                mSaveCommand = mSaveCommand ?? new MvxCommand(SaveInfoToServer);
                return mSaveCommand;
            }
        }

        public ICommand GotoAddWitnessCommand
        {
            get
            {
                mGotoAddWitnessCommand = mGotoAddWitnessCommand ?? new MvxCommand(GoToAddWitnessAction);
                return mGotoAddWitnessCommand;
            }
        }

        public ICommand GotoAddOherDriversCommand
        {
            get
            {
                mGotoAddOherDriversCommand = mGotoAddOherDriversCommand ?? new MvxCommand(GoToAddOtherDriversAction);
                return mGotoAddOherDriversCommand;
            }
        }

        public ICommand GotoAddPicturesCommand
        {
            get
            {
                mGotoAddPicturesCommand = mGotoAddPicturesCommand ?? new MvxCommand(GoToAddPicturesAction);
                return mGotoAddPicturesCommand;
            }
        }

        public ICommand GotoAddVoiceMemosCommand
        {
            get
            {
                mGotoAddVoiceMemosCommand = mGotoAddVoiceMemosCommand ?? new MvxCommand(GoToAddVoiceMemosAction);
                return mGotoAddVoiceMemosCommand;
            }
        }

        public ICommand GotoAddContactCommand
        {
            get
            {
                mGotoAddContactCommand = mGotoAddContactCommand ?? new MvxCommand(GoToAddContactAction);
                return mGotoAddContactCommand;
            }
        }



        public async void SaveInfoToServer()
        {
            if (client == null || string.IsNullOrEmpty(AccidentType))
            {
                return;
            }

            var item = new AccidentInfoData
            {
                accidentType = AccidentType,
                accidentTime = AccidentTime,
                accidentDetail = AccidentDetail,
                accidentNote = AccidentNote
            };

            try
            {
                await infoTable.InsertAsync(item);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public AccidentViewModel()
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

                infoTable = client.GetTable<AccidentInfoData>();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

        private void GoToAddWitnessAction()
        {
            ShowViewModel<AddWitnessViewModel>();
        }

        private void GoToAddOtherDriversAction()
        {
            ShowViewModel<AddOtherDriversViewModel>();
        }

        private void GoToAddPicturesAction()
        {
            ShowViewModel<AddPicturesViewModel>();
        }

        private void GoToAddVoiceMemosAction()
        {
            ShowViewModel<AddVoiceMemosViewModel>();
        }
        private void GoToAddContactAction()
        {
            ShowViewModel<AddContactsViewModel>();
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