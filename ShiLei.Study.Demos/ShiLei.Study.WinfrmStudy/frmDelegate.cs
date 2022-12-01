using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiLei.Study.WinfrmStudy
{
    public partial class frmDelegate : Form
    {
        //1：委托是一种类型，但是它的参数是固定返回值和固定参数列表的方法
        //2：调用委托+=表示注册方法，-=表示取消注册方法，执行的时候是按照链式执行的，也就是一个委托链条上的方法全部都会执行
        //3：委托调用前，必须先声明，否则不可以执行
        //4：系统委托无返回值Action，有返回值Func<T,T>
        //5：自定义的委托可以不赋初值，但系统委托Func和Action必须赋初值（也就是将一个方法赋值给系统委托）
        //6：事件event同样可以+=和-=进行注册和取消注册,shijia
        //
        //
        //
        //




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




        public frmDelegate()
        {
            InitializeComponent();
        }
        private void frmCalculation_Load(object sender, EventArgs e)
        {


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


    }



}
