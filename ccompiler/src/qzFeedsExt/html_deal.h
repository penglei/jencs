/*
 *  xhtml.h
 *  ISDWebCharacter
 *
 *  Created by Xiao Xu on 09-7-13.
 *  Copyright 2009 Tencent.. All rights reserved.
 *
 */

#ifndef _ISDWEBCHARACTER_XHTML_4CS_
#define _ISDWEBCHARACTER_XHTML_4CS_

#include "common.h"
#include "string.h"

/**
 * 是否处理HTML中的空白字符为实体
 */
typedef struct {
	size_t len;
	char ta[7];
}escapeXHTMLDefineNode4CS;

#define PRO_SPACE_YES 1
#define PRO_SPACE_NO  0

/**
 * XML/HTML 实体转义
 *     策略：
 *         <        &lt;
 *         >        &gt;
 *         "        &quot;
 *         '        &#39;       (本应该是 &apos; 无奈IE不解析...)
 *         &        &amp;
 * 主要用于将文本插入XML/HTML段落时，用户看到的是原文
 */
int escapeXHTMLEntity4CS(char * resultBuffer, const char * sourceStr, size_t resultBufferSize, int procSpace, int level);

/**
 * 反方向还原
 *
 */
int unescapeXHTMLEntity4CS(char * resultBuffer, const char * sourceStr, size_t resultBufferSize, int procSpace, int level);

#endif

