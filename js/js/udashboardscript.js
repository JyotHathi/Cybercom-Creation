
var SessionDataBase=new Array();
if(localStorage.getItem("SessionDatabase")!=null)
{
    SessionDataBase=JSON.parse(localStorage.getDate("SessionDatabase"));
}
var emailId=JSON.parse(sessionStorage.getItem("userEmail"));
var jsDatabase=JSON.parse(localStorage.getItem("jsDatabase"));
var user=jsDatabase.find((ele)=>ele.email===emailId);
document.getElementById("lblname").innerHTML="Hello, "+user.userName;

class SessionTime
{
    constructor(name,time1,time2)
    {
        this.userName=name;
        this.loginTime=time1;
        this.logouttime=time2;
    }
}
var loggeninUser=new SessionTime(user.userName,(new Date()).getTime(),"");


var date=new Date(user.dateOfBirth);
var today=new Date();
if(today.getMonth()===date.getMonth() && today.getDate()===date.getDate())
{
    document.getElementById("lblwishbirthday").innerHTML="Happy BirthDay";
}
document.getElementById("lbllogout").addEventListener
("click",()=>
{ window.location = "./index.html"; 
sessionStorage.removeItem("userEmail");
loggeninUser.logouttime=(new Date()).getTime();
SessionDataBase.push(loggeninUser);
localStorage.setItem("SessionDataBase",JSON.stringify(SessionDataBase));
}
);
document.getElementById("lbldashboard").addEventListener("click",()=>window.location="./udashboard.html");



