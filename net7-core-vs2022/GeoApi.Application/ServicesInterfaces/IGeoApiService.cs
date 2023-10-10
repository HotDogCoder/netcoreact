using GeoApi.Domain;

namespace GeoApi.Application;
public interface IGeoApiService
{
    public Task<GeoApiClass> GetGeoApiClasses(GeoApiFilterClass geoApiFilterClass);
    public Task<List<GeoApiClass>> PostGeoApiClasses(List<GeoApiFilterClass> geoApiFilterClasses);
}
