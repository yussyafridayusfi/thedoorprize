const express = require('express');
const router = express.Router();

router.get('/', (req, res) => {
  res.render('item')
});

module.exports = router;
