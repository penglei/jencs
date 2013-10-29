#include "CSTPL_test.h"

void CSTPL_test(HDF *hdf, CS_Render fun) {
    HDFNode root = {hdf, true};
    //cs_poop_t *pool;
    //cs_pool_init(&pool);

    HDFNode hn_one = NEW_HDFNODE();
    CSValue val_0_hn_one = NEW_VALUE();
    bool bool_bs0_0 = true, bool_bs0_1 = true, bool_bs0_2 = true, bool_bs0_3 = true, bool_bs0_4 = true, bool_bs0_5 = true, bool_bs0_6 = true, bool_bs0_7 = true;

    FetchHDFNode(&root, "one", &hn_one);
    GetHDFValue(&hn_one, &val_0_hn_one);
    if (cs_val_istrue(val_0_hn_one)){
        print_string("\n	one condition\n");
    }
    print_string("\n");
    do {
        HDFNode hn_foo = NEW_HDFNODE(), hn_kbp = NEW_HDFNODE();
        CSValue val_1_hn_foo = NEW_VALUE(), val_1_hn_kbp = NEW_VALUE();

        FetchHDFNode(&root, "foo", &hn_foo);
        GetHDFValue(&hn_foo, &val_1_hn_foo);
        if (cs_val_istrue(val_1_hn_foo)) break;
        FetchHDFNode(&root, "kbp", &hn_kbp);
        GetHDFValue(&hn_kbp, &val_1_hn_kbp);
        if (cs_val_istrue(val_1_hn_kbp)) break;
        bool_bs0_0 = false;
    } while(0);
    if (bool_bs0_0){
        print_string("\n	two condition\n");
    }
    print_string("\n\n");
    do {
        HDFNode hn_foo = NEW_HDFNODE(), hn_bar = NEW_HDFNODE(), hn_gar = NEW_HDFNODE();
        CSValue val_1_hn_foo = NEW_VALUE(), val_1_hn_bar = NEW_VALUE(), val_1_hn_gar = NEW_VALUE();

        FetchHDFNode(&root, "foo", &hn_foo);
        GetHDFValue(&hn_foo, &val_1_hn_foo);
        if (cs_val_istrue(val_1_hn_foo)) break;
        FetchHDFNode(&root, "bar", &hn_bar);
        GetHDFValue(&hn_bar, &val_1_hn_bar);
        if (cs_val_istrue(val_1_hn_bar)) break;
        FetchHDFNode(&root, "gar", &hn_gar);
        GetHDFValue(&hn_gar, &val_1_hn_gar);
        bool_bs0_1 = false;
    } while(0);
    if (bool_bs0_1){
        print_string("\n	three condition\n");
    }
    print_string("\n\n");

    print_string("\n\n");

    print_string("\n	true constant\n");

    print_string("\n\n");
    do {
        HDFNode hn_foo_left_false = NEW_HDFNODE();
        CSValue val_1_hn_foo_left_false = NEW_VALUE();

        FetchHDFNode(&root, "foo_left_false", &hn_foo_left_false);
        GetHDFValue(&hn_foo_left_false, &val_1_hn_foo_left_false);
        if (cs_val_istrue(val_1_hn_foo_left_false)) break;
        bool_bs0_4 = false;
    } while(0);
    if (bool_bs0_4){
        print_string("\n	constant left false and hdf\n");
    }
    print_string("\n\n");
    do {
        HDFNode hn_foo_left_true = NEW_HDFNODE();
        CSValue val_1_hn_foo_left_true = NEW_VALUE();

        FetchHDFNode(&root, "foo_left_true", &hn_foo_left_true);
        GetHDFValue(&hn_foo_left_true, &val_1_hn_foo_left_true);
        if (cs_val_istrue(val_1_hn_foo_left_true)) break;
        bool_bs0_5 = false;
    } while(0);
    if (bool_bs0_5){
        print_string("\n	constant left true and hdf\n");
    }
    print_string("\n\n");
    do {
        HDFNode hn_foo1 = NEW_HDFNODE();
        CSValue val_1_hn_foo1 = NEW_VALUE();

        FetchHDFNode(&root, "foo1", &hn_foo1);
        GetHDFValue(&hn_foo1, &val_1_hn_foo1);
        if (cs_val_istrue(val_1_hn_foo1)) break;
        bool_bs0_6 = false;
    } while(0);
    if (bool_bs0_6){
        print_string("\n	constant right false and hdf\n");
    }
    print_string("\n\n");
    do {
        HDFNode hn_foo2 = NEW_HDFNODE();
        CSValue val_1_hn_foo2 = NEW_VALUE();

        FetchHDFNode(&root, "foo2", &hn_foo2);
        GetHDFValue(&hn_foo2, &val_1_hn_foo2);
        if (cs_val_istrue(val_1_hn_foo2)) break;
        bool_bs0_7 = false;
    } while(0);
    if (bool_bs0_7){
        print_string("\n	constant right true and hdf\n");
    }
    print_string("\n");

    //cs_pool_destroy(poop);
};
