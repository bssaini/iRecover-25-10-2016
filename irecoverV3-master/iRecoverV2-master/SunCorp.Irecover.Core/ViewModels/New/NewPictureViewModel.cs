using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;
using SunCorp.IRecover.ViewModels.Add;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace SunCorp.IRecover.ViewModels.New
{
    public class NewPictureViewModel : NewItemViewModel
    {
        private byte[] mPictureBytes;
        private Action mOnUploadImageAction;
        private ICommand mAddPicture;
        private ICommand mSaveCommand;
        private int mPictureSize;

        private IMvxPictureChooserTask mPictureChooserTask;
        public string Name { get; set; }

        public NewPictureViewModel()
        {
            try
            {
                mPictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public int PictureSize
        {
            get
            {
                return mPictureSize;
            }
            set
            {
                mPictureSize = value;
                RaisePropertyChanged(() => PictureSize);
            }
        }

        public byte[] PictureBytes
        {
            get { return mPictureBytes; }
            set
            {
                mPictureBytes = value;
                RaisePropertyChanged(() => PictureBytes);
                RaisePropertyChanged(() => WasPictureChosen);
            }
        }

        public bool WasPictureChosen => mPictureBytes != null;

        public override void SaveAction()
        {
            Close(this);
        }

        public ICommand AddPicture
        {
            get
            {
                mAddPicture = mAddPicture ?? new MvxCommand(AddPictureAction);
                return mAddPicture;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                mSaveCommand = mSaveCommand ?? new MvxCommand(UploadImage);
                return mSaveCommand;
            }
        }

        public void ChoosePicture()
        {
            mPictureChooserTask.ChoosePictureFromLibrary(PictureSize, 95, OnPictureAvailable, OnChoosePictureCancelled);
        }

        public void TakePicture()
        {
            mPictureChooserTask.TakePicture(PictureSize, 95, OnPictureAvailable, OnChoosePictureCancelled);
        }

        public void AddPictureAction()
        {
            if (mOnUploadImageAction == null)
            {
                throw new Exception("Activity must implement on Add Image Action");
            }

            mOnUploadImageAction();
        }
        public void SetOnUploadImageAction(Action onUploadImageAction)
        {
            mOnUploadImageAction = onUploadImageAction;
        }

        private void OnPictureAvailable(Stream imageStream)
        {

            var pictureBytes = GetBytesFromStream(imageStream);
            if (pictureBytes == null)
            {
                return;
            }

            PictureBytes = pictureBytes;
        }

        private void OnChoosePictureCancelled()
        {
        }

        private static byte[] GetBytesFromStream(Stream imageStream)
        {
            if (imageStream == null)
            {
                return null;
            }
            using (var ms = new MemoryStream())
            {
                imageStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private async void UploadImage()
        {
            var uriAzure = "https://n8983631.blob.core.windows.net/accidentpictures/";
            var title = Name + ".jpg";
            var sas = "?st=2016-10-28T02%3A05%3A00Z&se=2016-10-29T02%3A05%3A00Z&sp=rwdl&sv=2015-12-11&sr=c&sig=PrkhLd4SRNnBZ%2FJNluBL5YFgmNYb5Mjooz%2BE2LcCExA%3D";

            byte[] bitmapData = PictureBytes;

            HttpClient client = new HttpClient();
            HttpContent requestContent = new ByteArrayContent(bitmapData);

            //Add headers, classify image as jpeg and as a blockblob.
            requestContent.Headers.Add("Content-Type", "image/jpeg");
            requestContent.Headers.Add("x-ms-blob-type", "BlockBlob");

            //upload the image
            HttpResponseMessage response = await client.PutAsync(uriAzure + title + sas, requestContent);

            //Success?
            if (response.IsSuccessStatusCode == true)
            {
                Debug.WriteLine("success");
                Close(this);
            }



        }
    }
}
