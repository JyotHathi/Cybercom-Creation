var jsDatabase;
if (localStorage.getItem("jsDatabase") !== null) {
    jsDatabase = JSON.parse(localStorage.getItem("jsDatabase"));
}
else {
    jsDatabase = new Array();
}


document.getElementById("lbllogout").addEventListener("click", () =>{ window.location = "./index.html"; sessionStorage.removeItem("userEmail");});

document.getElementById("lblUser").addEventListener("click", () => window.location = "./users.html");

document.getElementById("lblsessions").addEventListener("click", () => window.location = "./sessions.html");

document.getElementById("lbldashboard").addEventListener("click", () => window.location = "./dashboard.html");

var emailId = JSON.parse(sessionStorage.getItem("userEmail"));
var jsDatabase = JSON.parse(localStorage.getItem("jsDatabase"));
var user = jsDatabase.find((ele) => ele.email === emailId);
document.getElementById("lblname").innerHTML = "Hello, " + user.userName;
CheckBirthDay();
AgeStatus();
function CheckBirthDay() {

var date,today = new Date();
var result="";
    for (let person of jsDatabase) 
    {
         date = new Date(person.dateOfBirth);
        if (today.getMonth() === date.getMonth() && today.getDate() === date.getDate()) {
            result+=person.userName+" ";
        }
    }
    document.getElementById("bdaystatus").innerHTML="Today is birthday of "+result;
    console.log(result);
}

function AgeStatus()
{
    var result1=0;
    var result2=0;
    var today = new Date().getFullYear(),dbo;
    var result3=0;
    for (let person of jsDatabase) 
    {
        if(person.userType===1)
            continue;
        dbo=new Date(person.dateOfBirth).getFullYear();
        age=today-dbo;
        if(age<18)
        {
            result1++;
        }
        else if(age>=18 && age<50)
        {
            result2++;
        }
        else{
            result3++;
        }
        
    }  
    document.getElementById("status1").innerHTML="Users < 18 Yeras<br />"+result1+" Users<br>";
    document.getElementById("status2").innerHTML="Users 18-50 Years<br />"+result2+" Users<br>";
    document.getElementById("status3").innerHTML="Users > 50 Years<br />"+result3+" Users<br>";
} 