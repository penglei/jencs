
/*
*------------------------------------------------------------
*   Qzone Portal
*   ========================================
*   Author  : xizhu<xizhu@tencent.com>
*   Web Site: http://qzone.qq.com
*   Copyright (c) 2004-2008 Tencent Inc. All Rights Reserved.
*   data 2008 04.01
*------------------------------------------------------------
*   Description : the encode qq functions
*------------------------------------------------------------
*/

#include <stdio.h>
#include <string.h>
#include <math.h>
#include <stdlib.h>
#include <inttypes.h>
#include "cs_secret.h"
#include "cs_sns_oi_tea.h"

	char g_version[] = C_SECRET_VERSION;

	int32_t hextostr(unsigned char* in, uint32_t inlen, unsigned char* outkey, uint32_t outlen)
	{
		uint32_t  i;
		uint32_t len =0;
		char* prout = (char*)outkey;

		memset(prout, 0, outlen);
			
		for (i=0; i<inlen; i++)
		{
			if(len > outlen)
			{
				break;
			}

			snprintf(prout+len, (outlen-len), "%02x", in[i]);
			len +=2;
		}
			
		return 1;
	}

	int32_t strtohex(unsigned char* in, uint32_t inlen, unsigned char* outkey, uint32_t *proutlen)
	{
		uint32_t  i;
		uint32_t len =0;
		char  tmphex[4] = {0};
		
		char* prin= (char*)in;
		unsigned char* prout = (unsigned char*)outkey;

		memset(prout, 0, *proutlen);
			
		for (i = 0; i<inlen; i = i + 2)
		{
			if(len > (*proutlen))
			{
				break;
			}

			strncpy(tmphex, prin+i, 2);

			*(prout + len) = strtoul((const char *)tmphex, NULL, 16);
			len++;
			
			//snprintf(prout+len, (outlen-len), "%02x", in[i]);
			//en = strlen(prout);
		}

		*proutlen = len;
		return 1;
	}

	/**
	 * 加密函数
	 * 由外部同步KEY
	 * 不做合法性判断,
	 * @param uint32_t qq 要编码的QQ号码
	 * @param uint32_t key 密钥
	 * @param unsigned char* outkey 输出缓存
	 * @param uint32_t outlen in 输出缓存的长度,不定长,按最大qq号,计算长度为,48.
	 * @return 1 0
	 */
	int32_t cs_encode_qq(char *strqq, char* key, char* struid, uint32_t *uidlen)
	{
		if((NULL == key) || (0 == strlen((char*)key)))
		{
			return 0;
		}

		if((NULL == struid) || (0 == *uidlen))
		{
			return 0;
		}

		if((NULL == strqq) || (0 == strlen((char*)strqq)))
		{
			return 0;
		}

		char  auchexoutbuf[C_QQ_ENCODE_MAXLEN] = {0};
		int32_t hexoutlen = C_QQ_ENCODE_MAXLEN;

		int32_t nInBufLen = strlen((char*)strqq);

		cs_oi_symmetry_encrypt2_solid((const unsigned char*)strqq, nInBufLen,  (const unsigned char*)key, (unsigned char*)auchexoutbuf, &hexoutlen);

		hextostr((unsigned char*)auchexoutbuf, hexoutlen, (unsigned char*)struid, *uidlen);

		*uidlen = strlen((const char*)struid);

		return 1;
	}


	/**
	 * 解密函数函数
	 * 由外部同步KEY
	 * @unsigned char* key 解密key
	 * @param char* struid 需要被加密用户id.
	 * @param unsigned char* strqq 解密得到的qq号
	 * @param uint32_t qqlen .qq 号长度,
	 * @return 1 0
	 */
	int32_t cs_decode_qq(char* struid, char* key, char* strqq, uint32_t* qqlen)
	{
		if((NULL == key) || (0 == strlen((char*)key)))
		{
			return 0;
		}

		if((NULL == struid) || (0 == strlen(struid)))
		{
			return 0;
		}

		if((NULL == strqq) || (NULL  == qqlen) || (0 == *qqlen))
		{
			return 0;
		}

		memset(strqq, 0, *qqlen);

		unsigned char auchexinbuf[C_QQ_ENCODE_MAXLEN] = {0};
		int32_t  hexinlen = C_QQ_ENCODE_MAXLEN;
		uint32_t nInBufLen = strlen(struid);
		
		strtohex((unsigned char*)struid, nInBufLen, (unsigned char*)auchexinbuf, (uint32_t*)&hexinlen);
		
		if(0 == cs_oi_symmetry_decrypt2_solid((const unsigned char*)auchexinbuf, hexinlen,  (const unsigned char*)key, (unsigned char*)strqq, (int32_t*)qqlen))
		{
			return 0;
		}

		return 1;
	}


