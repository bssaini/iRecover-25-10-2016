using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.Add;

namespace SunCorp.IRecover.ViewModels.Rehab
{
    public class MyHelpersViewModel : AddListViewModel
    {
        public MyHelpersViewModel()
        {
            Items = new ObservableCollection<string>();
            for (var i = 1; i <= 5; i++)
            {
                Items.Add($"Helper {i}");
            }
        }

        public override void AddListAction()
        {
            ShowViewModel<ViewHelperViewModel>();
        }
    }
}
