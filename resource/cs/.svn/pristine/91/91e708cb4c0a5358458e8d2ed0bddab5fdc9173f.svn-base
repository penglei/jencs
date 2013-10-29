#该文件只是方便模板同步到相应目录
#
#$(PATH_QZONEPLATFORM_PRJ) == /usr/local/qzone
#/data/release/qzone =>$(PATH_QZONEPLATFORM_PRJ/release

QZ_SVN_PATH=http://tc-svn.tencent.com/isd/isd_qzoneplatformdev_rep/QzoneBasePlatform_proj/trunk/application/feeds/cgi/htdocs

IS_BRANCH=0
QZ_CS_TRUNK:=$(QZ_SVN_PATH)/cs

MKDIR=mkdir -p

define checkAndMkdir 
	test -d $(1) ||  $(MKDIR) $(1)
endef

ifdef branch
IS_BRANCH=1
QZ_CS_BRANCH := $(QZ_SVN_PATH)/cs_branches/$(branch)
endif

#FEEDS_HTDOCS_CS=$(PATH_QZONEPLATFORM_PRJ)/application/feeds/cgi/htdocs/cs
FEEDS_HTDOCS_CS=$$(pwd)

CMD_CP_CS := cp -rf $(FEEDS_HTDOCS_CS)

all:update release _end
.PHONY:update release _end info

update:
ifeq ($(IS_BRANCH),1)
	@echo
	@echo "switch&update to branch: <$(branch)>"
	@svn switch $(QZ_CS_BRANCH) .
else
	svn up
endif
	@echo

release:
	@echo publish to: $(PATH_QZONEPLATFORM_PRJ)/release/ic/htdocs/cs
	@$(CMD_CP_CS)/* $(PATH_QZONEPLATFORM_PRJ)/release/ic/htdocs/cs
	@echo publish to: $(PATH_QZONEPLATFORM_PRJ)/release/r/htdocs/cs
	@$(CMD_CP_CS)/* $(PATH_QZONEPLATFORM_PRJ)/release/r/htdocs/cs
	@echo publish to: $(PATH_QZONEPLATFORM_PRJ)/release/r/htdocs/feeds/cs
	@$(CMD_CP_CS)/* $(PATH_QZONEPLATFORM_PRJ)/release/r/htdocs/feeds/cs
	@echo publish to: $(PATH_QZONEPLATFORM_PRJ)/release/rsh/htdocs/feeds/cs
	@$(CMD_CP_CS)/* $(PATH_QZONEPLATFORM_PRJ)/release/rsh/htdocs/feeds/cs
	@echo publish to: $(PATH_QZONEPLATFORM_PRJ)/release/firstpage/htdocs/cs
	@$(call checkAndMkdir,$(PATH_QZONEPLATFORM_PRJ)/release/firstpage/htdocs/cs)
	@echo publish to: $(PATH_QZONEPLATFORM_PRJ)/release/firstpage/htdocs
	@$(CMD_CP_CS)/* $(PATH_QZONEPLATFORM_PRJ)/release/firstpage/htdocs/cs
	@$(call checkAndMkdir,$(PATH_QZONEPLATFORM_PRJ)/release/tsh/htdocs/ticker/qzone_cs)
	@echo publish to: $(PATH_QZONEPLATFORM_PRJ)/release/tsh/htdocs/ticker/qzone_cs
	@$(CMD_CP_CS)/* $(PATH_QZONEPLATFORM_PRJ)/release/tsh/htdocs/ticker/qzone_cs	
	$(CMD_CP_CS)/famous_feeds.html $(PATH_QZONEPLATFORM_PRJ)/release/r/htdocs/famous_space/
	$(CMD_CP_CS)/famous_more_feeds.html  $(PATH_QZONEPLATFORM_PRJ)/release/r/htdocs/famous_space/

info:
	@echo
	@echo last synchronize version:
	@tail log_sync

_end:
ifeq ($(IS_BRANCH),1)
	@echo
	@echo switch current directory back to trunk.
	@svn switch $(QZ_CS_TRUNK) > /dev/null 
	@echo OK.
endif
