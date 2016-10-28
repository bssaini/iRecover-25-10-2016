using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.IRecover.ViewModels.Add
{
    public class AddVoiceMemosViewModel : AddListViewModel
    {
        public AddVoiceMemosViewModel()
        {
            Items = new ObservableCollection<string>();
            for (var i = 1; i <= 5; i++)
            {
                Items.Add($"Voice Memo {i}");
            }
        }

        public override void AddListAction()
        {
            ShowViewModel<NewVoiceMemoViewModel>();
        }
    }
}
