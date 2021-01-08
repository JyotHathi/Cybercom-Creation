var teamOfJohn, teamOfMike, teamOfMarry, avgOfJohn = 0, avgOfMike = 0, avgOfMarry = 0;

//------------------------------- Array Of Scores of Each Person with Default data
teamOfJohn = [89, 120, 103];
teamOfMike = [116, 94, 123];
teamOfMarry = [97, 134, 105];

for (var i = 0; i < teamOfJohn.length; i++) {
    avgOfJohn += teamOfJohn[i];
    avgOfMike += teamOfMike[i];
    avgOfMarry += teamOfMarry[i];
}

avgOfJohn /= teamOfJohn.length;
avgOfMike /= teamOfMike.length;
avgOfMarry /= teamOfMarry.length;

console.log("Score Of John's Team is 89, 120, 103 & Average Score is : " + avgOfJohn);
console.log("Score Of Mike's Team is 116, 94, 123 & Average Score is : " + avgOfMike);
console.log("Score Of John's Team is 105, 134, 105 & Average Score is : " + avgOfMarry);

console.log("\nResult of John's Team and Mike's Team (With Given Default Data)\n");
JohnvsMike();

console.log("\nResult of John's Team, Mike's Team and Marry's Team (With Given Default Data)");
JohnvsMikevsMarry();
JohnvsMike2();

//-----------------------Part 2 (Best Out of Mike and John)
//Function To Compare John and Mike
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
//----------------------Part 4 (Best Out of Mike, john and Marry)
//Function To Comapre John, Mike and Marry
function JohnvsMikevsMarry()
{
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

//----------------------Part 3 (Best Out of John and Mike With Chnage in Default Score)
function JohnvsMike2()
{
teamOfJohn = [105, 108, 123,108,78];
teamOfMike = [102, 108, 122,109,77];

avgOfJohn = 0;
avgOfMike = 0;

for (var i = 0; i < teamOfJohn.length; i++) {
    avgOfJohn += teamOfJohn[i];
    avgOfMike += teamOfMike[i];
}
avgOfJohn /= teamOfJohn.length;
avgOfMike /= teamOfMike.length;

console.log("\nScore Of John's Team is 105,108,123,108,78 & Average Score is : " + avgOfJohn);
console.log("Score Of Mike's Team is 102,108,122,109,77 & Average Score is : " + avgOfMike);

JohnvsMike();
}


