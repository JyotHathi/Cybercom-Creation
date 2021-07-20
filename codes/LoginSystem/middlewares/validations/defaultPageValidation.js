/*****************************************************************************************************
                            Import Section
 *****************************************************************************************************/
const { check } = require('express-validator');

/*****************************************************************************************************
                            Middleware Functions
 *****************************************************************************************************/
function validateDefaultPage(validateArea) {
    switch (validateArea) {
        case 'login':
            return [
                check('UserId', 'UserId Is Required').isLength({ min: 1 }),
                check('Password', 'Password Is Required').isLength({ min: 1 })
            ];
        case 'register': return [
            check('UserName', "Please Enter UserName").exists().isLength({ min: 1 }),
            check('UserName', "UserName Must Contain Alphabets and Space Only").matches("^[A-Za-z\\s]+$"),
            check('Email', "Please Enter Email").exists().isLength({ min: 1 }),
            check('Email', "Please Enter Valid Email").exists().isEmail(),
            check('ContactNumber', "Please Enter Contact Number").exists().isLength({ min: 1 }),
            check('ContactNumber', "Please Enter Contact Number Properly (Digits Only and Length Must Be 10)").matches("^[1-9][0-9]{9}$"),
            check('Password', 'Please Enter Password').exists().isLength({ min: 1 }),
            check("Password", "Passowrd Must Follow Constraint: Require At least one digit, one lowercase, one uppercase, one special character, length 8-32 ").isStrongPassword(),
            check('ConfPassword', "Please Enter Confirm Password").exists().isLength({ min: 1 }),
            check('ConfPassword').custom(
                async (ConfPassword, { req }) => {
                    const passowrd = req.body.Password;
                    if (passowrd != ConfPassword) {
                        throw new Error("Passowrd and Confirm Password are Mis-Matched")
                    }
                })
        ]
        case 'resend_email': return [
            check('Email_For_Verify', "Please Enter Email for Verficaton").exists().isLength({ min: 1 }),
            check('Email_For_Verify', "Please Enter Email Properly for Verficaton").isEmail()
        ]
    }
}

/*******************************************************************************************************
                                Export Export Section
*******************************************************************************************************/
module.exports = { validateDefaultPage };