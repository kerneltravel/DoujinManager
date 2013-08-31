同人志下载管理器
===================================
介绍 Introduction
-----------------------------------
同人志下载管理器<br />
自动抓取封面，自动解析网盘地址并下载<br />
本地管理功能（未完成）<br />
由C#&.Net 4.0编写，可由mono提供跨平台支持<br />
<br />
Doujinshi Download Manager<br />
Automatically grab the cover and download files stored in netdisk or server<br />
Local management (unfinished)<br />
Written in C#&.Net 4.0<br />
cross-platform support provided by mono<br />

使用方法 Usage
-----------------------------------
Windows:[.net 4.0](http://www.microsoft.com/zh-cn/download/details.aspx?id=17718)<br />
其他平台(Other Platform):[mono](http://www.go-mono.com/mono-downloads/download.html) 使用 "mono doujinmanager.exe" 运行/Start using "mono doujinmanager.exe"<br />
右键封面可下载<br />
Right-Click Cover can download it<br />

支持网站
-----------------------------------
1、[魂+（soulplus）](http://bbs.soul-plus.net) 搜索解析模块完成<br />
2、[E-hentai](http://e-hentai.org) 搜索解析模块完成<br />

支持下载方式
-----------------------------------
1、[百度盘](http://pan.baidu.com)（单文件解析，可带密码，不支持文件夹及多文件解析） 完成<br />
2、[Howfile](http://www.howfile.com) 完成<br />
3、[JBpan](http://jbpan.tk) 完成<br />
4、[迅雷快传](http://kuai.xunlei.com) 暂无支持计划<br />
5、[115](http://115.com) 暂无支持计划<br />
6、[E-hentai 暴力抓取](http://e-hentai.org) 暂未支持<br />
7、[E-hentai GP下载](http://e-hentai.org) 暂未支持<br />
8、[BT下载（借由迅雷离线实现）](http://e-hentai.org) 暂未支持<br />

简单的搜索命令语法（soulplus可用）
-----------------------------------
<--ch--> 搜索在soulplus的汉化区同人志<br />
<--C84--> 搜索在soulplus的C84区同人志<br />
<--(fid)--> 搜索指定(fid)的同人志（无括号，fid为数字，可从每个版块的url中获得）<br />