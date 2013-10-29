#ifndef __CS_TO_C__TYPE__
#define __CS_TO_C__TYPE__

#define CSVAL_SET_INT(symbol, val) symbol.value.lval = val; \
                                symbol.type = CS_T_INTEGER;

#define CSVAL_SET_STRING(symbol, val) symbol.value.str = val; \
                                symbol.type = CS_T_STRING;

#define cs_val_istrue(val) ((val.type & CS_T_INTEGER && val.value.lval != 0) || \
  (val.type & CS_T_STRING && (val.value.str != NULL && val.value.str[0] != '\0')))

typedef struct {
    HDF *hdf;
    bool cache;
} HDFNode;

typedef enum {
    CS_T_STRING = (1 << 0),
    CS_T_INTEGER = (1 << 1),
    CS_T_FLOAT = (1 << 2)
} cs_val_type;

typedef union {
    long long int lval;
    double dval;
    /*
    struct {
        const char *val;
        int len;
    } str;
    */
    const char *str;
} cs_val_value;

typedef struct {
    cs_val_type type;
    cs_val_value value;
} CSValue;

#endif
