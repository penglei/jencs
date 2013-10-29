<?cs def:textUserLink(uin, name) ?>
    <a  
        class="q_namecard q_des comment_nickname c_tx"
        href="http://user.qzone.qq.com/<?cs var:uin ?>"
        link="nameCard_<?cs var:uin ?> des_<?cs var:uin ?>"
    target="_blank"><?cs var:name ?></a>
<?cs /def ?>

<?cs def:commentList(list) ?>
	<?cs def:comment-items(item) ?>
            <div class="feeds_comm_list bg2">
                <div class="feeds_comment_cont">
                    <p class="feeds_comment_text">
                    	<?cs call:textUserLink(item.uin, item.nickname) ?>
                    	<?cs var:item.content ?>
                    </p>
                    <p class="feeds_comment_op">
                        <span class="feeds_time c_tx3">
                        	<?cs var:item.datetime ?>
                        </span>
                    </p>
                </div>
            </div>
        <?cs /def ?>
	<?cs if:list.0 || subcount(list.0) > 0 ?>
		<?cs each:item = list ?>
			<?cs call:comment-items(item) ?>
		<?cs /each ?>
	<?cs elif:list || subcount(list) > 0 ?>
		<?cs call:comment-items(list) ?>
	<?cs /if ?>
<?cs /def ?>

<div class="feeds_tp_4">
	<?cs if:qz_metadata.description || subcount(qz_metadata.description) > 0 ?>
		<div class="txtbox">
		<p>
			<?cs var:qz_metadata.description ?>
			<br/>
 		</p>
                </div>
	<?cs /if ?>
</div>
<div class="feeds_tp_5">
	<div class="feeds_tp_operate">
		<qz:reply type="link">回答</qz:reply>
		<a target="_blank" href="http://rc.qzone.qq.com/myhome/<?cs var:qz_metadata.qzone_app_ww ?>/<?cs var:qz_metadata.qzone_app_ww_question ?>/<?cs var:qz_metadata.qid ?>/" class="c_tx">查看详情</a>
	</div>
	<div class="feeds_comment">
		<div  class="comment_arrow c_bg2">◆</div>
		<?cs if:subcount(qz_metadata.comments) > 0 && qz_metadata.comments.count > 0 && subcount(qz_metadata.comments.item) > 0 ?>
			<?cs if:subcount(qz_metadata.comments) > 0 && qz_metadata.comments.count > 2 ?>
	                        <div class="more_feeds_comment bg2">
	                            <a target="_blank" href="http://rc.qzone.qq.com/myhome/<?cs var:qz_metadata.qzone_app_ww ?>/<?cs var:qz_metadata.qzone_app_ww_question ?>/<?cs var:qz_metadata.qid ?>/" class="c_tx">查看全部<?cs var:qz_metadata.comments.count ?>条回答<![CDATA[>>]]>
	                            </a>
	                        </div>
			<?cs /if ?>
			<?cs call:commentList(qz_metadata.comments.item) ?>
		<?cs /if ?>
                <qz:reply action="http://wenwen.qq.com/z/api/answer/submitfeed?format=html&amp;enc=utf-8" version="6" param="<?cs var:qz_metadata.qid ?>" type="text" charset="UTF-8" title="我来回答...">我来回答...</qz:reply>
	</div>
</div>
