/*
 *  str.cpp
 *  ISDWebCharacter
 *
 *  Created by Xiao Xu on 09-7-14.
 *  Copyright 2009 Tencent.. All rights reserved.
 *
 */

#include "json_deal.h"

	escapeCStringDefineNode4CS CStringEscapeMap4CS[128] = {
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{2,"\\t"},
	{2,"\\n"},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{1," "},
	{1,"!"},
	{4,"\\x22"},
	{1,"#"},
	{1,"$"},
	{1,"%"},
	{1,"&"},
	{4,"\\x27"},
	{1,"("},
	{1,")"},
	{1,"*"},
	{1,"+"},
	{1,","},
	{1,"-"},
	{1,"."},
	{2,"\\/"},
	{1,"0"},
	{1,"1"},
	{1,"2"},
	{1,"3"},
	{1,"4"},
	{1,"5"},
	{1,"6"},
	{1,"7"},
	{1,"8"},
	{1,"9"},
	{1,":"},
	{1,";"},
	{4,"\\x3C"},
	{1,"="},
	{1,">"},
	{1,"?"},
	{1,"@"},
	{1,"A"},
	{1,"B"},
	{1,"C"},
	{1,"D"},
	{1,"E"},
	{1,"F"},
	{1,"G"},
	{1,"H"},
	{1,"I"},
	{1,"J"},
	{1,"K"},
	{1,"L"},
	{1,"M"},
	{1,"N"},
	{1,"O"},
	{1,"P"},
	{1,"Q"},
	{1,"R"},
	{1,"S"},
	{1,"T"},
	{1,"U"},
	{1,"V"},
	{1,"W"},
	{1,"X"},
	{1,"Y"},
	{1,"Z"},
	{1,"["},
	{2,"\\\\"},
	{1,"]"},
	{1,"^"},
	{1,"_"},
	{1,"`"},
	{1,"a"},
	{1,"b"},
	{1,"c"},
	{1,"d"},
	{1,"e"},
	{1,"f"},
	{1,"g"},
	{1,"h"},
	{1,"i"},
	{1,"j"},
	{1,"k"},
	{1,"l"},
	{1,"m"},
	{1,"n"},
	{1,"o"},
	{1,"p"},
	{1,"q"},
	{1,"r"},
	{1,"s"},
	{1,"t"},
	{1,"u"},
	{1,"v"},
	{1,"w"},
	{1,"x"},
	{1,"y"},
	{1,"z"},
	{1,"{"},
	{1,"|"},
	{1,"}"},
	{1,"~"},
	{1,"\x7F"}
	
	};



