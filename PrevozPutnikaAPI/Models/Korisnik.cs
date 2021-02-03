using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrevozPutnikaAPI.Models
{
    [Table("Korisnik")]

    public class Korisnik
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("punoIme")]
        [MaxLength(255)]

        public string punoIme{get;set;}

        [Column("username")]
        public string username {get;set;}

        [Column("password")]
        public string password {get;set;}
        
        [JsonIgnore]
        public virtual List<Rezervacija> rezervacije {get;set;}
    }
}