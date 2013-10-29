
/*
*------------------------------------------------------------
*   Qzone Portal
*   ========================================
*   Author  : xizhu<xizhu@tencent.com>
*   Web Site: http://qzone.qq.com
*   Copyright (c) 2004-2010 Tencent Inc. All Rights Reserved.
*------------------------------------------------------------
*   Description : the encode qq functions
*------------------------------------------------------------
*/
#ifndef _C_SECRET_H_
#define _C_SECRET_H_

#include <stdint.h>
	#define C_SECRET_VERSION  "version 1.0.5"

	#define C_QQ_MAXLEN  12
	#define C_QQ_KEY_MAXLEN  (50)
	#define C_QQ_ENCODE_MAXLEN  (512)

	/**-------------------------------校友加密接口------------------------*/
	/**
	 * 加密函数
	 * 由外部同步KEY
	 * 不做合法性判断,
	 * @param uint32_t strqq 要编码的QQ号码
	 * @param uint32_t key 密钥
	 * @param unsigned char* outkey 输出缓存
	 * @param uint32_t outlen in 输出缓存的长度,不定长,按最大qq号,计算长度为,48.
	 * @return true false
	 */
	int32_t cs_encode_qq(char *strqq, char* key, char* struid, uint32_t *uidlen);


	/**
	 * 解密函数函数
	 * 由外部同步KEY
	 * @unsigned char* key 解密key
	 * @param char* struid 需要被加密用户id.
	 * @param unsigned char* strqq 解密得到的qq号
	 * @param uint32_t qqlen .qq 号长度,
	 * @return true false
	 */
	int32_t cs_decode_qq(char* struid, char* key, char* strqq, uint32_t* qqlen);

/*
	int32_t hextostr(unsigned char* in, uint32_t inlen, unsigned char* outkey, uint32_t outlen);
	int32_t strtohex(unsigned char* in, uint32_t inlen, unsigned char* outkey, uint32_t *proutlen);
*/

#endif



