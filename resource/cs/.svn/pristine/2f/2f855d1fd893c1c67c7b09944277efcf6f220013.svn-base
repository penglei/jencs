<?cs #公用得分组件?>
<?cs def:grade_star(item) ?>
	<?cs if:string.length(item.grade) > 0 || item.percent?>
	<p class="f_votestar">
		<span class="votestar">
		<?cs with:grade = item.grade ?>
		<?cs if:string.find(grade, ".")  != #-1 ?>
			<?cs set: item.grade = string.slice(grade,0,string.find(grade, "."))?>
		<?cs /if ?>
		<?cs /with?>

		<span class="<?cs if:item.grade || item.percent?>votestar_i <?cs /if ?>
			<?cs if:item.grade ?>star_<?cs var:item.grade?><?cs /if?>" style="<?cs if:item.percent?>width:<?cs var:item.percent?>;<?cs /if?>">
		</span>

		</span>
		<span class="votescore"><span class="c_tx3 ui_mr10"><?cs var:item.score ?></span></span>
	</p>
	<?cs /if ?>
<?cs /def?>