

var fs = require("fs"),
    path = require("path"),
    hdfParser = require("hdf2json");

var content = fs.readFileSync("data.hdf");

debugger
var r = hdfParser.parse(content);
console.log(r);
