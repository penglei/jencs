<?cs #:来源 ?>
<?cs set:qz_source_type[1] = 'QQ空间'?>
<?cs set:qz_source_type[2] = '手机QQ空间'?>
<?cs set:qz_source_type[3] = 'QQ签名'?>
<?cs set:qz_source_type[4] = '手机QQ空间软件版'?>
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
<?cs set:qz_source_type[17] = '朋友网iphone客户端'?>
<?cs set:qz_source_type[18] = '朋友网ipad客户端'?>
<?cs set:qz_source_type[19] = '朋友网android客户端'?>
<?cs set:qz_source_type[20] = '朋友网sybiam客户端'?>
<?cs set:qz_source_type[21] = '朋友网WM客户端'?>
<?cs set:qz_source_type[22] = '朋友网WP7客户端'?>
<?cs set:qz_source_type[23] = 'iphone客户端'?>
<?cs set:qz_source_type[23].url = 'http://mobile.qq.com/qzone/iphone/'?>
<?cs set:qz_source_type[24] = 'ipad客户端'?>
<?cs set:qz_source_type[24].url = 'http://mobile.qq.com/qzone/iphone/'?>
<?cs set:qz_source_type[25] = 'android客户端'?>
<?cs set:qz_source_type[25].url = 'http://mobile.qq.com/qzone/iphone/'?>
<?cs set:qz_source_type[26] = 'sybiam客户端'?>
<?cs set:qz_source_type[26].url = 'http://mobile.qq.com/qzone/s60v5/'?>
<?cs set:qz_source_type[27] = 'WM客户端'?>
<?cs set:qz_source_type[27].url = 'http://mobile.qq.com/qzone/windowsmobile/'?>
<?cs set:qz_source_type[28] = 'WP7客户端'?>
<?cs set:qz_source_type[28].url = 'http://mobile.qq.com/qzone/wp7/'?>
<?cs set:qz_source_type[29] = '手机Qzone'?>
<?cs set:qz_source_type[30] = '手机短信'?>
<?cs set:qz_source_type[31] = '手机彩信'?>
<?cs set:qz_source_type[32] = 'QPAI'?>
<?cs set:qz_source_type[33] = '微博客户端'?>
<?cs set:qz_source_type[34] = '微博iphone客户端'?>
<?cs set:qz_source_type[35] = '微博ipad客户端'?>
<?cs set:qz_source_type[36] = '微博android客户端'?>
<?cs set:qz_source_type[37] = '微博sybiam客户端'?>
<?cs set:qz_source_type[38] = '微博WM客户端'?>
<?cs set:qz_source_type[39] = '微博WP7客户端'?>

<?cs def:source(mod) ?>
    <?cs if:qz_metadata.source.type || subcount(qz_metadata.source) > 0 ?>
        <span class="ui_mr10">
        <?cs if:qz_metadata.source.type != 0 ?>         
        <?cs with:stype = qz_metadata.source.type ?>
            <?cs if:qz_source_type[stype] ?>
            通过
                <?cs if:qz_source_type[stype].url ?>
                    <a class="c_tx" href="<?cs var:qz_source_type[stype].url ?>" target="_blank"><?cs var:qz_source_type[stype] ?></a>
                <?cs else ?>
                    <?cs var:qz_source_type[stype] ?>
                <?cs /if ?>
            <?cs /if ?>
        <?cs /with ?>
        <?cs elif:qz_metadata.source.type == 0 ?>
            <?cs if:qz_metadata.source.url ?>
                通过<a class="c_tx" href="<?cs var:qz_metadata.source.url ?>" target="_blank"><?cs var:qz_metadata.source.name ?></a>
            <?cs else ?>
                通过<?cs var:qz_metadata.source.name ?>
            <?cs /if ?>
        <?cs else ?>
        来源未知
        <?cs /if ?>
        </span>
    <?cs /if ?>
<?cs /def ?>