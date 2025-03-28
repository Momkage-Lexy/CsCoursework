using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Streaming1.Models;

[Table("Person")]
public partial class Person
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("JustWatchPersonID")]
    public int JustWatchPersonId { get; set; }

    [StringLength(50)]
    public string FullName { get; set; } = null!;

    [InverseProperty("Person")]
    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
