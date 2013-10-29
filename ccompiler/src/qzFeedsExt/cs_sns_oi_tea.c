/*
*------------------------------------------------------------
*   Qzone Portal
*   ========================================
*   Author  : xizhu<xizhu@tencent.com>
*   Web Site: http://qzone.qq.com
*   Copyright (c) 2004-2008 Tencent Inc. All Rights Reserved.
*   data 2010 06.01
*------------------------------------------------------------
*   Description : the encode qq functions
*------------------------------------------------------------
*/

#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <string.h>
#include <math.h>
#include <time.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include "cs_sns_oi_tea.h"

//#ifdef __cplusplus
//extern "C" {
//#endif
///////////////////////////////////////////////////////////////////////////////////////////////
#define SALT_LEN 2
#define ZERO_LEN 7
#define OITEA_RAND_NUM 'Q'

/*pKey为16byte*/
/*
	输入:pInBuf为需加密的明文部分(Body),nInBufLen为pInBuf长度;
	输出:pOutBuf为密文格式,pOutBufLen为pOutBuf的长度是8byte的倍数;
*/
/*TEA加密算法,CBC模式*/
/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(8byte)*/
void cs_oi_symmetry_encrypt2_solid(const unsigned char* pInBuf, int32_t nInBufLen, const unsigned char* pKey, unsigned char* pOutBuf, int32_t *pOutBufLen)
{

	int32_t nPadSaltBodyZeroLen/*PadLen(1byte)+Salt+Body+Zero的长度*/;
	int32_t nPadlen;
	unsigned char src_buf[8], iv_plain[8], *iv_crypt;
	int32_t src_i, i, j;

	/*根据Body长度计算PadLen,最小必需长度必需为8byte的整数倍*/
	nPadSaltBodyZeroLen = nInBufLen/*Body长度*/+1+SALT_LEN+ZERO_LEN/*PadLen(1byte)+Salt(2byte)+Zero(8byte)*/;
	if ((nPadlen=(nPadSaltBodyZeroLen%8)) != 0)
	{
		/*模8余0需补0,余1补7,余2补6,...,余7补1*/
		nPadlen=8-nPadlen;
	}


	/*srand( (uint32_t)time( NULL ) ); 初始化随机数*/
	/*加密第一块数据(8byte),取前面10byte*/
	src_buf[0] = (((unsigned char)OITEA_RAND_NUM) & 0x0f8)/*最低三位存PadLen,清零*/ | (unsigned char)nPadlen;
	src_i = 1; /*src_i指向src_buf下一个位置*/

	while(nPadlen--)
		src_buf[src_i++]=(unsigned char)OITEA_RAND_NUM; /*Padding*/

	/*come here, src_i must <= 8*/

	for ( i=0; i<8; i++)
		iv_plain[i] = 0;

	iv_crypt = iv_plain; /*make zero iv*/
	*pOutBufLen = 0; /*init OutBufLen*/

	for (i=1;i<=SALT_LEN;) /*Salt(2byte)*/
	{
		if (src_i<8)
		{
			src_buf[src_i++]=(unsigned char)OITEA_RAND_NUM;
			i++; /*i inc in here*/
		}



		if (src_i==8)

		{

			/*src_i==8*/



			for (j=0;j<8;j++) /*加密前异或前8个byte的密文(iv_crypt指向的)*/

				src_buf[j]^=iv_crypt[j];



			/*pOutBuffer、pInBuffer均为8byte, pKey为16byte*/

			/*加密*/

			cs_TeaEncryptECB(src_buf, pKey, pOutBuf);

			for (j=0;j<8;j++) /*加密后异或前8个byte的明文(iv_plain指向的)*/

				pOutBuf[j]^=iv_plain[j];



			/*保存当前的iv_plain*/

			for (j=0;j<8;j++)

				iv_plain[j]=src_buf[j];



			/*更新iv_crypt*/

			src_i=0;

			iv_crypt=pOutBuf;

			*pOutBufLen+=8;

			pOutBuf+=8;

		}

	}



	/*src_i指向src_buf下一个位置*/



	while(nInBufLen)

	{

		if (src_i<8)

		{

			src_buf[src_i++]=*(pInBuf++);

			nInBufLen--;

		}



		if (src_i==8)

		{

			/*src_i==8*/



			for (j=0;j<8;j++) /*加密前异或前8个byte的密文(iv_crypt指向的)*/

				src_buf[j]^=iv_crypt[j];

			/*pOutBuffer、pInBuffer均为8byte, pKey为16byte*/

			cs_TeaEncryptECB(src_buf, pKey, pOutBuf);



			for (j=0;j<8;j++) /*加密后异或前8个byte的明文(iv_plain指向的)*/

				pOutBuf[j]^=iv_plain[j];



			/*保存当前的iv_plain*/

			for (j=0;j<8;j++)

				iv_plain[j]=src_buf[j];



			src_i=0;

			iv_crypt=pOutBuf;

			*pOutBufLen+=8;

			pOutBuf+=8;

		}

	}



	/*src_i指向src_buf下一个位置*/



	for (i=1;i<=ZERO_LEN;)

	{

		if (src_i<8)

		{

			src_buf[src_i++]=0;

			i++; /*i inc in here*/

		}



		if (src_i==8)

		{

			/*src_i==8*/



			for (j=0;j<8;j++) /*加密前异或前8个byte的密文(iv_crypt指向的)*/

				src_buf[j]^=iv_crypt[j];

			/*pOutBuffer、pInBuffer均为8byte, pKey为16byte*/

			cs_TeaEncryptECB(src_buf, pKey, pOutBuf);



			for (j=0;j<8;j++) /*加密后异或前8个byte的明文(iv_plain指向的)*/

				pOutBuf[j]^=iv_plain[j];



			/*保存当前的iv_plain*/

			for (j=0;j<8;j++)

				iv_plain[j]=src_buf[j];



			src_i=0;

			iv_crypt=pOutBuf;

			*pOutBufLen+=8;

			pOutBuf+=8;

		}

	}



}



