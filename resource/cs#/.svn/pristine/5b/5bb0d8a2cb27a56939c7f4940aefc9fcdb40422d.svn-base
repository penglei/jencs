<?cs def:tieban_contentBox_start(tieban)?>
    <qz:plugin name="Tieban" config="url=<?cs escape:'url'?><?cs var:html_decode(tieban.url,1) ?><?cs /escape?>&tb_id=<?cs var:html_decode(tieban.tb_id,1) ?>">
	<div class="f_ct f_ct_2 bor3 bg3 f_ct_webpage">
	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
	<div class="f_ct_imgtxt">
	<?cs set:g_f_ct_x_has_closed = 0?>
<?cs /def?>

<?cs def:tieban_contentBox_end()?>
		</div><?cs #必须先闭合 .f_ct_imgtxt?>
	</div>
	</qz:plugin>
<?cs /def?>
<?cs call:title()?>

<?cs #-------------------- ?>
<?cs #{coupon.cs}?>
<?cs #-------------------- ?>

<?cs #:优惠券组件  ?>
<?cs def:coupon_content(coupon) ?>
<?cs #:这里为什么要做html_decode？？ 因为后台多做了一次encode！  ?>
<div class="f_ct f_ct_2 bor3 bg3">
<qz:plugin name="Coupon" data-clicklog="shangcheng" config="url=<?cs escape:'url'?><?cs var:coupon.url ?><?cs /escape?>&height=<?cs var:coupon.height ?>">
	<div class="f_ct_imgtxt" style="cursor:pointer">
		<div class="img_box"><a href="javascript:;"><img src="<?cs var:coupon.pic ?>" alt=""></a></div>
		<div class="txt_box">
			<p><?cs call:conCommon(qfv.content.cntText.con)?></p>
			<p><a href="javascript:;" title="<?cs var:coupon.btn ?>"><?cs var:coupon.btn ?></a></p>
		</div>
	</div>
</qz:plugin>
</div>
<?cs /def ?>

<?cs if:subcount(qfv.content.coupon)?>
    <?cs call:summary_start()?>
	<?cs call:coupon_content(qfv.content.coupon)?>
	<?cs call:operate()?>
	<?cs call:comments-like()?>
	<?cs call:summary_end()?>
<?cs elif:subcount(qfv.content.tieban)==0?>
    <?cs call:summary("","")?>
<?cs else ?>
    <?cs call:summary_start()?>
	<?cs call:tieban_contentBox_start(qfv.content.tieban)?>
	<?cs call:contentMedia()?>
	<?cs call:contentTxt_start("")?>
	<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
	<?cs call:conCommon(qfv.content.cntText.con)?>
	<?cs call:contentTxt_end()?>
	<?cs call:tieban_contentBox_end()?>
	<?cs call:operate()?>
	<?cs call:summary_end()?>
<?cs /if ?>
