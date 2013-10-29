<?cs set:SENCE_REQ_HOME = 2?>

<?cs set:isWupfeed = bitmap_value_ex(feeds.feedsflag, 17, 1)?>

<?cs include:"feeds_item_v6.cs" ?>
<?cs if:isWupfeed == 1 && feeds.refer != "qq_qz"?><?cs #qz是不区分v6,v8的，因此还需要外框?>
	<?cs if:feeds.refer == SENCE_REQ_HOME?>
		<?cs set:isV8 = bitmap_value_ex(feeds.unbitmap, 40, 1)?><?cs #被访问空间是不是v8。但是主页只有主人自己的feeds，所以判断每一条是对的?>
		<?cs #set:visitor_isV8 = bitmap_value_ex(feeds.hostunbitmap, 40, 1)?><?cs #当前登录者是不是v8?>

		<?cs #自己看自己主页也没问题?>
		<?cs #if:isV8 == 1 && visitor_isV8 == 1?>
		<?cs if:isV8 == 1?>
			<?cs var:feeds.summary?><?cs #v8 的 wup feed没有外框，输出就是一条完整的feed?>
		<?cs else ?>
			<?cs call:genFeedHTML(feeds) ?>
		<?cs /if?>
	<?cs else ?>
		<?cs set:isV8 = bitmap_value_ex(feeds.hostunbitmap, 40, 1)?>
		<?cs if:isV8 == 1?>
			<?cs var:feeds.summary?>
		<?cs else ?>
			<?cs call:genFeedHTML(feeds) ?>
		<?cs /if?>
	<?cs /if?>
<?cs else ?><?cs #非wupfeeds，即使用户是v8，但该条feed还是需要外框(因为是common_widget.cs)?>
	<?cs call:genFeedHTML(feeds) ?>
<?cs /if?>
