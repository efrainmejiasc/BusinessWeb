﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models
{
    [Table("AsistenciaClase")]
    public class AsistenciaClase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "INT")]
        public int Id { get; set; }

        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string Dni { get; set; }

        [Column(Order = 3, TypeName = "INT")]
        public int IdCompany { get; set; }

        [Column(Order = 4, TypeName = "BIT")]
        public bool Status { get; set; }

        [Column(Order = 5, TypeName = "DATETIME")]
        public DateTime CreateDate { get; set; }

        [Column(Order = 6, TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string DniAdm { get; set; }

        [Column(Order =7, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Materia { get; set; }

        [Column(Order = 8, TypeName = "INT")]
        public int Turno { get; set; }

        [Column(Order = 9, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string EmailSend  { get; set; }


        [Column(Order = 10, TypeName = "VARCHAR")]
        [StringLength(20)]
        public string Grado { get; set; }

        [Column(Order = 11, TypeName = "VARCHAR")]
        [StringLength(20)]
        public string Grupo { get; set; }
    }
}