require('dotenv').config();

const express = require('express');
const path = require('path');
const indexRoutes = require('./routes/indexRoutes');

const app = express();

app.set('view engine', 'ejs');
app.use(express.static(path.join(__dirname, 'public')));

app.listen(4000);

app.use(indexRoutes);