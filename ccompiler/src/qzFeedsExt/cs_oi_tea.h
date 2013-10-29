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


#ifndef _INCLUDED_OICQCRYPT_H_

#define _INCLUDED_OICQCRYPT_H_

#include <inttypes.h>

//统一64位和32位平台数据类型 add by stevenrao 2011-12-28 

#define MD5_DIGEST_LENGTH	16

char cs_Md5Test(); /*测试MD5函数是否按照rfc1321工作*/


/*

	输入const unsigned char *inBuffer、int32_t length

	输出unsigned char *outBuffer

	其中length可为0,outBuffer的长度为MD5_DIGEST_LENGTH(16byte)

*/

void cs_Md5HashBuffer( unsigned char *outBuffer, const unsigned char *inBuffer, int32_t length);





/*

	输入const unsigned char *inBuffer、int32_t length

	输出unsigned char *outBuffer

	其中length可为0,outBuffer的长度为MD5_DIGEST_LENGTH(16byte)

*/





//pOutBuffer、pInBuffer均为8byte, pKey为16byte

void cs_TeaEncryptECB(const unsigned char *pInBuf, const unsigned char *pKey, unsigned char *pOutBuf);

void cs_TeaDecryptECB(const unsigned char *pInBuf, const unsigned char *pKey, unsigned char *pOutBuf);



/*pKey为16byte*/

/*

	输入:pInBuf为需加密的明文部分(Body),nInBufLen为pInBuf长度;

	输出:pOutBuf为密文格式,pOutBufLen为pOutBuf的长度是8byte的倍数,至少应预留nInBufLen+17;

*/

/*TEA加密算法,CBC模式*/

/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(7byte)*/

void cs_oi_symmetry_encrypt(const unsigned char* pInBuf, int32_t nInBufLen, const unsigned char* pKey, unsigned char* pOutBuf, int32_t *pOutBufLen);



/*pKey为16byte*/

/*

	输入:pInBuf为密文格式,nInBufLen为pInBuf的长度是8byte的倍数; *pOutBufLen为接收缓冲区的长度

		特别注意*pOutBufLen应预置接收缓冲区的长度!

	输出:pOutBuf为明文(Body),pOutBufLen为pOutBuf的长度,至少应预留nInBufLen-10;

	返回值:如果格式正确返回TRUE;

*/

/*TEA解密算法,CBC模式*/

/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(7byte)*/

int cs_oi_symmetry_decrypt(const unsigned char* pInBuf, int32_t nInBufLen, const unsigned char* pKey, unsigned char* pOutBuf, int32_t *pOutBufLen);





/*pKey为16byte*/

/*

	输入:pInBuf为需加密的明文部分(Body),nInBufLen为pInBuf长度;

	输出:pOutBuf为密文格式,pOutBufLen为pOutBuf的长度是8byte的倍数,至少应预留nInBufLen+17;

*/

/*TEA加密算法,CBC模式*/

/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(7byte)*/

void cs_oi_symmetry_encrypt2(const unsigned char* pInBuf, int32_t nInBufLen, const unsigned char* pKey, unsigned char* pOutBuf, int32_t *pOutBufLen);



/*pKey为16byte*/

/*

	输入:pInBuf为密文格式,nInBufLen为pInBuf的长度是8byte的倍数; *pOutBufLen为接收缓冲区的长度

		特别注意*pOutBufLen应预置接收缓冲区的长度!

	输出:pOutBuf为明文(Body),pOutBufLen为pOutBuf的长度,至少应预留nInBufLen-10;

	返回值:如果格式正确返回TRUE;

*/

/*TEA解密算法,CBC模式*/

/*密文格式:PadLen(1byte)+Padding(var,0-7byte)+Salt(2byte)+Body(var byte)+Zero(7byte)*/

int cs_oi_symmetry_decrypt2(const unsigned char* pInBuf, int32_t nInBufLen, const unsigned char* pKey, unsigned char* pOutBuf, int32_t *pOutBufLen);

#endif

