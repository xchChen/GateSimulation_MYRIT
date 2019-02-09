using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication21.事件类
{
    public class SortedClass
    {
        //New 新建事件插入事件表排序方法
        public void InsertSortEP(List<EventPointer> Eplist, EventPointer Ep)
        {
            bool IsInsert = false;//事件Ep是否被插入list标志
            for (int i = 1; i < Eplist.Count; i++)
            {
                EventPointer EPNext = Eplist[i];
                if (Ep.Time <= EPNext.Time)
                {
                    Eplist.Insert(i, Ep);//指定位置插入该事件
                    IsInsert = true;
                    break;
                }
            }
            //若经过循环，事件Ep未被插入事件list，则Ep添加至list末尾
            if (IsInsert == false)
            {
                Eplist.Add(Ep);
            }
        }
    }
}
