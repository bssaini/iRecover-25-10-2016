using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using SunCorp.IRecover.ViewModels.Add;
using SunCorp.IRecover.ViewModels.New;

namespace SunCorp.IRecover.ViewModels
{
    public class NewClaimViewModel : NewItemViewModel
    {
        #region private members
        
        #endregion
        
        #region private methods
       
        #endregion

        public override void SaveAction()
        {
            Close(this);
        }
    }
}