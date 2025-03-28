using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Streaming1.Models.DTO {
    public class ShowDTO
    {
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int ShowTypeId { get; set; }

    public int ReleaseYear { get; set; }

    public int? AgeCertificationId { get; set; }

    public int Runtime { get; set; }

    public string? ImdbId { get; set; }

    public double? ImdbScore { get; set; }

    public double? ImdbVotes { get; set; }

    public double? TmdbPopularity { get; set; }

    public double? TmdbScore { get; set; }
    
    public string Director { get; set; }
    }
}

namespace Streaming1.ExtensionMethods
{
    public static class ShowExtensions
    {
        public static Streaming1.Models.DTO.ShowDTO toDTO(this Streaming1.Models.Show show)
        {
            var directorName = show.Credits
                .Where(c => c.RoleId == 2) 
                .Select(c => c.Person.FullName) 
                .FirstOrDefault();
            return new Streaming1.Models.DTO.ShowDTO
            {
                Id = show.Id,
                Title = show.Title,
                Description = show.Description,
                ShowTypeId = show.ShowTypeId,
                ReleaseYear = show.ReleaseYear,
                AgeCertificationId = show.AgeCertificationId,
                Runtime = show.Runtime,
                ImdbId = show.ImdbId,
                ImdbScore = show.ImdbScore,
                ImdbVotes = show.ImdbVotes,
                TmdbPopularity = show.TmdbPopularity,
                TmdbScore = show.TmdbScore,
                Director = directorName
            };
        } 
    }
}