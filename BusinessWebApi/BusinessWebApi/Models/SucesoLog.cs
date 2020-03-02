using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models
{
    [Table("SucesoLog")]
    public class SucesoLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Column(Order = 2, TypeName = "DATETIME")]
        public DateTime Fecha { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Metodo { get; set; }

        [Column(Order = 4, TypeName = "VARCHAR")]
        [StringLength(8000)]
        public string Excepcion { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Email { get; set; }
    }
}