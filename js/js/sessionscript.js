var jsDatabase;
if(localStorage.getItem("jsDatabase")!==null)
{
    jsDatabase=JSON.parse(localStorage.getItem("jsDatabase"));
}
else{
    jsDatabase=new Array();
}

var SessionDataBase;
if(localStorage.getItem("SessionDataBase")!==null)
{
    SessionDataBase=JSON.parse(localStorage.getItem("SessionDataBase"));
}
else{
    SessionDataBase=new Array();
}

var tableRow,tableCell,textNode;
for(let session of SessionDataBase)
{
    tableRow=document.createElement("tr");
    for(let sessionprop in session)
    {
        tableCell=document.createElement("td");
        textNode=document.createTextNode(`${session[sessionprop]}`);
        tableCell.appendChild(textNode);
        tableRow.appendChild(tableCell);
    }
    document.getElementById("userTable").appendChild(tableRow);
}


document.getElementById("lbllogout").addEventListener("click",()=>{ window.location = "./index.html"; sessionStorage.removeItem("userEmail");});

document.getElementById("lblUser").addEventListener("click",()=>window.location="./users.html");

document.getElementById("lblsessions").addEventListener("click",()=>window.location="./sessions.html");

document.getElementById("lbldashboard").addEventListener("click",()=>window.location="./dashboard.html");

var emailId = JSON.parse(sessionStorage.getItem("userEmail"));
var jsDatabase = JSON.parse(localStorage.getItem("jsDatabase"));
var user = jsDatabase.find((ele) => ele.email === emailId);
document.getElementById("lblname").innerHTML = "Hello, " + user.userName;