/*pKey为16byte*/

/*

	输入:pInBuf为密文格式,nInBufLen为pInBuf的长度是8byte的倍数;

	输出:pOutBuf为明文(Body),pOutBufLen为pOutBuf的长度;

	返回值:如果格式正确返回TRUE;

*/

/*TEA解密算法,CBC模式*/

/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(8byte)*/

char cs_oi_symmetry_decrypt2_solid(const unsigned char* pInBuf, int32_t nInBufLen, const unsigned char* pKey, unsigned char* pOutBuf, int32_t *pOutBufLen)

{
	int32_t nPadLen, nPlainLen;
	unsigned char dest_buf[8], zero_buf[8];
	const unsigned char *iv_pre_crypt, *iv_cur_crypt;
	int32_t dest_i, i, j;
	//if (nInBufLen%8) return FALSE; BUG!
	if ((nInBufLen%8) || (nInBufLen<16)) return FALSE;
	cs_TeaDecryptECB(pInBuf, pKey, dest_buf);
	nPadLen = dest_buf[0] & 0x7/*只要最低三位*/;
	/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(8byte)*/
	i = nInBufLen-1/*PadLen(1byte)*/-nPadLen-SALT_LEN-ZERO_LEN; /*明文长度*/
	if (*pOutBufLen<i) return FALSE;
	*pOutBufLen = i;
	if (*pOutBufLen < 0) return FALSE;
	for ( i=0; i<8; i++)zero_buf[i] = 0;
	iv_pre_crypt = zero_buf;
	iv_cur_crypt = pInBuf; /*init iv*/
	nInBufLen -= 8;
	pInBuf += 8;
	dest_i=1; /*dest_i指向dest_buf下一个位置*/
	/*把Padding滤掉*/
	dest_i+=nPadLen;
	/*dest_i must <=8*/
	/*把Salt滤掉*/
	for (i=1; i<=SALT_LEN;)
	{
		if (dest_i<8)
		{
			dest_i++;
			i++;
		}
		else if (dest_i==8)
		{
			/*解开一个新的加密块*/
			/*改变前一个加密块的指针*/
			iv_pre_crypt = iv_cur_crypt;
			iv_cur_crypt = pInBuf;
			/*异或前一块明文(在dest_buf[]中)*/
			for (j=0; j<8; j++)dest_buf[j]^=pInBuf[j];
			/*dest_i==8*/
			cs_TeaDecryptECB(dest_buf, pKey, dest_buf);
			/*在取出的时候才异或前一块密文(iv_pre_crypt)*/
			nInBufLen -= 8;
			pInBuf += 8;
			dest_i=0; /*dest_i指向dest_buf下一个位置*/
		}
	}

	/*还原明文*/
	nPlainLen=*pOutBufLen;
	while (nPlainLen)
	{
		if (dest_i<8)
		{
			*(pOutBuf++)=dest_buf[dest_i]^iv_pre_crypt[dest_i];
			dest_i++;
			nPlainLen--;
		}
		else if (dest_i==8)
		{
			/*dest_i==8*/
			/*改变前一个加密块的指针*/
			iv_pre_crypt = iv_cur_crypt;
			iv_cur_crypt = pInBuf;
			/*解开一个新的加密块*/
			/*异或前一块明文(在dest_buf[]中)*/
			for (j=0; j<8; j++)dest_buf[j]^=pInBuf[j];
			cs_TeaDecryptECB(dest_buf, pKey, dest_buf);
			/*在取出的时候才异或前一块密文(iv_pre_crypt)*/
			nInBufLen -= 8;
			pInBuf += 8;
			dest_i=0; /*dest_i指向dest_buf下一个位置*/
		}
	}

	/*校验Zero*/
	for (i=1;i<=ZERO_LEN;)
	{
		if (dest_i<8)
		{
			if(dest_buf[dest_i]^iv_pre_crypt[dest_i]) return FALSE;
			dest_i++;
			i++;
		}
		else if (dest_i==8)
		{
			/*改变前一个加密块的指针*/
			iv_pre_crypt = iv_cur_crypt;
			iv_cur_crypt = pInBuf;
			/*解开一个新的加密块*/
			/*异或前一块明文(在dest_buf[]中)*/
			for (j=0; j<8; j++)dest_buf[j]^=pInBuf[j];
			cs_TeaDecryptECB(dest_buf, pKey, dest_buf);
			/*在取出的时候才异或前一块密文(iv_pre_crypt)*/
			nInBufLen -= 8;
			pInBuf += 8;
			dest_i=0; /*dest_i指向dest_buf下一个位置*/
		}
	}
	return TRUE;
}

