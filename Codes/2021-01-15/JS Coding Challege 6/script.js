// JS Script for fibonacic Series (JS Coding Challenge 6)
//--------------------------------------------------------

// Binding Click Event of Submit To Function
document.querySelector("#btnlimitinpt").addEventListener('click',calcOutput)

//Fibonacic calc
function calcOutput()
{
    var limit;
    limit=parseInt(document.querySelector("#txtboxlimit").value,10);
    var result="",pprev=0,prev=1,current=1;
    if(!limit)
        limit=5;
    for(var i=1;i<=limit;i++)
    {
        result+=current.toString()+", ";
        current=pprev+prev;
        pprev=prev;
        prev=current; 
    }
    result+="...";
    document.querySelector("#poutput").innerHTML=result;
}