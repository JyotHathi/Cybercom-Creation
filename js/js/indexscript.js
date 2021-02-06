// Indexscript.js contains Javascript Code For Index.html filr
// 1. Login and 2. Registration and Validation



var jsDatabase = new Array();

// To Refresh the Page When Page Got Load or Refresh and to load the data
function onLoadPage() {
    document.getElementById("txtemail").value = "";
    document.getElementById("txtpasssword").value = "";
    if (localStorage.getItem("jsDatabase") != null) {
        jsDatabase = JSON.parse(localStorage.getItem("jsDatabase"));
    }
}


// function that called when login button is press
function loginUser() {
    // If No Users Are There 
    if (jsDatabase.length == 0) {
        alert("No Users Are available If You are Admin Please Register Your Self First!!");
    }

    // If No Users Are There
    else {
        var emailValue = document.getElementById("txtemail").value;
        var passwordValue = document.getElementById("txtpasssword").value;
        
        // Checking Whether Inputs are insrete Or Not
        if ((emailValue != null && emailValue != "") && (passwordValue != null && passwordValue != "")) 
        {
            // Finding User if exists
            let user = jsDatabase.find((ele) =>ele.email===emailValue && ele.password===passwordValue); 
            
            // I fuser Exists then 
            if (user!=null) {
                switch (user.userType) {
                    case 1: window.location = "./dashboard.html";
                        break;
                    case 2: window.location = "./udashboard.html";
                        break;
                }
            }
            
            // if not user found
            else {
                
                alert("Invalid Id or Password");
            }
        }
        
        else {
            alert("Please Enter Email and Password");
        }

    }
}


// Function Which Execute When User Clcik Register User
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