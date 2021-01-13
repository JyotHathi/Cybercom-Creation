/* 
John Object Which Contains Three arrays

1. restaurantsBills  : Bills of each Place  
2. tipOnBill :Tip on Bill of each place
3. finalPayableAmount : Final Payable amount

4. calculateTip : To Calculate Tip On Bill and Store it in tipOnBill
5. calculateFinalAmount: To Calulate Final Payable Amount
6. displayFinalBill : To Print All Detailed Bills
Note : AMount In Dollars
*/

var john =
{
    //Data Members
    personName:'John',
    restaurantsBills : [124,48,268],
    tipOnBill:[0,0,0],
    finalPayableAmount:[124,48,268],

    //Methods
    
    calculateTip :
    function ()
    {
        var i=0,n=this.restaurantsBills.length,amount=0;
        for(i=0;i<n;i++)
        {
            amount=this.restaurantsBills[i];
            if(amount < 50)
            {
                //tip : 20% of bill
                this.tipOnBill[i]=(amount*20)/100;
            }
            else if(amount>=50 &&  amount <= 200)
            {
                //tip : 15% of bill
                this.tipOnBill[i]=(amount*15)/100;
            }
            else
            {
                //tip: 10 % of bill
                this.tipOnBill[i]=(amount*10)/100;
            }
        }
    },

    calculateFinalAmount : 
    function ()
    {
        var i=0,n=this.restaurantsBills.length;
        for(i=0;i<n;i++)
        {
            this.finalPayableAmount[i]+=this.tipOnBill[i];
        }
    },

    displayFinalBill :
    function ()
    {
        var i=0,n=this.restaurantsBills.length,result="";
        result+="Bill Details of "+this.personName+"\n\n\n";
        for(i=0;i<n;i++)
        {
            result+="---------\nBill-"+(i+1)+":\n---------\n"+ this.restaurantsBills[i] + " $ (Bill Amount)  +  " + this.tipOnBill[i] + " $ (Tip On Bill)  =  " + this.finalPayableAmount[i] + " $ (Final Bill Amount).\n\n"  
        }  
        return result;
    }    
};


//Caling Functions One By One
john.calculateTip();
john.calculateFinalAmount();
console.log(john.displayFinalBill());