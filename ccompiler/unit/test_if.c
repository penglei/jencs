#include "CSTPL_test.h"

void CSTPL_test(HDF *hdf, CS_Render fun) {
    HDFNode root = {hdf, true};
    //cs_poop_t *pool;
    //cs_pool_init(&pool);

    HDFNode bar = NEW_HDFNODE();
    CSValue val_bar = NEW_VALUE();

    FetchHDFNode(&root, "bar", &bar);
    GetHDFValue(&bar, &val_bar);
    if (val_bar == 1) {
        print_string("bar11111");
    } else {
        FetchHDFNode(&root, "foo", &foo);
        GetHDFValue(&foo, &val_foo);
        if (isEqual(&val_bar, &val_foo)){
            print_string("bar22222");
        } else {
            if (val_bar.valInt == 3){
                print_string("bar333333");
            } else {
                print_string("bar444444");
            }
        }
    }
    print_string("\n");

    //cs_pool_destroy(poop);
};
