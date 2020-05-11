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

        [Column(Order = 3, TypeName = "INT")]
        public int IdUserApi { get; set; }

        [Column(Order = 4, TypeName = "INT")]
        public int IdTypeUser { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Dni { get; set; }

        [Index(IsUnique = true)]
        [Column(Order = 6, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Column(Order = 7, TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Imei { get; set; }

        [Column(Order = 8, TypeName = "DATETIME")]
        public DateTime CreateDate { get; set; }

        [Column(Order = 9, TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Nombre { get; set; }
    }
}