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

// Admin User CLass Which extends Default User and Alos Have Unique Fileds like State and City.
class AdminUser extends DefaultUser {
    constructor(username, email, password, city, state, userType) {
        super(username, email, password, userType);
        this.city = city;
        this.state = state;
    }
}

// Normal User CLass Which extends Default User and Also Have Addition Filed named Date of Birth.
class NormalUser extends DefaultUser {
    constructor(username, email, password, dateofbirth, userType) {
        super(username, email, password, userType);
        this.dateOfBirth = dateofbirth;
    }
}

document.getElementById("btnregister").addEventListener("click",registerAdmin);

function registerAdmin()
{
    let username=document.getElementById("txtname").value;
    console.log(username);
    let email=document.getElementById("txtemail").value;
    let password=document.getElementById("txtpasssword").value;
    let confirmpassword=document.getElementById("txtconpassword").value;
    let city=document.getElementById("drodowncity").value;
    let state=document.getElementById("drodownstate").value;

    if(checkdata(username,email,password,confirmpassword,city,state)==1)
    {
        var adminUser=new AdminUser(username,email,password,city,state,1);
        jsDatabase.push(adminUser);
        localStorage.setItem("jsDatabase",JSON.stringify(jsDatabase));
        window.location="./index.html";
    }
}

function checkdata(username,email,password,confirmpassword,city,state)
{
    if(username===null || username==="")
    {
        alert("Please Enter Name");
        return 0;
    }
    else if(email===null || email==="")
    {
        alert("Please Enter Email");
        return 0;
    }
    else if(password===null || password==="")
    {
        alert("Please Enter Password");
        return 0;
    }
    else if(confirmpassword===null || confirmpassword==="")
    {
        alert("Please Enter Confirm Password");
        return 0;
    }
    else if(password!=confirmpassword)
    {
        alert("Password And Confirm Password Mismatch");
        return 0;
    }
    else if(city==="-1")
    {
        alert("Please Select city");
        return 0;
    }
    else if(state==="-1")
    {
        alert("Please Select State");
        return 0;
    }
    else{
        return 1;
    }
}
