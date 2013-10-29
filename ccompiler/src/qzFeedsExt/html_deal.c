/*
 *  xhtml.cpp
 *  ISDWebCharacter
 *
 *  Created by Xiao Xu on 09-7-13.
 *  Copyright 2009 Tencent.. All rights reserved.
 *
 */


#include "html_deal.h"

static escapeXHTMLDefineNode4CS XHTMLEscapeMap4CS[128] = {

	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{6,"&nbsp;"},
	{6,"<br />"},
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
	{6,"&nbsp;"},
	{1,"!"},
	{6,"&quot;"},
	{1,"#"},
	{1,"$"},
	{1,"%"},
	{5,"&amp;"},
	{5,"&#39;"},
	{1,"("},
	{1,")"},
	{1,"*"},
	{1,"+"},
	{1,","},
	{1,"-"},
	{1,"."},
	{1,"/"},
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
	{4,"&lt;"},
	{1,"="},
	{4,"&gt;"},
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
	{1,"\\"},
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

	static escapeXHTMLDefineNode4CS XHTMLEscapeMapNoWhite4CS[128] = {
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{0,{0}},
	{1,"\t"},
	{1,"\n"},
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
	{6,"&quot;"},
	{1,"#"},
	{1,"$"},
	{1,"%"},
	{5,"&amp;"},
	{5,"&#39;"},
	{1,"("},
	{1,")"},
	{1,"*"},
	{1,"+"},
	{1,","},
	{1,"-"},
	{1,"."},
	{1,"/"},
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
	{4,"&lt;"},
	{1,"="},
	{4,"&gt;"},
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
	{1,"\\"},
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


int escapeXHTMLEntity4CS(char * resultBuffer,
									   const char * sourceStr,
									   size_t resultBufferSize,
									   int procSpace,
									   int level){

	if(sourceStr == NULL || resultBuffer == NULL){
		return ERR_NULL_PARAMS;
	}

	register size_t blen = resultBufferSize - 1,
		c = 0;
	register unsigned char * p = (unsigned char *)sourceStr;
	register escapeXHTMLDefineNode4CS * baseMap = !procSpace ? XHTMLEscapeMapNoWhite4CS : XHTMLEscapeMap4CS;
	register escapeXHTMLDefineNode4CS * tmp = NULL;
	int res = OK;

	if(level == STRING_DEAL_NO_CHECK){
		while(*p != '\0'){
			if(*p < 0x80){
				tmp = (baseMap + (*p));
				if((*tmp).len < 1){
					//skip one
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
	}else if(level == STRING_DEAL_UTF8_CHECK){
		while(*p != '\0'){
			if(*p < 0x80){
				tmp = (baseMap + (*p));
				if((*tmp).len < 1){
					//skip one
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
	}else if(level == STRING_DEAL_GBK_CHECK){
		while(*p != '\0'){
			if(*p < 0x80){
				tmp = (baseMap + (*p));
				if((*tmp).len < 1){
					//skip one
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

int unescapeXHTMLEntity4CS(char * resultBuffer,
						const char * sourceStr,
						size_t resultBufferSize,
						int procSpace,
						int level){
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

			if('&' == *p){
				if(*(p + 1) == 'l'){
					if(*(p + 2) == 't'){
						if(*(p + 3) == ';'){
							*(resultBuffer + c) = '<';
							p += 4;
							++c;
							continue;
						}
					}
				}
				if(*(p + 1) == 'g'){
					if(*(p + 2) == 't'){
						if(*(p + 3) == ';'){
							*(resultBuffer + c) = '>';
							p += 4;
							++c;
							continue;
						}
					}
				}
				if(*(p + 1) == 'a'){
					if(*(p + 2) == 'm'){
						if(*(p + 3) == 'p'){
							if(*(p + 4) == ';'){
								*(resultBuffer + c) = '&';
								p += 5;
								++c;
								continue;
							}
						}
					}
				}
				if(*(p + 1) == '#'){
					if(*(p + 2) == '3'){
						if(*(p + 3) == '9'){
							if(*(p + 4) == ';'){
								*(resultBuffer + c) = '\'';
								p += 5;
								++c;
								continue;
							}
						}
					}
				}
				if(*(p + 1) == 'q'){
					if(*(p + 2) == 'u'){
						if(*(p + 3) == 'o'){
							if(*(p + 4) == 't'){
								if(*(p + 5) == ';'){
									*(resultBuffer + c) = '\"';
									p += 6;
									++c;
									continue;
								}
							}
						}
					}
				}
				if(procSpace){
					if(*(p + 1) == 'n'){
						if(*(p + 2) == 'b'){
							if(*(p + 3) == 's'){
								if(*(p + 4) == 'p'){
									if(*(p + 5) == ';'){
										*(resultBuffer + c) = ' ';
										p += 6;
										++c;
										continue;
									}
								}
							}
						}
					}
				}
				*(resultBuffer + c) = '&';
			}else if(procSpace && ('<' == *p)){
				if(*(p + 1) == 'b'){
					if(*(p + 2) == 'r'){
						if(*(p + 3) == ' '){
							if(*(p + 4) == '/'){
								if(*(p + 5) == '>'){
									*(resultBuffer + c) = '\n';
									p += 6;
									++c;
									continue;
								}
							}
						}
					}
				}
				*(resultBuffer + c) = '<';
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

			if('&' == *p){
				if(*(p + 1) == 'l'){
					if(*(p + 2) == 't'){
						if(*(p + 3) == ';'){
							*(resultBuffer + c) = '<';
							p += 4;
							++c;
							continue;
						}
					}
				}
				if(*(p + 1) == 'g'){
					if(*(p + 2) == 't'){
						if(*(p + 3) == ';'){
							*(resultBuffer + c) = '>';
							p += 4;
							++c;
							continue;
						}
					}
				}
				if(*(p + 1) == 'a'){
					if(*(p + 2) == 'm'){
						if(*(p + 3) == 'p'){
							if(*(p + 4) == ';'){
								*(resultBuffer + c) = '&';
								p += 5;
								++c;
								continue;
							}
						}
					}
				}
				if(*(p + 1) == '#'){
					if(*(p + 2) == '3'){
						if(*(p + 3) == '9'){
							if(*(p + 4) == ';'){
								*(resultBuffer + c) = '\'';
								p += 5;
								++c;
								continue;
							}
						}
					}
				}
				if(*(p + 1) == 'q'){
					if(*(p + 2) == 'u'){
						if(*(p + 3) == 'o'){
							if(*(p + 4) == 't'){
								if(*(p + 5) == ';'){
									*(resultBuffer + c) = '\"';
									p += 6;
									++c;
									continue;
								}
							}
						}
					}
				}
				if(procSpace){
					if(*(p + 1) == 'n'){
						if(*(p + 2) == 'b'){
							if(*(p + 3) == 's'){
								if(*(p + 4) == 'p'){
									if(*(p + 5) == ';'){
										*(resultBuffer + c) = ' ';
										p += 6;
										++c;
										continue;
									}
								}
							}
						}
					}
				}
				*(resultBuffer + c) = '&';
			}else if(procSpace && ('<' == *p)){
				if(*(p + 1) == 'b'){
					if(*(p + 2) == 'r'){
						if(*(p + 3) == ' '){
							if(*(p + 4) == '/'){
								if(*(p + 5) == '>'){
									*(resultBuffer + c) = '\n';
									p += 6;
									++c;
									continue;
								}
							}
						}
					}
				}
				*(resultBuffer + c) = '<';
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

			if('&' == *p){
				if(*(p + 1) == 'l'){
					if(*(p + 2) == 't'){
						if(*(p + 3) == ';'){
							*(resultBuffer + c) = '<';
							p += 4;
							++c;
							continue;
						}
					}
				}
				if(*(p + 1) == 'g'){
					if(*(p + 2) == 't'){
						if(*(p + 3) == ';'){
							*(resultBuffer + c) = '>';
							p += 4;
							++c;
							continue;
						}
					}
				}
				if(*(p + 1) == 'a'){
					if(*(p + 2) == 'm'){
						if(*(p + 3) == 'p'){
							if(*(p + 4) == ';'){
								*(resultBuffer + c) = '&';
								p += 5;
								++c;
								continue;
							}
						}
					}
				}
				if(*(p + 1) == '#'){
					if(*(p + 2) == '3'){
						if(*(p + 3) == '9'){
							if(*(p + 4) == ';'){
								*(resultBuffer + c) = '\'';
								p += 5;
								++c;
								continue;
							}
						}
					}
				}
				if(*(p + 1) == 'q'){
					if(*(p + 2) == 'u'){
						if(*(p + 3) == 'o'){
							if(*(p + 4) == 't'){
								if(*(p + 5) == ';'){
									*(resultBuffer + c) = '\"';
									p += 6;
									++c;
									continue;
								}
							}
						}
					}
				}
				if(procSpace){
					if(*(p + 1) == 'n'){
						if(*(p + 2) == 'b'){
							if(*(p + 3) == 's'){
								if(*(p + 4) == 'p'){
									if(*(p + 5) == ';'){
										*(resultBuffer + c) = ' ';
										p += 6;
										++c;
										continue;
									}
								}
							}
						}
					}
				}
				*(resultBuffer + c) = '&';
			}else if(procSpace && ('<' == *p)){
				if(*(p + 1) == 'b'){
					if(*(p + 2) == 'r'){
						if(*(p + 3) == ' '){
							if(*(p + 4) == '/'){
								if(*(p + 5) == '>'){
									*(resultBuffer + c) = '\n';
									p += 6;
									++c;
									continue;
								}
							}
						}
					}
				}
				*(resultBuffer + c) = '<';
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

