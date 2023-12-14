const express = require('express');
const router = express.Router();

router.get('/', (req, res) => {
  res.render('index')
});

router.get('/tes', (req, res) => {
  res.render('tes')
});

module.exports = router;