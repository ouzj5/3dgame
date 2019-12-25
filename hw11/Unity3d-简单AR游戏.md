## Unity3d-简单AR游戏

### 一、图片识别与建模

#### Vufria模块的导入

首先是安装Vuforia 模块，2017版本后的可以直接使用Unity Hub安装，安装完成后可以直接在软件中使用。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/1.png" alt="1" style="zoom: 33%;" />



然后在菜单目录的GameObject->Vuforia->AR Camera添加AR 摄像头，添加后软件会要求导入Vuforia相关的文件，选择import即可。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/2.png" alt="2" style="zoom:33%;" />



导入成功后，可以在下方的文件管理菜单看到Vuria相关的模块。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/4.png" style="zoom: 50%;" />

#### 创建Vufria项目

导入模块成功后，需要将该项目设置为AR项目，点击在主菜单的File->Build Setting，打开构建设置。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/5.png" style="zoom: 50%;" />

在然后点击playing setting ，从而在右边弹出Playing setting窗口，在这里将XR Setting 项中的Vuforia Realit 勾上。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/6.png" style="zoom: 33%;" />



#### Vuforia证书添加

以上就将游戏项目设置为AR项目了，但是下面还要进行图像处理的一些设置才能使用。首先是vuforia证书的添加，点击刚刚创建的ARCamera进入其Inspector页面，打开该页面下的Open Vuforia configuration。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/7.png" style="zoom:33%;" />



点击后进入VuforiaConfiguration的Inspector，这里点击Add License按钮，然后会弹出一个Vuforia的官方网站。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/8.png" style="zoom: 50%;" />



点击网站后，首先要求登录账号，这里还有账号的需要注册一个。登录完会是下面的证书管理页面，该页面有列出了该账号有效的证书，如果没有有效的证书就点击Get Development Key创建一个。

![](https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/9.png)



证书创建页面输入一个不同的名字确认就行，创建完成后在刚刚的证书管理页面点击创建好的证书，然后复制灰色框的内容。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/11.png" style="zoom:33%;" />



接着回到AR Camera的Inspector页面，将证书内容粘贴到App License Key的输入框中，证书的添加就完成了。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/12.png" style="zoom: 67%;" />

#### 添加图片数据库

证书添加完成后，AR Camera就可以进行图像识别了，但现在只能识别其内置的图片，接下来是添加自己的识别图片。首先同样在AR Camera页面，打开Databases属性，点击Add Database按钮，进入Vufria图片数据库识别网站。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/13.png" style="zoom: 50%;" />



打开后进入Target Manager页面，这里列出的是登录账号已经识别过的图像数据库，新建则点击 Add Database按钮。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/14.png" style="zoom: 33%;" />



在创建数据库页面输入数据库的名字，然后类型选择为设备（Device）。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/15.png" style="zoom:33%;" />



创建后进入图片数据库页面，在该页面选择Add target添加对应的图片，填写好图片的类型、宽度以及名字，然后会弹出正在识别的进度条，等待上传成功。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/16.png" style="zoom:33%;" />



上传成功后数据库会多出对应的图片，然后就可以下载对应的数据库了。在左边勾上数据库中要识别的图片，在右上方点击Download Database，弹出一个Download Databases 对话框，这里选项选择Unity Editor，然后点击下载按钮。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/17.png" style="zoom:33%;" />



下载完成后，找到对应下载的文件，后缀名为`.unitypackage`，直接将其拖到unity3d 中，弹出导入窗口，点击导入按钮。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/19.png" style="zoom: 50%;" />



最后再次回到AR Camera的Inspector页面，点开Databases可以看到这时多出了一个刚刚创建数据库的选项，将其以及其后面出现的Active选项勾上，这样图片识别数据库的添加就完成了，对应的数据库中的图片就可以被识别了。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/20.png" style="zoom: 50%;" />

#### 图像识别建模

数据库添加完，就可以进行图片识别以及建模了，在对象树空白处单击右键，选择Vuforia->Image，创建出一个Target Image。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/21.png" style="zoom:33%;" />

单击ImageTarget进入Inspector页面，在Image Target Behaviour 中选择对应要识别的数据库以及图片。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/22.png" style="zoom: 50%;" />



然后是对对应的图片建模，给ImageTarget添加任意的3D子对象，然后就可以运行了，运行后可以看到，在图片上方有对应创建的3D文件。

![](https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/23.png)



### 虚拟按键小游戏

不知道做什么虚拟按键游戏，这里就直接使用了之前写过的巡航兵游戏，利用图片进行巡航兵游戏的建模，然后添加四个虚拟按键分别代表上下左右。

#### 添加识别图片以及模型

在图片识别中，这里直接使用了Vufria自带的图片，新建一个ImageTarget，在Databases选择VuforiaMars_Images，Image Target选择Astronaut。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/28.png" style="zoom:50%;" />

将原来游戏中的地图添加为ImageTarget的子对象。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/29.png" style="zoom:50%;" />

#### 添加虚拟按钮

点击ImageTarget进入Inspector页面，在Image Target Behaviour选项中打开Advanced选项，点击Add Virual Button按钮，添加虚拟按钮。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/24.png" style="zoom: 50%;" />

设置好虚拟按钮的名字大小以及位置，名字需要唯一。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/25.png" style="zoom: 50%;" />

虚拟按钮没有实体，需要为每个按钮添加实体以及文本，为每个虚拟按钮添加对应的3dObject以及3dtext文本。

<img src="https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/26.png" style="zoom:50%;" />

添加后的效果如下。

![](https://raw.githubusercontent.com/ouzj5/3dgame/master/hw11/pic/27.png)