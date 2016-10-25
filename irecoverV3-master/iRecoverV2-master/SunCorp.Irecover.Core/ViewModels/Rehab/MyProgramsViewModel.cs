using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.Add;

namespace SunCorp.IRecover.ViewModels.Rehab
{
    public class MyProgramsViewModel : AddListViewModel
    {
        public MyProgramsViewModel()
        {
            Items = new ObservableCollection<string>();
            for (var i = 1; i <= 5; i++)
            {
                Items.Add($"Program {i}");
            }
        }

        public override void AddListAction()
        {
            ShowViewModel<ViewProgramViewModel>();
        }
    }
}
