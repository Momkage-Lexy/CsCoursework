using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Streaming1.Models.DTO {
    public class CreditDTO
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int PersonId { get; set; }
        public int RoleId { get; set; }

    }
}

namespace Streaming1.ExtensionMethods
{
    public static class CreditExtensions
    {
        public static Streaming1.Models.DTO.CreditDTO toDTO(this Streaming1.Models.Credit credit)
        {
            return new Streaming1.Models.DTO.CreditDTO
            {
                Id = credit.Id,
                ShowId = credit.ShowId,
                PersonId = credit.PersonId,
                RoleId = credit.RoleId
            };
        } 
    }
}