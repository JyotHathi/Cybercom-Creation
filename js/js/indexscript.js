// Indexscript.js contains Javascript Code For Index.html filr
// 1. Login and 2. Registration and Validation



var jsDatabase = new Array();
onLoadPage();
// To Refresh the Page When Page Got Load or Refresh 
function onLoadPage() {
    document.getElementById("txtemail").value = "";
    document.getElementById("txtpasssword").value = "";
    if (localStorage.getItem("jsDatabase") != null) {
        jsDatabase = JSON.parse(localStorage.getItem("jsDatabase"));
    }
}

// Default User Class Which Contains Common Fileds of Uesr
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

// EventListerner For Login Button
document.getElementById("btnlogin").addEventListener("click", loginUser);
function loginUser() {
    if (jsDatabase.length == 0) {
        alert("No Users Are available If You are Admin Please Register Your Self First!!");
    }
    else {
        var emailValue = document.getElementById("txtemail").value;
        var passwordValue = document.getElementById("txtpasssword").value;
        
        if ((emailValue != null && emailValue != "") && (passwordValue != null && passwordValue != "")) 
        {
            let user = jsDatabase.find((ele) =>ele.email===emailValue && ele.password===passwordValue); 
        
            if (user !=null) {
                sessionStorage.setItem("userEmail",JSON.stringify(user.email));
                switch (user.userType) {
                    case 1: 
                    window.location = "./dashboard.html";
                        break;
                    case 2: window.location = "./udashboard.html";
                        break;
                }
            }
            else {
                
                alert("Invalid Id or Password");
            }
        }
        else {
            alert("Please Enter Email and Password");
        }

    }
}

// EventListerner For Register Button
document.getElementById("btnregister").addEventListener("click", registerAdmin);

function registerAdmin() {
    if (jsDatabase.length === 0) {
        console.log(jsDatabase.length);
        window.location = ("./adminregistration.html");
    }
    else {

        let user = jsDatabase.find(
            (ele)=>ele.userType==="1"
        );
        if (user != -1) {
            alert("Admin Already Registered!!");
        }
        else {
            window.location = ("./adminregistration.html");
        }
    }
}