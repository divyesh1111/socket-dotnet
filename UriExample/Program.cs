using System;

class Program
{
    static void Main()
    {
        const string uriString = "https://www.fdu.edu/campuses/vancouver-campus/about-vancouver-campus/this-month/campus-updates/#ugtuition";

        Uri canonicalUri = new Uri(uriString);

        Console.WriteLine("Host: " + canonicalUri.Host);
        Console.WriteLine("Path: " + canonicalUri.PathAndQuery);
        Console.WriteLine("Fragment: " + canonicalUri.Fragment);
    }
}