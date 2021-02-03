using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrevozPutnikaAPI.Models;

namespace PrevozPutnikaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrevozController : ControllerBase
    {
        public PrevozContext Context { get; set; }

        public PrevozController(PrevozContext context)
        {
            Context = context;
        }

        //Zakomentarisani delovi koda su korisceni samo za
        //unos podataka u tabeli, nije planirano da korisnicima
        //ove opcije budu dostupne
 /*

        [Route("UpisiRelaciju")]
        [HttpPost]

       public async Task UpisiRelaciju([FromBody] Relacija rel)
        {
            Context.Relacije.Add(rel);

            for(int i=0;i<rel.brojSedista;i++)
            {
                Sediste sediste= new Sediste();
                sediste.brojSedista=i+1;
                sediste.rezervacija=null;
                sediste.relacija=rel;
                sediste.zauzeto=false;
                Context.Sedista.Add(sediste);
            }

            await Context.SaveChangesAsync();
        }
*/
        [Route("PreuzmiRelacije")]
        [HttpGet]

        public async Task<List<Relacija>> PreuzmiRelacije()
        {
            return await Context.Relacije.ToListAsync();
            
        }

        [Route("PreuzmiSedista")]
        [HttpPost]

        public async Task<List<Sediste>> PreuzmiSedista([FromBody] Relacija rel)
        {
            return await Context.Sedista.Where(p=> p.relacija.ulaz==rel.ulaz&&p.relacija.izlaz==rel.izlaz&&p.relacija.vremePolaska==rel.vremePolaska).ToListAsync();
        }

        [Route("PreuzmiRezervacije/{username}")]
        [HttpGet]

        public async Task<List<Rezervacija>> PreuzmiRezervacije(string username)
        {
           return await Context.Rezervacije.Include(p=>p.relacija)
                                           .Include(p=>p.sedista)
                                           .Where(p=>p.korisnik.username==username)
                                           .ToListAsync();
            
        }



/*
        [Route("UpisiKorisnika")]
        [HttpPost]

       public async Task UpisiKorisnika([FromBody] Korisnik user)
        {
            Context.Korisnici.Add(user);
            await Context.SaveChangesAsync();

        }
        */

        [Route("Login")]
        [HttpPost]
        //koristim post da se ne bi video user i pass u url-u

        public async Task<Korisnik> Login([FromBody] Korisnik user)
        {
            var kor= await Context.Korisnici.Where(p=> p.username==user.username&&p.password==user.password).FirstOrDefaultAsync();

            if(kor!=null)
            {
                return kor;
            }
            else
            {
                Korisnik invalid = new Korisnik();
                invalid.username="invalid";
                return invalid;
            }

        }

        [Route("OslobodiSediste/{username}/{brSed}")]
        [HttpDelete]

        public async Task<IActionResult> OslobodiSediste(string username,int brSed, [FromBody] Relacija rel)
        {
            var relacija=await Context.Relacije.Where(p=> p.ulaz==rel.ulaz&&p.izlaz==rel.izlaz&&p.vremePolaska==rel.vremePolaska).FirstOrDefaultAsync();
            if(relacija!=null)
            {
                var korisnik= Context.Korisnici.Where(p=> p.username==username).FirstOrDefault();
                if(korisnik!=null)
                {
                    var rez=Context.Rezervacije.Where(p=> p.korisnik==korisnik&&p.relacija==relacija).FirstOrDefault();
                    if(rez!=null)
                    {
                        var sed= await Context.Sedista.Where(p=> p.brojSedista==brSed&&p.relacija==relacija).FirstOrDefaultAsync();
                        if(sed!=null)
                        {
                            //ovo radim jer sam imao problema sa menjanjem stranog kljuca postojeceg sedista
                            Sediste novoSediste= new Sediste();
                            novoSediste.ID=sed.ID;
                            novoSediste.brojSedista=sed.brojSedista;
                            novoSediste.relacija=sed.relacija;
                            novoSediste.rezervacija=null;
                            novoSediste.zauzeto=false;

                            Context.Sedista.Remove(sed);
                            Context.Sedista.Add(novoSediste);
                            await Context.SaveChangesAsync();
                            var rezProvera= await Context.Rezervacije.Where(p=> p.korisnik==korisnik&&p.relacija==relacija).Include(p=>p.sedista).FirstOrDefaultAsync();
                            if(rezProvera.sedista.Count==0)
                            {
                                Context.Remove(rezProvera);
                                await Context.SaveChangesAsync();
                            }
                            return Ok();

                        }
                        else
                        {
                            return StatusCode(406);
                        }
                    }
                    else
                    {
                        return StatusCode(406);
                    }
                }
                else
                {
                    return StatusCode(406);
                }

            }
            else
            {
                return StatusCode(406);
            }
        }


        [Route("ZauzmiSediste/{username}/{brSed}")]
        [HttpPut]

        public async Task<IActionResult> ZauzmiSediste(string username,int brSed, [FromBody] Relacija rel)
        {
            var relacija=await Context.Relacije.Where(p=> p.ulaz==rel.ulaz&&p.izlaz==rel.izlaz&&p.vremePolaska==rel.vremePolaska).FirstOrDefaultAsync();
            if(relacija!=null)
            {
                var korisnik= Context.Korisnici.Where(p=> p.username==username).FirstOrDefault();
                if(korisnik!=null)
                {
                    var sed= await Context.Sedista.Where(p=> p.brojSedista==brSed&&p.relacija==relacija).FirstOrDefaultAsync();
                    var rez=Context.Rezervacije.Where(p=> p.korisnik==korisnik&&p.relacija==relacija).FirstOrDefault();
                    if(rez!=null)
                    {
                        if(sed!=null)
                        {
                            sed.rezervacija=rez;
                            sed.zauzeto=true;
                            Context.Update<Sediste>(sed);
                            await Context.SaveChangesAsync();
                            return Ok();  
                        }
                        else
                        {
                            return StatusCode(406);
                        }
                    }
                    else
                    {
                        if(sed!=null)
                        {
                            Rezervacija novaRez=new Rezervacija();
                            novaRez.korisnik=korisnik;
                            novaRez.relacija=relacija;
                            Context.Rezervacije.Add(novaRez);
                            sed.rezervacija=novaRez;
                            sed.zauzeto=true;
                            Context.Update<Sediste>(sed);
                            await Context.SaveChangesAsync(); 
                            return Ok(); 

                        }
                        else
                        {
                            return StatusCode(406);
                        }
                    }
                }
                else{
                    return StatusCode(406);
                }
            }
            else
            {
                return StatusCode(406);
            }
        }
    }
}

