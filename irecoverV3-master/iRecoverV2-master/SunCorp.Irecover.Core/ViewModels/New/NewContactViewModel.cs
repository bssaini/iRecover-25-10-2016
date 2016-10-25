using System.Collections.ObjectModel;
using SunCorp.IRecover.ViewModels.Add;

namespace SunCorp.IRecover.ViewModels.New
{
    public class NewContactViewModel : NewItemViewModel
    {
        public override void SaveAction()
        {
            Close(this);
        }
    }
}
