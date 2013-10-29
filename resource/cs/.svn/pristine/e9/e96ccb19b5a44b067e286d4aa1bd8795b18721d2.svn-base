<?cs include:"feeds_list_v6.cs" ?>

<?cs set:SENCE_REQ_HOME = 2?>

<?cs each:item = feeds.feeds_list ?>
	<?cs set:isWupfeed = bitmap_value_ex(item.feedsflag, 17, 1)?>

	<?cs if:isWupfeed == 1?>
		<?cs if:item.refer == SENCE_REQ_HOME?>
			<?cs set:isV8 = bitmap_value_ex(item.unbitmap, 40, 1)?>
			<?cs #set:visitor_isV8 = bitmap_value_ex(item.hostunbitmap, 40, 1)?>

			<?cs #自己看自己主页也没问题?>
			<?cs #if:isV8 == 1 && visitor_isV8 == 1?>
			<?cs if:isV8 == 1?>
				<?cs var:item.summary?><?cs #v8 的 wup feed没有外框，输出就是一条完整的feed?>
			<?cs else ?>
				<?cs call:genFeedHTML(item, feeds.oldfeed)?>
			<?cs /if?>
		<?cs else ?>
			<?cs set:isV8 = bitmap_value_ex(item.hostunbitmap, 40, 1)?>
			<?cs if:isV8 == 1?>
				<?cs var:item.summary?>
			<?cs else ?>
				<?cs call:genFeedHTML(item, feeds.oldfeed)?>
			<?cs /if?>
		<?cs /if?>
	<?cs else ?><?cs #非wupfeeds，即使用户是v8，但该条feed还是需要外框(因为是common_widget.cs)?>
		<?cs call:genFeedHTML(item, feeds.oldfeed)?>
	<?cs /if?>
<?cs /each ?>
