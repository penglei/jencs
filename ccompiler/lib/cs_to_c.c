#include <assert.h>
#include "cs_to_c.h"

bool FetchHDFNode(HDFNode *parent, const char *name, HDFNode *target)
{
    assert(parent->cache == true);
    assert(target != NULL);

    target->cache = true;
    if (target == NULL || name == NULL) {
        //这是有可能的，比如 foo[notExist]
        return false;
    }
    if (parent == NULL || parent->hdf == NULL) {
        //parent->hdf为null说明这个节点是不存在的
        //当然也要把标志置true
        //如果通过 obj[nameObj]修改了target（逻辑上target的hdf有值），就需要更新这个标志
        //所有修改hdf的操作都是通过set指令进行，
        //当然也要考虑api直接修改hdf数据的方式（暂无）
        target->hdf = NULL;
        return false;
    }
    target->hdf = hdf_get_obj(parent->hdf, name);
    return target->hdf != NULL;
}

bool FetchHDFNodeByCSValue(HDFNode *parent, CSValue *val, HDFNode *target){
    if (val->type & CS_T_STRING) {
        return FetchHDFNode(parent, val->value.str, target);
    } else if (val->type & CS_T_INTEGER){
        char buffer[MAX_INT_STRLEN] = {0};
        sprintf(buffer, "%d", val->value.lval);
        return FetchHDFNode(parent, buffer, target);
    }
    return false;
}

bool FetchOrNewHDFNode(HDFNode *parent, const char *name, HDFNode *target){
    if (parent && parent->hdf){
        NEOERR *err = hdf_get_node(parent->hdf, name, &(target->hdf));
        if (err){
            nerr_log_error(err);
        }
    }
    target->cache = true;
    return true;
}

bool FetchOrNewHDFNodeByCSValue(HDFNode *parent, CSValue *val, HDFNode *target){
    if (val->type & CS_T_STRING){
        return FetchOrNewHDFNode(parent, val->value.str, target);
    } else if (val->type == CS_T_INTEGER){
        char buffer[MAX_INT_STRLEN] = {0};
        sprintf(buffer, "%d", val->value.lval);
        return FetchOrNewHDFNode(parent, buffer, target);
    } else if (val->type & CS_T_FLOAT){
        //XXX
    }
}

const char* GetHDFValue(HDFNode *hdfNode, CSValue *valResult){
    valResult->type = CS_T_STRING;
    if (hdfNode->hdf == NULL) return NULL;
    const char *tmp = hdf_obj_value(hdfNode->hdf);
    valResult->value.str = tmp;
    return tmp;
}

//TODO hdf的内存管理是如何做的呢？会不会出现内存泄漏
bool SetHDFValue(HDFNode *hdfNode, const char *val){
    if(hdfNode && hdfNode->hdf){
        NEOERR *err = hdf_set_value(hdfNode->hdf, NULL, val);
        if (err){
            nerr_log_error(err);
            return false;
        }
        return true;
    }
    return false;
}

bool SetHDFCSValue(HDFNode *hdfNode, CSValue *csval) {
    assert(csval);
    if(hdfNode && hdfNode->hdf){
        if (csval->type & CS_T_STRING){
            return SetHDFValue(hdfNode, csval->value.str);
        } else if (csval->type & CS_T_INTEGER){
            char buffer[MAX_INT_STRLEN] = {0};
            sprintf(buffer, "%d", csval->value.lval);
            //TODO 用内存池分配函数
            const char *str = (char *) malloc(strlen(buffer) + 1);
            return SetHDFValue(hdfNode, str);
        }
        return true;
    }
    return false;
}

void print_node(HDFNode *hdfNode){
    if (hdfNode && hdfNode->hdf && hdfNode->hdf->value){
        printf(hdfNode->hdf->value);
    }
}

void print_value(CSValue *csval){
    if (!csval) return;
    if (csval->type & CS_T_STRING){
        if (csval->value.str) printf("%s", csval->value.str);
    } else if (csval->type & CS_T_INTEGER) {
        printf("%d", csval->value.lval);
    } else if (csval->type & CS_T_FLOAT){
        printf("%8.8f", csval->value.dval);
    }
}

void print_string(char *str){
    if (str != NULL) printf(str);
}

