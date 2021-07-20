/*****************************************************************************************************
                                Include/Import Needed Modules                    
*************************************************************************************************** */
const jwt = require('jsonwebtoken');
const nodeMailer = require("nodemailer");
const basicUserService = require("../service/user/UserService");

/*****************************************************************************************************
                                Functions                    
*************************************************************************************************** */

// Get Mailer
const mailer = function () {
    let mailer = nodeMailer.createTransport(
        {
            service: 'gmail',
            auth: {
                user: process.env.TEAM_EMAILID,
                pass: process.env.EMAIL_PASSWORD
            }
        }
    );
    return mailer;
}

// MailFunction For Verification of User
const verficationMail = async function (data) {
    const recipent = data.Email;
    var isMailSent = true;
    const verifyAccount = { UserId: data.UserId };

    const text = `Kindly Click this Link To Verify:
    ${process.env.PROTOCOL}://${process.env.HOST}:${process.env.PORT}/verifyAccount?token=${createToken(verifyAccount, process.env.EMAIL_TOKEN_CRYPT_KEY, process.env.EMAIL_TOKEN_EXPIRATION)}`;

    mail = {
        from: process.env.TEAM_EMAILID,
        to: recipent,
        subject: process.env.EMAIL_VERFICATION_SUBJECT,
        text: text
    };
    isMailSent = await mailer().sendMail(mail).then((response) => { return true; })
        .catch((err) => { console.log(err.message); return false });
    return isMailSent;

}

// Verify Account
const verifyAccount = async function (token) {
    var isVerified = false;
    const decodedData = await verifyToken(token, process.env.EMAIL_TOKEN_CRYPT_KEY);
    if (decodedData != null) {
        const userId = decodedData.UserId;
        isVerified = await basicUserService.verifyAndUpdateAccount(userId);
    }
    return isVerified;
}

// create token
const createToken = function (data, key, expiry) {
    return jwt.sign(data, key, { expiresIn: expiry });
}

// Verify Token
const verifyToken = async function (token, key) {
    let tokenValid = null;
    tokenValid = await jwt.verify(token, key, async (error, decode) => {
        if (error === null) {
            return decode;
        }
        else {
            console.log(error.message);
            return null;
        }
    });
    return tokenValid;
}


/*****************************************************************************************************
                                Export Section
 ****************************************************************************************************/
module.exports = {
    verficationMail, verifyAccount, createToken, verifyToken
}