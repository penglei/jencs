var fs = require("fs"),
    path = require("path"),
    hdfParser = require("../../interpreter/hdf");

var content = fs.readFileSync("test.hdf");

var r = hdfParser.parse(content);
console.log(hdfParser.dumpHdf(r));
