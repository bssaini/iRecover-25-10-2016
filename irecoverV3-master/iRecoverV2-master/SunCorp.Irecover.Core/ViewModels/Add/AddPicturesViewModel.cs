using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.IRecover.ViewModels.Add
{
    public class AddPicturesViewModel : AddListViewModel
    {
        public AddPicturesViewModel()
        {
            Items = new ObservableCollection<string>();
            for (var i = 1; i <= 5; i++)
            {
                Items.Add($"Picture {i}");
            }
        }

        public override void AddListAction()
        {
            ShowViewModel<NewPictureViewModel>();
        }
    }
}
