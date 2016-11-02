using SunCorp.Irecover.Core.Models2;
using System.Threading.Tasks;

namespace SunCorp.Irecover.Core.Interfaces2
{
    public interface IGeoCoder
    {
        /// <summary>
        /// Returns a city/locality based on a provided GeoLocation
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        Task<string> GetCityFromLocation(GeoLocation location);

        /// <summary>
        /// Returns a GeoLocation from a location search string
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<GeoLocation> GetLocationFromAddress(string address);
    }
}
