require('dotenv').config();

const express = require('express');
const path = require('path');
const indexRoutes = require('./routes/indexRoutes');
const itemRoutes = require('./routes/itemRoutes');
const participantRoutes = require('./routes/participantRoutes');
const winnerRoutes = require('./routes/winnerRoutes');

const app = express();

app.set('view engine', 'ejs');
app.use(express.static(path.join(__dirname, 'public')));

app.listen(3000);

app.use(indexRoutes);
app.use('/item', itemRoutes);
app.use('/participant', participantRoutes);
app.use('/winner', winnerRoutes);