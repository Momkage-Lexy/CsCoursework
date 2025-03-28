using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Streaming1.Models;

[Table("Role")]
public partial class Role
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(32)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
