using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrevozPutnikaAPI.Models
{
    [Table("Sediste")]
    public class Sediste
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("brojSedista")]
        public int brojSedista { get; set; }

        [Column("zauzeto")]
        public bool zauzeto{get;set;}

        [JsonIgnore]
        public Rezervacija rezervacija {get;set;}

        [JsonIgnore]
        public Relacija relacija {get;set;}


    }
}