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

        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Apellido { get; set; }

        [Column(Order = 4, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Dni { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Matricula { get; set; }

        [Column(Order = 6, TypeName = "VARCHAR")]
        [StringLength(20)]
        [Required]
        public string Rh { get; set; }

        [Column(Order = 7, TypeName = "VARCHAR")]
        [StringLength(20)]
        [Required]
        public string Grado { get; set; }

        [Column(Order = 8, TypeName = "VARCHAR")]
        [StringLength(20)]
        [Required]
        public string Grupo { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 9, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [Column(Order = 10, TypeName = "INT")]
        public int IdCompany { get; set; }

        [Column(Order = 11, TypeName = "VARCHAR")]
        [StringLength(200)]
        [Required]
        public string Company { get; set; }

        [Column(Order = 12, TypeName = "DATETIME")]
        public DateTime Date { get; set; }

        [Column(Order = 13, TypeName = "BIT")]
        public bool Status { get; set; }

        [Column(Order = 14, TypeName = "VARCHAR")]
        [Required]
        public string Foto { get; set; }

        [Column(Order = 15, TypeName = "VARCHAR")]
        [Required]
        public string Qr { get; set; }


    }
}