#!/bin/bash

MODULE_PATH='module'

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

trim_file(){
    file=$1
    if [[ -f $file ]]; then
    	echo $file
        tr -d "\t\n\r" < $file > $file.tmp && mv -f $file.tmp $file
    fi
}

trim_dir $MODULE_PATH

trim_file "vistorfeeds_content.cs"
trim_file "vistorfeeds_title.cs"
trim_file "common_widget.cs"
trim_file "common_widget_title.cs"
trim_file "qqtab_feed_item.cs"
trim_file "qqtab_feeds_list.cs"
trim_file "qqtab_feedSummary.cs"
trim_file "im_specialcare_suggest.cs"
trim_file "feeds_list.cs"
trim_file "feeds_item.cs"
trim_file "wupmain.cs"
trim_file "wupmain_v8.cs"

echo "all done"

exit 0
