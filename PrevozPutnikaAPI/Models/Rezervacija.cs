using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrevozPutnikaAPI.Models
{

    [Table("Rezervacija")]
    public class Rezervacija
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        [JsonIgnore]
        public Korisnik korisnik {get;set;}
        
        public Relacija relacija{get;set;}

        public virtual List<Sediste> sedista{get;set;}
    }
    
}