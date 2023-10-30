using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ShiLei.Study.WinfrmStudy.DelegateMain;
using System.Runtime.InteropServices;

namespace ShiLei.Study.WinfrmStudy
{



public partial class frmDelegate : Form
    {
        //1：委托是一种类型，但是它的参数是固定返回值和固定参数列表的方法
        //2：调用委托+=表示注册方法，-=表示取消注册方法，执行的时候是按照链式执行的，也就是一个委托链条上的方法全部都会执行
        //3：委托调用前，必须先声明，否则不可以执行
        //4：系统委托无返回值Action，有返回值Func<T,T>
        //5：自定义的委托可以不赋初值，但系统委托Func和Action必须赋初值（也就是将一个方法赋值给系统委托）
        //6：事件event同样可以+=和-=进行注册和取消注册
        //7：将委托视为方法的容器，可以增加或者减少方法
        //8：多播委托多用于返回值为void的方法，因为多播委托只返回最后一个执行方法的返回值。
        //9: 委托注册方法最好放在只执行一次的方法中，因为该方法多次执行，
        //委托会多次注册，导致方法执行多遍，如放在buttonClick按钮中，点击一次则多注册一次
        //10：


        //事件和委托的区别
        //1：事件只能在其定义的类中调用执行，执行前判定是否为空，否则会空值报错
        //2：事件在其他类的中可以使用+=注册方法（订阅事件） 使用-=取消注册方法（取消订阅事件），但是无法在其他类中进行调用
        //3：可以有很多其他的类对事件进行调用，事件发生时（在其定义的类中），对于已经订阅该事件的方法，则会按照注册顺序进行执行
        //4：事件的特性非常适合用于，某个状态发生变化后，需要执行一系列的方法（发生角色死亡事件：1-刷新界面，2-hp归零，3敌人的经验增加）
        //5：事件是特殊的委托，委托可以在任何注册方法的类中被执行，是存在不安全性的，如A操作取消注册，B则执行已经被取消方法后的委托，则整个程序报错


        //声明一个委托，委托使用前必须要先声明，类似于类的声明总要有个Class关键字和{}体
        public delegate int calculate<in T>(T num1, T num2);
        //创建一个委托对象，相当于new 了一个类的对象
        calculate<int> calMethod;

        //声明一个委托，用于计算平方
        public delegate int SquareCalculateHandler(int num);
        //创建了一个委托对象
        public SquareCalculateHandler squareCalculate;

        //创建一个事件，相当于一个委托对象
        public event SquareCalculateHandler onSquare;



        #region 事件委托--案例：不同的用户订阅书籍出版事件，分别打印名称，价格，简介

        //事件是一个特殊的委托，委托可以定义在类的内部也可定义在类的外部，任何类都可以对公用委托进行调用
        //事件则不能通过其他类调用，而且事件只能定义在类的内部，不能定义在类的外部，只能被定义的类去调用，如果对外调用，则应该通过方法提供给外部类
        //事件关键字event，通常约定事件变量名称以Event结尾
        //调用事件的方法，通常约定为事件变量名称前面加on
        public event EventHandler<Book> BookPublishEvent;


        //可以由不同的类订阅书籍出版事件后，获取书籍名称、书籍价格、书籍简介
        public void GetBookName(object sender,EventArgs args)
        {
            if (sender is frmDelegate)
            {
                var book = (Book)args;
                Console.WriteLine("获取书籍名称" + book.BookName);
            }
        }
        public void GetBookPrice(object sender, EventArgs args)
        {
            if (sender is frmDelegate)

                {
                    var book = (Book)args;
                Console.WriteLine("获取书籍价格" + book.Price);
            }
        }
        public void GetBookInfo(object sender, EventArgs args)
        {
            if (sender is frmDelegate)
                {
                    var book = (Book)args;
                Console.WriteLine("获取书籍简介" + book.BookInfo);
            }
        }
        #endregion

        public void onBookPublishEvent(Book  book)
        {
            BookPublishEvent?.Invoke(this, book);
        }

        /// <summary>
        /// 订阅事件（也叫注册事件），一般只订阅一次就行
        /// </summary>
        public void RegisterBookEvent()
        {
            BookPublishEvent +=GetBookName;
            BookPublishEvent += GetBookPrice;
            BookPublishEvent += GetBookInfo;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            onBookPublishEvent(new Book() {  BookName= "《平凡的世界》", BookInfo="作者路遥，描述上世纪80年代孙少平和孙少安顺应时代潮流，积极拥抱新世界的故事",Price=56.75 });
        }


        public frmDelegate()
        {
            InitializeComponent();

        }
        private void frmCalculation_Load(object sender, EventArgs e)
        {

            //委托-事件：书籍出版本，发布信息的注册
            RegisterBookEvent();
        }

