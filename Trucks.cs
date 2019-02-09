using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication21.轨道类;

namespace WindowsFormsApplication21.列车类
{
    [Serializable]
    public class Trucks
    {
        public int Code;    //卡车编号
        public string Type;   //卡车类型 con sdz big car
        public bool IsFailCheck;//门区检查失败
        public int OuccerTime;    //卡车生成时间
        public int Arrive_GateEnter_Time;  //到达到达场时间
        public int InspectionTimeIn;//进门检查时间
        public int Leave_GateEnter_Time;   //离开gate时间
        public GateCheckPlat Gate;
        public int GateCheckTime;
        public int GateWaitTime;
        public int ShapeK;
        public double Lambda;
        public int K;//爱尔朗分布 参数K
        public double Lam;//爱尔朗分布 参数Lam

        //门区生产卡车
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TPList"></生成时间段及分布相关参数>
        /// <param name="TruckTypeRatio"></卡车类型相关参数>
        public void TruckCreat(List<string> TPList, List<int> TruckTypeRatio,int S,List<int> TruckShapeK,List<double>TruckLambda)//lam代表平均每分钟卡车数量,S仿真循环次数，影响随机种子
        {
            //提取分布数据
            int TPBeginTime = Convert.ToInt32(TPList[0]) * 60 * 60;
            int TPEndTime = Convert.ToInt32(TPList[1]) * 60 * 60;
            string DisType = TPList[2];
            double DisValue = Convert.ToDouble(TPList[3]);
            //卡车产生时刻的范围：根据门区开放时间确定，如8：00-18：00
            int GateBeginTime = StaticData.GateTimeWindowStart * 60 * 60;
            int GateCloseTime = StaticData.GateTimeWindowEnd * 60 * 60;
            //时间变量
            int ouccurtime = TPBeginTime;//赋卡车产生时刻下限=门区开放时刻
            int RandomSeed = 0;//随机种子
            bool Juge=true;
            int Code = 0;
            do
            {
                Code++;
                RandomSeed++;//随机种子变化
                RandomSeed += S * 10;//随机种子受仿真循环次数影响

                Trucks truck = new Trucks();
                truck.Code = Code;//卡车编号
                //生成时间
                RandomGenerator RanGenerator = new RandomGenerator();
                 int TimeIntervel =0;
                //不同分布类型
                if(DisType=="Poisson distribution")
                {TimeIntervel = Convert.ToInt32(RanGenerator.ngtIndex(DisValue, RandomSeed) * 60);}
                if (DisType == "Uniform distribution")//均匀分布
                {TimeIntervel = Convert.ToInt32((double)60/DisValue);}
                //TruckTimeList.Add(TimeIntervel);//存储时间间隔
                ouccurtime += TimeIntervel;//卡车生成时间
                truck.OuccerTime = ouccurtime;//赋值
                //卡车类型
                Random Ran = new Random(unchecked((int)(DateTime.Now.Ticks + RandomSeed)));
                int TypeRandom = Ran.Next(1, 10001);//类型比例精确至两位，总和为10000；
                for (int j = 0; j < TruckTypeRatio.Count; j++)
                {
                    if (TypeRandom <= TruckTypeRatio[j])
                    {
                        truck.Type = (j + 1).ToString();
                        break;
                    }
                }
                truck.ShapeK = TruckShapeK[Convert.ToInt32(truck.Type) - 1];
                truck.Lambda = TruckLambda[Convert.ToInt32(truck.Type) - 1];
                //卡车检查失败
                int FailNum = Convert.ToInt32(1000 * StaticData.TruckFailCheckRatio);
                int FailRandom = Ran.Next(1, 1001);
                if (FailRandom < FailNum)
                { truck.IsFailCheck = true; }
                else
                { truck.IsFailCheck = false; }

                //卡车初始默认的门区：适用于Non_Pool类型
                int GateIndex = Ran.Next(0, StaticData.EnterGateCheckNum);//默认的大门
                truck.Gate = StaticData.EnterGateCheckList[GateIndex];

                if (truck.OuccerTime <= TPEndTime)//循环终止判断：
                {
                    StaticData.Truck.Add(truck);
                }
                else
                {
                    break;
                }
            }
            while (Juge);
            double FailCheckNum = 0;
            foreach (var truck in StaticData.Truck)
            {
                if (truck.IsFailCheck == true)
                { FailCheckNum++; }
            }
            double Ratio = FailCheckNum / StaticData.Truck.Count();
        }
    }
    
}
