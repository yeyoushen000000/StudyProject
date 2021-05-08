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
    public partial class frmCalculation : Form
    {


        //声明一个委托
        public delegate int calculate<in T>(T num1, T num2);
        //创建一个委托对象
        calculate<int> calMethod;

        public frmCalculation()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            int number1 = Convert.ToInt32(textBox1.Text) ;
            int number2 = Convert.ToInt32(textBox6.Text);
            calMethod(1,2);

        }



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

        private void frmCalculation_Load(object sender, EventArgs e)
        {
           
           
        }

        //给委托赋值
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            string calCharacter = comboBox1.SelectedValue.ToString();

            switch (calCharacter)
            {
                case "+":
                    calMethod = Add;
                    break;
                case "-":
                    calMethod = Subtraction;
                    break;
                case "*":
                    calMethod = Multiple;
                    break;
                case "/":
                    calMethod = Devision;
                    break;
                default:
                    break;
            }
        }


   
       

        private void button2_Click(object sender, EventArgs e)
        {
            Func<int, int> action = null;
            //使用+= 或 -= 来增加或移除委托实例
            action += First;
            action += Cal;
            action += Three;
            action -= Cal;
            //如果有返回值的话，返回的是最后一次执行的结果
            int a = action(5);
            Console.WriteLine(a);
        }




        public int First(int a)
        {
            Console.WriteLine($"第{1}次执行" + a); return a + 1;
        }

        public int Three(int a)
        {
            Console.WriteLine($"第{1}次执行" + a); return a + 1;
        }



        private int Cal(int num)
        {
            return num * num;
        }
    }



}
