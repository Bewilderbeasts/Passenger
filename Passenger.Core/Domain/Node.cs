using System;
using System.Text.RegularExpressions;

public class Node
{
     public static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
    public string Address { get; protected set; }
    public double Longitude { get; protected set; }
    public double Latitude { get; protected set; }
    public DateTime UpdatedAt {get; set;}

    protected Node()
    {  
    }

    protected Node(string Address, double Longitude, double Latitude)
    {
        SetAddress(Address);
        SetLongitude(Longitude);
        SetLatitude(Latitude);
    }

    private void SetLatitude(double latitude)
    {
       if (latitude < 0 || latitude > 90)
        {
            throw new Exception("Latitude is invalid.");
        }
        if (Latitude == latitude)
        {
            return;
        }
        Latitude = latitude;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetLongitude(double longitude)
    {
        if (longitude < 0 || longitude > 180)
        {
            throw new Exception("Longitude is invalid.");
        }
        if (Longitude == longitude)
        {
            return;
        }
        Longitude = longitude;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetAddress(string address)
    {
         if (!NameRegex.IsMatch(address))
        {
            throw new Exception("Address is invalid.");
        }
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }
    public static Node Create(string address, double longitude, double latitude)
			=> new Node(address, longitude, latitude);
}

