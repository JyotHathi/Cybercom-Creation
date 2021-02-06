var jsDatabase;
if(localStorage.getItem("jsDatabase")!==null)
{
    jsDatabase=JSON.parse(localStorage.getItem("jsDatabase"));
}
else{
    jsDatabase=new Array();
}


document.getElementById("lbllogout").addEventListener("click",()=>window.location="./index.html");

document.getElementById("lblUser").addEventListener("click",()=>window.location="./users.html");

document.getElementById("lblsessions").addEventListener("click",()=>window.location="./sessions.html");

document.getElementById("lbldashboard").addEventListener("click",()=>window.location="./dashboard.html");
