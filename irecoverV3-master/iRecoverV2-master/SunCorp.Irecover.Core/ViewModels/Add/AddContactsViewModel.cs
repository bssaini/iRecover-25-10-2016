using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.IRecover.ViewModels.Add
{
    public class AddContactsViewModel : AddListViewModel
    {
        public AddContactsViewModel()
        {
            Items = new ObservableCollection<string>();
            for (var i = 1; i <= 5; i++)
            {
                Items.Add($"Contact {i}");
            }
        }

        public override void AddListAction()
        {
            ShowViewModel<NewContactViewModel>();
        }
    }
}
