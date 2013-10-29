#include <stdio.h>
#include <stdlib.h>

#include "CSTPL_test.h" //编译生成的文件,主要是需要获得 CSTPL_test的定义

void render_output(char *);

int main(int argc, char *argv[])
{

    HDF *hdf;
    NEOERR *err;
    char *hdf_file = argv[1];

    err = hdf_init(&hdf);
    err = hdf_read_file(hdf, hdf_file);
    //hdf_dump(hdf, NULL);
    CSTPL_test(hdf, render_output);
    return 0;
}

void render_output(char *s)
{
    puts(s);
}
