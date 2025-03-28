using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Streaming1.Models;

[Table("Show")]
public partial class Show
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("JustWatchShowID")]
    [StringLength(12)]
    public string JustWatchShowId { get; set; } = null!;

    [StringLength(128)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Column("ShowTypeID")]
    public int ShowTypeId { get; set; }

    public int ReleaseYear { get; set; }

    [Column("AgeCertificationID")]
    public int? AgeCertificationId { get; set; }

    public int Runtime { get; set; }

    public int? Seasons { get; set; }

    [Column("ImdbID")]
    [StringLength(12)]
    public string? ImdbId { get; set; }

    public double? ImdbScore { get; set; }

    public double? ImdbVotes { get; set; }

    public double? TmdbPopularity { get; set; }

    public double? TmdbScore { get; set; }

    [ForeignKey("AgeCertificationId")]
    [InverseProperty("Shows")]
    public virtual AgeCertification? AgeCertification { get; set; }

    [InverseProperty("Show")]
    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();

    [InverseProperty("Show")]
    public virtual ICollection<GenreAssignment> GenreAssignments { get; set; } = new List<GenreAssignment>();

    [InverseProperty("Show")]
    public virtual ICollection<ProductionCountryAssignment> ProductionCountryAssignments { get; set; } = new List<ProductionCountryAssignment>();

    [ForeignKey("ShowTypeId")]
    [InverseProperty("Shows")]
    public virtual ShowType ShowType { get; set; } = null!;
}
