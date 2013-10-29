<?cs #:timeline组件  ?>
<?cs def:timeline(mod) ?>
<?cs if:subcount(qz_metadata.timeline) > 0 ?>
<?cs with:timeline = qz_metadata.timeline ?>
<p class="f_reprint c_tx3">
	<?cs if:timeline.time.text ?>于<span class="c_tx3 ui_mr5"><?cs var:timeline.time.text ?></span><?cs /if ?><?cs if:timeline.place ?><span class="c_tx3 ui_mr5" title="<?cs var:timeline.orgplace ?>">在<?cs var:timeline.place ?></span><?cs /if ?>
	<?cs def:person-items(item) ?>
		<?cs call:userLink(item, '') ?>
	<?cs /def ?>
	<?cs if:timeline.person.user.0 || subcount(timeline.person.user.0) > 0 || string.length(timeline.person.user.0) > 0 ?>
		和<?cs loop:i = 0, subcount(timeline.person.user) - 1, 1 ?>
			<?cs call:person-items(timeline.person.user[i]) ?>
			<?cs if:i != subcount(timeline.person.user) - 1 ?>、<?cs /if ?>
		<?cs /loop ?><?cs if:timeline.person.totalperson > 2 ?>等<?cs var:timeline.person.totalperson ?>人<?cs /if ?>
	<?cs elif:timeline.person.user || subcount(timeline.person.user) > 0 || string.length(timeline.person.user) > 0 ?>
			和<?cs call:person-items(timeline.person.user) ?>
	<?cs /if ?>
</p>
<?cs /with ?>
<?cs /if ?>
<?cs /def ?>