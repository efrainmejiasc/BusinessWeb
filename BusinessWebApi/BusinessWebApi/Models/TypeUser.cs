using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models
{
    [Table("TypeUser")]
    public class TypeUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Type { get; set; }
    }
}