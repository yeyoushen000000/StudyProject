using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiLei.Study.WinfrmStudy
{
    public partial class frmThreads : Form
    {

        #region 知识点
        ///Thread 托管线程和原始线程
        //1：托管线程是微软从原始线程中构建的一套整体线程架构，原始线程是操作系统创建的，托管线程本质上仍然是原始线程，但是创作等操作赋值给了
        //2：原生线程，由操作系统负责创建、运行、切换、终止的线程就是原生线程，由于单逻辑逻辑核心同一刻只能运行一个线程（并行），所以并发处理时需要线程流转也就是线程切换。
        //3：托管线程，基于原生线程，由.NET管理的线程。.NET为了更方便的管理和使用原生线程，抽象出来的一套线程模型就是Thread




        //事件和委托的区别
        //1：事件只能在其定义的类中调用执行，执行前判定是否为空，否则会空值报错
        //2：事件在其他类的中可以使用+=注册方法（订阅事件） 使用-=取消注册方法（取消订阅事件），但是无法在其他类中进行调用
        //3：可以有很多其他的类对事件进行调用，事件发生时（在其定义的类中），对于已经订阅该事件的方法，则会按照注册顺序进行执行
        //4：事件的特性非常适合用于，某个状态发生变化后，需要执行一系列的方法（发生角色死亡事件：1-刷新界面，2-hp归零，3敌人的经验增加）
        //5：事件是特殊的委托，委托可以在任何注册方法的类中被执行，是存在不安全性的，如A操作取消注册，B则执行已经被取消方法后的委托，则整个程序报错


        #endregion






        public frmThreads()
        {
            InitializeComponent();
        }

        private void frmThreads_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            //线程开始的委托，赋值初方法,不带参数

            //for (int i = 0; i < 200; i++)
            //{
            //    ThreadStart ts = OutPutInfo;
             
            //    Thread th1 = new Thread(ts);
            //    th1.Start();
            //}

            //
            for (int i = 0; i < 1000; i++)
            {
                //带参数
                ParameterizedThreadStart tsParam = OutPutInfoByParam;
                Thread th1 = new Thread(tsParam);
                th1.Start(i);
            }
        }

        public void OutPutInfo()
        {
           // Console.WriteLine("此处开始一个线程");
           while (true) {

                Console.WriteLine("循环执行线程");
                Thread.Sleep(1000);
            }
        }

        public void OutPutInfoByParam(object num)
        {
            // Console.WriteLine("此处开始一个线程");
            while (true)
            {
                Console.WriteLine("循环执行线程"+"，线程数字"+num);
                Thread.Sleep(1000);
            }
        }




        #region 线程本地存储
        [ThreadStatic]
        private int a=0;
        [ThreadStatic]
        private int b=0;
        private int c;  
        private int d;  

        public void ThreadTest1()
        { 
            a= 1;
            b = 2;
            c = 3;
            d=4;    
            Console.WriteLine("message from ThreadTest1,a="+a);
            Console.WriteLine("message from ThreadTest1,b=" + b);
            Console.WriteLine("message from ThreadTest1,c=" + c);
            Console.WriteLine("message from ThreadTest1,d=" + d);

        }

        public void ThreadTest2() 
        {
            //a = 10;
            //b = 20;
            c = 30;
            d = 40;
            Console.WriteLine("message from ThreadTest2,a=" + a);
            Console.WriteLine("message from ThreadTest2,b=" + b);
            Console.WriteLine("message from ThreadTest2,c=" + c);
            Console.WriteLine("message from ThreadTest2,d=" + d);
        }

        public void ThreadLocal()
        {
            Thread th1 = new Thread(ThreadTest1);
            Thread th2 = new Thread(ThreadTest2);
            th1.Start();
            th2.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThreadLocal();
        }
        #endregion







        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

      
    }
}
