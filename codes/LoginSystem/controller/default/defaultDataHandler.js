/***************************************************************************************************
                                Basic Imports
***************************************************************************************************/

const { validationResult } = require('express-validator');
const basicUserOpsService = require("../../service/user/userService");
const { verficationMail, verifyAccount } = require('../../core/functions');
const jwtTokenAuth=require("../../middlewares/Authentication/authenticate_jwt_token"); // JWT Based Authentication
const sessionAuth=require("../../middlewares/Authentication/authentication_session"); // Session Based Auth
/***************************************************************************************************
                                Functions
***************************************************************************************************/
// Handle Register User Route:'/register'
const registerUser = async function (request, response, next) {
    const errors = validationResult(request).errors;
    const data = {
        UserName: request.body.UserName,
        Email: request.body.Email,
        ContactNumber: request.body.ContactNumber
    };
    if (errors != null && errors.length != 0) {
        response.status(403).render('default/register', { layout: 'layouts/defaultLayout', title: 'Register', errors: errors, data: data });
    }
    else {
        const user = {
            UserName: request.body.UserName,
            Email: request.body.Email,
            ContactNumber: request.body.ContactNumber,
            Password: request.body.Password
        }
        const responseData = await basicUserOpsService.createUser(user);
        if (responseData != null) {
            await verficationMailHandling(response,responseData);
        }
        else {
            response.status(403).render('default/register', { layout: 'layouts/defaultLayout', title: 'Register', errors: [{ msg: "User Already Registered With Email" }], data: data });
        }
    }
}

// Handle Login User Route:'/login'
const loginUser = async function (request, response, next) {
    const errors = validationResult(request).errors;
    if (errors != null && errors.length != 0) {
        response.status(403).render('default/login', { layout: 'layouts/defaultLayout', title: 'Login', errors: errors, successRegistration: false })
    }
    else {
        const userIsValid = await basicUserOpsService.loginUser(
            {
                UserId: request.body.UserId,
                Password: request.body.Password,
            }
        );
        if (userIsValid) {
            // Adding Token In Case of JWT Token Managment

            //response=jwtTokenAuth.createJWTToken(request.body.UserId,response); // -- USING JWT TOKEN AUTHENTICATION
            request=sessionAuth.createSession(request); //-- Using Session Authentication

            request.url="http://localhost:5000/user";
            response.status(200).render('user/home', { layout: 'layouts/userLayout', title: 'Home' });
        }
        else {
            const error =
            {
                msg: "User Id or Password is Invalid or Kindly Verify Email First."
            }
            response.status(200).render('default/login', { layout: 'layouts/defaultLayout', title: 'Login', successRegistration: false, errors: [error] });
        }

    }
}

// Is User Exits : For Api testing
const isUserExists = async function (request, response, next) {

    const isUser = await basicUserOpsService.userExits(request.query.email);
    response.status(200).send({ isUser: isUser });
}

// Verify Account
const verfifyAccount = async function (request, response, next) {
    const getToken = request.query.token;
    const isVerfied = await verifyAccount(getToken);
    if (isVerfied === true) {
        response.redirect('/login');
    }
    else {
        response.status(200).render('default/login', { title: 'Login', layout: 'layouts/defaultLayout', successRegistration: false, errors: [{ msg: "Verification Failed,Contact Admin Team" }] })
    }
}

// Resend Email Verfication
const resendEmailVerification = async function (request, response) {
    const errors = validationResult(request).errors;
    if (errors && errors != null && errors.length != 0) {
        console.log(errors);
        response.status(200).render('default/login', { title: 'Login', layout: 'layouts/defaultLayout', successRegistration: false, errors: errors })
    }
    else {
        const email = request.body.Email_For_Verify;
        const userExits = await basicUserOpsService.userExits(email);
        if (userExits === true) {
            const userData = await basicUserOpsService.isUserVerified(email);
            if (userData != null) {
                await verficationMailHandling(response,userData);   
            }
            else {
                response.status(200).render('default/login', { title: 'Login', layout: 'layouts/defaultLayout', successRegistration: false, errors: [{ msg: "User Already Verified." }] })
            }
            
        }
        else {
            response.status(200).render('default/login', { title: 'Login', layout: 'layouts/defaultLayout', successRegistration: false, errors: [{ msg: "User Not Registered." }] })
        }

    }

}
/*******************************************************************************************************
                                Simplified Functions Section
*******************************************************************************************************/
// Send Email of Verfication
async function verficationMailHandling(response,userData) {
    const isMailSent = await verficationMail(userData);
    if (isMailSent === true) {
        response.status(200).render('default/login', { title: 'Login', layout: 'layouts/defaultLayout', successRegistration: true, errors: null })
    }
    else {
        const error = { msg: "Issue In Mail sending Kindly Click on Resend Verification Email" }
        response.status(200).render('default/login', { layout: 'layouts/defaultLayout', title: 'Login', successRegistration: false, errors: [error] });
    }   
}

/*******************************************************************************************************
                                Export Section
*******************************************************************************************************/
module.exports = { loginUser, registerUser, isUserExists, verfifyAccount, resendEmailVerification };