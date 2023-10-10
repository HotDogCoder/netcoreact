namespace GeoApi.Domain;
public class GeoApiFilterClass
{
    public string ip { get; set;}
    public string address { get; set;}
    public string country { get; set;}

    public GeoApiFilterClass(string ip, string address, string country){
        this.ip = ip;
        this.address = address;
        this.country = country;
    }
}