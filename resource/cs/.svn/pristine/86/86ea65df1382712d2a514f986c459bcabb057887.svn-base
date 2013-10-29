/**
 * @fileoverview F4A 合并文件
 * @author 
 * @version 
 */
/*load JS Library*/
var INPUT_PATH = (arguments[0] || ".") + "/";

try {
	load(INPUT_PATH + "./Rhino/API/io.js");
	load(INPUT_PATH + "./Rhino/API/jsmin.js");
	IO.setEncoding("utf-8");
} 
catch (e) {
	print('Base input path error, please input the path. ');
}

/**
 * 执行脚本的环境地址
 */
var USER_DIR = arguments[1] || environment["user.dir"];

/**
 * 合并后的注释
 */
var COMMENT = "  Qzone Project By Qzone SNS Group. \n  Copyright 2008 - 2010";

/**
 * 不对target进行压缩处理
 */
var NO_MIN = (typeof(arguments[2])=='undefined' || arguments[2]=="false")?true:false;

/**
 * 安静模式，不在屏幕上输出过多信息
 */
var SILENT = false;

var logCache = [];
var split_flag = (typeof(arguments[3]) !='undefined' && arguments[3]=="split") ? true:false;

var overwrite_flag = (typeof(arguments[4]) !='undefined' && arguments[4]=="overwrite") ? true:false;
/**
 * 源文件列表
 * projectName : project
 */
var SOURCE_LIST = {
	//
	"common_widget":{
		target: "../common_widget.cs",
		include:[
			'qz_static_summary.cs',
			'macro.cs',
			'writeUserName.cs',
			'user_icon.cs',
			'user_link.cs',
			'voice_cnt.cs',
			'gradestar.cs',
			'cmt_user_link.cs',
			'rich_content.cs',
			'opr.cs',
			'sub_title.cs',
			'quote.cs',
			'vote_viewer.cs',
			'coupon.cs',
			'clock_viewer.cs',
			'music.cs',
			'game.cs',
			'profile_viewer.cs',
			'magicemotion.cs',
			'location_info.cs',
			'attach_info.cs',
			'timeline.cs',
			'markPhotoInfo.cs',
			'extend_info.cs',
			'attach.cs',
			'content_box.cs',
			'comment.cs',
			'qz_data.cs',
			'main_summary.cs',
			'mod_layout.cs',
			'summary_start.cs'
		]
	},
	"common_widget_title":{
		target: "../common_widget_title.cs",
		include:[
			'writeUserName.cs',
			'user_link.cs',
			'rich_content_title.cs',
			'rich_title.cs',
			'feed_title.cs',
			'checkin.cs',
			'qz_static_title.cs',
			'action_title.cs',
			'main_title.cs',
			'mod_layout.cs',
			'title_start.cs'
		]
	}
};

/**
 * 主进程
 */
function main(){
	if (SILENT) {
		print("processing ...");
	};
	if (!split_flag){
		log("[Merge Type]:" + (NO_MIN ? "merge" : "min"));
	} else {
		log("[split]");
	}

	var arr = [];
	for (var k in SOURCE_LIST) {
		arr.push(SOURCE_LIST[k]);
	}
	if (split_flag) arr.reverse();

	for(var i in arr){
		log("[" + arr[i].target + "] in process  --------------------");
		if (split_flag){
			process_split(arr[i].target);
		} else {
			process(arr[i]);
		}
		log("[" +arr[i].target + "] success --------------------");
		log("");
		log("");
	}

	if (SILENT) {
		print("done.");
	};
	
	writeLog();
}

/**
 * 处理进程
 * @param {object} project 项目对象
 */
function process(project/*object*/){
	var _files = project.include;
	var _fileCache = [];
	var i = 0;
	for (var k in _files) {
		var _fv = trim(IO.readFile(_files[k]));
		log(">> " + _files[k] + "     loaded... " + _fv.length + " byte.");
		_fv =  "<?cs #-------------------- ?>\r\n" + "<?cs #{" + _files[k] +"}?>" + "\r\n<?cs #-------------------- ?>\r\n\r\n"  + _fv ;
		if (i++ !=0){
			_fv = "\r\n\r\n\r\n" + _fv;
		}
		_fileCache.push(_fv);
	}
	
	log("");
	log("merge target files...");
	var _bf = _fileCache.join("");
	
	if (project.debugTarget) {
		log("Write file for debug... done.  File length: " + _bf.length + " byte.");
		IO.saveFile(USER_DIR + project.debugTarget, _bf);
	};
	
	if (project.target) {
		log("");
		log("min target file...");
		var _mfv = NO_MIN ? _bf : jsmin("/*\n" + COMMENT + "\n*/", _bf, 2);
		log("Write file for release... done.  File length: " + _mfv.length + " byte.");

		IO.saveFile(USER_DIR + project.target, _mfv);
	};
	log("");
}


/**
 * 分割处理进程
 * @param {object} project 项目对象
 */
var spilt_reg = /<\?cs #-+ \?>[\t\r\n ]+<\?cs #\{([^\n\r\}]+)\}\?>[\t\r\n ]+<\?cs #-+ \?>[\t\r\n ]/;
/*
<?cs #-------------------- ?>
<?cs #{user_icon.cs}?>
<?cs #-------------------- ?>
*/
function process_split(file){
	log("split file " + file);
	var filecnt = IO.readFile(file);

	var filelist = filecnt.split(spilt_reg);
	filelist.shift();
	var len = filelist.length;

	var source_folder = "cs/" + file + "-source";
	if (!overwrite_flag){
		var filereader = new Packages.java.io.File(source_folder);
		if (!filereader.exists()) filereader.mkdir();  
	}
	for(var i = 0; i < len; i +=2){
		var filename = trim(filelist[i]);
		var content = trim(filelist[i+1]);
		print(filename);
		if (overwrite_flag){
			IO.saveFile(filename, content);
		} else {
			IO.saveFile(source_folder + "/" + filename, content);
		}
	}
	return;
}
function trim(str){
	return (str + "").replace(/^[\s]+/, "").replace(/[\s]+$/, "");
}
/**
 * 记录log
 * @param {Object} str log信息
 */
function log(str){
	logCache.push(str + "\r\n");
	if (!SILENT) {
		print(str);
	}
}

/**
 * 把最近一次log写入文件
 */
function writeLog(){
	var _log = logCache.join("");
	var logfile = "/merge.log";
	if (split_flag) logfile = "/split.log";
	IO.saveFile(USER_DIR + logfile, _log);
}

main();
