import {Rezervacija} from "./rezervacija.js"
import {Relacija} from "./relacija.js"

export class Korisnik
{
    constructor(ime,us,pass)
    {
        this.punoIme=ime;
        this.username=us;
        this.password=pass;
        this.rezervacije=[];

    }
    vratiUsername()
    {
        return this.username;
    }

    crtajRezervacije(host,selektovana,hostForma,sveRel)
    {
        
        host.innerHTML="";
        //hostForma.innerHTML="";
        var vaseRezervacije=document.createElement("h3");
        vaseRezervacije.innerHTML="Vaše rezervacije:";
        host.appendChild(vaseRezervacije);

        this.rezervacije.forEach(element => {
            if(element==selektovana)
            {
                element.crtajRezervaciju(host,true,hostForma,sveRel); 
            }
            else
            {
                element.crtajRezervaciju(host,false,hostForma,sveRel);
            }
  
        });
        var novaRez=document.createElement("button");
        novaRez.innerHTML="Nova rezervacija";

        host.appendChild(novaRez);

        novaRez.onclick= (ev) =>
        {
            this.crtajRezervacije(host,null,hostForma,sveRel);
            this.crtajNovuRezervaciju(hostForma,sveRel,host,null);
        }
    }

    ucitajRezervacije(sveRelacije,relacija,divBus,flegovi,hostRez,hostForma)
    {
        this.rezervacije.splice(0, this.rezervacije.length)
        fetch("https://localhost:5001/Prevoz/PreuzmiRezervacije/"+this.username).then(p=>
        {
            p.json().then(data=>
                {
                    data.forEach(rez => {
                        var tmpRel
                        sveRelacije.forEach(element => {
                            if(element.ulaz==rez.relacija.ulaz&&element.izlaz==rez.relacija.izlaz&&element.vremePolaska==rez.relacija.vremePolaska)
                            {
                                tmpRel=element;
                            }
                        });
                        var tmpRez=new Rezervacija(this,tmpRel);
                        rez.sedista.forEach(element => {
                            tmpRez.rezervisiSediste(element.brojSedista);
                        });
                        this.rezervacije.push(tmpRez);
                    });
                    this.crtajRezervacije(hostRez,null,hostForma,sveRelacije);
                    if(relacija!=null)
                        relacija.crtajSedista(divBus,this.username,flegovi);
                })
        })
    }

