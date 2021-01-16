//--------------------------------JS File for HTML Practice 6

// Binding Event click of sendnow button with function
document.querySelector("#btnsend").addEventListener('click',alertShow);

//Function Which is envoke when click event of send now button wiil invoke
function alertShow()
{
    alert("Your Request Has Been Submitted!");
}

// Function When Page Loaded or Refresh
function ResetWeb()
{
    document.querySelector("#txtboxname").value="";
    document.querySelector("#txtboxemail").value="";
    document.querySelector("#txtboxcontact").value="";
    document.querySelector("#txtareacomments").value="";
}


