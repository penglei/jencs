#ifndef __CKVN__HEAD__INCLUDED__
#define __CKVN__HEAD__INCLUDED__

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <string>
#include <vector>
#include <map>

#include "util/neo_hdf.h"
#include "cs/html_deal.h"
#include "cs/json_deal.h"
#include "cs/uri_deal.h"

using namespace std;


class __CKVN_
{
public:
    __CKVN_();
    __CKVN_(HDF *hdf);
    __CKVN_(const char *name);
    __CKVN_(const char *name, int strsize);
    __CKVN_(const char *name, int strsize, const char *value);
    __CKVN_(const char *name, int strsize, int value);
    __CKVN_(__CKVN_ &other);
    __CKVN_(const __CKVN_ &other);
    ~__CKVN_();

    __CKVN_& operator[](const __CKVN_ &other);
    __CKVN_& operator[](int name);
    __CKVN_& operator[](const char *name);

    __CKVN_& operator+(const __CKVN_ &other);
    __CKVN_& operator+(int value);
    __CKVN_& operator+(const char *value);

    __CKVN_& operator-(const __CKVN_ &other);
    __CKVN_& operator-(const int value);
    __CKVN_& operator-(const char *value);

    __CKVN_ &operator*(const __CKVN_ &other);
    int operator*(const int value);
    int operator*(const char *value);

    int operator/(const __CKVN_ &other);
    int operator/(const int value);
    int operator/(const char *value);

    int operator%(const __CKVN_ &other);
    int operator%(const int value);
    int operator%(const char *value);

    __CKVN_& operator=(const __CKVN_ &other);
    __CKVN_& operator=(const int value);
    __CKVN_& operator=(const char *value);

    bool operator==(const __CKVN_ &other) const;
    bool operator==(const int value) const;
    bool operator==(const char *value) const;

    bool operator!=(const __CKVN_ &other) const;
    bool operator!=(const int value) const;
    bool operator!=(const char *value) const;

    bool operator<(const __CKVN_ &other);
    bool operator<(const int value);
    bool operator<(const char *value);

    bool operator>(const __CKVN_ &other);
    bool operator>(const int value);
    bool operator>(const char *value);

    bool operator<=(const __CKVN_ &other);
    bool operator<=(const int value);
    bool operator<=(const char *value);

    bool operator>=(const __CKVN_ &other);
    bool operator>=(const int value);
    bool operator>=(const char *value);

    operator bool() const;
    operator int() const;
    char *GetValue() const;
    HDF *GetObj() const;

    static HDF *g_hdf;

    static int subcount(const __CKVN_ &param);
    static int abs(const __CKVN_ &param);
    static int min(int n1, int n2);
    static int stringlen(const __CKVN_ &param);
    static int stringfind(const __CKVN_ &param, const char *substr);
    static __CKVN_ string_firstwords_filter(const __CKVN_ &param, const char *str);
    static __CKVN_ stringslice(const __CKVN_ &param, int b, int e);
    static __CKVN_ html_encode(const __CKVN_ &param, int flag);
    static __CKVN_ html_decode(const __CKVN_ &param, int flag);
    static __CKVN_ uri_encode(const __CKVN_ &param);
    static __CKVN_ json_encode(const __CKVN_ &param, int flag);
    static int bitmap_value_ex(const __CKVN_ &param, int start, int num);
    static int get_nodes(const __CKVN_ &param, vector<string> &nodes);

    HDF *m_hdf;
    char m_name[256];
    int m_name_len;
    char *m_value;
    int value_alloced;

    int iop;

};

__CKVN_ operator+(const int value, const __CKVN_ &value2);
__CKVN_ operator+(const char *value, const __CKVN_ &value2);
bool operator<(const int value, const __CKVN_ &value2);
bool operator<=(const int value, const __CKVN_ &value2);
bool operator>(const int value, const __CKVN_ &value2);
bool operator>=(const int value, const __CKVN_ &value2);
bool operator!(const __CKVN_ value);
//int operator=(int &value, const __CKVN_ &value2);
   
class CKvnOut
{ 
public:
    CKvnOut(){};
    ~CKvnOut(){};

    void Init(string *str)
    {
        m_modcount = 0;
        m_str = str;
    }

    void PushEscMode(int mode)
    {
        m_mod[m_modcount++] = mode;
    }

    void PopEscMode()
    {
        m_modcount --;
    }

    void AddStr(const char *value);
    void AddStr(char *start, int len);
    void AddStr(const __CKVN_ &other);
    void AddStr(const int value);

private:
    string *m_str;
    int m_modcount;
    int m_mod[1024];
};

#define MAX_ALL_CS_FUNCS 64*1024

class CKvnTool
{
public:
    static void *get_func(const char *soname,  const char *funcname);
    static map<const string, void*> m_funcs;
    static map<const string, void*> m_dlhandls;
    static void *m_func_all[MAX_ALL_CS_FUNCS];
    static void *m_func_render[MAX_ALL_CS_FUNCS];
    static int all_func_loaded;
};

#endif

