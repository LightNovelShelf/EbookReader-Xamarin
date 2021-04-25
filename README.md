##### **备忘录**

###### **不管什么情况打开书籍时**
Web调用readBook，并在Window上挂一个全局函数loadBook，原生读取完数据后调用，成功打开epub后调用SetResultOK，Web呈现阅读页面

###### **从外部打开并导入书籍时**
Web调用choiceBook，OnActivityResult中处理1 调用LoadData打开新页面，新页面退出后，在OnActivityResult中处理2，添加进书架

###### **从书架打开书籍**
Web调用openBook，OnActivityResult中处理4