using Android.Support.Design.Widget;
using Android.Support.V4.Widget;

namespace SunCorp.Irecover.Android.Interfaces
{
    public interface INavigationActivity
    {
        DrawerLayout GetDrawerLayout();

        CoordinatorLayout GetCoordinateLayout();
    }
}
