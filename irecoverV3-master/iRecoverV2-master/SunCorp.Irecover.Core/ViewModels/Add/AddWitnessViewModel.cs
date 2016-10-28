using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.IRecover.ViewModels.Add
{
    public class AddWitnessViewModel : AddListViewModel
    {
        public AddWitnessViewModel()
        {
            Items = new ObservableCollection<string>();
            for (var i = 1; i <= 5; i++)
            {
                Items.Add($"Witness {i}");
            }
        }

        public override void AddListAction()
        {
            ShowViewModel<NewWitnessViewModel>();
        }
    }
}
