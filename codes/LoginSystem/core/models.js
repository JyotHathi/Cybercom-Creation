/*****************************************************************************************************
                            Import Section
 *****************************************************************************************************/
const path = require('path');
const fs = require('fs');
const db = {};
const dirName = path.join(__dirname, '../db/models/');
const basename = "index.js";
const Sequelize = require('sequelize');
const sequelize = require('../core/dbConnection');

/*****************************************************************************************************
                            Logic to get List of Models
 *****************************************************************************************************/
fs
  .readdirSync(dirName)
  .filter(file => {
    return (file.indexOf('.') !== 0) && (file !== basename) && (file.slice(-3) === '.js');
  })
  .forEach(file => {
    const model = require(path.join(dirName, file))(sequelize, Sequelize.DataTypes);
    db[model.name] = model;
  });

/*******************************************************************************************************
                                Export Section
*******************************************************************************************************/
module.exports = db;