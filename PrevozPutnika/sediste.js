export class Sediste
{
    constructor(broj,putnik,relacija)
    {
        this.brojSedista=broj;
        this.korisnik=putnik;
        this.relacija=relacija;
        this.zauzeto=false;
    }

    zauzmiSediste(putnik)
    {
        this.korisnik=putnik;
        this.zauzeto=true;
    }
    oslobodiSediste()
    {
        this.korisnik=null;
        this.zauzeto=false
    }

    crtajPrazno(host)
    {
        var prazno=document.createElement("div");
        prazno.className="sed"

        host.appendChild(prazno);
        
    }

    crtaj(host,userName,flegovi)
    {
        var divSed=document.createElement("div");
        divSed.className="sed";

        var lblBr=document.createElement("label");
        lblBr.innerHTML=this.brojSedista;
        lblBr.className="brSed";

        var sed=document.createElement("img");
        sed.className="sed"
        if(flegovi[this.brojSedista]==1)
        {
            sed.src="./images/sediste-selektovano.png";

            sed.onclick = (ev)=>
            {
                flegovi[this.brojSedista]--;
                this.relacija.crtajSedista(host.parentElement,userName,flegovi);
            
            }
        }
        else if(this.zauzeto==false || flegovi[this.brojSedista]==-1)
        {
            sed.src="./images/sediste-slobodno.png";
            sed.onclick = (ev)=>
            {

                flegovi[this.brojSedista]++;
                this.relacija.crtajSedista(host.parentElement,userName,flegovi);
            }
        }
        else if(this.korisnik!=null&&this.korisnik.username==userName)
        {
            sed.src="./images/sediste-rezervisano.png";
            lblBr.style.color="white";
            sed.onclick = (ev)=>
            {
                flegovi[this.brojSedista]--;
                this.relacija.crtajSedista(host.parentElement,userName,flegovi);
            
            }
        }

        else
        {
            sed.src="./images/sediste-zauzeto.png";
            lblBr.style.color="white";
        }
        divSed.appendChild(sed);
        divSed.append(lblBr);
        host.appendChild(divSed);
    }
}
