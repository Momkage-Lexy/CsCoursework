using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Streaming1.Models;

[Table("ProductionCountry")]
public partial class ProductionCountry
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(64)]
    public string CountryIdentifier { get; set; } = null!;

    [InverseProperty("ProductionCountry")]
    public virtual ICollection<ProductionCountryAssignment> ProductionCountryAssignments { get; set; } = new List<ProductionCountryAssignment>();
}
