<?cs #:目前只用于亲子相册 后续再扩展 ?>
<?cs def:title_extend()?>
    <?cs if:qfv.titleExtend == 1 ?>
        <?cs if:qfv.titleExtend.type == ALBUM_BABY ?>
            <div class="f-sp-act bor2 qinzi-box">
                <div class="img-box">
                    <a href="<?cs var:qfv.titleExtend.url ?>" target="_blank" class=""><i class="icon-feed-kids-b"></i></a>
                </div>
                <div class="btn-box">
                    <a href="<?cs var:qfv.titleExtend.url ?>" target="_blank" class="btn-gray bgr2 c_tx_2"><span class="btn-inner">查看详情</span></a>
                </div>
                <div class="txt-box">
                    <div>
                        <span class="c_tx3">亲子相册</span> 
                        <a href="<?cs var:qfv.titleExtend.url ?>" target="_blank" class="f-name"><?cs var:html_encode(qfv.titleExtend.albumName,1) ?></a>
                    </div>
                    <div>
                        <span class="c_tx3">出生&nbsp;-&nbsp;
                            <?cs var:html_encode(qfv.titleExtend.extendinfo,1) ?>
                            的
                            <?cs var:html_encode(qfv.titleExtend.num,1) ?>
                            个瞬间</span>
                    </div>
                </div>
            </div>
        <?cs elif:qfv.titleExtend.type == ALBUM_TRAVEL ?>
            <div class="f-sp-act bor2 travel-box">
                <div class="img-box">
                    <a href="<?cs var:qfv.titleExtend.url ?>" target="_blank" class=""><i class="icon-feed-travel-b"></i></a>
                </div>
                <div class="btn-box">
                    <a href="<?cs var:qfv.titleExtend.url ?>" target="_blank" class="btn-gray bgr2 c_tx_2"><span class="btn-inner">查看详情</span></a>
                </div>
                <div class="txt-box">
                    <div>
                        <span class="c_tx3">旅游相册</span> 
                    </div>
                    <div>
                        <span class="c_tx3">去看看我在&nbsp;<a href="<?cs var:qfv.titleExtend.url ?>" target="_blank"><?cs var:html_encode(qfv.titleExtend.albumName,1) ?></a>
                        &nbsp;的更多精彩瞬间吧</span>
                    </div>
                </div>
            </div>
        <?cs /if ?>
    <?cs /if ?>
<?cs /def?>