using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Streaming1.Models;

[Table("Genre")]
public partial class Genre
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(32)]
    public string GenreString { get; set; } = null!;

    [InverseProperty("Genre")]
    public virtual ICollection<GenreAssignment> GenreAssignments { get; set; } = new List<GenreAssignment>();
}
