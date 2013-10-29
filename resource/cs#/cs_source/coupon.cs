<?cs #:优惠券组件  ?>
<?cs def:coupon(mod) ?>
<?cs if:qz_metadata.content_box.content.con.type=="coupon_box" ?>
<?cs with:coupon = qz_metadata.content_box.content.con ?>
<?cs #:这里为什么要做html_decode？？ 因为后台多做了一次encode！  ?>
<qz:plugin name="Coupon" config="url=<?cs escape:'url'?><?cs var:html_decode(coupon.url,1) ?><?cs /escape?>">
	<div class="f_ct_imgtxt" style="cursor:pointer">
		<div class="img_box"><a href="javascript:;"><img src="<?cs var:coupon.pic ?>" alt=""></a></div>
		<div class="txt_box">
			<p><?cs var:coupon.content ?></p>
			<p><a href="javascript:;" title="点击领取">点击领取</a></p>
		</div>
	</div>
</qz:plugin>
<?cs /with ?>
<?cs /if ?>
<?cs /def ?>