CXX = g++
INC = -I../csinclude/
LIB = -L../lib/ -lCsKvn -Wl,-rpath,/usr/local/qzone_v3.0/lib/feeds/
BINARY = $(patsubst %.cpp,%,$(wildcard *.cpp))

all:$(BINARY)

%:%.cpp
	    @echo -e "Make $(GREEN)$@$(RESET) begin......\c"
		    $(CXX) $(INC) $< -shared -o ../lib/feeds/$@.so $(LIB)  
			    @echo -e $(RED)"ok."$(RESET)

clean:
	    rm *.so
