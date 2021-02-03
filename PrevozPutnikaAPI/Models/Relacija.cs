using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrevozPutnikaAPI.Models
{
    [Table("Relacija")]
    public class Relacija
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("ulaz")]
        public string ulaz{get;set;}

        [Column("izlaz")]
        public string izlaz{get;set;}

        [Column("brojSedista")]
        public int brojSedista{get;set;}

        [Column("vremePolaska")]
        public DateTime vremePolaska {get;set;}

        [JsonIgnore]
        public virtual List<Sediste> skupSedista {get;set;}



    }

}