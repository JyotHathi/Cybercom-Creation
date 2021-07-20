/*****************************************************************************************************
                            Import Section
 *****************************************************************************************************/
require('dotenv').config();
const Op = require("sequelize");
const models = require('../../core/models');
const User = models.User;
const crypt = require('crypto');
const algorithm = process.env.ALGORITHM
const key = process.env.PASSWORD_CRYPT_KEY.slice(0, 32);;
const iv = process.env.INITIAL_VECTOR.slice(0, 16);

/*****************************************************************************************************
                            Service Functions
 *****************************************************************************************************/
//--------------------------- Register User
const createUser = async function (user) {

    const isUserExists = await userExits(user.Email);
    if (!isUserExists) {
        let cipher = crypt.createCipheriv(
            algorithm,
            Buffer.from(key),
            iv);
        let encrypted = cipher.update(user.Password);
        encrypted = Buffer.concat([encrypted, cipher.final()]);

        const response = await User.create({
            UserName: user.UserName,
            Email: user.Email,
            ContactNumber: user.ContactNumber,
            Password: encrypted.toString('hex')
        });

        return {
            UserId: response.dataValues.UserId,
            Email: response.dataValues.Email,
        };
    }
    else {
        return null;
    }

}

//------------------------ Login User
const loginUser = async function (userData) {
    let cipher = crypt.createCipheriv(
        algorithm,
        Buffer.from(key),
        iv);
    let encrypted = cipher.update(userData.Password);
    encrypted = Buffer.concat([encrypted, cipher.final()]);

    const response = await User.count({
        where: {
            Email: userData.UserId,
            Password:encrypted.toString('hex'),
            IsEmailVerified:true
        }
    });
    return response === 1 ? true : false;

}

// To check User is Exists Or Not
const userExits = async function (email) {
    const usersCount = await User.count({
        where: {
            Email: email,
        }
    });
    return usersCount != 0 ? true : false;
}


// Update User For Verify Account
const verifyAndUpdateAccount=async function(userId) {
    try
    {
        const user=await User.update({IsEmailVerified:true},{where:{UserId:userId}});
        if(user[0]!=0)
            return true;
        else 
            return false;
    }
    catch
    {
        return false;
    }
}

const isUserVerified = async function(email) {
    const userVerfied=await User.findAll({where: {Email:email,IsEmailVerified:true}});
    if(userVerfied.length > 0)
    {
        return null;
    }
    else{
        const userData=(await User.findAll({where: {Email:email}}))[0].dataValues;
        return {UserId:userData.UserId,Email:userData.Email}
    }
}
/*******************************************************************************************************
                                Export Session
*******************************************************************************************************/
module.exports = { createUser, loginUser, userExits,verifyAndUpdateAccount,isUserVerified };