'use strict';
module.exports = {
  up: async (queryInterface, Sequelize) => {
    await queryInterface.createTable('Users', {
      UserId: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      UserName: {
        type: Sequelize.STRING(50),
        allowNull: false,
      },
      Email:{
        type: Sequelize.STRING(100),
        allowNull: false,
        unique:true
      },
      ContactNumber:{
        type: Sequelize.STRING(15),
        allowNull: false
      },
      Password:{
        type: Sequelize.STRING,
        allowNull: false,
      },
      IsEmailVerified:{
        type: Sequelize.BOOLEAN,
        allowNull: false,
        defaultValue:false,
      },
      IsActive:{
        type: Sequelize.BOOLEAN,
        allowNull: false,
        defaultValue:true
      },
      createdAt: {
        allowNull: false,
        type: Sequelize.DATE
      },
      updatedAt: {
        allowNull: false,
        type: Sequelize.DATE
      }
    });
  },
  down: async (queryInterface, Sequelize) => {
    await queryInterface.dropTable('Users');
  }
};