.PHONY:parse parse-debug

all:parse

parse:
	@cd parse && \
	jison clearsilver.y clearsilver.l

#jison clearsilver.y clearsilver.l -m amd


parse-debug:
	@cd parse && \
	jison clearsilver.y clearsilver.l --debug true
