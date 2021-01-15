//***************************************Function Testing************************************/
// // //Using Function Statement and Expression
// // var functions = function (inputpara)
// // {
// //     console.log(inputpara);
// // }


// // //Regular Function
// // function functionr(inputpara) 
// // {
// //     console.log(inputpara);    
// // }


// // //Function Calls for Output Purpose
// // functionr("Jyot");
// // functions("Hathi");


// // //----------------------------------------------------------------------------

// // var arraydata = [5,4,78];

// // function printarray (arrayinput)
// // {
// //     avg=0;
// //     for(var i=0;i<arraydata.length;i++)
// //     {
// //         avg+=arrayinput[i];
// //     }
// //     return avg;
// // }

// // console.log(printarray(arraydata));

//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//***************************************Practice Problem 3************************************/
// var teamOfJohn, teamOfMike, teamOfMarry, avgOfJohn = 0, avgOfMike = 0, avgOfMarry = 0;

// //------------------------ Array Of Scores of Each Person with Default data
// teamOfJohn = [89, 120, 103];
// teamOfMike = [116, 94, 123];
// teamOfMarry = [97, 134, 105];

// //*********************************Part 2 and 4 Calculation for Average Score*******************************************************
// avgOfJohn = AverageScore(teamOfJohn);
// avgOfMike = AverageScore(teamOfMike);
// avgOfMarry = AverageScore(teamOfMarry);

// console.log("Score Of John's Team is 89, 120, 103 & Average Score is : " + avgOfJohn);
// console.log("Score Of Mike's Team is 116, 94, 123 & Average Score is : " + avgOfMike);
// console.log("Score Of John's Team is 105, 134, 105 & Average Score is : " + avgOfMarry);

// //-----------------------Part 2 (Best Out of Mike and John With Default Data)
// console.log("\nResult of John's Team and Mike's Team (With Given Default Data)\n");
// JohnvsMike();

// //-----------------------Part 4 (Best Out of Mike,John and John With Default Data)

// console.log("\nResult of John's Team, Mike's Team and Marry's Team (With Given Default Data)");
// JohnvsMikevsMarry();
// //*************************************************************************************************************************************



// //****************************************Part 3 (Best Out of Mike and John with cganged Data)****************************************
// teamOfJohn = [105, 108, 123, 108, 78];
// teamOfMike = [102, 108, 122, 109, 77];

// avgOfJohn = AverageScore(teamOfJohn);
// avgOfMike = AverageScore(teamOfMike);

// console.log("\nScore Of John's Team is 105,108,123,108,78 & Average Score is : " + avgOfJohn);
// console.log("Score Of Mike's Team is 102,108,122,109,77 & Average Score is : " + avgOfMike);

// JohnvsMike();
// //**************************************************************************************************************************************


// //-------------------------------------------------Average Function : To Find The Average 
// function AverageScore(arrayinput) {
//     var avg = 0;
//     for (var i = 0; i < arrayinput.length; i++) {
//         avg += arrayinput[i];
//     }
//     return (avg / arrayinput.length);
// }


// //-------------------------------------------------Function To Compare John and Mike
// function JohnvsMike() {

//     if (avgOfJohn > avgOfMike) {
//         console.log("John's Team is winner and avrage score is " + avgOfJohn + "Points");
//     }
//     else if (avgOfMike > avgOfJohn) {
//         console.log("Mike's Team is winner and avrage score is " + avgOfMike + " Points");
//     }
//     else {
//         console.log("Draw");
//     }

// }

// //-------------------------------------------------Function To Comapre John, Mike and Marry
// function JohnvsMikevsMarry() {
//     if (avgOfJohn > avgOfMike && avgOfJohn > avgOfMarry) {
//         console.log("John's Team is winner with avrage score is " + avgOfJohn + " Points");
//     }
//     else if (avgOfMike > avgOfMarry) {
//         console.log("Mike's Team is Winner with Average Score is " + avgOfMike + " Points");
//     }
//     else {
//         if (avgOfJohn == avgOfMarry && avgOfMarry == avgOfMike) {
//             console.log("Draw");
//         }
//         else {
//             console.log("Marry's Taem is Winner with Average Score " + avgOfMarry + " Points")
//         }
//     }
// }

//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//***************************************Array Revision************************************/

// var array = ["Jyot","Hathi",23,01,2000];

// console.log(array);

// array[7]="Cybercom-Creation";
// console.log("Element at 7th Pos");
// console.log(array);

// array.push("Kachchh");
// console.log("Added Element by push");
// console.log(array);

// array.unshift("Er.")
// console.log("Added Element by unshift");
// console.log(array);

// array.shift();
// console.log("Element Removed by shift");
// console.log(array);

// array.pop();
// console.log("Element Removed by pop");
// console.log(array);

// console.log("Element Removed by pop: "+array.pop());
// console.log("Element Removed by pop: "+array.shift());
// console.log("Element by push: "+array.push(".."));
// console.log(array);
// console.log("Element by push: "+array.unshift(".."));
// console.log(array);
// console.log("Element by push: "+(array[5]=".."));
// console.log(array);
//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//***************************************Scoping Practice************************************
// first.params="Hello";
// console.log(first.params);
// function first()
// {
//     console.log("Function first : "+first.params);
//     first.second=second;
    
//     function second()
//     {
//         console.log("Function second");
//     }       
// }
// first();
// first.second();
// function third()
// {
//     console.log("Function third");
    
//     function fourth()
//     {
//         console.log("Function forth");
//     }   
// }
//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//***************************************Object Practice************************************


// var person = {
//     unmae:"",
//     yod:0,
//     calcage:function () {
    
//         console.log(2021-this.yod);
//     }    
// };

// var john =Object.create(person);
// john.unmae="John";
// john.yod=1938;
// john.calcage();

// person.calcage();
//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------
//***************************************Practice 15th Jan 2021************************************

document.querySelector("#myp").innerHTML="Hello";
console.log(document.querySelector("#h1").innerHTML);    

// Function Cons
document.querySelector("#txtboxtesting").innerHTML="Jyot Hathi"
var dataname = function()
{
    return "Hello";
}
var Person = function (name,age)
{
    this.name=name;
    this.Age=age;
}
Person.prototype.Admin = "Jyot Hathi";

var User1= new Person('jyoth001',21);


console.log(User1);
console.log(User1.Admin);

// create method

var person2 = {
    name:"",
    Age:0,
    Admin:"JyotHathi"
};
var user2= Object.create(person2,{name:{value:"JyotHathi"},Age:{value:23}});
console.log(user2);
console.log(user2.Admin);


