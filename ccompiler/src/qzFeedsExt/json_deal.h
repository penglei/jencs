/*
 *  str.h
 *  ISDWebCharacter
 *
 *  Created by Xiao Xu on 09-7-13.
 *  Copyright 2009 Tencent.. All rights reserved.
 *
 */

#ifndef _ISDWEBCHARACTER_STR_4CS_
#define _ISDWEBCHARACTER_STR_4CS_

#include "string.h"
#include "common.h"

typedef struct {
	size_t len;
	char ta[5];
}escapeCStringDefineNode4CS ;

/**
 * 主要用于将用户数据封入字符串常量(主要是JSON封装使用)
 * 由于javascript string 于 C/C++ string 有完全相同的转义关键字，因此称为CString
 *     策略：
 *         \        \\
 *         "        \"
 *         '        \'
 *         (0x0A)   \n
 *         (0x0D)   (丢弃)
 *
 */
int escapeCString4CS(char * resultBuffer, const char * sourceStr, size_t resultBufferSize, int level);
/**
 * 反向
 *
 */
int unescapeCString4CS(char * resultBuffer, const char * sourceStr, size_t resultBufferSize, int level);

extern escapeCStringDefineNode4CS CStringEscapeMap4CS[128] ;

#endif
