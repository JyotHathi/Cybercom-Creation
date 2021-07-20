// Login Page
const getLoginPage=function(request, response,next){
    response.status(200).render('default/login',{title:'Login',layout:'layouts/defaultLayout',successRegistration:false,errors:null})
}

// Register Page
const getRegisterPage=function(request,response,next){
    response.status(200).render('default/register',{title:'Register',layout:'layouts/defaultLayout',errors:null,data:{UserName:"",Email:"",ContactNumber:""}})
}

module.exports={getLoginPage,getRegisterPage};