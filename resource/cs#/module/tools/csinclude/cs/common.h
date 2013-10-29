/*
 *  common.h
 *  ISDWebCharacter
 *
 *  Created by Xiao Xu on 09-7-13.
 *  Copyright 2009 Tencent.. All rights reserved.
 *
 */

#ifndef _ISDWEBCHARACTER_COMMON_4CS_
#define _ISDWEBCHARACTER_COMMON_4CS_

#include <stdio.h>

#define STRING_DEAL_NO_CHECK 0
#define STRING_DEAL_UTF8_CHECK 1
#define STRING_DEAL_GBK_CHECK 2

/**
 * 错误码定义
 */
enum errorCode4CS {
	ERR_NULL_PARAMS = -100,
	ERR_BUFFER_TOO_SMALL = -99,
	ERR_ENCODE = -98,
	OK = 0
};

/**
 * 字符集检查/容错开关参数
 */
static inline void showVersion4CS(void){
	fprintf(stderr, "ISDWebCharacter: 1.0.1\n");
};

#endif
