//Using Function Statement and Expression
var functions = function (inputpara)
{
    console.log(inputpara);
}


//Regular Function
function functionr(inputpara) 
{
    console.log(inputpara);    
}


//Function Calls for Output Purpose
functionr("Jyot");
functions("Hathi");


//----------------------------------------------------------------------------

var arraydata = new Array(5,4,78);

function printarray (array)
{
    for(var i=0;i<array.Length;i++);
    {
        console.log("array["+i+"] : "+array[i]);
    }
}

printarray.apply(arraydata);