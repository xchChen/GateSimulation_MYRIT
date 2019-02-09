using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication21.列车类;
using WindowsFormsApplication21.轨道类;
using WindowsFormsApplication21.事件类;



namespace WindowsFormsApplication21
{
    public static class StaticData
    {
        //门区仿真专用
        public static List<EventPointer> EventList = new List<EventPointer>();
        public static List<EventPointer> EventList_Pool = new List<EventPointer>();
        public static List<Trucks> Truck = new List<Trucks>();
        public static double TruckFailCheckRatio;
        public static List<GateCheckPlat> EnterGateCheckList = new List<GateCheckPlat>();//进入门区检查口
        public static int EnterGateCheckNum=5;
        public static int GateTimeWindowStart = 6;//早8点
        public static int GateTimeWindowEnd = 20;//至晚8点
        public static int Gate_k = 0;
        public static double Gate_lam = 0;
        public static int SimRepeatNum = 1;//门区仿真循环次数
        public static double ArrivalRate_Ratio = 1;//到达率敏感性分析
        public static int Stop_Go_Time = 0;//5s
        public static int TimePeriodUnit = 0;//单位：分钟
        public static int PeakBegin = 0;//高峰时刻起点
        public static int PeakEnd = 0;//高峰时刻终点
        public static List<List<Trucks>> TruckLList = new List<List<Trucks>>();

    }
}