    izvrsiRezervacije(flegovi,br,max,rel,errFlag)
    {
        if(flegovi[br]==-1)
        {
            fetch("https://localhost:5001/Prevoz/OslobodiSediste/"+this.username+"/"+br,{
            method:"DELETE",
            headers:{
                        "Content-Type": "application/json"
                    },
            body: JSON.stringify(
                {
                    ulaz:rel.ulaz,
                    izlaz:rel.izlaz,
                    vremePolaska:rel.vremePolaska
                }
            )
            }).then(data=>
            {
                if(data.ok)
                {
                    if(br<max)
                    {
                        return this.izvrsiRezervacije(flegovi,br+1,max,rel,errFlag);
                    }

                }
                else
                {
                    alert("Došlo je do greške!");
                    errFlag=true;
                    return this.izvrsiRezervacije(flegovi,br+1,max,rel,errFlag);
                }
            })
        } 
        else if(flegovi[br]==1)
        {
            fetch("https://localhost:5001/Prevoz/ZauzmiSediste/"+this.username+"/"+br,{
                method:"PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(
                    {
                        ulaz:rel.ulaz,
                        izlaz:rel.izlaz,
                        vremePolaska:rel.vremePolaska
                    }
                )
            }).then(data=>
                {
                    if(data.ok)
                    {
                        if(br<max)
                        {
                            return this.izvrsiRezervacije(flegovi,br+1,max,rel,errFlag);
                        }

                    }
                    else
                    {
                        alert("Došlo je do greške!");
                        errFlag=true;
                        return this.izvrsiRezervacije(flegovi,br+1,max,rel,errFlag);
                    }
                })
        }
        else
        {
            if(br<max)
            {
               return this.izvrsiRezervacije(flegovi,br+1,max,rel,errFlag);
            }
            else
            {
                return errFlag;
            }
        }
    }


    dodajRezervaciju(rez)
    {
        this.rezervacije.push(rez);
        
    }

    obrisiRezervaciju(rez)
    {

    }

    crtajNovuRezervaciju(host,sveRel,hostRez,selektovanaRelacija)
    {
        var forma=document.createElement("div");
        forma.className="forma";

        var lblRel = document.createElement("p");
        lblRel.innerHTML="Odaberite relaciju: "

        var selRel=document.createElement("select");

        for(var i=0;i<sveRel.length;i++)
        {
            var opt=document.createElement("option");
            opt.value=i;
            opt.innerHTML= sveRel[i].ulaz + " - "+ sveRel[i].izlaz+ " "+sveRel[i].vremePolaska;
            selRel.appendChild(opt);

            if(sveRel[i]==selektovanaRelacija)
            {
                selRel.value=i;
            }
            

        }
        forma.appendChild(lblRel);
        forma.appendChild(selRel);
        host.innerHTML="";
        host.appendChild(forma);

        var divLegenda=document.createElement("div");
        divLegenda.className="legenda";

        var kvBeli=document.createElement("div");
        kvBeli.className="kvadratic beli";
        kvBeli.innerHTML="Slobodno";
        var kvPlavi=document.createElement("div");
        kvPlavi.className="kvadratic plavi";
        kvPlavi.innerHTML="Zauzeto";
        var kvZeleni=document.createElement("div");
        kvZeleni.className="kvadratic zeleni";
        kvZeleni.innerHTML="Vaše mesto";
        var kvSZeleni=document.createElement("div");
        kvSZeleni.className="kvadratic szeleni";
        kvSZeleni.innerHTML="Željeno mesto";

        divLegenda.appendChild(kvBeli);
        divLegenda.appendChild(kvPlavi);
        divLegenda.appendChild(kvZeleni);
        divLegenda.appendChild(kvSZeleni);

        host.appendChild(divLegenda);

        var divBus=document.createElement("div");
        divBus.className="bus";
        var flegovi= new Array(sveRel[selRel.value].brojSedista+1).fill(0);

        
        
        host.appendChild(divBus);

        selRel.onchange = (ev)=>
        {
            divBus.innerHTML="";

            fetch("https://localhost:5001/Prevoz/PreuzmiSedista",{
            method:"POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(
                {
                    ulaz:sveRel[selRel.value].ulaz,
                    izlaz:sveRel[selRel.value].izlaz,
                    vremePolaska:sveRel[selRel.value].vremePolaska
                }
            )
            }).then(d=>
            {
            d.json().then(data1=>
                {
                    data1.forEach(sed => {
                        if(sed.zauzeto)
                        {
                            sveRel[selRel.value].rezervisi(sed.brojSedista,null);
                        }
                        else
                        {
                            sveRel[selRel.value].oslobodi(sed.brojSedista);
                        }
                    });
                    var flg= new Array(sveRel[selRel.value].brojSedista+1).fill(0);
                    flegovi=flg;
                    if(this.username!=null)
                    {
                        this.ucitajRezervacije(sveRel,sveRel[selRel.value],divBus,flegovi,hostRez,host);
                    }
                    else
                        sveRel[selRel.value].crtajSedista(divBus,this.username,flegovi);
                })
            });

        }
        selRel.onchange();
        var rezervisi=document.createElement("button");
        rezervisi.innerHTML="Izvrši rezervaciju";

        host.appendChild(rezervisi);
        
        rezervisi.onclick = (ev) =>
        {
            if(this.username!=null)
            {
                var err=this.izvrsiRezervacije(flegovi,1,sveRel[selRel.value].brojSedista+1,sveRel[selRel.value],false)
                if(!err)
                {
                    host.innerHTML="";
                    hostRez.innerHTML="";
                    var ob=document.createElement("h2");
                    ob.innerHTML="Rezervacija uspešna!";
                    host.appendChild(ob);

                    var btn=document.createElement("button");
                    btn.innerHTML="Nazad";
                    host.appendChild(btn);

                    btn.onclick= (ev)=>
                    {
                        
                        host.innerHTML="";
                        this.ucitajRezervacije(sveRel,null,null,null,hostRez,host);
                    }
                }
                
            }
            else
            {
                alert("Ulogujte se da biste izvršili rezervaciju");
            }
        }

    }

}