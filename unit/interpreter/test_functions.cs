{
  functionsCallList: {
     json_encode: 1,
     html_encode: 1,
     string_firstwords_replace: 1,
     bitmap_value_ex: 1,
     subcount: 1,
     uri_encode: 1,
     'string.length': 1,
     'string.slice': 1,
     abs: 1,
     html_decode: 1
   }
}
<?cs set:jsonStr = "\"'\\"?>
<?cs set:htmlStr = "&"?>
<?cs set:firstwords = "javascript://alert(\"xss\")"?>
<?cs set:str_lenStr = "abcd"?>
<?cs set:str_sliceStr = "aaabbb"?>
<?cs set:abs_Num = -100?>
<?cs set:html_decodestr = "&amp;"?>

<?cs var:json_encode(jsonStr, 1)?>=\"\'\\
<?cs var:html_encode(htmlStr, 1)?>=&amp;
<?cs var:string_firstwords_replace(firstwords, "javascript:", "http:")?>=http://alert("xss")
<?cs var:string.length(str_lenStr)?>=4
<?cs var:string.slice(str_sliceStr, 1, 5)?>=aabb
<?cs var:abs(abs_Num)?>=100
