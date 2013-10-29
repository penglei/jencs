<?cs set:UC_PLATFORM_ID_QZONE = 50?>				<?cs #/*空间*/?>
<?cs set:UC_PLATFORM_ID_PENGYOU = 51?>				<?cs #/*朋友*/?>
<?cs set:UC_PLATFORM_ID_MOBILE = 52?>				<?cs #/*手机*/?>
<?cs set:UC_PLATFORM_ID_WEIBO = 53?>				<?cs #/*微博*/?>
<?cs set:UC_PLATFORM_ID_THIRDAPP = 54?>				<?cs #/*第三方应用*/?>
<?cs set:UC_PLATFORM_ID_PHONEMANAGE = 55?>			<?cs #/*手机管家PC版*/?>
<?cs set:UC_PLATFORM_ID_OTHER = 59 ?>				<?cs #/*业务自定义，通过私有协议传*/?>

<?cs #subplatformid 为0表示默认，逻辑里面暂时没有用到 ?>

<?cs #/*空间*/?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][0] = "QQ空间"?><?cs #:1?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][1] = "QQ签名"?><?cs #:3?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][2] = "时光轴"?><?cs #:9?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][3] = "taotao主站"?><?cs #:10?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][4] = "心情机器人"?><?cs #:11?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][5] = "QQ美食"?><?cs #:12?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][6] = "QQ秀"?><?cs #:13?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][7] = "QQ浏览器"?><?cs #:14?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][8] = "QQ"?><?cs #:5?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][9] = "网页"?><?cs #:7?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][10] = "QQ提醒"?><?cs #:44?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][10].url = "http://tixing.qq.com"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_QZONE][11] = "QQ空间触屏版"?>


<?cs #/*朋友*/?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][0] = "朋友网"?><?cs #:8?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][1] = "朋友网二手交易"?><?cs #:15?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][2] = "朋友网客户端"?><?cs #:16?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][3] = "朋友网iPhone客户端"?><?cs #:17?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][4] = "朋友网iPad客户端"?><?cs #:18?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][5] = "朋友网android客户端"?><?cs #:19?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][6] = "朋友网android客户端"?><?cs #:20?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][7] = "朋友网WM客户端"?><?cs #:21?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][8] = "朋友网WP7客户端"?><?cs #:22?>
<?cs loop:i = 3, 8, 1?>
	<?cs set:qz_source_platform[UC_PLATFORM_ID_PENGYOU][i].url = "http://www.pengyou.com/mobile?from=home"?>
<?cs /loop?>


<?cs #/*手机*/?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][0] = "QQ空间手机版"?><?cs #:2?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][0].url = "http://z.qzone.com?from=kjavagrzxpl"?>

<?cs #所有默认的手机版?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][1] = "QQ空间手机版"?><?cs #:4?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][1].url = "http://z.qzone.com?from=kjavagrzxpl"?>

<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][2] = "iPhone"?><?cs #:23?><?cs #别用这个，用12?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][2].url = "http://z.qzone.com?from=iphonegrzxpl"?>

<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][3] = "iPad"?><?cs #:24?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][3].url = "http://z.qzone.com?from=ipadgrzxpl"?>

<?cs #所有Android都为4?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][4] = "Android"?><?cs #:25?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][4].url = "http://z.qzone.com?from=androidgrzxpl"?>

<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][5] = "QQ空间手机版"?><?cs #:26?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][5].url = "http://z.qzone.com?from=kjavagrzxpl"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][6] = "QQ空间手机版"?><?cs #:27?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][6].url = "http://z.qzone.com?from=kjavagrzxpl"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][7] = "QQ空间手机版"?><?cs #:28?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][7].url = "http://z.qzone.com?from=kjavagrzxpl"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][8] = "QQ空间手机版"?><?cs #:29?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][8].url = "http://z.qzone.com?from=kjavagrzxpl"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][9] = "手机短信"?><?cs #:30?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][10] = "手机彩信"?><?cs #:31?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][11] = "Q拍"?><?cs #:32?>

<?cs #所有iphone都为12, type为41?>
<?cs #对于(2:23),(3:24)就当作兼容版本，对外不再使用?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][12] = "iPhone"?><?cs #:41?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][12].url = "http://z.qzone.com?from=iphonegrzxpl"?>

<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][13] = "手机QQ"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][14] = "腾讯照片管家"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][14].url = "http://zhaopian.qq.com"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][15] = "Windows Phone"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][17] = "QQ Service"?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_MOBILE][18] = "微信上传助手"?>


<?cs #/*微博*/?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_WEIBO][0] = "腾讯微博"?><?cs #:6?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_WEIBO][1] = "微博客户端"?><?cs #:33?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_WEIBO][2] = "微博iPhone客户端"?><?cs #:34?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_WEIBO][3] = "微博iPad客户端"?><?cs #:35?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_WEIBO][4] = "微博android客户端"?><?cs #:36?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_WEIBO][5] = "微博symbian客户端"?><?cs #:37?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_WEIBO][6] = "微博WM客户端"?><?cs #:38?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_WEIBO][7] = "微博WP7客户端"?><?cs #:39?>


<?cs #/*第三方应用*/?>
<?cs #set:qz_source_platform[UC_PLATFORM_ID_THIRDAPP]?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_THIRDAPP][1] = "SOSO地图"?><?cs #:1?>


<?cs #/*手机管家PC版*/?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PHONEMANAGE] = "手机管家PC版"?><?cs #:43?>
<?cs set:qz_source_platform[UC_PLATFORM_ID_PHONEMANAGE].url = "http://sj.qq.com/?qid=990484"?>
