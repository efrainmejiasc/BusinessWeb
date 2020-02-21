using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string User { get; set; }

        [Column(Order = 4, TypeName = "VARCHAR")]
        [StringLength(200)]
        [Required]
        public string Password { get; set; }

        [Column(Order = 5, TypeName = "INT")]
        [Required]
        public int IdCompany { get; set; }

        [Column(Order = 6, TypeName = "DATETIME")]
        public DateTime CreateDate { get; set; }

        [Column(Order = 7, TypeName = "BIT")]
        public bool Status { get; set; }

        [Column(Order = 8, TypeName = "INT")]
        public int IdTypeUser { get; set; }

        [Column(Order = 9, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; }

        [Column(Order = 10, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Apellido { get; set; }

        [Column(Order = 11, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Dni { get; set; }
    }
}