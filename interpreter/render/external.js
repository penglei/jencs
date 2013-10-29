var Scope = require("./scope");

var HNode = require("hdf2json").HNode;
var CSValue = Scope.CSValue;

function subcount(arg){
    if (arg instanceof HNode){
        return new CSValue(CSValue.Number, arg.subcount());
    } else {
        return new CSValue(CSValue.Number, 0);
    }
}

Scope.addExternInterface("subcount", subcount);
