all: test

#../../../bin/jencs test.hdf test.cs

#test13 lvar 
#tet16 lvar and in each 

#test_uvar none escape 不实现


#test_comma.cs 暂不实现

#test_crc.cs 不实现

#test_escape.cs 待完善
CS_TESTS = test.cs test2.cs test3.cs test4.cs test5.cs test6.cs test7.cs \
			test8.cs test9.cs test10.cs test11.cs test12.cs \
			test14.cs test15.cs test17.cs test18.cs test19.cs \
			test_var.cs test_paren.cs test_chuck.cs test_trak1.cs test_iter.cs \
			test_each_array.cs test_name.cs test_with.cs test_numbers.cs \
			test_splice.cs test_joo.cs test_first_last.cs test_abs_max_min.cs \
			test_macro_set.cs test_func.cs \
			test_loop.cs


test: $(CS_TESTS)
	@echo "Running cs regression tests..."
	@failed=0; \
	for test in $(CS_TESTS); do \
		rm -f $$test.out; \
		echo "Parsing $$test" > $$test.out; \
		../../../bin/jencs test.hdf $$test >> $$test.out; \
		diff $$test.out $$test.gold > $$test.diff; \
		return_code=$$?; \
		if [ $$return_code -ne 0 ]; then \
			echo "Failed Syntax Regression Test(回归测试): $$test"; \
			echo " See $$test.out and $$test.diff"; \
			failed=1; \
		fi; \
	done; \
	echo ""; \
	if [ $$failed -eq 1 ]; then \
		echo "Test Failed!"; \
	else \
		echo "All Regression Test passed!"; \
	fi

#可以先跑一遍拿到diff的返回值，再把有错的对比打出来
#diff $$test.out $$test.gold 2>&1 > /dev/null; \
#diff $$test.gold $$test.out > $$test.err; \
