<?cs ####
	/**
	 * 智能排版 data层
	 */
?>
<?cs ####
/**
* 1. 最基本的筛选：basic_condition
*      因为有的feeds图片没有带宽高信息，或者图片尺寸很小。这种情况下，统一走原来九宫格的逻辑
* 2. 关于突出：smart_layout_is_outstanding
*      需求单的关于图片“突出”在逻辑上的概念没有明确，这里把有头像的图片等同于突出
* 3. 基础条件：
*      选择了方案后，必须满足图片要铺满的原则，如果不能把图片铺满，就放弃那种方案。
* 4. 排版选择的核心算法如下：
* 若4张图片里面有 1张突出，且突出图片高>宽，选A
* 若4张图片里面有 1张突出，且突出图片宽>高，选C
* 若4张图片里面有 2张突出，且两张突出图片同时满足宽>高 ，选B
* 若4张图片里面有 2张突出，且两张突出图片同时满足 高>宽，选D
* 若4张图片里面有 2张突出，且一张突出图片满足 高>宽，另一张满足 宽>高，选B（优先横图）
* 若4张图片里面有 3张突出，且剩余一张图片满足 高>宽，选 A
* 若4张图片里面有 3张突出，且剩余一张图片满足 宽>高，选 B 
* 若4张图片里面有 4张突出，选九宫格
* 5. 尽量保证顺序
*     A B C D 四个图片，如果C是突出的，那么ABD还是按顺序输出
* A B C D 四个图片，如果B,D是突出的，那么AC还是按顺序输出
* 
*/
?>
<?cs ####
/**
由于判断逻辑较多，这里采用评分机制来选择最优方案，
若突出图片数为1 那么 4_A 、4_C  +100
同理突出图片数为2 那么 4_B 、4_D +100
如此类推。。。

若突出图片高>宽， 那么 4_A + 10
若突出图片宽>高， 那么 4_B + 10
*/
?>
<?cs #: 数字前面加#可以强制转换字符串为整形 ?>
<?cs set:SMART_LAYOUT_OUTSTANDING_COUNT_POINT = 100 ?> <?cs #:突出图片数的基础分 ?>
<?cs set:SMART_LAYOUT_WIDTH_GREATER_POINT = 20?> <?cs #: 宽比高大的基础分 ?>
<?cs set:SMART_LAYOUT_HEIGHT_GREATER_POINT = 10?> <?cs #: 高比宽大的基础分 ?>


<?cs def:_print(name,value) ?>
	<?cs if:1==0 ?>
		<?cs var:name ?> = <?cs var:value ?><br>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/**
	 * 走智能排版的基本条件，所有图片拥有宽高信息，且最大尺寸图片宽高大于100 
	 */
?>
<?cs def:smart_layout_basic_condition(piclist) ?>
	<?cs set:_is_satisfied = 0 ?>

	<?cs if:subcount(piclist)>0 ?>
		<?cs set:_is_satisfied = 1 ?>
		<?cs set:_end = subcount(piclist) - 1 ?>
		<?cs loop:i = 0, _end, 1?>
			<?cs #call:_print("i", i) ?>
			<?cs with:picinfo = piclist[i].picinfo?>
				<?cs #:拿最大尺寸来比较 ?>
				<?cs set:largest = subcount(picinfo) -1  ?>
				<?cs #call:_print("largest", largest) ?>
				<?cs if:picinfo[largest].width < 100 || picinfo[largest].height < 100 ?>
					<?cs #call:_print("第"+ i + "张图片不满足宽高大于100","") ?>
					<?cs #call:_print("图片宽为",picinfo[largest].width) ?>
					<?cs #call:_print("图片高为",picinfo[largest].height) ?>
					<?cs set:_is_satisfied = 0?>
					<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break  ?>
				<?cs /if ?>
			<?cs /with ?>
		<?cs /loop ?>
	<?cs /if ?>
	<?cs set:smart_layout_basic_condition.ret = _is_satisfied?>
<?cs /def ?>

<?cs ####
	/**
	 * 判断图片是否突出
	 */
?>
<?cs def:smart_layout_is_outstanding(pic) ?>
	<?cs if:pic.picinfo[0].extendinfo.withhead == 1 ?>
		<?cs set:smart_layout_is_outstanding.ret = 1 ?>
	<?cs else ?>
		<?cs set:smart_layout_is_outstanding.ret = 0 ?>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/**
	 * 算出总共有多少张图片突出，序号是多少
	 */
