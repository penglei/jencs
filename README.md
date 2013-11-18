jsrcs
=====

clearsilver template engine

issue:

1. if语句中的elif分支需要抽出来
2. loop循环的初始语句要两次
3. content节点被直接执行，无法返回到主入口末尾
4. macro应该加一个执行到末尾时的暂停
5. 同一行的语句需要一次执行完
6. 锁的问题，前台发起请求太快(后端上一次执行还未结束)
7. include子模板中可以定义同名宏
