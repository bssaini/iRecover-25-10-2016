using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.IRecover.ViewModels.Rehab
{
    public class ViewHelperViewModel : NewItemViewModel
    {
        public override void SaveAction()
        {
            Close(this);
        }
    }
}
