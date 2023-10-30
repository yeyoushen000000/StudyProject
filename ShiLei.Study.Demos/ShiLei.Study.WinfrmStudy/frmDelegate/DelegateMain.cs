using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiLei.Study.WinfrmStudy
{
    public class DelegateMain
    {
        
        public  delegate void RelaxMyself();


        public  static RelaxMyself myEnjoy;

        public static event RelaxMyself OnShowDelegate;
        //方法的容器


        #region 引入Console在Form中输出
        //[DllImport("kernel32.dll")]
        //public static extern Boolean AllocConsole();
        //[DllImport("kernel32.dll")]
        //public static extern Boolean FreeConsole();
        #endregion
        public void EatFood()
        {
           // AllocConsole();
            Console.WriteLine("吃美食");
            //MessageBox.Show("吃美食");
        }


        public void DrinkHotWater()
        {
            Console.WriteLine("喝热水");

            //MessageBox.Show("喝热水");

        }
        public void ListenMusic()
        {
            Console.WriteLine("听音乐");

            //MessageBox.Show("听音乐");
        }


        public void WatchGames()
        {
            Console.WriteLine("看比赛");

            //MessageBox.Show("看比赛"); 
        }

        //声明委托， 声明事件

        //使用系统委托


        public  void UseEvent()
        {
            if (OnShowDelegate != null)
                OnShowDelegate();
        }


    }
}
