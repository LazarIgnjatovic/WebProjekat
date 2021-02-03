

export class Rezervacija
{
    constructor(korisnik,rel)
    {
        this.korisnik=korisnik;
        this.relacija=rel;
        this.sedista=[];
    }

    crtajRezervaciju(host,rezFlag,hostForma,sveRel)
    {
        var divRez=document.createElement("div");
        divRez.className="rezervacija";

        var podaci =document.createElement("p");
        podaci.innerHTML=this.relacija.vratiUlaz() + " - " + this.relacija.vratiIzlaz();

        var vreme = document.createElement("p");
        vreme.innerHTML=this.relacija.vratiVreme().toLocaleString();

        if(rezFlag==true)
        {
            divRez.className="rezervacija selektovana";
        }

        divRez.appendChild(podaci);
        divRez.appendChild(vreme);

        host.appendChild(divRez);

        if(rezFlag!=true)
        {
            divRez.onclick= (ev)=> {
                this.korisnik.crtajRezervacije(host,this,hostForma,sveRel);
                this.korisnik.crtajNovuRezervaciju(hostForma,sveRel,host,this.relacija);

            }
        }
        
    }

    rezervisiSediste(br)
    {
        this.relacija.rezervisi(br,this.korisnik)
        this.sedista.push(this.relacija.vratiSediste(br));
    }

    ukloniSediste(sed)
    {
        this.sedista.splice(this.sedista.find(sed),1);
    }

    vratiKorisnika()
    {
        return this.korisnik;
    }


}
