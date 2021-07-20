require('dotenv').config();
const Sequelize = require('sequelize');
const env = process.env.NODE_ENV; // Get Node Environment
let dbConfig;

// Load Configuration for Database Connectivity
if (env === "production") {
    dbConfig = {
        "username": process.env.DB_USERNAME,
        "password": process.env.DB_PASSWORD,
        "database": process.env.DB_NAME,
        "host": process.env.DB_HOST,
        "port": process.env.DB_PORT,
        "dialect": process.env.DB_DIALECT
    }
}
else {
    dbConfig = require('../config/config.json')[env];
}

// Testing Connection 
const database = new Sequelize(dbConfig);
database
    .authenticate()
    .then(() => {
        console.log("Database Connected");
    })
    .catch((err) => {
        console.log(err.message);
    });

//---------------------------------------------------------------------------------
module.exports = database;