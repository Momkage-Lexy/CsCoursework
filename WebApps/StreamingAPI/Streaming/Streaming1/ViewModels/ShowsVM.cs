using NuGet.Protocol.Plugins;
using Streaming1.Models;

namespace Streaming1.ViewModels
{
public class ShowsVM
{
    public List<Show> Shows { get; set; }
    public List<string> DescriptionList { get; set; }
    public int ShowCount { get; set; }
    public int movieCount { get; set; }
    public (int show, int movie, int tv) Type { get; set; }
    public Show Popularity { get; set; }
    public Show Votes { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Role> Roles { get; set; }
    public List<Person> person { get; set; }
    public List<Credit> credits { get; set; }

    // New properties
    public string PersonName { get; set; } 
    public List<string> ShowTitles { get; set; } = new List<string>();
}

}