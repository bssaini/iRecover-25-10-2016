using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.New;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Diagnostics;



namespace SunCorp.IRecover.ViewModels.Add
{
    public class AddPicturesViewModel : AddListViewModel
    {
        private bool _isBusy;
        const string applicationURL = "https://n8983631.blob.core.windows.net/accidentpictures?comp=list&maxresults=20";
        private HttpClient client;

        public Boolean IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }


        public AddPicturesViewModel()
        {
            Items = new ObservableCollection<string>();
            Items.Add("test.jpg");
            Items.Add("secondjpg");

            download();
                        
        }
     
        private async void download()
        {

            var progressHandler = new ProgressHandler();
            progressHandler.BusyStateChange += (busy) =>
            {
                IsBusy = busy;

            };

        }


        public override void AddListAction()
        {
            ShowViewModel<NewPictureViewModel>();
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