uint64_t htonll(uint64_t v) 
{
	union { uint32_t lv[2]; uint64_t llv; } u;
	u.lv[0] = htonl( (uint32_t) (v >> 32) );
	u.lv[1] = htonl( (uint32_t)(v & (uint64_t)0xFFFFFFFF) );
	return u.llv;
}

uint64_t ntohll(uint64_t v) 
{
	union { uint32_t lv[2]; uint64_t llv; } u;
	u.llv = v;
	return ((uint64_t)ntohl(u.lv[0]) << 32) | (uint64_t)ntohl(u.lv[1]);
}

//获得加密mkey的函数   chOutBuf >= 128+1 Bytes
int cs_create_oitea_key(uint64_t uin, char *chOutBuf, int *len, char *oi_key)
{
	if(*len <= 128 || oi_key == NULL || chOutBuf == NULL )
	{
		return FALSE;
	}

	unsigned char chInBuf[128] = {0};
	unsigned char chOutHex[64] = {0};
	int hex_len= sizeof(chOutHex);


	uint64_t uin_n = htonll(uin);
	uint64_t now   = time(NULL);
	now= htonll(now);
	
	bzero(chInBuf, sizeof(chInBuf));
	memcpy(chInBuf, (char*)&uin_n, sizeof(uin_n));
	memcpy(chInBuf+sizeof(uin_n), (char*)&now,  sizeof(now));

	cs_oi_symmetry_encrypt2((unsigned char *)chInBuf, sizeof(uin_n)+sizeof(now), (unsigned char *)oi_key, (unsigned char *)chOutHex, &hex_len);

	int i = 0;
	for (i = 0; i < hex_len; i++)
	{
		snprintf(chOutBuf + 2 * i, (64 - 2 * i), "%02x", (unsigned char)chOutHex[i]);
	}
	chOutBuf[hex_len * 2] = '\0';
	*len = hex_len * 2 + 1;
	
	return TRUE;
}

//#ifdef __cplusplus
//}
//#endif
