/*

OIcqCrypt.h
OICQ加密
hyj@oicq.com
1999/12/24
  实现下列算法:
  Hash算法: MD5,已实现
  对称算法: DES,未实现
  非对称算法: RSA,未实现
*/

#ifndef OI_TEA_HEAD_FILE_CHASE_TENCENT_SNS
#define OI_TEA_HEAD_FILE_CHASE_TENCENT_SNS

#include "cs_oi_tea.h"


#define TRUE  1
#define FALSE 0
#define MD5_DIGEST_LENGTH	16

	/*pKey为16byte*/
	/*
	输入:pInBuf为需加密的明文部分(Body),nInBufLen为pInBuf长度;
	输出:pOutBuf为密文格式,pOutBufLen为pOutBuf的长度是8byte的倍数,至少应预留nInBufLen+17;
	*/
	/*TEA加密算法,CBC模式*/
	/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(7byte)*/
	void cs_oi_symmetry_encrypt2_solid(const unsigned char* pInBuf, int32_t nInBufLen, const unsigned char* pKey, unsigned char* pOutBuf, int32_t *pOutBufLen);


	/*pKey为16byte*/
	/*
	输入:pInBuf为密文格式,nInBufLen为pInBuf的长度是8byte的倍数; *pOutBufLen为接收缓冲区的长度
		特别注意*pOutBufLen应预置接收缓冲区的长度!
	输出:pOutBuf为明文(Body),pOutBufLen为pOutBuf的长度,至少应预留nInBufLen-10;
	返回值:如果格式正确返回TRUE;
	*/

	/*TEA解密算法,CBC模式*/
	/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(7byte)*/
	char cs_oi_symmetry_decrypt2_solid(const unsigned char* pInBuf, int32_t nInBufLen, const unsigned char* pKey, unsigned char* pOutBuf, int32_t *pOutBufLen);

	//获得加密mkey的函数
	int32_t cs_create_oitea_key(uint64_t uin, char *chOutBuf, int32_t *len, char *oi_key);

	//网络字节序转换，兼容32位和64位平台
	uint64_t htonll(uint64_t v);

	//网络字节序转换，兼容32位和64位平台
	uint64_t ntohll(uint64_t v);

#endif	// !defined(OI_TEA_HEAD_FILE_CHASE_TENCENT_20051123)