        #region 委托赋值-进行加减乘除

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            int a = 15, b = 5;
            calMethod = Add;
            int result = calMethod(a, b);
            richTextBox1.AppendText("\r\n" + $"a=15,b=5,委托执行，相加结果：" + result);
            calMethod = Subtraction;
            result = calMethod(a, b);
            richTextBox1.AppendText("\r\n" + $"a=15,b=5,委托执行，相减结果：" + result);
            calMethod = Multiple;
            result = calMethod(a, b);
            richTextBox1.AppendText("\r\n" + $"a=15,b=5,委托执行，相乘结果：" + result);
            calMethod = Devision;
            result = calMethod(a, b);
            richTextBox1.AppendText("\r\n" + $"a=15,b=5,委托执行，相除结果：" + result);


        }


        #region 加减乘除方法

        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtraction(int a, int b)
        {
            return a - b;
        }
        public int Multiple(int a, int b)
        {
            return a * b;
        }
        public int Devision(int a, int b)
        {
            return a / b;
        }
        #endregion
        #endregion
  
        
        #region -=和+=进行多播委托操作,系统一次性执行完毕所有委托
        private void button2_Click(object sender, EventArgs e)
        {
            //使用+= 或 -= 来增加或移除委托实例
            squareCalculate += First;
            squareCalculate += Second;
            squareCalculate += Three;
            squareCalculate += Square;

            //如果有返回值的话，返回的是最后一次执行的结果
            int a = squareCalculate(5);
            richTextBox1.AppendText("\r\n" + $"委托+=符号执行完毕,输入为5，平方结果值为:" + a);


            squareCalculate -= Square;
            richTextBox1.AppendText("\r\n" + $"执行-=Square方法");

            int b = squareCalculate(6);
            richTextBox1.AppendText("\r\n" + $"委托-=符号执行完毕,输入为6，结果值为:" + b);
        }
        #endregion
   

        #region 委托执行方法链
        public int First(int a)
        {
            richTextBox1.AppendText("\r\n" + $"执行方法名：First");
            return a + 1;
        }

        public int Second(int a)
        {
            richTextBox1.AppendText("\r\n" + $"执行方法名：Second");
            return a + 1;
        }

        public int Three(int a)
        {
            richTextBox1.AppendText("\r\n" + $"执行方法名：Three");
            return a + 1;
        }

        private int Square(int num)
        {
            richTextBox1.AppendText("\r\n" + $"执行方法名：Square");
            return num * num;
        }

        #endregion


        #region 利用系统委托Action和Func进行计算，
        private void button3_Click(object sender, EventArgs e)
        {
            //系统的委托Action<>和Func<> 必须赋初始方法
            Func<int, int> action1;
            action1 = First;
            action1 += Second;
            action1 += Three;
            action1 += Square;
            int a = action1(10);
            richTextBox1.AppendText("\r\n" + $"执行系统委托Func,输入为10，平方结果值为:" + a);
        }
        #endregion



        #region 利用事件订阅方法，然后执行方法
        private void button4_Click(object sender, EventArgs e)
        {
            //
            onSquare += OnThreeSquare;
            richTextBox1.AppendText("\r\n" + $"事件绑定方法：OnThreeSquare");
            onSquare += OnQuartSquare;
            richTextBox1.AppendText("\r\n" + $"事件绑定方法：OnQuartSquare");

            if (onSquare != null)
            {
                int b = onSquare(3);
                richTextBox1.AppendText("\r\n" + $"onSquare事件执行，输入值为3，结果为：" + b);
            }
        }

        public int OnThreeSquare(int num)
        {
            richTextBox1.AppendText("\r\n" + $"执行方法名：OnThreeSquare");

            return num * num * num;
        }

        public int OnQuartSquare(int num)
        {
            richTextBox1.AppendText("\r\n" + $"执行方法名：OnQuartSquare");

            return num * num * num * num;
        }
        #endregion




        private void button5_Click(object sender, EventArgs e)
        {
            GetMyReleaxDelegate();
            //只返回最后一个方法的返回值
            myEnjoy?.Invoke();

            //给事件注册方法然后调用，定义事件类中调用事件的方法应是私有的，不对外开放，否则又变成所有的类都可以调用
            //此处开放则是为了测试调用
            DelegateMain dm2 = new DelegateMain();
            GetMyReleaxByEvent(dm2);
            dm2.UseEvent();
        }

        //多播委托的增减方法使用+=和-=
        private void GetMyReleaxDelegate()
        {
            DelegateMain dm = new DelegateMain();
            myEnjoy += dm.EatFood;
            myEnjoy += dm.DrinkHotWater;
            myEnjoy += dm.ListenMusic;
            myEnjoy += dm.WatchGames;

        }


        private void GetMyReleaxByEvent(DelegateMain dm)
        {
            OnShowDelegate += dm.WatchGames;
            OnShowDelegate += GoSwimming;
            OnShowDelegate += dm.ListenMusic;
            OnShowDelegate += GoShopping;
        }

        public void GoSwimming()
        {

            Console.WriteLine("去游泳");        
        }


        public void GoShopping()
        {
            Console.WriteLine("去购物");
        }

    }



}
