using Android.App;
using MvvmCross.Core.ViewModels;

namespace SunCorp.Irecover.Android.Application
{
#if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
    public class IRecoverApplication : MvxApplication
    {
       
    }
}