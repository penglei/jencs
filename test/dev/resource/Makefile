all:test_my

MY_TESTS_DATA=wupdata-1.hdf wupdata-2.hdf
test_my:
	@echo "Running wup cs tests";
	@failed=0;\
	for wupdata in $(MY_TESTS_DATA); do \
		jencs cs/wupmain.cs $$wupdata --include=./cs/module > $$wupdata.out.html; \
		diff $$wupdata.out.html $$wupdata.gold.html > $$wupdata.diff; \
		return_code=$$?; \
		if [ $$return_code -ne 0 ]; then \
			echo "$$wupdata" test failed!; \
			failed=1; \
		else \
			rm -rf $$wupdata.out.html; \
			rm -rf $$wupdata.diff; \
		fi; \
	done; \
	if [ $$failed -eq 1 ]; then \
		echo "Test wupdata Failed!"; \
	else \
		echo "All wupdata test passed!"; \
	fi;