int escapeCString4CS(char * resultBuffer, const char * sourceStr, size_t resultBufferSize, int level){
	if(sourceStr == NULL || resultBuffer == NULL){
		return ERR_NULL_PARAMS;
	}
	
	register size_t blen = resultBufferSize - 1,
		c = 0;
	register unsigned char * p = (unsigned char *)sourceStr;
	register escapeCStringDefineNode4CS * tmp = NULL;
	int res = OK;
	
	if(level == STRING_DEAL_NO_CHECK){//不做编码检查	
		while(*p != '\0'){
			if(*p < 0x80){
				tmp = &CStringEscapeMap4CS[*p];
				if((*tmp).len < 1){
				}else if(c + (*tmp).len > blen){
					res = ERR_BUFFER_TOO_SMALL;
					break;
				}else{
					memcpy(resultBuffer + c, (*tmp).ta, (*tmp).len);
					c += (*tmp).len;
				}
			}else{
				if(c + 1 > blen){
					res = ERR_BUFFER_TOO_SMALL;
					break;
				}
				*(resultBuffer + c) = *p;
				++c;
			}
			++p;
		}
	}else if(level == STRING_DEAL_UTF8_CHECK){//不做编码检查	
		while(*p != '\0'){
			if(*p < 0x80){
				tmp = &CStringEscapeMap4CS[*p];
				if((*tmp).len < 1){
				}else if(c + (*tmp).len > blen){
					res = ERR_BUFFER_TOO_SMALL;
					break;
				}else{
					memcpy(resultBuffer + c, (*tmp).ta, (*tmp).len);
					c += (*tmp).len;
				}
			}else{
				if(c + 1 > blen){
					res = ERR_BUFFER_TOO_SMALL;
					break;
				}

				if((*p >> 5) == 0x6){//可能的 2Bytes 序列
					if((*(p+1) >> 6) == 0x2){//正是
						if(c + 2 > blen){
							res = ERR_BUFFER_TOO_SMALL;
							break;
						}
						memcpy(resultBuffer + c, p, 2);
						p += 2;
						c += 2;
						continue;
					}
				}else if((*p >> 4) == 0xE){//可能的 3Bytes 序列
					if(((*(p+1) >> 6) == 0x2) && ((*(p+2) >> 6) == 0x2)){//正是
						if(c + 3 > blen){
							res = ERR_BUFFER_TOO_SMALL;
							break;
						}
						memcpy(resultBuffer + c, p, 3);
						p += 3;
						c += 3;
						continue;
					}
				}else if((*p >> 3) == 0x1E){//可能的 4Bytes 序列
					if(((*(p+1) >> 6) == 0x2) && ((*(p+2) >> 6) == 0x2) && ((*(p+3) >> 6) == 0x2)){//正是
						if(c + 4 > blen){
							res = ERR_BUFFER_TOO_SMALL;
							break;
						}
						memcpy(resultBuffer + c, p, 4);
						p += 4;
						c += 4;
						continue;
					}
				}

				*(resultBuffer + c) = '?';
				++c;
			}
			++p;
		}
	}else if(level == STRING_DEAL_GBK_CHECK){//不做编码检查	
		while(*p != '\0'){
			if(*p < 0x80){
				tmp = &CStringEscapeMap4CS[*p];
				if((*tmp).len < 1){
				}else if(c + (*tmp).len > blen){
					res = ERR_BUFFER_TOO_SMALL;
					break;
				}else{
					memcpy(resultBuffer + c, (*tmp).ta, (*tmp).len);
					c += (*tmp).len;
				}
			}else{
				if(c + 1 > blen){
					res = ERR_BUFFER_TOO_SMALL;
					break;
				}

				if(0x80 < *p && *p < 0xFF){ // GBK level 3
					if(*(p+1) != 0x7F && 0x3F < *(p+1) && *(p+1) < 0xFF){//yes
						if(c + 2 > blen){
							res = ERR_BUFFER_TOO_SMALL;
							break;
						}
						memcpy(resultBuffer + c, p, 2);
						p += 2;
						c += 2;
						continue;
					}
				}else if(*p == 0x80){ // 欧元符号
					*(resultBuffer + c) = 0x80;
					++p;
					++c;
					continue;
				}

				*(resultBuffer + c) = '?';
				++c;
			}
			++p;
		}
	}
	
	
	*(resultBuffer + c) = '\0';
	
	return res;
	
}


