using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;
using SunCorp.IRecover.ViewModels.Add;

namespace SunCorp.IRecover.ViewModels.New
{
    public class NewPictureViewModel : NewItemViewModel
    {
        private byte[] mPictureBytes;
        private Action mOnUploadImageAction;
        private ICommand mAddPicture;
        private int mPictureSize;

        private IMvxPictureChooserTask mPictureChooserTask;

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
    }
}
