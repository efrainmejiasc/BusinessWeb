using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models
{
    [Table("DevicesCompany")]
    public class DevicesCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Column(Order = 2, TypeName = "INT")]
        public int IdCompany { get; set; }

        [Column(Order = 3, TypeName = "DATETIME")]
        public DateTime CreateDate { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 4, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string User { get; set; }

        [Column(Order = 6, TypeName = "VARCHAR")]
        [StringLength(200)]
        public string Password { get; set; }

        [Column(Order = 7, TypeName = "INT")]
        public int IdTypeUser { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 8, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Phone { get; set; }
    }
}