int unescapeCString4CS(char * resultBuffer, const char * sourceStr, size_t resultBufferSize, int level){
	if(sourceStr == NULL || resultBuffer == NULL){
		return ERR_NULL_PARAMS;
	}
	register size_t blen = resultBufferSize - 1,
		c = 0;
	register unsigned char * p = (unsigned char *)sourceStr;
	int res = OK;
	
	if(level == STRING_DEAL_NO_CHECK){
		while(*p != '\0'){
			if(c + 1 > blen){
				res = ERR_BUFFER_TOO_SMALL;
				break;
			}
			
			if('\\' == *p){
				if(*(p + 1) == 't'){
					*(resultBuffer + c) = '\t';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == 'n'){
					*(resultBuffer + c) = '\n';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\\'){
					*(resultBuffer + c) = '\\';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '/'){
					*(resultBuffer + c) = '/';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\''){
					*(resultBuffer + c) = '\'';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\"'){
					*(resultBuffer + c) = '\"';
					p += 2;
					++c;
					continue;
				}
                if(*(p + 1) == 'x'){
                    if(*(p + 2) == '3' && *(p + 3) == 'C'){
                        *(resultBuffer + c) = '<';
                    }
                    else if (*(p + 2) == '2' && *(p + 3) == '2'){
                        *(resultBuffer + c) = '\"';
                    }
                    else if (*(p + 2) == '2' && *(p + 3) == '7'){
                        *(resultBuffer + c) = '\'';
                    }
                    p += 4;
                    ++c;
                    continue;
                }
				*(resultBuffer + c) = '\\';
			}else{
				*(resultBuffer + c) = *p;
			}
			++p;
			++c;
		}
	}else if(level == STRING_DEAL_UTF8_CHECK){
		while(*p != '\0'){
			if(c + 1 > blen){
				res = ERR_BUFFER_TOO_SMALL;
				break;
			}
			
			if('\\' == *p){
				if(*(p + 1) == 't'){
					*(resultBuffer + c) = '\t';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == 'n'){
					*(resultBuffer + c) = '\n';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\\'){
					*(resultBuffer + c) = '\\';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\''){
					*(resultBuffer + c) = '\'';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '/'){
					*(resultBuffer + c) = '/';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\"'){
					*(resultBuffer + c) = '\"';
					p += 2;
					++c;
					continue;
				}
                if(*(p + 1) == 'x'){
                    if(*(p + 2) == '3' && *(p + 3) == 'C'){
                        *(resultBuffer + c) = '<';
                    }
                    else if (*(p + 2) == '2' && *(p + 3) == '2'){
                        *(resultBuffer + c) = '\"';
                    }
                    else if (*(p + 2) == '2' && *(p + 3) == '7'){
                        *(resultBuffer + c) = '\'';
                    }
                    p += 4;
                    ++c;
                    continue;
                }
				*(resultBuffer + c) = '\\';
			}else{
				if(*p < 0x80){
					*(resultBuffer + c) = *p;
					++p;
					++c;
					continue;
				}else if((*p >> 5) == 0x6){//可能的 2Bytes 序列
					if((*(p+1) >> 6) == 0x2){//正是
						if(c + 2 > blen){
							res = ERR_BUFFER_TOO_SMALL;
							break;
						}
						memcpy(resultBuffer + c, p, 2);
						p += 2;
						c += 2;
						continue;
					}
				}else if((*p >> 4) == 0xE){//可能的 3Bytes 序列
					if(((*(p+1) >> 6) == 0x2) && ((*(p+2) >> 6) == 0x2)){//正是
						if(c + 3 > blen){
							res = ERR_BUFFER_TOO_SMALL;
							break;
						}
						memcpy(resultBuffer + c, p, 3);
						p += 3;
						c += 3;
						continue;
					}
				}else if((*p >> 3) == 0x1E){//可能的 4Bytes 序列
					if(((*(p+1) >> 6) == 0x2) && ((*(p+2) >> 6) == 0x2) && ((*(p+3) >> 6) == 0x2)){//正是
						if(c + 4 > blen){
							res = ERR_BUFFER_TOO_SMALL;
							break;
						}
						memcpy(resultBuffer + c, p, 4);
						p += 4;
						c += 4;
						continue;
					}
				}
				*(resultBuffer + c) = '?';
			}
			++p;
			++c;
		}
	}else if(level == STRING_DEAL_GBK_CHECK){
		while(*p != '\0'){
			if(c + 1 > blen){
				res = ERR_BUFFER_TOO_SMALL;
				break;
			}
			
			if('\\' == *p){
				if(*(p + 1) == 't'){
					*(resultBuffer + c) = '\t';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == 'n'){
					*(resultBuffer + c) = '\n';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\\'){
					*(resultBuffer + c) = '\\';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\''){
					*(resultBuffer + c) = '\'';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '/'){
					*(resultBuffer + c) = '/';
					p += 2;
					++c;
					continue;
				}
				if(*(p + 1) == '\"'){
					*(resultBuffer + c) = '\"';
					p += 2;
					++c;
					continue;
				}
                if(*(p + 1) == 'x'){
                    if(*(p + 2) == '3' && *(p + 3) == 'C'){
                        *(resultBuffer + c) = '<';
                    }
                    else if (*(p + 2) == '2' && *(p + 3) == '2'){
                        *(resultBuffer + c) = '\"';
                    }
                    else if (*(p + 2) == '2' && *(p + 3) == '7'){
                        *(resultBuffer + c) = '\'';
                    }
                    p += 4;
                    ++c;
                    continue;
                }
				*(resultBuffer + c) = '\\';
			}else{
				if(0x80 < *p && *p < 0xFF){ // GBK level 3
					if(*(p+1) != 0x7F && 0x3F < *(p+1) && *(p+1) < 0xFF){//yes
						if(c + 2 > blen){
							res = ERR_BUFFER_TOO_SMALL;
							break;
						}
						memcpy(resultBuffer + c, p, 2);
						p += 2;
						c += 2;
						continue;
					}
				}else if(0x81 > *p){
					if(c + 1 > blen){
						res = ERR_BUFFER_TOO_SMALL;
						break;
					}
					*(resultBuffer + c) = *p;
					++p;
					++c;
					continue;
				}
				*(resultBuffer + c) = '?';
			}
			++p;
			++c;
		}
	}
	
	*(resultBuffer + c) = '\0';
	
	
	return res;
}
