using GeoApi.Application;
using GeoApi.Domain;
using MaxMind.GeoIP2;

namespace GeoApi.Infrastructure;
public class GeoApiRepository : IGeoApiRepository
{
    public GeoApiClass GetGeoApiClasses(GeoApiFilterClass geoApiFilterClass) {
        
        string currentPath = Directory.GetCurrentDirectory();
        string path = "GeoLite2-Country.mmdb";
        GeoApiClass geoApiClass = null;
        try
        {
            using (var reader = new DatabaseReader(path))
            {
                var response = reader.Country(geoApiFilterClass.ip);
                //var response = reader.Country("181.67.138.98");

                Console.WriteLine(response);
                geoApiClass = new GeoApiClass(
                ip: geoApiFilterClass.ip,
                address: response.Country.Name,
                country: response.Country.IsoCode
                );
            }

        }
        catch (Exception ex)
        {
            geoApiClass = new GeoApiClass(
            ip: geoApiFilterClass.ip,
            address: "",
            country: ""
            );
            geoApiClass.error = ex.Message;
        }
        
        return geoApiClass;
    }

    public List<GeoApiClass> PostGeoApiClasses(List<GeoApiFilterClass> geoApiFilterClasses) {
        
        string currentPath = Directory.GetCurrentDirectory();
        string path = "GeoLite2-Country.mmdb";
        List<GeoApiClass> geoApiClasses = new List<GeoApiClass>();
        GeoApiClass geoApiClass = null;

        for (int i = 0; i < geoApiFilterClasses.Count; i++)
        {
            try
            {
                using (var reader = new DatabaseReader(path))
                {
                    var response = reader.Country(geoApiFilterClasses[i].ip);

                    Console.WriteLine(response);
                    geoApiClass = new GeoApiClass(
                    ip: geoApiFilterClasses[i].ip,
                    address: response.Country.Name,
                    country: response.Country.IsoCode
                    );
                    geoApiClasses.Add(geoApiClass);
                }
            }
            catch (Exception ex)
            {
                geoApiClass = new GeoApiClass(
                ip: geoApiFilterClasses[i].ip,
                address: "",
                country: ""
                );
                geoApiClass.error = ex.Message;
                geoApiClasses.Add(geoApiClass);
            }
            
        }

        return geoApiClasses;
    }
}

