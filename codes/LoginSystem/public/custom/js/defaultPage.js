
// Load File
$(document).ready(function () {
    ResetRegisterControls();
})

// Reset Login Control
function ResetLoginControls() {

    const userId = document.getElementById("UserId");
    const password = document.getElementById("Password");
    if (userId) {
        userId.value = "";
    }
    if (password) {
        password.value = "";
    }
}

// Reset Register Control Button
function ResetRegisterControls() {

    const userId = document.getElementById("Pre_Fill_Added_Data");
    if (document.getElementById("Pre_Fill_Added_Data")===null) {
        const userName = document.getElementById("UserName");
        const email = document.getElementById("Email");
        const ContactNumber = document.getElementById("ContactNumber");
        const Password = document.getElementById("Password");
        const ConfPassword = document.getElementById("ConfPassword");

        if (ConfPassword) {
            ConfPassword.value = "";
        }
        if (Password) {
            Password.value = "";
        }
        if (ContactNumber) {
            ContactNumber.value = "";
        }
        if (email) {
            email.value = "";
        }
        if (userName) {
            userName.value = "";
        }
    }

    const errorSummary = document.getElementById("registerErrorSummary");

    if (errorSummary) {
        errorSummary.style = "display:none"
    }


}

// While Submit of Registration Page ---- Validation For Form 
function onRegisterSubmit() {
    const userName = document.getElementById("UserName");
    const ContactNumber = document.getElementById("ContactNumber");
    const Password = document.getElementById("Password");
    const ConfPassword = document.getElementById("ConfPassword");
    const errorSummary = document.getElementById("registerErrorSummary");
    let isValid = true;
    errorSummary.innerHTML = "";

    let userNamePattern = new RegExp("^[A-Za-z\\s]+$");
    let contactPattern = new RegExp("^[1-9][0-9]{9}$");
    let passwordPattern = new RegExp("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

    if (userName && userNamePattern.test(userName.value) === false) {
        errorSummary.innerHTML += "<li>Please Enter Name Properly (Only Alphabets and Space)</li>"
        isValid = false;
    }
    if (ContactNumber && contactPattern.test(ContactNumber.value) === false) {
        errorSummary.innerHTML += "<li>Please Enter Contact Number Properly (Only Digits and Length Must be 10)</li>"
        isValid = false;
    }
    if (Password && passwordPattern.test(Password.value) === false) {
        errorSummary.innerHTML += `<li>Passowrd Must Follow Constraint: Require At least one digit, one lowercase, one uppercase, one special character, length 8-32 </li>`
        isValid = false;
    }
    if (Password && ConfPassword && Password.value != ConfPassword.value) {
        errorSummary.innerHTML += `<li>Passowrd and Confirm Password are Mis-Matched </li>`
        isValid = false;
    }

    // Errors Displaying Based on Result
    if (isValid === false) {
        errorSummary.style = "display:block";
    }
    else {
        errorSummary.style = "display:none";
    }
    return isValid;
}

// Check for Existanse
$("#Email").change(function () {
    $.ajax({
        url: 'http://localhost:5000/api/isUserExists?email=' + document.getElementById("Email").value,
        method: 'GET',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            const errorSummary = document.getElementById("registerErrorSummary");
            errorSummary.innerHTML = false;
            console.log(data);
            if (data != null && data.isUser === true) {
                errorSummary.innerHTML = "<li>User Already Exists With Entered Email</li>";
                errorSummary.style = "display:block";
            }
            else {
                errorSummary.style = "display:none";
            }
        },
        error: function (data) {
            alert("Error Occured So Please Try Again, By Refereshing Page Or Closing Web");
        }
    });
});

// Clear Email field of Modal
function clearEmailField()
{
    const email=document.getElementById("Email_For_Verify");
    if(email)
    {
        email.value="";
    }
}