/*****************************************************************************************************
                            Import Section
 *****************************************************************************************************/
// To Create Custom Error
const Error = require('http-errors');

/*****************************************************************************************************
                            Middleware Functions
 *****************************************************************************************************/

// Summary : In case No Middleware is there To Handle Request
const unHandledRequest = function (request, response, next) {
    {
        const error = new Error("UnHandled Error");
        error.status = 403;
        next(error);
    }
}

// Summary: To Handle Error Globally.
const handleError = function (error, request, response, next) {
    const errorResponse = {
        "code": error.status || 500,
        "details": error.message || "Internal Server Error"
    }
    response.status(error.status || 500).render("error/error", { error: errorResponse, title: "Error", layout: "layouts/nolayout" });
}

/*******************************************************************************************************
                                Export Section
*******************************************************************************************************/

// Exporing Middleware's Functions
module.exports = { unHandledRequest, handleError }