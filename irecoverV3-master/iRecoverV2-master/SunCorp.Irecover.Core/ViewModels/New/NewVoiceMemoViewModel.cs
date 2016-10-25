namespace SunCorp.IRecover.ViewModels.New
{
    public class NewVoiceMemoViewModel : NewItemViewModel
    {
        public override void SaveAction()
        {
            Close(this);
        }
    }
}
