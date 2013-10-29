#!/bin/bash

trim_dir(){
	old_dir=`pwd`
	cd $1
	PWD=`pwd`
	for file in $(ls) 
	do
		echo $PWD/$file
		if [[ -d $file ]]; then
			(trim_dir $file)
		elif [[ -f $file && $file != "debug_print.cs" && $file =~ ".+\.cs" ]]; then
			#tr -d "\t\n\r" < $file > $file.tmp && cp -f $file.tmp $file
			tr -d "\t\n\r" < $file > $file.tmp && mv -f $file.tmp $file
		fi
	done
	cd $old_dir
}

trim_dir ../../
