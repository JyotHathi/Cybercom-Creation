using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Practice_Code1_ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            // To Set Basic ColorCodes of Input, Background and To Set Title
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "Practice Code1 - ATM Transactions";
            Console.Clear();

            bool? isQuit = null;
            byte userChoice;
            AtmUsers user;
            int amount;
            do
            {
                userChoice = WelocmeMethod();
                switch (userChoice)
                {
                    case 1:
                        user = new AtmUsers();
                        user.CreateAccount();
                        break;
                    case 2:
                        user = AtmUsers.IsUserExists();
                        if (user != null)
                        {
                            byte userSubChoice;
                            do
                            {
                                UserWait();
                                userSubChoice = LoginChoice(user.AtmUser.UserName);
                                switch (userSubChoice)
                                {
                                    case 1:
                                        SuccessMessage($"Your Balance is {user.AtmUser.UserBalance}");
                                        break;
                                    case 2:
                                        OutputText("Please Enter Amount: ");
                                        if (int.TryParse(Console.ReadLine(), out amount))
                                        {
                                            user.CashWithdrawal(amount);
                                        }
                                        else
                                        {
                                            ErrorMessage("Please Enter Valid Amount");
                                        }
                                        break;
                                    case 3:
                                        OutputText("Please Enter Amount: ");
                                        if (int.TryParse(Console.ReadLine(), out amount))
                                        {
                                            user.CashDeposition(amount);
                                        }
                                        else
                                        {
                                            ErrorMessage("Please Enter Valid Amount");
                                        }
                                        break;
                                    case 4:
                                        SuccessMessage("Logging out Successfully");
                                        user = null;
                                        break;
                                }
                            } while (userSubChoice != 4);
                        }
                        else
                        {
                            ErrorMessage("Invalid PIN or First Register Your Self");
                        }
                        break;
                    case 3:
                        OutputText("Please Enter Admin Pin: ");//ADMIN PIN FOR SHUT DOWN OR TERMINATE APP IS 20210201
                        if (!Console.ReadLine().Equals("20210201"))
                        {
                            isQuit = false;
                            ErrorMessage("Invalid Pin");
                        }
                        else
                        {
                            isQuit = true;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Shuuting Down");
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        break;
                }
                UserWait();
            } while (isQuit != true);

        }
        #region Methods

        //-----------------------------------To Print Error Messages
        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\a" + message);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        //-----------------------------------To Clean Screen
        public static void UserWait()
        {
            Console.WriteLine("Please Wait.....");
            Console.CursorVisible = false;
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            Console.CursorVisible = true;
        }

        //----------------------------------To Print Succes Messages
        static public void SuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        //---------------------------------To Print Output Messages
        static public void OutputText(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        //--------------------------------------Welcome Messages
        /// <summary>
        /// Welocme Method For;<br />1.Display Greeting and Choices<br />2. To Take User Choice for Transaction
        /// </summary>
        /// <returns></returns>
        static public byte WelocmeMethod()
        {
            byte userChoice = 0;
            bool userDoneChoice;

            // Loop Till User Not Enter Valid Choice
            do
            {
                //Greeting Message and Choices
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Welocome To Our ATM");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Please Choose the Option to proceed Further" +
                    "\n\n" +
                    "1.Create Account\n" +
                    "2.Login\n" +
                    "3.Shut Down the ATM\n");
                Console.ForegroundColor = ConsoleColor.Black;
                // To Take Choice and To Validate it
                userDoneChoice = (Byte.TryParse(Console.ReadLine(), out userChoice)) && (userChoice <= 3 && userChoice >= 1);

                if (!userDoneChoice) //Process If Choice Is Not Valid
                {
                    ErrorMessage("Please Enter Valid Choice");
                    Console.CursorVisible = false;
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }

            } while (!userDoneChoice); // Loop Continue Till User's Choice is Not Valid.

            return userChoice;
        }

        //-------------------------------------Choices After Login
        static public byte LoginChoice(string name)
        {
            byte userChoice = 0;
            bool userDoneChoice;

            // Loop Till User Not Enter Valid Choice
            do
            {
                //Choices
                SuccessMessage("---------------------------------------------------------------");
                SuccessMessage($"Welcome {name}");
                SuccessMessage("---------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Please Choose the Option to proceed Further" +
                    "\n\n" +
                    "1.Check Balance\n" +
                    "2.Cash Withdrawal\n" +
                    "3.Cash Deposition\n" +
                    "4.Quit\n");
                Console.ForegroundColor = ConsoleColor.Black;
                // To Take Choice and To Validate it
                userDoneChoice = (Byte.TryParse(Console.ReadLine(), out userChoice)) && (userChoice <= 4 && userChoice >= 1);

                if (!userDoneChoice) //Process If Choice Is Not Valid
                {
                    ErrorMessage("Please Enter Valid Choice");
                    Console.CursorVisible = false;
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }

            } while (!userDoneChoice); // Loop Continue Till User's Choice is Not Valid.

            return userChoice;
        }

        #endregion
    }
    //------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------

    /// <summary>
    /// Interface That Gives List of Basic Methods Which All Atms Need To Implement
    /// </summary>
    //-------------------------------------Interface IUniAtmTransactions-----------------------------
    interface IUniAtmTransactions
    {
        /// <summary>
        /// To Create Account or Add/Register New User
        /// </summary>
        void CreateAccount();
        /// <summary>
        /// To Check User's Current Balance
        /// </summary>
        int CheckBalance();
        /// <summary>
        /// For Cash Withdrawal Process By User
        /// </summary>
        void CashWithdrawal(int amount);
        /// <summary>
        /// For Cash Deposition Process By User
        /// </summary>
        void CashDeposition(int amount);
    }

    /// <summary>
    /// An Abstract Class Which Gives Details About Particualr ATM, 
    /// Whihc Inherited From IUniAtmTransactions and Have Own New Methods for Particular ATM.
    /// </summary>
    //---------------------------------------- class AtmTransaction ---------------------------------
    abstract class AtmTransaction : IUniAtmTransactions
    {
        #region Data Members
        /// <summary>
        /// Minimum Withdrawal amount<br />Default: 100 rupees
        /// </summary>
        public static readonly int minWithdrawalAmount = 100;

        /// <summary>
        /// Maximum Withdrawal amount<br />Default: 5000 rupees
        /// </summary>
        public static readonly int maxWithsdrawalAmount = 5000;

        /// <summary>
        /// Minimum Balance amount<br />Default: 500 rupees
        /// </summary>
        public static readonly int minBalance = 500;
        #endregion

        #region Methods Form IUniAtmTransactions (All Are virtual)
        public virtual void CreateAccount()
        {

        }
        public virtual int CheckBalance()
        {
            return 0;
        }
        public virtual void CashWithdrawal(int amount)
        {

        }
        public virtual void CashDeposition(int amount)
        {

        }
        #endregion

        #region New Methods Which Cant Not override by child
        /// <summary>
        /// To Check that is amount Satisfy Minimum Balance as per Bank Rules
        /// </summary>
        /// <value>
        /// <code>true</code>:If Amount Grater or Equal to Minimum Balance Amount
        /// <code>false</code>: If Amount less then Minimum Balcnce Amount
        /// </value>
        /// <param name="balance"></param>
        /// <returns></returns>
        public static bool IsMinBalanceMaintain(int balance)
        {
            return balance >= minBalance ? true : false;
        }

        /// <summary>
        /// To Check Whether Amount is Greter Than Minimum Withdrawal Amount 
        /// </summary>
        /// <value>
        /// <code>true</code>:If Amount Grater or Equal to Minimum Withdrawal Amount
        /// <code>false</code>: If Amount less then Minimum Withdrawal Amount
        /// </value>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool IsMinWithdrawalAmount(int amount)
        {
            return amount >= minWithdrawalAmount ? true : false;
        }

        /// <summary>
        /// To Check Whether Amount is Greter Than Maximum Withdrawal Amount 
        /// </summary>
        /// <value>
        /// <code>true</code>:If Amount Grater then Maximum Withdrawal Amount
        /// <code>false</code>: If Amount less or Equalto Maximum Withdrawal Amount
        /// </value>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool IsMaxWithDrawalAmount(int amount)
        {
            return amount > maxWithsdrawalAmount ? true : false;
        }
        #endregion

    }

    /// <summary>
    /// Class For Individual ATM User which Inherited From AtmTransaction
    /// </summary>
    /// <remarks>
    /// It Has Structure Which Contain User Details. 
    /// </remarks>

    //--------------------------------------- class AtmTransaction------------------------------------
    class AtmUsers : AtmTransaction
    {
        User atmUser;
        public static AtmUsers[] users = new AtmUsers[0];
        public AtmUsers()
        {
            atmUser = new User();
        }
        public AtmUsers(User user)
        {
            this.atmUser = user;
        }
        public User AtmUser
        {
            get { return this.atmUser; }
            set
            {
                this.atmUser = value;
            }
        }
        public override void CreateAccount()
        {

            try
            {
                Program.OutputText("Please Enter Your Name: ");
                atmUser.UserName = Console.ReadLine();
                Program.OutputText("Please Enter Your Mobile Number: ");
                atmUser.MobileNumber = Console.ReadLine();
                Program.OutputText("Please Enter Your User Pin: ");
                atmUser.UserPin = Console.ReadLine();
                Program.OutputText("Please Enter Your Balance: ");
                atmUser.UserBalance = Convert.ToInt32(Console.ReadLine());
                Program.OutputText("Please Enter Withdrawal Amount: ");
                atmUser.UserWithdrawalAmount = Convert.ToInt32(Console.ReadLine());

                Array.Resize(ref User.usedUserPins, User.usedUserPins.Length + 1);
                User.usedUserPins[User.usedUserPins.Length - 1] = atmUser.UserPin;


                Array.Resize(ref users, users.Length + 1);
                users[users.Length - 1] = this;
                Program.SuccessMessage("User Created Successfully\nMessage Sent To Your Mobile Number.");

            }
            catch (InvalidUserInputException ex)
            {
                Program.ErrorMessage(ex.Message + "\n" + "User Registration Failed\nTry Again");
            }
            catch
            {
                Program.ErrorMessage("Invalid Input" + "\n" + "User Registration Failed\nTry Again");
            }
        }
        public override int CheckBalance()
        {
            return this.atmUser.UserBalance;
        }
        public override void CashDeposition(int amount)
        {
            this.atmUser.UserBalance += amount;
            Program.SuccessMessage($"{amount} Deposited Sucessfully\n" +
                                                $"SMS Sent on your number {AtmUser.MobileNumber}");
        }
        public override void CashWithdrawal(int amount)
        {
            if (atmUser.UserWithdrawalAmount >= amount && atmUser.UserBalance >= amount)
            {
                if (atmUser.UserBalance - amount >= AtmTransaction.minBalance)
                {
                    this.atmUser.UserBalance -= amount;
                    Program.SuccessMessage($"{amount} Withdrawaled Sucessfully\n" +
                                                    $"SMS Sent on your number {AtmUser.MobileNumber}");
                }
                else
                {
                    Program.ErrorMessage("Sorry Can Not Proceed Transaction, Minimum Blance Have To Maintain");
                }
            }
            else
            {
                Program.ErrorMessage("Sorry Can Not Proceed Transaction, Transaction Amount Exceeds Limit");
            }
        }
        public static AtmUsers IsUserExists()
        {
            Program.OutputText("Please Enter Your PIN: ");
            string userpin = Console.ReadLine();
            AtmUsers resultuser = null;
            foreach (AtmUsers user in users)
            {
                if (user.atmUser.UserPin == userpin)
                {
                    resultuser = user;
                }
            }
            return resultuser;
        }

    }

    /// <summary>
    /// Structure User is to store User Details
    /// </summary>

    //--------------------------------------- Structure User -----------------------------------------
    struct User
    {
        string userName;
        string mobileNumber;
        string userPin;
        int userBlance;
        int userWithdrawalAmount;
        public static string[] usedUserPins = new string[0];

        public string UserName
        {
            get { return this.userName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.userName = value;
                }
                else
                {
                    throw new InvalidUserInputException("InvalidUserInputException for User Name value");
                }
            }
        }

        /// <summary>
        /// Mobile Number Must be of 10 Digits only
        /// </summary>
        public string MobileNumber
        {
            get { return this.mobileNumber; }
            set
            {
                Regex mobilenumberregex = new Regex("^[0-9]{10}$"); // Regex To Validate Mobile Number
                if (!string.IsNullOrEmpty(value) && mobilenumberregex.IsMatch(value))
                {
                    this.mobileNumber = value;
                }
                else
                {
                    throw new InvalidUserInputException("InvalidUserInput Exception for MobileNumber");
                }
            }
        }

        /// <summary>
        /// UserPin Must be of 4-Digits Only as Well Not Start With Zero
        /// </summary>
        public string UserPin
        {
            get { return this.userPin; }
            set
            {

                Regex userPinRegex = new Regex("^[1-9][0-9]{3}$");
                if (userPinRegex.IsMatch(value) && !usedUserPins.Contains(value))
                {
                    this.userPin = value;
                }
                else
                {
                    throw new InvalidUserInputException("InvalidUserInput Exception for User Pin \n " +
                        "Invalid Pin or This User Pin is Not Available");
                }
            }
        }

        /// <summary>
        /// User Balance is Must Be Grater or Equal 500
        /// </summary>
        public int UserBalance
        {
            get { return userBlance; }
            set
            {
                if (AtmTransaction.IsMinBalanceMaintain(value))
                {
                    this.userBlance = value;
                }
                else
                {
                    throw new Exception($"InvalidUserException Amount is Less then Minimum Balance\n it Must Be Greater on Equal {AtmTransaction.minBalance}");
                }
            }
        }

        /// <summary>
        /// UserWithdrawal Amount Must be Between 100 - 5000 only
        /// </summary>
        public int UserWithdrawalAmount
        {
            get { return userWithdrawalAmount; }
            set
            {
                if (AtmTransaction.IsMinWithdrawalAmount(value) && !AtmTransaction.IsMaxWithDrawalAmount(value))
                {
                    this.userWithdrawalAmount = value;
                }
                else
                {
                    throw new InvalidUserInputException($"InvalidUserInputException \n withdrawal Amount must Between {AtmTransaction.minWithdrawalAmount}-{AtmTransaction.maxWithsdrawalAmount}");
                }
            }
        }
    }

    /// <summary>
    /// This Custome Exception,When Invalid Input is Enter By User
    /// </summary>

    //-----------------------------------Class (Exception) InvalidUserInput---------------------------
    class InvalidUserInputException : Exception
    {
        public InvalidUserInputException() : base() { }

        public InvalidUserInputException(string message) : base(message) { }

        public InvalidUserInputException(string message, Exception inerrexception) : base(message, inerrexception) { }

    }
}
