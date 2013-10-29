<?cs #:来源 ?>
<?cs set:qz_source_type[1] = 'QQ空间'?>
<?cs set:qz_source_type[2] = 'QQ空间手机版'?>
<?cs set:qz_source_type[2].url = 'http://z.qzone.com?from=kjavagrzxpl'?>
<?cs set:qz_source_type[3] = 'QQ签名'?>
<?cs set:qz_source_type[4] = 'QQ空间手机版'?>
<?cs set:qz_source_type[4].url = 'http://z.qzone.com?from=kjavagrzxpl'?>
<?cs set:qz_source_type[5] = 'QQ'?>
<?cs set:qz_source_type[6] = '腾讯微博'?>
<?cs set:qz_source_type[7] = '网页'?>
<?cs set:qz_source_type[8] = '朋友网'?>
<?cs set:qz_source_type[9] = '时光轴'?>
<?cs set:qz_source_type[10] = 'taotao主站'?>
<?cs set:qz_source_type[11] = '心情机器人'?>
<?cs set:qz_source_type[12] = 'QQ美食'?>
<?cs set:qz_source_type[13] = 'QQ秀'?>
<?cs set:qz_source_type[14] = 'QQ浏览器'?>
<?cs set:qz_source_type[15] = '朋友网二手交易'?>
<?cs set:qz_source_type[16] = '朋友网客户端'?>
<?cs set:qz_source_type[17] = '朋友网iPhone客户端'?>
<?cs set:qz_source_type[18] = '朋友网iPad客户端'?>
<?cs set:qz_source_type[19] = '朋友网android客户端'?>
<?cs set:qz_source_type[20] = '朋友网android客户端'?>
<?cs set:qz_source_type[21] = '朋友网WM客户端'?>
<?cs set:qz_source_type[22] = '朋友网WP7客户端'?>
<?cs set:qz_source_type_pengyouClient  = 'http://www.pengyou.com/mobile?from=home' ?>

<?cs set:qz_source_type[23] = 'QQ空间iPhone版'?>
<?cs set:qz_source_type[23].url = 'http://z.qzone.com?from=iphonegrzxpl'?>
<?cs set:qz_source_type[24] = 'QQ空间iPhone版'?><?cs #以前的ipad?>
<?cs set:qz_source_type[24].url = 'http://z.qzone.com?from=iphonegrzxpl'?>

<?cs set:qz_source_type[25] = 'QQ空间Android版'?>
<?cs set:qz_source_type[25].url = 'http://z.qzone.com?from=androidgrzxpl'?>
<?cs set:qz_source_type[26] = 'QQ空间手机版'?>
<?cs set:qz_source_type[26].url = 'http://z.qzone.com?from=kjavagrzxpl'?>
<?cs set:qz_source_type[27] = 'QQ空间手机版'?>
<?cs set:qz_source_type[27].url = 'http://z.qzone.com?from=kjavagrzxpl'?>
<?cs set:qz_source_type[28] = 'QQ空间手机版'?>
<?cs set:qz_source_type[28].url = 'http://z.qzone.com?from=kjavagrzxpl'?>
<?cs set:qz_source_type[29] = 'QQ空间手机版'?>

<?cs set:qz_source_type[30] = '手机短信'?>
<?cs set:qz_source_type[31] = '手机彩信'?>
<?cs set:qz_source_type[32] = 'Q拍'?>
<?cs set:qz_source_type[33] = '微博客户端'?>
<?cs set:qz_source_type[34] = '微博iPhone客户端'?>
<?cs set:qz_source_type[35] = '微博iPad客户端'?>
<?cs set:qz_source_type[36] = '微博android客户端'?>
<?cs set:qz_source_type[37] = '微博symbian客户端'?>
<?cs set:qz_source_type[38] = '微博WM客户端'?>
<?cs set:qz_source_type[39] = '微博WP7客户端'?>
<?cs set:qz_source_type[40] = '影视频道'?>
<?cs set:qz_source_type[40].url = 'http://rc.qzone.qq.com/appsetup/video?source=vote.feed' ?>

<?cs set:qz_source_type[41] = 'QQ空间iPhone版'?>
<?cs set:qz_source_type[41].url = 'http://z.qzone.com?from=iphonegrzxpl' ?>

<?cs set:qz_source_type[42] = 'QQ影像空间版'?>
<?cs set:qz_source_type[42].url = 'http://image.qq.com/qzone/' ?>
<?cs set:qz_source_type[43] = '手机管家PC版'?>
<?cs set:qz_source_type[43].url = 'http://sj.qq.com/?qid=990484' ?>
<?cs set:qz_source_type[44] = 'QQ提醒'?>
<?cs set:qz_source_type[44].url = 'http://rc.qzone.qq.com/myhome/904' ?>


<?cs # 为了兼容现存的来源数据，会检查名称是否在展示范围，以后新增来源必须按规定带上platformid和subplatformid  ?>
<?cs # 等业务都带上指定的字段后，这个方法连同这个文件一起抛弃掉。2013-03-20 ?>
<?cs def:opr_name_filter(name) ?>
	<?cs set: opr_name_filter.ret = 0 ?>
	<?cs if: string.find(name, "iPhone") > -1
			|| string.find(name, "iphone") > -1
			|| string.find(name, "GALAXY") > -1
			|| string.find(name, "galaxy") > -1
			|| string.find(name, "iPad") > -1
			|| string.find(name, "ipad") > -1
			|| string.find(name, "android") > -1
			|| string.find(name, "Android") > -1
			|| string.find(name, "QQ空间触屏版") > -1 ?>
		<?cs set: opr_name_filter.ret = 1 ?>
	<?cs /if ?>
<?cs /def ?>


<?cs def:opr_source_deprecated()?>

<?cs call:opr_name_filter(qz_source_type[qz_metadata.source.type]) ?>
<?cs set:filter_ret_1 = opr_name_filter.ret ?>
<?cs call:opr_name_filter(qz_metadata.source.name) ?>
<?cs set:filter_ret_2 = opr_name_filter.ret ?>

<?cs if:qz_metadata.source.type || subcount(qz_metadata.source) > 0 ?>
<?cs if:filter_ret_1 || filter_ret_2 ?>
	<span class="ui_mr10">
	<?cs if:qz_metadata.source.type != 0?>
		<?cs if:qz_source_type[qz_metadata.source.type] ?>
		来自
			<?cs if:qz_source_type[qz_metadata.source.type].url ?>
				<a class="c_tx3" href="<?cs var:qz_source_type[qz_metadata.source.type].url ?>" target="_blank"><?cs var:qz_source_type[qz_metadata.source.type] ?></a>
			<?cs elif: qz_metadata.source.type >= 15 && qz_metadata.source.type <= 22 ?><?cs #朋友客户端 ?>
				<a class="c_tx3" href="<?cs var:qz_source_type_pengyouClient ?>" target="_blank"><?cs var:qz_source_type[qz_metadata.source.type] ?></a>
			<?cs else ?>
				<?cs var:qz_source_type[qz_metadata.source.type] ?>
			<?cs /if ?>
		<?cs /if ?>
	<?cs elif:qz_metadata.source.type == 0 ?>
		<?cs if:qz_metadata.source.url ?>
			来自<a class="c_tx3" href="<?cs var:qz_metadata.source.url ?>" target="_blank"><?cs var:qz_metadata.source.name ?></a>
		<?cs else ?>
			来自<?cs var:qz_metadata.source.name?>
		<?cs /if ?>
	<?cs else ?>
	来源未知
	<?cs /if ?>
	</span>
<?cs /if ?>
<?cs /if ?>

<?cs /def?>

