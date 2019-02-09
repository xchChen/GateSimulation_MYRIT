using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication21
{
    public class RandomGenerator
    {
        /// <summary>
        /// 正态分布随机数
        /// </summary>
        const int N = 100;
        const int MAX = 50;
        const double MIN = 0.1;
        const int MIU = 40;
        const int SIGMA = 1;
        static Random aa = new Random((int)(DateTime.Now.Ticks / 10000));//对于正态分布及负指数分布，无用
        public double AverageRandom(double min, double max,int i)//产生(min,max)之间均匀分布的随机数
        {
            int MINnteger = (int)(min * 10000);
            int MAXnteger = (int)(max * 10000);
            Random R = new Random((int)(DateTime.Now.Ticks / 10000) + i);
            int resultInteger = R.Next(MINnteger, MAXnteger);
            return resultInteger / 10000.0;
        }
        public double Normal(double x, double miu, double sigma) //正态分布概率密度函数
        {
            return 1.0 / (x * Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * (Math.Log(x) - miu) * (Math.Log(x) - miu) / (2 * sigma * sigma));
        }

        /// <summary>
        /// 负指数分布随机数产生
        /// </summary>
        /// <param name="lam">参数：间隔平均值的倒数</param>
        /// <returns></returns>
        public double ngtIndex(double lam, int i)
        {
            Random ran = new Random((unchecked((int)DateTime.Now.Ticks + i)));//i-变化因子，保证产生的随机数不同
            double dec = ran.NextDouble();
            while (dec == 0)
                dec = ran.NextDouble();
            return -Math.Log(dec) / lam;
        }

        //产生K阶爱尔朗分布
        public double ErlangIndex(int k, double miu, int seed)
        {
            double ErlangNum = 0;
            double lam = miu;

            for (int i = 0; i < k; i++)
            {
                ErlangNum += ngtIndex(lam, seed + i);
            }
            return ErlangNum;
        }
    }
}
