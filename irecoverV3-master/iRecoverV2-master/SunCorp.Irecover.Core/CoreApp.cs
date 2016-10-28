using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace SunCorp.IRecover
{
    public class CoreApp : MvxApplication
    {

        #region private fields

        private static AppStart mCurrentAppStart;

        #endregion

        #region public members
        
        #endregion

        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            mCurrentAppStart = new AppStart();
            RegisterAppStart(mCurrentAppStart);
        }

        #region public methods
        
        #endregion
    }
}