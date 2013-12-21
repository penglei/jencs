jencs
=====

clearsilver template engine with debugger

##usage

    Usage: jencs <path> [data] [options]

    path     clearsilver template file
    data     hdf data file

    Options:
       --debug               enable debugger  [false]
       --debug-brk           enable debugger and break on first line.  [false]
       --include             the dir of include command <?cs include:... ?>  [.]
       --ignore-whitespace   ignore \r\n\t character in clearsilver template file  [false]
       --port                web listening port of debugger server  [10080]

use `make test` to run example. Visit http://127.0.0.1:10080/ to start debugging
