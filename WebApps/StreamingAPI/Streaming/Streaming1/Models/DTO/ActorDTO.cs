using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Streaming1.Models.DTO {
    public class ActorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }

       // public virtual ICollection<CreditDTO> Credits { get; set; } = new List<CreditDTO>();
       public int JustWatchPersonId { get; set; }
       public List<int> ShowIds { get; set; } = new List<int>();  
    }
}

namespace Streaming1.ExtensionMethods
{
    public static class ActorExtensions
    {
        public static Streaming1.Models.DTO.ActorDTO toDTO(this Streaming1.Models.Person person)
        {
            return new Streaming1.Models.DTO.ActorDTO
            {
                Id = person.Id,
                FullName = person.FullName,
                JustWatchPersonId = person.JustWatchPersonId,
                ShowIds = person.Credits
                    .Where(c => c.RoleId == 1)  // Only include credits with RoleId == 1
                    .Select(c => c.ShowId)       // Select the ShowId from each matching credit
                    .ToList() 
            };
        } 
        public static Streaming1.Models.Person ToActor(this Streaming1.Models.DTO.ActorDTO actor)
        {
            return new Streaming1.Models.Person
            {
                Id = actor.Id,
                FullName = actor.FullName,
                JustWatchPersonId = actor.JustWatchPersonId,

            };
        }
    }
}