//operator
//两个hdf数据相加
void add_operate(CSValue *left, CSValue *right, CSValue *result)
{
    //TODO 如何处理浮点数?
    //说明都是数字
    if (left->type & CS_T_STRING && right->type & CS_T_STRING){
        //这种情况就做字符串连接
        int size = 0;
        char *str = NULL;
        if (left->value.str) {
            size += strlen(left->value.str);
        }
        if (right->value.str) {
            size += strlen(right->value.str);
        }
        if (size > 0){
            //TODO pool使用
            str = (char *) malloc(size + 1);
            if (left->value.str) strcpy(str, left->value.str);
            if (right->value.str) strcat(str, right->value.str);
            result->value.str = str;
            result->type = CS_T_STRING;
        }
    } else if (left->type & CS_T_INTEGER){
        if (right->type & CS_T_STRING){
            result->value.lval = left->value.lval + cs_parseInt(right->value.str);
        } else {//Interger
            result->value.lval = left->value.lval + right->value.lval;
        }
        result->type = CS_T_INTEGER;
    } else if (right->type & CS_T_INTEGER){
        //有一个是数字，就要按照数字处理
        if (left->type & CS_T_STRING){
            result->value.lval = cs_parseInt(left->value.str) + right->value.lval;
        } else {
            result->value.lval = left->value.lval + right->value.lval;
        }
        result->type = CS_T_INTEGER;
    } else {
        //这应该是浮点数处理，的确有点麻烦
    }
}

int cs_parseInt(const char *str){
    if (str){
        return atoi(str);
    }
    return 0;
}

char *cs_str_concat(const char *str1, const char *str2)
{
    int str1_len = 0, str2_len = 0, size;
    if (str1){
        str1_len = strlen(str1);
    }
    if (str2){
        str2_len = strlen(str2);
    }
    size = str1_len + str2_len;

    if (size){
        //TODO 内存池支持字符串的释放
        char *temp = (char *) malloc(size + 1);
        memset(temp, 0, size + 1);
        if (str1) strcpy(temp, str1);
        if (str2) strcat(temp, str2);
        return temp;
    }
    return NULL;
}


bool cs_isEqual(CSValue *leftVal, CSValue *rightVal){
    return cs_compare(leftVal, rightVal) == 0;
}

bool cs_isBigger(CSValue *leftVal, CSValue *rightVal){
    return cs_compare(leftVal, rightVal) == 1;
}

bool cs_isSmaller(CSValue *leftVal, CSValue *rightVal){
    return cs_compare(leftVal, rightVal) == -1;
}

int cs_compare(CSValue *leftVal, CSValue *rightVal){
    if (leftVal->type & CS_T_STRING && rightVal->type & CS_T_STRING){
        if (leftVal->value.str && rightVal->value.str){//这是
            return strcmp(leftVal->value.str, rightVal->value.str);
        }
        if (leftVal->value.str && !rightVal->value.str){
            return 1;
        } else if (rightVal->value.str && !leftVal->value.str){
            return -1;
        } else {//空值
            return 0;
        }
    } else if (leftVal->type & CS_T_INTEGER){
        if (rightVal->type & CS_T_INTEGER){
            if (leftVal->value.lval > rightVal->value.lval) return 1;
            else if (leftVal->value.lval < rightVal->value.lval) return -1;
            else return 0;
        } else {
            if (rightVal->value.str){
                //必须转换成字符来比较
                char leftValStrbuffer[MAX_INT_STRLEN];
                sprintf(leftValStrbuffer, "%d", leftVal->value.lval);
                return strcmp(leftValStrbuffer, rightVal->value.str);
            } else {
                if (leftVal->value.lval) return 1;
                else return 0;//0 和 ""值谁大？还是相等
            }
        }
    } else if (rightVal->type & CS_T_INTEGER){
        //leftVal肯定不是CS_T_INTEGER了
        if (leftVal->value.str){
            char rightValStrbuffer[MAX_INT_STRLEN];
            sprintf(rightValStrbuffer, "%d", rightVal->value.lval);
            return strcmp(leftVal->value.str, rightValStrbuffer);
        } else {
            if (leftVal->value.lval) return 1;
            else return 0;
        }
    }
}
