using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(200)]
        public string NameCompany { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(Order = 4, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Column(Order = 5, TypeName = "DATETIME")]
        public DateTime CreateDate { get; set; }

        [Column(Order = 6, TypeName = "BIT")]
        public bool Status { get; set; }

        [Column(Order = 7, TypeName = "INT")]
        public int NumberDevices{ get; set; }
    }
}