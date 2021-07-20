/*****************************************************************************************************
                            Import Section
 *****************************************************************************************************/
const { createToken, verifyToken } = require('../../core/functions');

/*****************************************************************************************************
                            Middleware Functions
 *****************************************************************************************************/

//craete JWT Token;
const createJWTToken = function (userId, response) {
    return response.cookie('Authentication',
        createToken({ userId: userId }, process.env.AUTH_TOKEN_CRYPT_KEY, process.env.AUTH_TOKEN_EXPIRATION),
        {
            expires: new Date(Date.now() + parseInt(process.env.AUTH_COOKIE_EXPIRATION_TIME)),
            secure: false,
            httpOnly: true,
        });
}

// Verfiy Token which is Passed By User
const verfyJWTToken = async function (request, response, next) {
    const token = request.cookies.Authentication;
    if (token && token != "") {
        const isTokenValid = await verifyToken(token, process.env.AUTH_TOKEN_CRYPT_KEY);
        if (isTokenValid != null) {
            next();
        }
        else {
            response.status(200).redirect('/login');
        }
    }
    else {
        response.status(200).redirect('/login');
    }

}

// To Destroy Cookie Which Contains Token
const destroyJWTTokenAuth = function (request, response, next) {
    response.cookie("Authentication", "", {
        expires: new Date(Date.now()),
        secure: false,
        httpOnly: true,
    });
    next();
}

/*******************************************************************************************************
                                Export Section
*******************************************************************************************************/
module.exports = { verfyJWTToken, createJWTToken, destroyJWTTokenAuth }