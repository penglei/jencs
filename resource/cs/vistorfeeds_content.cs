<?cs def:getUserIcon(uin) ?>
		<?cs set:uinmod = uin % 4 + 1 ?>
		<?cs set:imgsrc = "http://qlogo"+uinmod+".store.qq.com/qzone/"+uin+"/"+uin+"/30"  ?>
		<?cs var:imgsrc ?>
<?cs /def ?>
<div class="f_ct bg2 bor2 f_ct_friendsvisit" 
	style="cursor:pointer" 
	onclick="QZONE.FrontPage.toApp(&quot;/friends/visitor/&quot;);QZONE.ICFeeds.VistorFeeds.sendClick(&quot;visitor_feeds_more&quot;);return false;">
	<?cs each:item=list ?>
		<div class="f_ct_imgtxt fv_list_item bor2">
			<div class="img_box clearfix">
				<?cs each:visitor=item.visitor_list ?>
					<a class="fri_ava q_namecard" link="nameCard_<?cs var:visitor.uin ?>" 
						href="http://user.qzone.qq.com/<?cs var:visitor.uin ?>" target="_blank" 
						onclick="QZONE.ICFeeds.VistorFeeds.sendClick(&quot;visitor_img_feeds&quot;);QZFL.event.cancelBubble();"
					>
						<img src="<?cs call:getUserIcon(visitor.uin) ?>">
					</a>
				<?cs /each ?>
			</div>
			<div class="txt_box">
				<p>
					<?cs if:item.type=="home" ?>
						<span class="c_tx3 mgr15">访问了</span><i class="icon_home"></i>我的主页
					<?cs elif:item.type=="post" ?>
						<span class="c_tx3 mgr15">访问了</span><i class="icon_post"></i>我的日志
					<?cs elif:item.type=="photo" ?>
						<span class="c_tx3 mgr15">访问了</span><i class="icon_photo"></i>我的相册
					<?cs elif:item.type=="poster" ?>
						<span class="c_tx3 mgr15">访问了</span><i class="icon_poster"></i>我的说说
					<?cs elif:item.type=="share" ?>
						<span class="c_tx3 mgr15">访问了</span><i class="icon_share"></i>我的分享
					<?cs elif:item.type=="video" ?>
						<span class="c_tx3 mgr15">访问了</span><i class="icon_video"></i>我的视频
					<?cs elif:item.type=="visitorgift" ?>
						<span class="c_tx3 mgr15">领取了</span><i class="icon_gift"></i>访客礼包
					<?cs /if ?>
				</p>
			</div>
		</div>
	<?cs /each ?>
	<div class="f_ct_imgtxt fv_list_item bor2 fv_list_item_total">
		<div class="txt_box">
			<p>
				<span>最近一周有<span class="c_tx ft16"><?cs var:week_cout ?></span>人来访，
					<?cs if:visitorgift_count ?>
					<a target="_blank" href="http://rc.qzone.qq.com/friends/visitor?tab=giftforvisitor" 
						onclick="QZONE.ICFeeds.VistorFeeds.sendClick(&quot;visitor_feeds_visitorgift_count&quot;);QZFL.event.cancelBubble();return true;" 
						class="c_tx ft16"
					>
						<?cs var:visitorgift_count ?>
					</a>人领了访客礼包，
					<?cs /if ?>
				</span>
				<a href="javascript:;" 
					onclick="QZONE.FrontPage.toApp(&quot;/friends/visitor/&quot;);QZONE.ICFeeds.VistorFeeds.sendClick(&quot;visitor_feeds_more&quot;);return false;"
				>
					看看大家都看了什么
				</a>
			</p>
		</div>
	</div>
</div>
