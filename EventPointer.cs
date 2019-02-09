using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication21.轨道类;
using WindowsFormsApplication21.列车类;

namespace WindowsFormsApplication21.事件类
{
    public class EventPointer//未来事件指针
    {
        public int Time;//事件发生时间
        public string Type;//事件类型
        public int TypeNum;//对应于事件类型的变量
        public Trucks Truck;
        public GateCheckPlat Gate;
    }


}
