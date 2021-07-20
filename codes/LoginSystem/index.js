/*****************************************************************************************************
                            Imporing Modules and Declaration
 * **************************************************************************************************/

// .env Loading
require('dotenv').config();

// Built-In Modules
const Express = require('express');
const app = Express();
const port = process.env.PORT || 5000
const path = require('path');
const expressEjsLayouts = require('express-ejs-layouts');
const requestHandler = require('./middlewares/error/errorHandler');
const routes = require('./core/routes');
const cors = require("cors");
const cookie_parser = require("cookie-parser");
const Session = require("express-session")

/******************************************************************************************************
                                Middlewares Section
 ****************************************************************************************************/

// Seting View Folder Path,View Engine and layout
app.set('views', path.join(__dirname, './views'));
app.set('view engine', 'ejs');
app.set('layout', path.join(__dirname, './views/layouts/defaultLayout.ejs'));

// Adding Middlewares in Pipe Line
app.use(Express.static('./public'));
app.use(expressEjsLayouts);
app.use(Express.json());
app.use(Express.urlencoded({ extended: false }));
app.use(cors());
app.use(cookie_parser());

//------------------- To Use Session ----------
app.use(Session(
    {
        secret: process.env.SESSION_SECRET_KEY,
        saveUninitialized: true,
        resave: true,
        name: "Login System",
        cookie: { maxAge: parseInt(process.env.AUTH_COOKIE_EXPIRATION_TIME) }
    }
));

app.use(routes);
app.use(requestHandler.unHandledRequest);
app.use(requestHandler.handleError);

// Starting Server
app.listen(port, () => { console.log(`Server is Running On Port: ${port}`) });

