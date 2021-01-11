// //Using Function Statement and Expression
// var functions = function (inputpara)
// {
//     console.log(inputpara);
// }


// //Regular Function
// function functionr(inputpara) 
// {
//     console.log(inputpara);    
// }


// //Function Calls for Output Purpose
// functionr("Jyot");
// functions("Hathi");


// //----------------------------------------------------------------------------

// var arraydata = [5,4,78];

// function printarray (arrayinput)
// {
//     avg=0;
//     for(var i=0;i<arraydata.length;i++)
//     {
//         avg+=arrayinput[i];
//     }
//     return avg;
// }

// console.log(printarray(arraydata));

//----------------------------------------------------------------------------

var teamOfJohn, teamOfMike, teamOfMarry, avgOfJohn = 0, avgOfMike = 0, avgOfMarry = 0;

//------------------------ Array Of Scores of Each Person with Default data
teamOfJohn = [89, 120, 103];
teamOfMike = [116, 94, 123];
teamOfMarry = [97, 134, 105];

//*********************************Part 2 and 4 Calculation for Average Score*******************************************************
avgOfJohn = AverageScore(teamOfJohn);
avgOfMike = AverageScore(teamOfMike);
avgOfMarry = AverageScore(teamOfMarry);

console.log("Score Of John's Team is 89, 120, 103 & Average Score is : " + avgOfJohn);
console.log("Score Of Mike's Team is 116, 94, 123 & Average Score is : " + avgOfMike);
console.log("Score Of John's Team is 105, 134, 105 & Average Score is : " + avgOfMarry);

//-----------------------Part 2 (Best Out of Mike and John With Default Data)
console.log("\nResult of John's Team and Mike's Team (With Given Default Data)\n");
JohnvsMike();

//-----------------------Part 4 (Best Out of Mike,John and John With Default Data)

console.log("\nResult of John's Team, Mike's Team and Marry's Team (With Given Default Data)");
JohnvsMikevsMarry();
//*************************************************************************************************************************************



//****************************************Part 3 (Best Out of Mike and John with cganged Data)****************************************
teamOfJohn = [105, 108, 123, 108, 78];
teamOfMike = [102, 108, 122, 109, 77];

avgOfJohn = AverageScore(teamOfJohn);
avgOfMike = AverageScore(teamOfMike);

console.log("\nScore Of John's Team is 105,108,123,108,78 & Average Score is : " + avgOfJohn);
console.log("Score Of Mike's Team is 102,108,122,109,77 & Average Score is : " + avgOfMike);

JohnvsMike();
//**************************************************************************************************************************************


//-------------------------------------------------Average Function : To Find The Average 
function AverageScore(arrayinput) {
    var avg = 0;
    for (var i = 0; i < arrayinput.length; i++) {
        avg += arrayinput[i];
    }
    return (avg / arrayinput.length);
}


//-------------------------------------------------Function To Compare John and Mike
function JohnvsMike() {

    if (avgOfJohn > avgOfMike) {
        console.log("John's Team is winner and avrage score is " + avgOfJohn + "Points");
    }
    else if (avgOfMike > avgOfJohn) {
        console.log("Mike's Team is winner and avrage score is " + avgOfMike + " Points");
    }
    else {
        console.log("Draw");
    }

}

//-------------------------------------------------Function To Comapre John, Mike and Marry
function JohnvsMikevsMarry() {
    if (avgOfJohn > avgOfMike && avgOfJohn > avgOfMarry) {
        console.log("John's Team is winner with avrage score is " + avgOfJohn + " Points");
    }
    else if (avgOfMike > avgOfMarry) {
        console.log("Mike's Team is Winner with Average Score is " + avgOfMike + " Points");
    }
    else {
        if (avgOfJohn == avgOfMarry && avgOfMarry == avgOfMike) {
            console.log("Draw");
        }
        else {
            console.log("Marry's Taem is Winner with Average Score " + avgOfMarry + " Points")
        }
    }
}


