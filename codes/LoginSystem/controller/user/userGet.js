// Get User Home Page
const getUserHomePage=function(request,response,next)
{
    response.status(200).render('user/home',{title:'Home',layout:'layouts/userLayout'})
}

// Log Out 
const loggingOutUser=function(request,response,next)
{
    response.redirect('/login');
}

module.exports={getUserHomePage,loggingOutUser};