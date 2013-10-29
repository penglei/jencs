#include "CSTPL_test.h"

void CSTPL_test(HDF *hdf, CS_Render fun) {
    HDFNode root = {hdf, true};
    //cs_poop_t *pool;
    //cs_pool_init(&pool);

    HDFNode x = NEW_HDFNODE(), x_yy = NEW_HDFNODE(), bbb = NEW_HDFNODE(), x_yy_ww = NEW_HDFNODE();
    CSValue val_x = NEW_VALUE(), value1 = NEW_VALUE();

    print_string("\n\n\n");
    FetchOrNewHDFNode(&root, "x", &x);
    SetHDFValue(&x, "11");
    do {
        FetchHDFNode(&x, "yy", &x_yy);
    }while (0);
    GetHDFValue(&x, &val_x);
    if (val_x.type == CS_T_INTEGER){
        value1.valInt = val_x.valInt + 11;
        value1.type = CS_T_INTEGER;
    } else {
        value1.str = cs_str_concat(val_x.str, "11");
        value1.type = CS_T_STRING;
    }
    FetchHDFNode(&root, "bbb", &bbb);
    print_string("\n    \n    \n    define macro start\n    \n    ");
    FetchOrNewHDFNode(&root, "p1", &x_yy);
    FetchOrNewHDFNode(&x_yy, "ww", &x_yy_ww);
    SetHDFValue(&x_yy_ww, "wwwwwww");
    FetchHDFNode(&root, "p2", &value1);
    print_node(&value1);
    print_string("\n    define macro end\n    \n");
    print_string("\n");
    print_node(&x);
    print_string("\n");

    //cs_pool_destroy(poop);
};