?>
<?cs def:smart_layout_get_outstanding_pics(piclist) ?>
	<?cs set:count = 0?>
	<?cs set:left_count = 0?>

	<?cs #loop:i = 0, subcount(piclist)-1, 1?>
		<?cs #loop:j = 0, 2, 1?>
			<?cs #call:_print("piclist[" + i + "].picinfo[" + j + "].extendinfo.withhead", piclist[i].picinfo[j].extendinfo.withhead) ?>
			<?cs #call:_print("piclist[" + i + "].picinfo[" + j + "].extendinfo.centerpoint_x",piclist[i].picinfo[j].extendinfo.centerpoint_x) ?>
			<?cs #call:_print("piclist[" + i + "].picinfo[" + j + "].extendinfo.centerpoint_y",piclist[i].picinfo[j].extendinfo.centerpoint_y) ?>
			<?cs #call:_print("piclist[" + i + "].picinfo[" + j + "].width",piclist[i].picinfo[j].width) ?>
			<?cs #call:_print("piclist[" + i + "].picinfo[" + j + "].height",piclist[i].picinfo[j].height) ?>
		<?cs #/loop ?>
	<?cs #/loop ?>
	
	<?cs loop:i = 0, subcount(piclist), 1?>
		<?cs with:pic = piclist[i]?>
			<?cs call:smart_layout_is_outstanding(pic) ?>
			<?cs if:smart_layout_is_outstanding.ret == 1 ?>
				<?cs set:smart_layout_get_outstanding_pics.pic[count] = i?>
				<?cs set:count = count +1?>
			<?cs else ?>
				<?cs set:smart_layout_get_outstanding_pics.left[left_count] = i?>
				<?cs #call:_print("smart_layout_get_outstanding_pics.left[left_count]", smart_layout_get_outstanding_pics.left[left_count]) ?>
				<?cs set:left_count = left_count +1?>
			<?cs /if ?>
		<?cs /with ?>
	<?cs /loop ?>
	<?cs set:smart_layout_get_outstanding_pics.count = count?>
<?cs /def ?>

<?cs ####
	/**
	 * 得到当前最优排版的各个容器的宽高
	 */
?>
<?cs def:smart_layout_box(layout) ?>
	<?cs #call:_print("layout.box[i].width", layout.box[i].width) ?>
	<?cs loop:i = 0, 3, 1?>
		<?cs set:smart_layout_box.ret[i].width  =  layout.box[i].width ?>
		<?cs set:smart_layout_box.ret[i].height =  layout.box[i].height ?>
	<?cs /loop ?>
<?cs /def ?>

<?cs #:四图 ?>
<?cs #### 4 - A 
	/**  
	 *	    | ——  	(223,400) (172,130)  
	 *      | ——              (172,130)
	 *		| ——              (172,130)
	 */      
?>
<?cs set:G_SMART_LAYOUT_4_A = "4_A"?>
<?cs set:G_SMART_LAYOUT_4_A.box.0.width =  223?>
<?cs set:G_SMART_LAYOUT_4_A.box.0.height = 400?>
<?cs set:G_SMART_LAYOUT_4_A.box.1.width =  172?>
<?cs set:G_SMART_LAYOUT_4_A.box.1.height = 130?>
<?cs set:G_SMART_LAYOUT_4_A.box.2.width =  172?>
<?cs set:G_SMART_LAYOUT_4_A.box.2.height = 130?>
<?cs set:G_SMART_LAYOUT_4_A.box.3.width =  172?>
<?cs set:G_SMART_LAYOUT_4_A.box.3.height = 130?>

<?cs ####
	/**
	 * 算出4_A 的图片输出顺序
	 */
?>
<?cs def:smart_layout_4_A_sequence() ?>
	<?cs #:只有一张突出图片 ?>
	<?cs if:smart_layout_get_outstanding_pics.count == 1 ?>
		<?cs set:_sequence.0 = smart_layout_get_outstanding_pics.pic[0] ?>
		<?cs set:_sequence.1 = smart_layout_get_outstanding_pics.left[0] ?>
		<?cs set:_sequence.2 = smart_layout_get_outstanding_pics.left[1] ?>
		<?cs set:_sequence.3 = smart_layout_get_outstanding_pics.left[2] ?>
	<?cs elif smart_layout_get_outstanding_pics.count == 3?>
		<?cs set:_sequence.0 = smart_layout_get_outstanding_pics.left[0] ?>
		<?cs set:_sequence.1 = smart_layout_get_outstanding_pics.pic[0] ?>
		<?cs set:_sequence.2 = smart_layout_get_outstanding_pics.pic[1] ?>
		<?cs set:_sequence.3 = smart_layout_get_outstanding_pics.pic[2] ?>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/**
	 * 计算4_A排版下的分数
	 */
