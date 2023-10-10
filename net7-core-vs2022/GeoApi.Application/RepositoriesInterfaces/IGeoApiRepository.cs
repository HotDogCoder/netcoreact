using GeoApi.Domain;

namespace GeoApi.Application
{
    public interface IGeoApiRepository
    {
        GeoApiClass GetGeoApiClasses(GeoApiFilterClass geoApiFilterClass);
        List<GeoApiClass> PostGeoApiClasses(List<GeoApiFilterClass> geoApiFilterClasses);
    }
}
