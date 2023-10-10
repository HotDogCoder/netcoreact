namespace GeoApi.Domain;
public class GeoApiClass
{
    public string ip { get; set;}
    public string address { get; set;}
    public string country { get; set;}
    public string? error { get; set; }

    public GeoApiClass(string ip, string address, string country){
        this.ip = ip;
        this.address = address;
        this.country = country;
    }
}
