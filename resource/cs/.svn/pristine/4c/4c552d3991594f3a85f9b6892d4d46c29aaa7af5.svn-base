<?cs #:
	/**
	 * 对指定值做指定次数的htmlencode之后输出
	 * @param  {String} value 需要encode的值
	 * @param  {Number} time  次数
	 * @param  {Number} notprint  不直接打印出来，值为真时不打印，值为假时打印
	 * @return htmlEncodeVar_Res
	 */
	function htmlEncodeVar(value,time,notprint){}
?>
<?cs def:htmlEncodeVar(value,time,notprint) ?>
	<?cs if: time<=0 ?>
		<?cs set:time=1 ?>
	<?cs /if ?>
	<?cs set:htmlEncodeVar_Res=value ?>
	<?cs loop:i=1, time, 1 ?>
		<?cs set:htmlEncodeVar_Res=html_encode(htmlEncodeVar_Res,1) ?>
	<?cs /loop ?>
	<?cs if:notprint==0 ?>
		<?cs var:htmlEncodeVar_Res ?>
	<?cs /if ?>
<?cs /def ?>