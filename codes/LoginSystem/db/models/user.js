'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class User extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  };
  User.init({
    UserId: {
      allowNull: false,
      autoIncrement: true,
      primaryKey: true,
      type: DataTypes.INTEGER
    },
    UserName: {
      type: DataTypes.STRING(50),
      allowNull: false,
    },
    Email: {
      type: DataTypes.STRING(100),
      allowNull: false,
      isUnique:true
    },
    ContactNumber: {
      type: DataTypes.STRING(15),
      allowNull: false
    },
    Password: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    IsEmailVerified: {
      type: DataTypes.BOOLEAN,
      allowNull: false,
      defaultValue: false
    }, IsActive: {
      type: DataTypes.BOOLEAN,
      allowNull: false,
      defaultValue: true,
    },
  }, {
    sequelize,
    modelName: 'User'
  });
  return User;
};