using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication21.列车类;
using WindowsFormsApplication21.事件类;

namespace WindowsFormsApplication21.轨道类
{
    [Serializable]
    public class GateCheckPlat
    {
        public bool IsBusy { get; set; }//是否被占用
        public bool IsClosed { get; set; }//是否被关闭
        public bool IsAboutClose { get; set; }//是否即将被关闭，作为标志
        public int Code { get; set; }//到发线编号
        public int BusyTimeStart;//开始占用时间
        public int BusyTimeEnd;//释放资源时间
        public Trucks Truck;
        public List<QueueState> QSList=new List<QueueState>();//门区对应的卡车队列
        public List<GateState> GSList = new List<GateState>();//记录门区占用状态
        public List<EventPointer> GateCheck_EP_Queue = new List<EventPointer>();//门区对应的卡车检查事件队列
        public List<GateStateOpen_Close> GSOC = new List<GateStateOpen_Close>();
        public int OpenPeriod_StartTime;//开放区间起点
        public int OpenPeriod_EndTime;//开放区间终点
    }

    public class GateState
    {
        public int BeginTime;
        public int EndTime;
    }

    public class GateStateOpen_Close
    {
       public string State;
       public int Time;
    }
}
