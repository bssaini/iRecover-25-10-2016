using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.IRecover.ViewModels.Add
{
    public class AddOtherDriversViewModel : AddListViewModel
    {
        public AddOtherDriversViewModel()
        {
            Items = new ObservableCollection<string>();
            for (var i = 1; i <= 5; i++)
            {
                Items.Add($"Other driver {i}");
            }
        }
        public override void AddListAction()
        {
            ShowViewModel<NewOtherDriverViewModel>();
        }
    }
}
