/*****************************************************************************************************
                            Import Section
 *****************************************************************************************************/


/*****************************************************************************************************
                            Middleware Functions
 *****************************************************************************************************/

//create Session Varibale
const createSession = function (request) {
    request.session.UserId = request.body.UserId;
    return request;
}

//Check For Authentication Using Session
const authValidateSession = function (request, response, next) {
    const userId = request.session.UserId;
    if (userId && userId != null) {
        next();
    }
    else {
        response.status(200).redirect('/login');
    }
}

// Destroy Auth-Session 
const destroySession = function (request, response, next) {
    request.session.UserId = null;
    next();
}

/*******************************************************************************************************
                                Export Section
*******************************************************************************************************/

module.exports = { createSession, authValidateSession, destroySession }