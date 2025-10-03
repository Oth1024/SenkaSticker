# SenkaSticker 千桦便签条
SenkaSticker 是一款提供了任务创建及管理的桌面软件。其形式为桌面窗口中最下层出现的便签，用户可以通过托盘进入修改模式，以创建和管理任务。

## 任务创建及管理功能


## 自定义便条窗口功能

### 开始开发
提供了 <b>[CustomPanel]</b> 的 Attribute 和 <b>CustomPanelViewModelBase</b> 的基类，用户需要在定义的面板控件的 ViewModel 中使用该特性<b>标记该类</b>，并且继承 CustomPanelViewModelBase 基类的情况下，SenkaSticker会自动识别并调用该类的一个<b>共有的</b>、<b>无参数的</b>、<b>非静态的</b>构造函数。

<i>注：只能接受一个满足上述条件的类型。</i>

以下为一个示例：
~~~csharp
[CustomPanel]/*在类的上方使用此特性*/
public class DemoViewModel : CustomPanelViewModelBase/*继承此抽象类*/
{
    #region Constructor
    public DemoViewModel()/*使用 Public, No Params, Not static 的构造函数*/
    {
    }
    #endregion
}
~~~

### 现有接口类
|类名|描述|形式|
|-|-|-|
|LanguageManager|语言管理器，提供了多语言切换的功能，用户可以从 Language.json 中自行配置现实内容。|Instance|
|ConfigManager|配置管理器，提供了配置的读取，默认配置管理，配置更新等功能|Instance|
|Singleton|单例类，用户可以通过继承或者组合的方式为类添加单例模式，需要有一个无参私有方法|Inheritance,Composition|
|ViewModelBase|实现了 INotifyPropertyChanged 接口的基类，用户可以通过在界面控件中继承此基类以达到动态更新的同时规避自定义面板的识别|Inheritance|

### 使用
将您的项目编译后生成的库文件（xxx.dll）放在软件安装目录下的 Extension 文件夹中，即可载入并在重启后显示。