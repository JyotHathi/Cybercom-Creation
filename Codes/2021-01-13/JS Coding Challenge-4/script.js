/*
 Have Persons With 
 1. Full Name
 2. Mass (in Kgs)
 3. Height (in metrs)
 4. BMI (in Kg/metr2)

 5.Calculate BMI
 6.PrintDetails
-------------------------------
 Person Details :
 1. Full Name : John Smith
    Mass : 30kg
    Height : 8m

2.  Full Name : Mark Roy
    Mass : 50kg
    Height : 7.5m
-------------------------------
*/


//------------Person Object Created Which is COmmon FOr All Person
var Person = 
{
    //Data members
    fullName:"",
    mass : 0,
    height : 0,
    BMI:0,

    //Methods
    calculateBMI :
    function ()
    {
        return (this.mass / (this.height * this.height));
    },

    displayDetails:
    function ()
    {
        var result="";
        result+="\nFull Name : " + this.fullName + "\nMass : " +this.mass+"Kgs\nHeight : " + this.height +"m\nBMI : " + this.BMI + " Kg / m^2.\n-------------------------------------\n"
        return result;
    }

};

// Filling Value of John Smith
var john = Object.create(Person);
john.fullName="John Smith";
john.mass=30;
john.height=8;
john.BMI=john.calculateBMI();


// Filling Value of Mark Roy
var mark=Object.create(Person);
mark.fullName="Mark Roy";
mark.mass=50;
mark.height=7.5;
mark.BMI=mark.calculateBMI();


//Dispalying Details OF Both The Person
console.log("Records:\n")
console.log("-------------------------------------\n1. "+john.displayDetails());
console.log("-------------------------------------\n2. "+mark.displayDetails());