?>
<?cs def:smart_layout_4_A(piclist) ?>
	<?cs set: _point = 0  ?>
	<?cs #:只有一张突出图片，加100 ?>
	<?cs if:smart_layout_get_outstanding_pics.count == 1 ?>
		<?cs #call:_print("goin", true) ?>
		<?cs set: _point = #_point + #SMART_LAYOUT_OUTSTANDING_COUNT_POINT ?>
		<?cs #call:_print("_point", _point) ?>
		<?cs #:拿到突出图片 ?>
		<?cs with: pic0 = piclist[#smart_layout_get_outstanding_pics.pic.0] ?>
			<?cs #:高>宽的话 加 10 ?>
			<?cs if:pic0.picinfo[0].height >= pic0.picinfo[0].width ?>
				<?cs set: _point = #_point + #SMART_LAYOUT_HEIGHT_GREATER_POINT ?>
			<?cs /if ?>
		<?cs /with ?>
		<?cs #:所有图片有能力铺满 ?>
		<?cs set:_is_satisfied = 1?>
		<?cs loop:i = 0, subcount(piclist), 1?>
			<?cs with:picinfo = piclist[i].picinfo?>
				<?cs #:拿最大尺寸来比较 ?>
				<?cs set:largest = subcount(picinfo) -1  ?>
				<?cs #:突出图片的判断 ?>
				<?cs #call:_print("smart_layout_get_outstanding_pics.left[0]", smart_layout_get_outstanding_pics.left[0]) ?>
				<?cs if:i == smart_layout_get_outstanding_pics.pic[0] ?>
					<?cs if:picinfo[largest].width < 223 || picinfo[largest].height < 400 ?>
						<?cs set:_is_satisfied = 0?>
						<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
					<?cs /if ?>
				<?cs else ?>
					<?cs if:picinfo[largest].width < 172 || picinfo[largest].height < 130 ?>
						<?cs set:_is_satisfied = 0?>
						<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
					<?cs /if ?>
				<?cs /if ?>
			<?cs /with ?>
		<?cs /loop ?>

		<?cs #:不能铺满就0分了 ?>
		<?cs if:_is_satisfied == 0 ?>
			<?cs set: _point = 0 ?>
		<?cs /if ?>

	<?cs #:有三张突出图片，加100 ?>

	<?cs elif smart_layout_get_outstanding_pics.count == 3?>
		<?cs #call:_print("有三张突出图片", "") ?>
		<?cs set: _point = #_point + #SMART_LAYOUT_OUTSTANDING_COUNT_POINT ?>
		<?cs #:拿到不突出图片 ?>
		<?cs with: pic0 = piclist[#smart_layout_get_outstanding_pics.left.0] ?>
			<?cs #:高>宽的话 加 10 ?>
			<?cs #call:_print("高>宽", "") ?>
			<?cs if:pic0.picinfo[0].height >= pic0.picinfo[0].width ?>
				<?cs set: _point = #_point + #SMART_LAYOUT_HEIGHT_GREATER_POINT ?>
			<?cs /if ?>
		<?cs /with ?>
		<?cs #:所有图片有能力铺满 ?>
		<?cs set:_is_satisfied = 1?>
		<?cs loop:i = 0, subcount(piclist)-1, 1?>
			<?cs with:picinfo = piclist[i].picinfo?>
				<?cs #:拿最大尺寸来比较 ?>
				<?cs set:largest = subcount(picinfo) -1  ?>
				<?cs #call:_print("picinfo[largest].width", picinfo[largest].width) ?>
				<?cs #call:_print("picinfo[0].width", picinfo[0].width) ?>
				<?cs #call:_print("smart_layout_get_outstanding_pics.left[0]", smart_layout_get_outstanding_pics.left[0]) ?>
				<?cs #call:_print("i", i) ?>
				<?cs #:突出图片的判断 ?>
				<?cs if:i != smart_layout_get_outstanding_pics.left[0] ?>
					<?cs #call:_print("i != smart_layout_get_outstanding_pics.left[0]", "") ?>
					<?cs #call:_print("picinfo[largest].width", picinfo[largest].width) ?>
					<?cs if:picinfo[largest].width < 172  || picinfo[largest].height < 130 ?>
						<?cs set:_is_satisfied = 0?>
						<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
						<?cs #call:_print("_is_satisfied", _is_satisfied) ?>
						<?cs #call:_print("i", i) ?>
					<?cs /if ?>
				<?cs else ?>
					<?cs #call:_print("else", "") ?>
					<?cs #call:_print("picinfo[largest].width", picinfo[largest].width) ?>
					<?cs if:picinfo[largest].width < 223 || picinfo[largest].height < 400 ?>
						<?cs set:_is_satisfied = 0?>
						<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
						<?cs #call:_print("_is_satisfied", _is_satisfied) ?>
						<?cs #call:_print("i", i) ?>
					<?cs /if ?>
				<?cs /if ?>
			<?cs /with ?>
		<?cs /loop ?>

		<?cs #:不能铺满就0分了 ?>
		<?cs if:_is_satisfied == 0 ?>
			<?cs set: _point = 0 ?>
		<?cs /if ?>
	<?cs /if ?>
	<?cs set: smart_layout_4_A.point = _point ?>
<?cs /def ?>


<?cs #### 4 - B 
	/**     —— - (250,196) (145,196)
	 *      - —— (145,196) (250,196)
	 */      
?>
<?cs set:G_SMART_LAYOUT_4_B = "4_B"?>
<?cs set:G_SMART_LAYOUT_4_B.box.0.width =  250?>
<?cs set:G_SMART_LAYOUT_4_B.box.0.height = 196?>
<?cs set:G_SMART_LAYOUT_4_B.box.1.width =  145?>
<?cs set:G_SMART_LAYOUT_4_B.box.1.height = 196?>
<?cs set:G_SMART_LAYOUT_4_B.box.2.width =  145?>
<?cs set:G_SMART_LAYOUT_4_B.box.2.height = 196?>
<?cs set:G_SMART_LAYOUT_4_B.box.3.width =  250?>
<?cs set:G_SMART_LAYOUT_4_B.box.3.height = 196?>

<?cs ####
	/**
	 * 算出4_B 的图片输出顺序
	 */
?>
<?cs def:smart_layout_4_B_sequence() ?>
	<?cs set:_sequence.0 = smart_layout_get_outstanding_pics.pic[0] ?>
	<?cs set:_sequence.1 = smart_layout_get_outstanding_pics.left[0] ?>
	<?cs set:_sequence.2 = smart_layout_get_outstanding_pics.left[1] ?>
	<?cs set:_sequence.3 = smart_layout_get_outstanding_pics.pic[1] ?>
<?cs /def ?>

<?cs ####
	/**
	 * 计算4_B排版下的分数
	 */
?>
<?cs def:smart_layout_4_B(piclist) ?>
	<?cs set: _point = 0 ?>
	<?cs if:smart_layout_get_outstanding_pics.count == 2 ?>
		<?cs #:一张突出图片+100 ?>
		<?cs set: _point = #_point + #SMART_LAYOUT_OUTSTANDING_COUNT_POINT ?>
		<?cs #call:_print("4_B:_point", _point) ?>
		<?cs #:拿到突出图片 ?>
		<?cs with: pic0 = piclist[#smart_layout_get_outstanding_pics.pic.0] ?>
			<?cs #:高<宽的话 加 20 ?>
			<?cs #call:_print("4_B:pic0.picinfo[0].height", pic0.picinfo[0].height) ?>
			<?cs if:pic0.picinfo[0].height <= pic0.picinfo[0].width ?>
				<?cs set: _point = #_point + #SMART_LAYOUT_WIDTH_GREATER_POINT ?>
				<?cs #call:_print("4_B_b:_point", _point) ?>
			<?cs /if ?>
		<?cs /with ?>
		<?cs with: pic1 = piclist[#smart_layout_get_outstanding_pics.pic.1] ?>
			<?cs #:高<宽的话 加 20 ?>
			<?cs if:pic1.picinfo[0].height <= pic1.picinfo[0].width ?>
				<?cs set: _point = #_point + #SMART_LAYOUT_WIDTH_GREATER_POINT ?>
				<?cs #call:_print("4_B_b_B:_point", _point) ?>
			<?cs /if ?>
		<?cs /with ?>

		<?cs #:所有图片有能力铺满 ?>
		<?cs set:_is_satisfied = 1?>
		<?cs loop:i = 0, subcount(piclist)-1, 1?>
			<?cs with:picinfo = piclist[i].picinfo?>
			<?cs #:拿最大尺寸来比较 ?>
			<?cs set:largest = subcount(picinfo) -1  ?>
			<?cs #:突出图片的判断 ?>
			<?cs if:i == smart_layout_get_outstanding_pics.pic[0] || i == smart_layout_get_outstanding_pics.pic[1] ?>
				<?cs if:picinfo[largest].width < 250 || picinfo[largest].height < 196 ?>
					<?cs set:_is_satisfied = 0?>
					<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
				<?cs /if ?>
			<?cs else ?>
				<?cs if:picinfo[largest].width < 145 || picinfo[largest].height < 196 ?>
					<?cs set:_is_satisfied = 0?>
					<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
				<?cs /if ?>
			<?cs /if ?>
			<?cs /with ?>
		<?cs /loop ?>

		<?cs #:不能铺满就0分了 ?>
		<?cs if:_is_satisfied == 0 ?>
			<?cs #call:_print("4_B_b_B_44:_point", _point) ?>
			<?cs set: _point = 0 ?>
		<?cs /if ?>
	<?cs /if ?>
	<?cs set: smart_layout_4_B.point = #_point ?>
<?cs /def ?>

<?cs #### 4 - C
	/**     ——————           (400,300)
	 *		_ _  _  (130,95)(130,95)(130,95)
	 */      
?>
<?cs set:G_SMART_LAYOUT_4_C = "4_C"?>
<?cs set:G_SMART_LAYOUT_4_C.box.0.width =  400?>
<?cs set:G_SMART_LAYOUT_4_C.box.0.height = 300?>
<?cs set:G_SMART_LAYOUT_4_C.box.1.width =  130?>
<?cs set:G_SMART_LAYOUT_4_C.box.1.height = 95?>
<?cs set:G_SMART_LAYOUT_4_C.box.2.width =  130?>
<?cs set:G_SMART_LAYOUT_4_C.box.2.height = 95?>
<?cs set:G_SMART_LAYOUT_4_C.box.3.width =  130?>
<?cs set:G_SMART_LAYOUT_4_C.box.3.height = 95?>

<?cs ####
	/**
	 * 算出4_C 的图片输出顺序
	 */
?>
<?cs def:smart_layout_4_C_sequence() ?>
	<?cs #:只有一张突出图片 ?>
	<?cs if:smart_layout_get_outstanding_pics.count == 1 ?>
		<?cs set:_sequence.0 = smart_layout_get_outstanding_pics.pic[0] ?>
		<?cs set:_sequence.1 = smart_layout_get_outstanding_pics.left[0] ?>
		<?cs set:_sequence.2 = smart_layout_get_outstanding_pics.left[1] ?>
		<?cs set:_sequence.3 = smart_layout_get_outstanding_pics.left[2] ?>
	<?cs elif smart_layout_get_outstanding_pics.count == 3?>
		<?cs set:_sequence.0 = smart_layout_get_outstanding_pics.left[0] ?>
		<?cs set:_sequence.1 = smart_layout_get_outstanding_pics.pic[0] ?>
		<?cs set:_sequence.2 = smart_layout_get_outstanding_pics.pic[1] ?>
		<?cs set:_sequence.3 = smart_layout_get_outstanding_pics.pic[2] ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:smart_layout_4_C(piclist) ?>
	<?cs set: _point = 0 ?>
	<?cs #:只有一张突出图片，加100 ?>
	<?cs if:smart_layout_get_outstanding_pics.count == 1 ?>
		<?cs set: _point = #_point + #SMART_LAYOUT_OUTSTANDING_COUNT_POINT ?>

		<?cs #:拿到突出图片 ?>
		<?cs set: pic0 = piclist[smart_layout_get_outstanding_pics.pic.0] ?>

		<?cs #: 宽>高的话 加 20 ?>
		<?cs with: pic0 = piclist[#smart_layout_get_outstanding_pics.pic.0] ?>
			<?cs if:pic0.picinfo[0].height <= pic0.picinfo[0].width ?>
				<?cs set: _point = #_point + #SMART_LAYOUT_WIDTH_GREATER_POINT ?>
			<?cs /if ?>
		<?cs /with ?>

		<?cs #:所有图片有能力铺满 ?>
		<?cs set:_is_satisfied = 1?>
		<?cs loop:i = 0, subcount(piclist)-1, 1?>
			<?cs with:picinfo = piclist[i].picinfo?>
			<?cs #:拿最大尺寸来比较 ?>
			<?cs set:largest = subcount(picinfo) -1  ?>
			<?cs #:突出图片的判断 ?>
			<?cs if:i == smart_layout_get_outstanding_pics.pic[0] ?>
				<?cs if:picinfo[largest].width < 400 || picinfo[largest].height < 300 ?>
					<?cs set:_is_satisfied = 0?>
					<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
				<?cs /if ?>
			<?cs else ?>
				<?cs if:picinfo[largest].width < 130 || picinfo[largest].height < 95 ?>
					<?cs set:_is_satisfied = 0?>
					<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
				<?cs /if ?>
			<?cs /if ?>
			<?cs /with ?>
		<?cs /loop ?>

		<?cs #:不能铺满就0分了 ?>
		<?cs if:_is_satisfied == 0 ?>
			<?cs set: _point = 0 ?>
		<?cs /if ?>

	<?cs #:有三张突出图片，加100 ?>
	<?cs elif smart_layout_get_outstanding_pics.count == 3?>
		<?cs set: _point = #_point + #SMART_LAYOUT_OUTSTANDING_COUNT_POINT ?>
		<?cs #:拿到不突出图片 ?>

				
		<?cs with: pic0 = piclist[#smart_layout_get_outstanding_pics.left.0] ?>
			<?cs #:高<宽的话 加 10 ?>
			<?cs if:pic0.picinfo[0].height <= pic0.picinfo[0].width ?>
				<?cs set: _point = #_point + #SMART_LAYOUT_WIDTH_GREATER_POINT ?>
			<?cs /if ?>
		<?cs /with ?>
		
		<?cs #:所有图片有能力铺满 ?>
		<?cs set:_is_satisfied = 1?>
		<?cs loop:i = 0, subcount(piclist)-1, 1?>
			<?cs with:picinfo = piclist[i].picinfo?>
				<?cs #:拿最大尺寸来比较 ?>
				<?cs set:largest = subcount(picinfo) -1  ?>
				<?cs #:突出图片的判断 ?>
				<?cs #call:_print("smart_layout_get_outstanding_pics.left[0]", smart_layout_get_outstanding_pics.left[0]) ?>
				<?cs if:i != smart_layout_get_outstanding_pics.left[0] ?>
					<?cs if:picinfo[largest].width < 130  || picinfo[largest].height < 95 ?>
						<?cs set:_is_satisfied = 0?>
						<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
					<?cs /if ?>
				<?cs else ?>
					<?cs if:picinfo[largest].width < 400 || picinfo[largest].height < 300 ?>
						<?cs set:_is_satisfied = 0?>
						<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
					<?cs /if ?>
				<?cs /if ?>
			<?cs /with ?>
		<?cs /loop ?>

		<?cs #:不能铺满就0分了 ?>
		<?cs if:_is_satisfied == 0 ?>
			<?cs set: _point = 0 ?>
		<?cs /if ?>
	<?cs /if ?>

	<?cs set: smart_layout_4_C.point = _point ?>

<?cs /def ?>

<?cs #### 4 - D
	/**     | -  (196,250)(196,145)
	 *		- |	 (196,145)(196,250)
	 */      
?>
<?cs set:G_SMART_LAYOUT_4_D = "4_D"?>
<?cs set:G_SMART_LAYOUT_4_D.box.0.width =  196?>
<?cs set:G_SMART_LAYOUT_4_D.box.0.height = 250?>
<?cs set:G_SMART_LAYOUT_4_D.box.1.width =  196?>
<?cs set:G_SMART_LAYOUT_4_D.box.1.height = 145?>
<?cs set:G_SMART_LAYOUT_4_D.box.2.width =  196?>
<?cs set:G_SMART_LAYOUT_4_D.box.2.height = 145?>
<?cs set:G_SMART_LAYOUT_4_D.box.3.width =  196?>
<?cs set:G_SMART_LAYOUT_4_D.box.3.height = 250?>

<?cs ####
	/**
	 * 算出4_D 的图片输出顺序
	 */
?>
<?cs def:smart_layout_4_D_sequence() ?>
	<?cs set:_sequence.0 = smart_layout_get_outstanding_pics.pic[0] ?>
	<?cs set:_sequence.1 = smart_layout_get_outstanding_pics.left[0] ?>
	<?cs set:_sequence.2 = smart_layout_get_outstanding_pics.left[1] ?>
	<?cs set:_sequence.3 = smart_layout_get_outstanding_pics.pic[1] ?>
<?cs /def ?>

<?cs ####
	/**
	 * 计算4_D排版下的分数
	 */
?>
<?cs def:smart_layout_4_D(piclist) ?>
	<?cs set: _point = 0 ?>
	<?cs if:smart_layout_get_outstanding_pics.count == 2 ?>
		<?cs #:一张突出图片+100 ?>
		<?cs set: _point = #_point + #SMART_LAYOUT_OUTSTANDING_COUNT_POINT ?>
		<?cs #call:_print("4_D:_point", _point) ?>
		<?cs #:拿到突出图片 ?>
		<?cs with: pic0 = piclist[#smart_layout_get_outstanding_pics.pic.0] ?>
			<?cs #:高>宽的话 加 10 ?>
			<?cs #call:_print("4_D:pic0.picinfo[0].height", pic0.picinfo[0].height) ?>
			<?cs if:pic0.picinfo[0].height > pic0.picinfo[0].width ?>
				<?cs set: _point = #_point + #SMART_LAYOUT_HEIGHT_GREATER_POINT ?>
			<?cs /if ?>
		<?cs /with ?>
		<?cs with: pic1 = piclist[#smart_layout_get_outstanding_pics.pic.1] ?>
			<?cs #:高>宽的话 加 10 ?>
			<?cs #call:_print("4_D:pic1.picinfo[0].height", pic1.picinfo[0].height) ?>
			<?cs if:pic1.picinfo[0].height > pic1.picinfo[0].width ?>
				<?cs set: _point = #_point + #SMART_LAYOUT_HEIGHT_GREATER_POINT ?>
			<?cs /if ?>
		<?cs /with ?>

		<?cs #:所有图片有能力铺满 ?>
		<?cs set:_is_satisfied = 1?>
		<?cs loop:i = 0, subcount(piclist)-1, 1?>
			<?cs with:picinfo = piclist[i].picinfo?>
			<?cs #:拿最大尺寸来比较 ?>
			<?cs set:largest = subcount(picinfo) -1  ?>
			<?cs #:突出图片的判断 ?>
			<?cs if:i == smart_layout_get_outstanding_pics.pic[0] || i == smart_layout_get_outstanding_pics.pic[1] ?>
				<?cs if:picinfo[largest].width < 196 || picinfo[largest].height < 250 ?>
					<?cs set:_is_satisfied = 0?>
					<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
				<?cs /if ?>
			<?cs else ?>
				<?cs if:picinfo[largest].width < 196 || picinfo[largest].height < 145 ?>
					<?cs set:_is_satisfied = 0?>
					<?cs set:i = subcount(piclist)?> <?cs #:尽快跳出循环，相当于break ?>
				<?cs /if ?>
			<?cs /if ?>
			<?cs /with ?>
		<?cs /loop ?>

		<?cs #:不能铺满就0分了 ?>
		<?cs if:_is_satisfied == 0 ?>
			<?cs set: _point = 0 ?>
		<?cs /if ?>
	<?cs /if ?>
	<?cs set: smart_layout_4_D.point = _point ?>
<?cs /def ?>

<?cs #:选择智能排版版式的入口 ?>
<?cs def:smart_layout_select(piclist) ?>
	<?cs call:smart_layout_basic_condition(piclist) ?>
	<?cs #:满足基本条件 ?>
	<?cs if:smart_layout_basic_condition.ret == 1 ?>
		<?cs if:subcount(piclist) == 4 ?>
			<?cs #:算出总共有多少张图片突出，序号是多少 ?>
			<?cs call:smart_layout_get_outstanding_pics(piclist) ?>

			<?cs #call:_print("smart_layout_get_outstanding_pics.count", smart_layout_get_outstanding_pics.count) ?>
			<?cs #call:_print("smart_layout_get_outstanding_pics.pic.0", smart_layout_get_outstanding_pics.pic.0) ?>
			<?cs #call:_print("smart_layout_get_outstanding_pics.pic.1", smart_layout_get_outstanding_pics.pic.1) ?>
			<?cs #call:_print("smart_layout_get_outstanding_pics.pic.2", smart_layout_get_outstanding_pics.pic.2) ?>
			<?cs #call:_print("smart_layout_get_outstanding_pics.pic.3", smart_layout_get_outstanding_pics.pic.3) ?>
			<?cs #:算分去 ?>
			<?cs call:smart_layout_4_A(piclist) ?>
			<?cs call:smart_layout_4_B(piclist) ?>
			<?cs call:smart_layout_4_C(piclist) ?>
			<?cs call:smart_layout_4_D(piclist) ?>

			<?cs #call:_print("smart_layout_4_A.point", smart_layout_4_A.point) ?>
			<?cs #call:_print("smart_layout_4_B.point", smart_layout_4_B.point) ?>
			<?cs #call:_print("smart_layout_4_C.point", smart_layout_4_C.point) ?>
			<?cs #call:_print("smart_layout_4_D.point", smart_layout_4_D.point) ?>

			<?cs #:择优 ?>
			<?cs set: _max_point = smart_layout_4_A.point ?>
			<?cs set: _select = G_SMART_LAYOUT_4_A ?>
			<?cs if:smart_layout_4_B.point > _max_point ?>
				<?cs set: _max_point = smart_layout_4_B.point ?>
				<?cs set: _select = G_SMART_LAYOUT_4_B ?>
			<?cs /if ?>
			<?cs if:smart_layout_4_C.point > _max_point ?>
				<?cs set: _max_point = smart_layout_4_C.point ?>
				<?cs set: _select = G_SMART_LAYOUT_4_C ?>
			<?cs /if ?>
			<?cs if:smart_layout_4_D.point > _max_point ?>
				<?cs set: _max_point = smart_layout_4_D.point ?>
				<?cs set: _select = G_SMART_LAYOUT_4_D ?>
			<?cs /if ?>
		<?cs /if ?>


		<?cs #:如果没有分的，还是走九宫格 ?>
		<?cs if:_max_point == 0 ?>
			<?cs set: smart_layout_select.ret = 0 ?>
		<?cs else ?>
			<?cs #call:_print("_select", _select) ?>
			<?cs if:   _select == G_SMART_LAYOUT_4_A?>
				<?cs call:smart_layout_4_A_sequence() ?>
				<?cs call:smart_layout_box(G_SMART_LAYOUT_4_A) ?>
			<?cs elif: _select == G_SMART_LAYOUT_4_B?>
				<?cs call:smart_layout_4_B_sequence() ?>
				<?cs call:smart_layout_box(G_SMART_LAYOUT_4_B) ?>
			<?cs elif: _select == G_SMART_LAYOUT_4_C?>
				<?cs call:smart_layout_4_C_sequence() ?>
				<?cs call:smart_layout_box(G_SMART_LAYOUT_4_C) ?>
			<?cs elif: _select == G_SMART_LAYOUT_4_D?>
				<?cs call:smart_layout_4_D_sequence() ?>
				<?cs call:smart_layout_box(G_SMART_LAYOUT_4_D) ?>
			<?cs /if ?>

			<?cs loop:i = 0, subcount(piclist)-1, 1?>
				<?cs set: smart_layout_select.ret.box[i].width = smart_layout_box.ret[i].width ?>
				<?cs set: smart_layout_select.ret.box[i].height = smart_layout_box.ret[i].height ?>
				<?cs set: smart_layout_select.ret.pic_sequence[i] = _sequence[i] ?>
				<?cs #call:_print("{_sequence[i] }", _sequence[i] ) ?>
				<?cs #call:_print("{smart_layout_box.ret[i].width}", smart_layout_box.ret[i].width) ?>
				<?cs #call:_print("{smart_layout_box.ret[i].height}", smart_layout_box.ret[i].height) ?>
			<?cs /loop ?>

			<?cs set: smart_layout_select.ret = 1 ?>
			<?cs set: smart_layout_select.ret.layout = _select ?>
		<?cs /if ?>
	<?cs else ?>
		<?cs set: smart_layout_select.ret = 0 ?>
	<?cs /if ?>
<?cs /def ?>
