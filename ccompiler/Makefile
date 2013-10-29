
OUTDIR ?= out
#buildtype will convert to param BUILDTYPE of make command
#and the buildtype must map to configurations in gyp file
buildtype ?=debug
ifeq ($(library), shared)
component = shared_library
else
component = static_library
endif

.PHONY: clean test runtest 

all:main

#$@ can get the target name of any rule.
#-I包括的文件会在所有的gyp文件的顶层节点中被引用（也就是被依赖的文件同样会自动include这个文件）
#GYP_GENERATORS使用make,然后执行gyp

#BUILDTYPE=$(shell echo $(buildtype) | python -c "print raw_input().capitalize()")
main:
	GYP_GENERATORS=make \
	gyp --generator-output="${OUTDIR}" build/all.gyp -Ibuild/common.gypi \
	--depth=. -DlibraryType=$(component) -Dbuildtype=$(buildtype)
	make -C ${OUTDIR} -f Makefile builddir="$(shell pwd)/$(OUTDIR)/$(buildtype).all"

#component -> -DlibraryType


test:
	@unset LD_PRELOAD
	@node test && make && out/debug.all/testCsCompile.exe test/test.hdf

runtest:
	@out/debug.all/testCsCompile test/test.hdf

clean:
	rm -rf ${OUTDIR}
