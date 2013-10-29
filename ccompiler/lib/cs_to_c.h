#ifndef __CS_TO_C__HEADER__
#define __CS_TO_C__HEADER__

#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "util/neo_misc.h"
#include "util/neo_err.h"
#include "util/ulist.h"
#include "util/neo_hdf.h"
#include "util/neo_str.h"

#include "cs_type.h"

//初始化一个数据节点
#define HDF_NODE(name) {NULL, false}
#define NEW_HDFNODE() HDF_NODE("")
#define NEW_VALUE() {CS_T_INTEGER, 0}
#define INIT_VALUE(type, val) {type, val}

#define MAX_INT_STRLEN 24

typedef void (*CS_Render)(char *);

/**
 *在某个节点下获取某个节点，如果获取成功，则返回true，否则返回false
 */
bool FetchHDFNode(HDFNode *, const char *, HDFNode *);
bool FetchHDFNodeByCSValue(HDFNode *, CSValue *, HDFNode *);

/**
 * 获取某个节点上的值
 */
const char* GetHDFValue(HDFNode *, CSValue *);

bool FetchOrNewHDFNode(HDFNode *, const char *, HDFNode *);
bool FetchOrNewHDFNodeByCSValue(HDFNode *, CSValue *, HDFNode *);

bool SetHDFValue(HDFNode *, const char *);
bool SetHDFCSValue(HDFNode *, CSValue *);


/////helper util/////
int cs_parseInt(const char *);

/**
 * 输出节点上的值
 */
void print_node(HDFNode *);

/**
 * 输出一个CS模板里对象的值
 */
void print_value(CSValue *);

/**
 * 输出字符串
 */
void print_string(char *);

//执行二元运算操作
void add_operate(CSValue *, CSValue *, CSValue *);
void sub_operate(CSValue *, CSValue *, CSValue *);
void mul_operate(CSValue *, CSValue *, CSValue *);
void div_operate(CSValue *, CSValue *, CSValue *);
void mod_operate(CSValue *, CSValue *, CSValue *);

bool cs_isEqual(CSValue *, CSValue *);
bool cs_isBigger(CSValue *, CSValue *);
bool cs_isSmaller(CSValue *, CSValue *);
int cs_compare(CSValue *, CSValue *);

char *cs_str_concat(const char *str1, const char *str2);
#endif
