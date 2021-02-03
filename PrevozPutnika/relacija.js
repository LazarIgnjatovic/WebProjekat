import {Sediste} from "./sediste.js"

export class Relacija
{
    constructor(ul,iz,br,vreme)
    {
        this.ulaz=ul;
        this.izlaz=iz;
        this.brojSedista=br;
        this.vremePolaska=vreme;
        this.skupSedista=[];
        //temp
        for(let i=0;i<br;i++)
        {
            this.skupSedista.push(new Sediste (i+1,null,this))
        }
    }

    vratiSediste(br)
    {
        return this.skupSedista[br-1];
    }

    vratiUlaz()
    {
        return this.ulaz;
    }
    vratiIzlaz()
    {
        return this.izlaz;
    }
    vratiVreme()
    {
        return this.vremePolaska;
    }
    
    rezervisi(br,korisnik)
    {
        this.skupSedista[br-1].korisnik=korisnik;
        this.skupSedista[br-1].zauzeto=true;
    }
    oslobodi(br)
    {
        this.skupSedista[br-1].korisnik=null;
        this.skupSedista[br-1].zauzeto=false;
    }

    crtajSedista(host,userName,flegovi)
    {
        host.innerHTML="";
        var i;
        for(i=0;i<this.skupSedista.length-5;i+=4)
        {
            var divRed=document.createElement("div");
            divRed.className="red";
            this.skupSedista[i].crtaj(divRed,userName,flegovi);
            this.skupSedista[i+1].crtaj(divRed,userName,flegovi);
            this.skupSedista[i].crtajPrazno(divRed,userName);
            this.skupSedista[i+2].crtaj(divRed,userName,flegovi);
            this.skupSedista[i+3].crtaj(divRed,userName,flegovi);

            host.appendChild(divRed);
        }
        var divRedZadnji=document.createElement("div");
        divRedZadnji.className="red";
        for(;i<this.skupSedista.length;i++)
        {
            this.skupSedista[i].crtaj(divRedZadnji,userName,flegovi);
        }
        host.appendChild(divRedZadnji);
    }
}



