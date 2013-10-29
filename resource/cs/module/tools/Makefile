all:v6

v6:
	./trim_dir.sh 
	./extinclude ../../wupmain.cs ../ > tmp.t
	./gendefconf ../../ wupmain.cs gendir
	gawk '{print "./gendefconf ../ "$$2" gendir"}' tmp.t > rdef.sh
	chmod 777 rdef.sh
	./rdef.sh
	./gencpp ../../ wupmain.cs gendir 1
	gawk '{print "./gencpp ../ "$$2" gendir"}' tmp.t > r.sh
	chmod 777 r.sh
	./r.sh
	cp lib/libCsKvn.so lib/feeds/
	cd gendir;make
	mkdir -p /usr/local/qzone_v3.0/lib/feeds/;
	cp lib/feeds/*.so /usr/local/qzone_v3.0/lib/feeds/

v8:
	./trim_dir.sh 
	./extinclude ../../wupmain_v8.cs ../ > tmp.t
	./gendefconf ../../ wupmain_v8.cs gendir 
	gawk '{print "./gendefconf ../ "$$2" gendir"}' tmp.t > rdef.sh
	chmod 777 rdef.sh
	./rdef.sh
	./gencpp ../../ wupmain_v8.cs gendir 1
	gawk '{print "./gencpp ../ "$$2" gendir"}' tmp.t > r.sh
	chmod 777 r.sh
	./r.sh
	cp lib/libCsKvn.so lib/feeds/
	cd gendir;make
	mkdir -p /usr/local/qzone_v3.0/lib/feeds/;
	cp lib/feeds/*.so /usr/local/qzone_v3.0/lib/feeds/

clean:
	rm -f lib/feeds/*.so gendir/*.cpp gendir/*.conf rdef.sh  r.sh  tmp.t
