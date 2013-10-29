<?cs ####
	/**
	 *richcontent主要是用来解析wup中 RichMsg的数据结构
	 *把RichMsg转换成展示层通用的con
	 */
?>
<?cs def:data_rich_msg(path, richmsg, color, mr)?>
	<?cs if:!mr?>
		<?cs set:mr = 0?><?cs #/*貌似这里不用加*/?>
	<?cs /if?>
	<?cs if:richmsg.type == 0?>		<?cs #/*纯文本*/?>
		<?cs if:string.length(richmsg.content)?>
			<?cs call:data_con_txt(path, richmsg.content, color, mr)?>
		<?cs /if?>
	<?cs elif:richmsg.type == 1?>		<?cs #/*用户结构*/?>
		<?cs if:!color?>
			<?cs set:color = "link"?>
		<?cs /if?>
		<?cs if:richmsg.uin?>
			<?cs set:richmsg.uin = html_encode(richmsg.uin, 1)?><?cs #@结构没有做格式校验,uin部分可以被xss?>
			<?cs call:data_con_nick(path, richmsg.uin, richmsg.who, richmsg.name, color, mr)?>
			<?cs call:data_nick_addprefix(path, "@")?>
		<?cs /if?>
	<?cs elif:richmsg.type == 2?>		<?cs #/*链接结构*/?>
		<?cs if:!color?>
			<?cs set:color = "link"?>
		<?cs /if?>
		<?cs if:string.length(richmsg.content)?>
			<?cs call:data_con_url(path, richmsg.content, richmsg.url, color, mr)?>
		<?cs /if?>
	<?cs /if?>
	<?cs set:data_rich_msg.ret.path = path?>
<?cs /def?>

