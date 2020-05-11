using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models
{
    [Table("Materias")]
    public class Materias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string NombreMateria { get; set; }

        [Column(Order = 3, TypeName = "INT")]
        public int IdCompany { get; set; }

    }
}