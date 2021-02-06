var jsDatabase;
if(localStorage.getItem("jsDatabase")!==null)
{
    jsDatabase=JSON.parse(localStorage.getItem("jsDatabase"));
}
else{
    jsDatabase=new Array();
}

class DefaultUser {
    constructor(username, email, password, usertype) {
        this.userName = username;
        this.password = password;
        this.email - email;
        this.userType = usertype
    }
}

class NormalUser extends DefaultUser {
    constructor(username, email, password, dateofbirth, userType) {
        super(username, email, password, userType);
        this.dateOfBirth = dateofbirth;
    }
}

document.getElementById("lbllogout").addEventListener("click",()=>window.location="./index.html");

document.getElementById("lblUser").addEventListener("click",()=>window.location="./users.html")

document.getElementById("lblsessions").addEventListener("click",()=>window.location="./sessions.html")

document.getElementById("lbldashboard").addEventListener("click",()=>window.location="./dashboard.html")