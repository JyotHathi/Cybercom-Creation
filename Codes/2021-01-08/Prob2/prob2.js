var massOfJohn, massOfMark, heightOfJohn, heightOfMark, bmiOfJohn, bmiOfMark, isMarkBmiHigher;

//---------------To Take Inputs of Johns from  prompt
alert("Please Enter Details of John");
massOfJohn = prompt("Please Enter Mass Of John (In Kgs)");
heightOfJohn = prompt ("Please Enter Heigt of John (In Mets)");

//--------------To Take Inputs of Mark from Prompt
alert("Please Enter Details of Mark");
massOfMark  = prompt("Please Enter Mass Of Mark (In Kgs)");
heightOfMark = prompt ("Please Enter Heigt of Mark (In Mets)");

//-------------Calculations of BMI
bmiOfJohn = massOfJohn / (heightOfJohn * heightOfJohn );
bmiOfMark = massOfMark / (heightOfMark * heightOfMark );

//------------Checking for Mark's BMI with JOhn
isMarkBmiHigher = bmiOfMark > bmiOfJohn ;

//-----------Output Message
console.log("Is BMI of Mark's heigher than Johan's BMI ? " + isMarkBmiHigher );

