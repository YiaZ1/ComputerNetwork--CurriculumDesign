# ComputerNetwork--CurriculumDesign

### 简介

利用Tcp与soclet实现了本地聊天室，显示聊天消息，显示在线用户。

### 编程环境

Windows10 64位 + VS2012 + .Net 4.5

### 模块介绍

#### Chatter

客户端，包括登陆界面和聊天界面，右侧窗口为在线用户列表，当有用户上下线时更新。

#### ChatterServer

服务器端，负责将一个客户端的消息发送给其他所有客户端。

服务器运行时不断检测客户端是否连接，如果没有客户端连接则阻塞等待客户端连接。

当客户端连接时，先检测注册名称是否重复，然后为其开启异步读取线程，直到其下线为止。

服务器运行时，窗口会显示debug信息，可除去。
