<?
cs #:�����ۻظ������������� Feed ?><?
cs if:string.length(qz_metadata.dotype) > 0 ?><?
	cs if:qz_metadata.dotype == 55702 ?><?
		cs call:level2TitleView() ?><?
	cs elif:qz_metadata.dotype == 55802 ?><?
		cs call:level3TitleView() ?><?
	cs /if ?><?
cs /if ?>