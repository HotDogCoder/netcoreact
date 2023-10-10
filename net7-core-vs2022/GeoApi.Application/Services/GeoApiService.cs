using GeoApi.Domain;

namespace GeoApi.Application;
public class GeoApiService : IGeoApiService
{
    private readonly IGeoApiRepository geoApiRepository;

    public GeoApiService(IGeoApiRepository geoApiRepository){
        this.geoApiRepository = geoApiRepository;
    }
    public async Task<GeoApiClass> GetGeoApiClasses(GeoApiFilterClass geoApiFilterClass) {
        GeoApiClass data = this.geoApiRepository.GetGeoApiClasses(geoApiFilterClass);
        return data;
    }

    public async Task<List<GeoApiClass>> PostGeoApiClasses(List<GeoApiFilterClass> geoApiFilterClasses){
        List<GeoApiClass> data = this.geoApiRepository.PostGeoApiClasses(geoApiFilterClasses);
        return data;
    }
}
