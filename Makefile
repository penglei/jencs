.PHONY:parse parse-debug test

all:parse

parse:
	@cd parse && \
	jison clearsilver.y clearsilver.l

#jison clearsilver.y clearsilver.l -m amd

parse-debug:
	@cd parse && \
	jison clearsilver.y clearsilver.l --debug true

test:
	@cd test/dev && \
	jencs test.cs test.hdf --debug-brk=true
