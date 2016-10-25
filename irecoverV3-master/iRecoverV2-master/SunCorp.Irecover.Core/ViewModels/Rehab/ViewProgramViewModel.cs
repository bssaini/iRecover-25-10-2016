using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.IRecover.ViewModels.Rehab
{
    public class ViewProgramViewModel : NewItemViewModel
    {
        public override void SaveAction()
        {
            Close(this);
        }
    }
}
