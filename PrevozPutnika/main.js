import { Korisnik } from "./korisnik.js"
import { Relacija } from "./relacija.js";
import { Rezervacija } from "./rezervacija.js";

var nullKorisnik=new Korisnik(null,null,null)
let tKorisnik=nullKorisnik;

//temp
var relacije= [];

window.onload = tempInit();

function tempInit()
{
    fetch("https://localhost:5001/Prevoz/PreuzmiRelacije").then(p=>
    {
        p.json().then(data=>
        {
            data.forEach(rel => {
                var relacija=new Relacija(rel.ulaz,rel.izlaz,rel.brojSedista,rel.vremePolaska);
                relacije.push(relacija);
                
            })
            crtajRezervacije(document.querySelector(".rezervacije"),document.querySelector(".glavno"));
        });     
    })

    var h=document.getElementsByClassName("login");
    crtajLogin(h[0]);
}




function login(user, pass)
{
    fetch("https://localhost:5001/Prevoz/Login",{
        method:"POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(
            {
                username:user,
                password:pass
            }
        )
    }).then (p=> {
            p.json().then(data=>{
                if(data.username!="invalid")
                {
                    tKorisnik=new Korisnik(data.punoIme,data.username,data.password);
                    crtajLogin(document.getElementsByClassName("login")[0]);
                    tKorisnik.ucitajRezervacije(relacije,null,null,null,document.querySelector(".rezervacije"),document.querySelector(".glavno"));
                    crtajRezervacije(document.querySelector(".rezervacije"),document.querySelector(".glavno"))
                }
                else
                {
                    alert("Neispravni login podaci!");
                    tKorisnik=nullKorisnik;
                }
            })
    })
}

function crtajLogin(host)
{
    if (tKorisnik!=nullKorisnik) 
    {
        host.innerHTML=null;
        let us = document.createElement("h3");
        us.innerHTML=tKorisnik.punoIme;
        let btnLogOut=document.createElement("button");
        btnLogOut.innerHTML="Log out";

        host.appendChild(us);
        host.appendChild(btnLogOut);

        btnLogOut.onclick = (ev) =>
        {
            tKorisnik=nullKorisnik;
            host.innerHTML=null;
            crtajLogin(host);
            crtajRezervacije(document.querySelector(".rezervacije"),document.querySelector(".glavno"));

        }        
    }
    else
    {
        host.innerHtml="";

        let divUser = document.createElement("div");
        let lblUser= document.createElement("label");
        lblUser.innerHTML="Username:";
        let br1=document.createElement("br");
        let inUser = document.createElement("input");
        inUser.className="user";


        divUser.appendChild(lblUser);
        divUser.appendChild(br1);
        divUser.appendChild(inUser);

        let divPass = document.createElement("div");
        let lblPass= document.createElement("label");
        lblPass.innerHTML="Password:";
        let br2=document.createElement("br");
        let inPass = document.createElement("input");
        inPass.className="user";
        inPass.type="password";
    
        divPass.appendChild(lblPass);
        divPass.appendChild(br2);
        divPass.appendChild(inPass);

        let btnLogin = document.createElement("button");
        btnLogin.innerHTML="Log in";
        
        host.appendChild(divUser);
        host.appendChild(divPass);
        host.appendChild(btnLogin);

        btnLogin.onclick = (ev) =>
        {
            login(inUser.value,inPass.value);
            inUser.value=null;
            inPass.value=null;
        }

    }
}

function crtajRezervacije(host,hostForma)
{
    if(tKorisnik==nullKorisnik)
    {
        host.innerHTML="";
        hostForma.innerHTML="";
        var obavestenje=document.createElement("p");
        obavestenje.innerHTML="Ulogujte se da biste videli rezervacije";
        host.appendChild(obavestenje);
        nullKorisnik.crtajNovuRezervaciju(hostForma,relacije,host,null);
    }
    else
    {
        hostForma.innerHTML="";
    }
}
   



