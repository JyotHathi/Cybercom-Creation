/***************************************************************************************************
                                Basic Imports
***************************************************************************************************/
const Express = require('express');
const router = Express.Router();

// Default Routes Related Import
const getRequestHandlerDefault = require("../controller/default/defaultGet");
const dataRelatedRequestHandlerDefault = require("../controller/default/defaultDataHandler");
const validationDefault = require("../middlewares/validations/defaultPageValidation");

// User Route Related Import
const getRequestsHandlerUser = require("../controller/user/userGet");

//Authentication Middlewares
const jwtAuth = require("../middlewares/Authentication/authenticate_jwt_token"); // JWT Based Authentication
const sessionAuth = require("../middlewares/Authentication/authentication_session"); // Session Based Auth

/***************************************************************************************************
                                Setting Up Routes
***************************************************************************************************/
//-------------------------------- Default Routes 

// Route: ''
router.get('/', getRequestHandlerDefault.getLoginPage); //-- Done

// GET_Route :'/login'
router.get('/login', getRequestHandlerDefault.getLoginPage); //-- Done

// GET_Route :'/register'
router.get('/register', getRequestHandlerDefault.getRegisterPage); //-- Done

// POST_Route :'/login'
router.post('/login', validationDefault.validateDefaultPage('login'), dataRelatedRequestHandlerDefault.loginUser);

// POST_Route :'/register'
router.post('/register', validationDefault.validateDefaultPage('register'), dataRelatedRequestHandlerDefault.registerUser);

// POST_Route :'/isUserExists'
router.get('/api/isUserExists', dataRelatedRequestHandlerDefault.isUserExists);

// GET_Route:'/verifyAccount'
router.get('/verifyAccount', dataRelatedRequestHandlerDefault.verfifyAccount)

// POST_Route:/email-verfication
router.post('/email-verfication', validationDefault.validateDefaultPage('resend_email'), dataRelatedRequestHandlerDefault.resendEmailVerification)

//------------------------------------ User Routes 

/*
//---JWT Based Auth---
// Routes :'/user/'
router.get('/user/',jwtAuth.verfyJWTToken,getRequestsHandlerUser.getUserHomePage)

// Routes :'/user/home'
router.get('/user/home',jwtAuth.verfyJWTToken,getRequestsHandlerUser.getUserHomePage)

// Routes :'/logout'
router.get('/user/logout',jwtAuth.destroyJWTTokenAuth,getRequestsHandlerUser.loggingOutUser) 
*/

///*
//--- Session Based Auth ---
// Routes :'/user/'
router.get('/user/', sessionAuth.authValidateSession, getRequestsHandlerUser.getUserHomePage)

// Routes :'/user/home'
router.get('/user/home', sessionAuth.authValidateSession, getRequestsHandlerUser.getUserHomePage)

// Routes :'/logout'
router.get('/user/logout', sessionAuth.destroySession, getRequestsHandlerUser.loggingOutUser)
// Routes*/

/*******************************************************************************************************
                                Export Section
*******************************************************************************************************/

module.exports = router;