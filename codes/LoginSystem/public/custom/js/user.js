// Document Ready
$(document).ready(function(){
    setGreetings();
})

// Set GreetingMessage()
function setGreetings(){
    const greetingLine=document.getElementById('greetingLine');
    if(greetingLine && greetingLine!=null)
    {
        const currentDate=new Date();
        const currentHour=currentDate.getHours();
        if(currentHour>=0 && currentHour<12)
        {
            greetingLine.textContent="Hello, Good Morning";
            greetingLine.className="text-primary";
        }    
        else if(currentHour>=12 && currentHour<17){
            greetingLine.textContent="Hello, Good Noon";
            greetingLine.className="text-warning";
        }
        else{
            greetingLine.textContent="Hello, Good Evening";
            greetingLine.className="text-success";   
        }
    }
}
