/*
 *  uri.h
 *  ISDWebCharacter
 *
 *  Created by Xiao Xu on 09-7-13.
 *  Copyright 2009 Tencent.. All rights reserved.
 *
 */

#ifndef _ISDWEBCHARACTER_URI_4CS_
#define _ISDWEBCHARACTER_URI_4CS_


#include "string.h"
#include "common.h"
#include "ctype.h"
	
typedef struct {
	size_t len;
	char ta[4];
}encodeURIDefineNode4CS;


typedef struct {
	int highVal;
	int lowVal;
}decodeURIDefineNode4CS;


/**
 * 编码URI C style 版本
 * 相对高效
 * @param {char *} resultBuffer 结果缓冲区 
 * @param {const char *} sourceStr 源串
 * @param {size_t} resultBufferSize 结果缓冲区的最小容量
 */
int encodeURIValue4CS(char * resultBuffer, const char * sourceStr, size_t resultBufferSize);
/**
 * 解码URI 高效版
 * @param {char *} resultBuffer 结果缓冲区 
 * @param {const char *} sourceStr 源串
 * @param {size_t} resultBufferSize 结果缓冲区的最小容量
 */
int decodeURIValue4CS(char * resultBuffer, const char * sourceStr, size_t resultBufferSize);

#endif
