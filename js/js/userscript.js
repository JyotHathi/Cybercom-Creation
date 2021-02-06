
var jsDatabase;
function getData() {
    console.log(JSON.parse(localStorage.getItem("jsDatabase")));
    if (localStorage.getItem("jsDatabase") != null) {
        jsDatabase = JSON.parse(localStorage.getItem("jsDatabase"));
    }
    else {
        jsDatabase = new Array();
    }
    CreateTabble(jsDatabase);;
}
getData();
class DefaultUser {
    constructor(username, email, password, usertype) {
        this.userName = username;
        this.password = password;
        this.email = email;
        this.userType = usertype
    }
}

class NormalUser extends DefaultUser {
    constructor(username, email, password, dateofbirth, userType) {
        super(username, email, password, userType);
        this.dateOfBirth = dateofbirth;
    }
}

document.getElementById("lbllogout").addEventListener("click", () => window.location = "./index.html");

document.getElementById("lblUser").addEventListener("click", () => window.location = "./users.html");

document.getElementById("lblsessions").addEventListener("click", () => window.location = "./sessions.html");

document.getElementById("lbldashboard").addEventListener("click", () => window.location = "./dashboard.html");

document.getElementById("btnadduser").addEventListener("click", SubmitData);

function SubmitData() {
    let name = document.getElementById("txtname").value;
    let email = document.getElementById("txtemail").value;
    let password = document.getElementById("txtpassword").value;
    let dob = document.getElementById("txtdob").value;
    if (ValidateData(name, email, password, dob)) {
        var normalUser = new NormalUser(name, email, password, dob, 2);
        jsDatabase.push(normalUser);
        localStorage.setItem("jsDatabase", JSON.stringify(jsDatabase));
        alert("Submitted");
        AddRow(normalUser);
    }
}
function ValidateData(name, email, password, dob) {


    if (name === "" || name === null) {
        alert("Please Enter Name");
        return false;
    }
    else if (email === "" || email === null) {
        alert("Please Enter Email");
        return false;
    }
    else if (password === "" || password === null) {
        alert("Please Enter password");
        return false;
    }
    else if (dob == "" || dob === null) {
        alert("Please Enter Date Of Borth");
        return false;
    }
    else {
        return true;
    }
}

function CreateTabble(jsDatabase) {
    var tableRow;
    var tablecol, text;

    for (let obj of jsDatabase) {
        if (obj.userType === 1)
            continue;
        tableRow = document.createElement("tr");
        for (let prop in obj) {
            if (prop === "userType")
                continue;
            tablecol = document.createElement("td");
            text = document.createTextNode(`${obj[prop]}`);
            tablecol.appendChild(text);
            tableRow.appendChild(tablecol);
        
        }
        tablecol = document.createElement("td");
        text = document.createTextNode(`${(new Date().getFullYear() - new Date(obj["dateOfBirth"]).getFullYear()).toString()}`);
        tablecol.appendChild(text);
        tableRow.appendChild(tablecol);

        tablecol = document.createElement("td");
        text = document.createTextNode("Edit");
        tablecol.appendChild(text);
        tableRow.appendChild(tablecol);


        tablecol = document.createElement("td");
        text = document.createTextNode("Delete");
        tablecol.appendChild(text);
        tableRow.appendChild(tablecol);
        document.getElementById("userTable").appendChild(tableRow);
    }
}

function AddRow(obj)
{
    var tableRow=document.createElement("tr");;
    var tablecol, text;
    for (let prop in obj) {
        if (prop === "userType")
            continue;
        tablecol = document.createElement("td");
        text = document.createTextNode(`${obj[prop]}`);
        tablecol.appendChild(text);
        tableRow.appendChild(tablecol);
    }
        tablecol = document.createElement("td");
        text = document.createTextNode(`${(new Date().getFullYear() - new Date(obj["dateOfBirth"]).getFullYear()).toString()}`);
        tablecol.appendChild(text);
        tableRow.appendChild(tablecol);

        tablecol = document.createElement("td");
        text = document.createTextNode("Edit");
        tablecol.appendChild(text);
        tableRow.appendChild(tablecol);


        tablecol = document.createElement("td");
        text = document.createTextNode("Delete");
        tablecol.appendChild(text);
        tableRow.appendChild(tablecol);
        
        document.getElementById("userTable").appendChild(tableRow);
}
