using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication21.列车类;
using WindowsFormsApplication21.事件类;
using WindowsFormsApplication21.轨道类;
using System.Diagnostics;
using System.IO;

namespace WindowsFormsApplication21
{ 
    public partial class GateSimulation : Form
    {
        public GateSimulation()
        {
            InitializeComponent();
        }

        #region 
        List<Label> TypeNameLabelList = new List<Label>();
        List<TextBox> TypeRatioTBoxList = new List<TextBox>();
        List<TextBox> ShapeKTBoxList = new List<TextBox>();
        List<TextBox> LambdaTBoxList = new List<TextBox>();
        List<Label> TimePeriodLabelList = new List<Label>();//时间段名称label列表
        List<TextBox> TimePeriodBeginTBoxList = new List<TextBox>();//时间段开始时刻列表
        List<TextBox> TimePeriodEndTBoxList = new List<TextBox>();//时间段结束时刻列表
        List<ComboBox> DisTypeComBoxList = new List<ComboBox>();//时间段分布类型combox列表
        List<Label> DisNameLabelList = new List<Label>();//分布参数名称Label列表
        List<TextBox> DisValueTboxList = new List<TextBox>();//分布参数值TextBox列表
        List<TextBox> GateNumTboxList = new List<TextBox>();//门区数量TextBox列表
        #endregion

        #region 窗口控件事件

        //加载预设参数
        DataTable Pooled_ResultDT = new DataTable();//结果数据库-Pool
        DataTable NPooled_ResultDT = new DataTable();//结果数据库-NonPool
        private void GateSimulation_Load(object sender, EventArgs e)
        {
            //控件预设置
            //TruckGenerate_Btn.Enabled = false;
            Sim_Btn.Enabled = false;
            Sim_NonP.Enabled = false;
            //参数预输入
            //重复次数
            RepeatNum_TBox.Text = "100000";
            //时间段最小单位
            TimeUnit_TBox.Text = "20";//分钟
            //卡车类型
            TypeNum_Tbox.Text = "3";
            TypeRatioTBoxList[0].Text = "46.7"; TypeRatioTBoxList[1].Text = "28.5"; TypeRatioTBoxList[2].Text = "24.8";
            ShapeKTBoxList[0].Text = "5"; ShapeKTBoxList[1].Text = "5"; ShapeKTBoxList[2].Text = "3";
            LambdaTBoxList[0].Text = "1.103"; LambdaTBoxList[1].Text = "1.253"; LambdaTBoxList[2].Text = "0.985";
            StaticData.TruckFailCheckRatio = 0;//卡车检查失败比例
            //门区数量
            GateNum_Tbox.Text = "6";

            //门区开放时间
            GBeginTime_Tbox.Text = "8";
            GEndTime_Tbox.Text = "18";
            //时间段设置
            TPNum_Tbox.Text = "3";
            TimePeriodBeginTBoxList[0].Text = "8"; TimePeriodEndTBoxList[0].Text = "10";//平峰
            TimePeriodBeginTBoxList[1].Text = "10"; TimePeriodEndTBoxList[1].Text = "12";//高峰
            TimePeriodBeginTBoxList[2].Text = "12"; TimePeriodEndTBoxList[2].Text = "18";//平峰
            StaticData.PeakBegin = 10;//高峰起点
            StaticData.PeakEnd = 12;//高峰终点

            //敏感性分析
            textBox1.Text = "1";//到达率比例
            textBox2.Text = "0";//go-stop-time
            textBox3.Text = "0";//trouble trasaction

            //go-and-stop time时间
            StaticData.Stop_Go_Time = 0;//单位：s
            //trouble-transaction ratio
            StaticData.TruckFailCheckRatio = 0;

            //DataGridView设置
            PoolSim_Result_DataGrid.RowHeadersVisible = false;//去除最左侧一列
            PoolSim_Result_DataGrid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;//内容居左
            PoolSim_Result_DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PoolSim_Result_DataGrid.AllowUserToResizeColumns = false;
            PoolSim_Result_DataGrid.AllowUserToResizeRows = false;
            PoolSim_Result_DataGrid.AllowUserToAddRows = false;
            PoolSim_Result_DataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 6, FontStyle.Bold);
            PoolSim_Result_DataGrid.RowsDefaultCellStyle.Font = new Font("Times New Roman", 8, FontStyle.Italic);
            Pooled_ResultDT.Columns.Add(new DataColumn("平均等待时间", typeof(string)));
            Pooled_ResultDT.Columns.Add(new DataColumn("平均队列长度", typeof(string)));
            Pooled_ResultDT.Columns.Add(new DataColumn("TypeA_平均等待时间", typeof(string)));
            Pooled_ResultDT.Columns.Add(new DataColumn("TypeB_平均等待时间", typeof(string)));
            Pooled_ResultDT.Columns.Add(new DataColumn("TypeC_平均等待时间", typeof(string)));

            NPoolSim_Result_DataGrid.RowHeadersVisible = false;//去除最左侧一列
            NPoolSim_Result_DataGrid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;//内容居左
            NPoolSim_Result_DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            NPoolSim_Result_DataGrid.AllowUserToResizeColumns = false;
            NPoolSim_Result_DataGrid.AllowUserToResizeRows = false;
            NPoolSim_Result_DataGrid.AllowUserToAddRows = false;
            NPoolSim_Result_DataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 6, FontStyle.Bold);
            NPoolSim_Result_DataGrid.RowsDefaultCellStyle.Font = new Font("Times New Roman", 8, FontStyle.Italic);
            NPooled_ResultDT.Columns.Add(new DataColumn("平均等待时间", typeof(string)));
            NPooled_ResultDT.Columns.Add(new DataColumn("平均队列长度", typeof(string)));
            NPooled_ResultDT.Columns.Add(new DataColumn("TypeA_平均等待时间", typeof(string)));
            NPooled_ResultDT.Columns.Add(new DataColumn("TypeB_平均等待时间", typeof(string)));
            NPooled_ResultDT.Columns.Add(new DataColumn("TypeC_平均等待时间", typeof(string)));
        }

        //设定时间段个数
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //初始化
            foreach (Label L in TimePeriodLabelList)
            { TimePeriod_GBox.Controls.Remove(L); }
            foreach (TextBox Tb in TimePeriodBeginTBoxList)
            { TimePeriod_GBox.Controls.Remove(Tb); }
            foreach (TextBox Tb in TimePeriodEndTBoxList)
            { TimePeriod_GBox.Controls.Remove(Tb); }
            TimePeriodLabelList.Clear();
            TimePeriodBeginTBoxList.Clear();
            TimePeriodEndTBoxList.Clear();
            //显示控件
            int TimePeriodNum = Convert.ToInt32(TPNum_Tbox.Text);//设定时间区间数量
            if (TimePeriodNum > 8)
                TimePeriodNum = 8;
            //生成Time Period Set控件
            for (int i = 0; i < TimePeriodNum; i++)
            {
                //标签
                Label L = new Label();
                L.Text = "Time Period" + " " + (i + 1).ToString();
                L.Font = new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular);
                L.Location = new Point(10, 80 + i * 25);
                L.Size = new System.Drawing.Size(75, 20);
                TimePeriodLabelList.Add(L);
                TimePeriod_GBox.Controls.Add(L);
                //时间段开始时刻Textbox
                TextBox Tb_begin = new TextBox();
                Tb_begin.Font = new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular);
                Tb_begin.Location = new Point(85, 80 + i * 25);
                Tb_begin.Size = new System.Drawing.Size(60, 20);
                if (i == 0)//第一个时间段的开始时刻赋值
                { Tb_begin.Text = GBeginTime_Tbox.Text; }
                TimePeriodBeginTBoxList.Add(Tb_begin);
                TimePeriod_GBox.Controls.Add(Tb_begin);
                //时间段结束时刻Textbox
                TextBox Tb_end = new TextBox();
                Tb_end.Font = new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular);
                Tb_end.Location = new Point(155, 80 + i * 25);
                Tb_end.Size = new System.Drawing.Size(60, 20);
                if (i == TimePeriodNum - 1)//最后一个时间段的结束时刻赋值
                { Tb_end.Text = GEndTime_Tbox.Text; }
                TimePeriodEndTBoxList.Add(Tb_end);
                TimePeriod_GBox.Controls.Add(Tb_end);
            }
        }

        //Apply控件设置:分布类型及参数
        private void button2_Click(object sender, EventArgs e)
        {
            //锁定TimePeriod_GBox控件
            TPNum_Tbox.Enabled = false;
            foreach (TextBox Tb in TimePeriodBeginTBoxList)
            { Tb.Enabled = false; }
            foreach (TextBox Tb in TimePeriodEndTBoxList)
            { Tb.Enabled = false; }
            //解锁控件
            //TruckGenerate_Btn.Enabled = true;
            Sim_Btn.Enabled = true;
            Sim_NonP.Enabled = true;
            //添加控件
            int I = 0;
            foreach (Label L in TimePeriodLabelList)
            {
                //时间段名称标签：label控件
                Label label = new Label();
                label.Text = L.Text;
                label.Font = L.Font;
                label.Size = L.Size;
                label.Location = new Point(10, 30 + I * 90);
                Distribution_Gbox.Controls.Add(label);
                //分布类型选择：combox控件
                ComboBox ComBox = new ComboBox();
                ComBox.Name = "CBox" + (I + 1).ToString();//控件名称
                ComBox.Items.Add("Uniform distribution");
                //ComBox.Items.Add("Normal distribution");
                ComBox.Items.Add("Poisson distribution");
                //ComBox.Items.Add("Erlang distribution");
                ComBox.Font = new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular);
                ComBox.Location = new Point(90, 30 + I * 90);
                ComBox.Size = new System.Drawing.Size(120, 20);
                Distribution_Gbox.Controls.Add(ComBox);//窗口添加
                ComBox.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);//添加消息处理
                DisTypeComBoxList.Add(ComBox);
                //分布参数名称Label
                Label DisName_L = new Label();
                DisName_L.Font = new Font("Times New Roman", 9, FontStyle.Regular);
                DisName_L.Text = "";
                DisName_L.Size = new System.Drawing.Size(120, 20);
                DisName_L.Location = new Point(10, ComBox.Location.Y + 25);
                Distribution_Gbox.Controls.Add(DisName_L);//窗口添加
                DisNameLabelList.Add(DisName_L);
                //分布参数TextBox
                TextBox Tbox = new TextBox();
                Tbox.Font = new Font("Times New Roman", 9, FontStyle.Regular);
                Tbox.Size = new System.Drawing.Size(80, 20);
                Tbox.Location = new Point(130, ComBox.Location.Y + 25);
                Distribution_Gbox.Controls.Add(Tbox);//窗口添加
                Tbox.Visible = false;
                DisValueTboxList.Add(Tbox);
                //门区数量Label
                Label GateNum_L = new Label();
                GateNum_L.Text = "GateNum";
                GateNum_L.Font = L.Font;
                GateNum_L.Size = L.Size;
                GateNum_L.Location = new Point(10, DisName_L.Location.Y + 25);
                Distribution_Gbox.Controls.Add(GateNum_L);//窗口添加
                //门区数量 TextBox
                TextBox GateNum_Tbox = new TextBox();
                GateNum_Tbox.Font = new Font("Times New Roman", 9, FontStyle.Regular);
                GateNum_Tbox.Size = new System.Drawing.Size(80, 20);
                GateNum_Tbox.Location = new Point(130, DisName_L.Location.Y + 25);
                Distribution_Gbox.Controls.Add(GateNum_Tbox);//窗口添加
                GateNumTboxList.Add(GateNum_Tbox);
                I++;
            }

            //数据预输入:时间段卡车到达分布类型
            DisTypeComBoxList[0].Text = "Poisson distribution";
            DisTypeComBoxList[1].Text = "Uniform distribution";
            DisTypeComBoxList[2].Text = "Poisson distribution";
            DisValueTboxList[0].Text = "1.087";
            DisValueTboxList[1].Text = "1.583";
            DisValueTboxList[2].Text = "1.206";
            //门区数量
            GateNumTboxList[0].Text = "6";
            GateNumTboxList[1].Text = "6";
            GateNumTboxList[2].Text = "6";
        }

        //combobox值变动事件
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (ComboBox cb in DisTypeComBoxList)
            {
                int n = cb.SelectedIndex;
            }
            for (int i = 0; i < DisTypeComBoxList.Count; i++)
            {
                //初始化
                DisNameLabelList[i].Text = "";
                DisValueTboxList[i].Visible = false;
                //赋值
                ComboBox CBoxNow = DisTypeComBoxList[i];
                CBoxNow = sender as ComboBox;
                if (DisTypeComBoxList[i].SelectedIndex == 0)//第一个索引，即Uniform distribution
                {
                    DisNameLabelList[i].Text = "Trucks Number/min:";
                    DisValueTboxList[i].Visible = true;
                }
                if (DisTypeComBoxList[i].SelectedIndex == 1)//第二个索引，即Poisson distribution
                {
                    DisNameLabelList[i].Text = "Mean Number/min:";
                    DisValueTboxList[i].Visible = true;
                }
            }
        }

        //卡车类型数量确认
        private void TypeNum_Tbox_TextChanged(object sender, EventArgs e)
        {
            //初始化
            foreach (Label L in TypeNameLabelList)
            { TruckType_GBox.Controls.Remove(L); }
            foreach (TextBox Tbox in TypeRatioTBoxList)
            { TruckType_GBox.Controls.Remove(Tbox); }
            //生成控件
            int TypeNum = Convert.ToInt32(TypeNum_Tbox.Text);
            if (TypeNum > 6)//Maximum number of types are 6
            { TypeNum = 6; }
            for (int i = 0; i < TypeNum; i++)
            {
                Label L = new Label();
                L.Text = (i + 1).ToString();
                L.Font = new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular);
                L.Size = new System.Drawing.Size(20, 20);
                L.Location = new Point(5, 70 + i * 25);
                TruckType_GBox.Controls.Add(L);
                TypeNameLabelList.Add(L);
                //%
                TextBox Tbox = new TextBox();
                Tbox.Font = new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular);
                Tbox.Size = new System.Drawing.Size(40, 20);
                Tbox.Location = new Point(30, 70 + i * 25);
                TruckType_GBox.Controls.Add(Tbox);
                TypeRatioTBoxList.Add(Tbox);
                //Shape k
                TextBox Tbox2 = new TextBox();
                Tbox2.Font = new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular);
                Tbox2.Size = new System.Drawing.Size(40, 20);
                Tbox2.Location = new Point(80, 70 + i * 25);
                TruckType_GBox.Controls.Add(Tbox2);
                ShapeKTBoxList.Add(Tbox2);
                //rate lambda
                TextBox Tbox3 = new TextBox();
                Tbox3.Font = new System.Drawing.Font("Times New Roman", 9, FontStyle.Regular);
                Tbox3.Size = new System.Drawing.Size(40, 20);
                Tbox3.Location = new Point(130, 70 + i * 25);
                TruckType_GBox.Controls.Add(Tbox3);
                LambdaTBoxList.Add(Tbox3);
            }
        }
        
        #endregion

        SortedClass SortClass = new SortedClass();

        //Pool_Sim
        List<QueueState> QSList = new List<QueueState>();//Pool门：统一队列
        public List<EventPointer> GateCheck_EPQueue = new List<EventPointer>();//Pool门：排队事件队列
        public List<EventPointer> GateClose_EPQueue = new List<EventPointer>();
        private void Sim_Btn_Click(object sender, EventArgs e)
        {
            //显示窗口重置
            Pooled_ResultDT.Rows.Clear();
            //仿真循环次数
            StaticData.SimRepeatNum = Convert.ToInt32(RepeatNum_TBox.Text);
            //时间段单位
            StaticData.TimePeriodUnit = Convert.ToInt32(TimeUnit_TBox.Text);
            //门区初始化
            StaticData.EnterGateCheckList.Clear();

            #region//参数获取+大门实体生成

            //卡车类型比例
            List<int> TkTypeRatioList = new List<int>();
            List<int> TkShapeKList = new List<int>();
            List<double> TkLambdaList = new List<double>();
            int TkTypeNum = Convert.ToInt32(TypeNum_Tbox.Text);//类型数量
            double SumR = 0;//总和
            for (int i = 0; i < TkTypeNum; i++)
            {
                SumR += Convert.ToDouble(TypeRatioTBoxList[i].Text);
                TkShapeKList.Add(Convert.ToInt32(ShapeKTBoxList[i].Text));
                TkLambdaList.Add(Convert.ToDouble(LambdaTBoxList[i].Text));
            }
            for (int i = 0; i < TkTypeNum; i++)//累积百分比
            {
                int TypeR = Convert.ToInt32(10000 * Convert.ToDouble(TypeRatioTBoxList[i].Text) / SumR);
                if (i == 0)
                { TkTypeRatioList.Add(TypeR); }
                else
                { TkTypeRatioList.Add(TypeR + TkTypeRatioList[i - 1]); }
            }
            //卡车时间段及分布
            double ArrivalRate_Rate = Convert.ToDouble(textBox1.Text);//到达率敏感性分析系数
            int TPNum = Convert.ToInt32(TPNum_Tbox.Text);//时间段数量
            List<List<string>> TP_ParaList = new List<List<string>>();//时间段参数列表
            for (int i = 0; i < TimePeriodLabelList.Count; i++)//提取数据，转换格式
            {
                List<string> TPList = new List<string>();
                TPList.Add(TimePeriodBeginTBoxList[i].Text);//时间段开始时刻
                TPList.Add(TimePeriodEndTBoxList[i].Text);//时间段结束时刻
                TPList.Add(DisTypeComBoxList[i].Text);//到达分布类型
                double ArrivalRate = Convert.ToDouble(DisValueTboxList[i].Text);//到达率参数-用户输入
                ArrivalRate *= ArrivalRate_Rate;//到达率敏感分析

                TPList.Add(ArrivalRate.ToString());//到达率
                TPList.Add(GateNumTboxList[i].Text);//门区数量

                TP_ParaList.Add(TPList);
            }
            //门区数量
            StaticData.EnterGateCheckNum = Convert.ToInt32(GateNum_Tbox.Text);
            //其它参数
            StaticData.Stop_Go_Time = Convert.ToInt32(textBox2.Text);
            StaticData.TruckFailCheckRatio = Convert.ToDouble(textBox3.Text);

            //生成大门实体
            for (int i = 0; i < StaticData.EnterGateCheckNum; i++)
            {
                GateCheckPlat Gate = new GateCheckPlat();
                Gate.Code = i + 1;
                Gate.IsBusy = false;
                Gate.IsClosed = false;
                Gate.IsAboutClose = false;

                StaticData.EnterGateCheckList.Add(Gate);
            }
            //灵活设置门区通道数量
            StaticData.GateTimeWindowStart = Convert.ToInt32(GBeginTime_Tbox.Text);//开放时间
            StaticData.GateTimeWindowEnd = Convert.ToInt32(GEndTime_Tbox.Text);//关闭时间

            #endregion

            #region//统计指标记录list声明

            List<double> MeanWaitTimeList = new List<double>();
            List<double> MeanWaitLenList = new List<double>();
            List<double> MaxWaitLenList = new List<double>();//最大队列长度列表
            List<double> MeanWaitTimeList_Peak = new List<double>();
            List<double> MeanWaitTimeList_OffPeak = new List<double>();
            List<double> MeanGateUseRatioList = new List<double>();//门区利用率

            //等待时间、队列长度、门区利用率 时间分布指标记录
            int TotalTime = (Convert.ToInt32(GEndTime_Tbox.Text) - Convert.ToInt32(GBeginTime_Tbox.Text)) * 60;
            int TimeIntervelNum = TotalTime / StaticData.TimePeriodUnit;//时间段
            List<List<double>> MeanWaitTimeList_TimeUnit = new List<List<double>>();//时间
            List<List<double>> MeanWaitLenList_TimeUnit = new List<List<double>>();//队列长度
            List<List<double>> MeanUseRatioList_TimeUnit = new List<List<double>>();//门区利用率
            for (int i = 0; i < TimeIntervelNum; i++)//初始化
            {
                List<double> WaitTimeList = new List<double>();
                MeanWaitTimeList_TimeUnit.Add(WaitTimeList);
                List<double> WaitLenList = new List<double>();
                MeanWaitLenList_TimeUnit.Add(WaitLenList);
                List<double> UseRatioList = new List<double>();
                MeanUseRatioList_TimeUnit.Add(UseRatioList);
            }

            #endregion


            //时间统计
            Stopwatch sw = new Stopwatch();
            sw.Start();//开始统计

            #region//仿真循环开始

            for (int S = 0; S < StaticData.SimRepeatNum; S++)
            {
                #region//初始化

                QSList.Clear();

                StaticData.Truck.Clear();

                //大门占用状况重置
                foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                {
                    Gate.IsBusy = false;
                    Gate.GSList.Clear();
                    //门区开放区间 起始、终止 时间赋初值
                    Gate.OpenPeriod_StartTime = StaticData.GateTimeWindowStart * 3600;
                    Gate.OpenPeriod_EndTime = StaticData.GateTimeWindowEnd * 3600;
                }
                //事件排队队列重置
                GateCheck_EPQueue.Clear();

                //产生卡车
                Trucks TK = new Trucks();
                for (int i = 0; i < TPNum; i++)//每个时间段分别产生卡车
                {
                    TK.TruckCreat(TP_ParaList[i], TkTypeRatioList, S,TkShapeKList,TkLambdaList);
                }

                //建立初始事件表
                foreach (Trucks tk in StaticData.Truck)
                {
                    EventPointer EP = new EventPointer();
                    EP.Time = tk.OuccerTime;
                    EP.Type = "GateCheck";
                    EP.Truck = tk;
                    EP.TypeNum = 1;//非解锁事件
                    StaticData.EventList.Add(EP);
                }

                #endregion

                #region//门区通道开闭事件

                //关闭初始事件
                int CloseNum1 = 0;//关闭通道数量
                int CloseTime1 = Convert.ToInt32(TimePeriodBeginTBoxList[0].Text) * 3600;
                for (int i = 0; i < CloseNum1; i++)
                {
                    EventPointer EP = new EventPointer();
                    EP.Time =CloseTime1;
                    EP.Gate = StaticData.EnterGateCheckList[i];
                    EP.TypeNum = 0;
                    EP.Type = "GateClose";
                    //插入事件并排序
                    StaticData.EventList.Insert(0, EP);
                    //SortClass.InsertSortEP(StaticData.EventList, EP);
                }
                //开放初始事件
                int OpenNum1 = CloseNum1;//开放门区数量
                int OpenTime1 = Convert.ToInt32(TimePeriodBeginTBoxList[1].Text) * 3600;
                OpenTime1 = 10 * 3600;
                for (int i = 0; i < OpenNum1; i++)
                {
                    EventPointer EP = new EventPointer();
                    EP.Time = OpenTime1;
                    EP.Gate = StaticData.EnterGateCheckList[i];
                    EP.TypeNum = 0;
                    EP.Type = "GateOpen";
                    //插入事件并排序
                    SortClass.InsertSortEP(StaticData.EventList, EP);
                }
                //关闭门区事件
                int CloseNum2 = 0;
                int CloseTime2 = (Convert.ToInt32(TimePeriodBeginTBoxList[2].Text) + 0) * 3600;
                CloseTime2 = 12 * 3600;
                for (int i = 0; i < CloseNum2; i++)
                {
                    EventPointer EP = new EventPointer();
                    EP.Time = CloseTime2;
                    EP.Gate = StaticData.EnterGateCheckList[i];
                    EP.TypeNum = 0;
                    EP.Type = "GateClose";
                    //插入事件并排序
                    SortClass.InsertSortEP(StaticData.EventList, EP);
                }

                #endregion

                //队列头处理
                QueueState QS_first = new QueueState();
                QS_first.Time = Convert.ToInt32(TP_ParaList[0][0]) * 60 * 60;//时段开始时刻
                QS_first.QueueLength = 0;
                QSList.Add(QS_first);

                //开始仿真
                do
                {
                    EventPointer EP = StaticData.EventList[0];
                    if (EP.Type == "GateCheck" || EP.Type == "GateCheck_Wait")
                    { GateCheck_P(EP,S); }
                    if (EP.Type == "Unlock")
                    { GateUnlock_P(EP); }
                    if (EP.Type == "GateClose")
                    { GateClose(EP); }
                    if (EP.Type == "GateOpen")
                    { GateOpen(EP); }
                    StaticData.EventList.Remove(EP);
                }
                while (StaticData.EventList.Count != 0);

                //仿真结束时刻
                int SimEndTime = StaticData.Truck[StaticData.Truck.Count - 1].Leave_GateEnter_Time;
                //队列末尾处理
                QueueState QS_end = new QueueState();
                QS_end.QueueLength = 0;
                QS_end.Time = SimEndTime;//总仿真时长
                QSList.Add(QS_end);
                //为每个队列状态添加结束时刻
                for (int i = 0; i < QSList.Count; i++)
                {
                    if (i != QSList.Count - 1)
                    {
                        QSList[i].EndTime = QSList[i + 1].Time;
                    }
                    else
                    {
                        QSList[i].EndTime = QS_end.Time;
                    }
                }

                #region 单次循环-统计记录

                #region 全过程统计

                //门区总开放时间（分母）
                int TotalGateTime = 0;
                foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                {
                    TotalGateTime += (Gate.OpenPeriod_EndTime - Gate.OpenPeriod_StartTime);
                }

                #region 等待时间
                int SumWaitTime = 0;
                int SumWaitTime_Peak = 0; int PeakNum = 0;
                int SumWaitTime_OffPeak = 0; int OffPeakNum = 0;
                foreach (Trucks tk in StaticData.Truck)
                {
                    SumWaitTime += tk.GateWaitTime;
                    //Peak/Off-Peak到达的卡车等待时间统计
                    if (tk.OuccerTime > StaticData.PeakBegin * 60 * 60 && tk.OuccerTime <= StaticData.PeakEnd * 60 * 60)
                    {
                        SumWaitTime_Peak += tk.GateWaitTime;
                        PeakNum++;
                    }
                    else
                    {
                        SumWaitTime_OffPeak += tk.GateWaitTime;
                        OffPeakNum++;
                    }
                }
                double MeanWaitTime = (double)SumWaitTime / (double)StaticData.Truck.Count;
                double MeanWaitTime_Peak = (double)SumWaitTime_Peak / (double)PeakNum;
                double MeanWaitTime_OffPeak = (double)SumWaitTime_OffPeak / (double)OffPeakNum;
                MeanWaitTimeList.Add(MeanWaitTime / 60);
                MeanWaitTimeList_Peak.Add(MeanWaitTime_Peak / 60);
                MeanWaitTimeList_OffPeak.Add(MeanWaitTime_OffPeak / 60);

                //test
                if (MeanWaitTime > 400)
                {
                    int test = 0;
                }
                List<int> TList = new List<int>();
                foreach (Trucks tk in StaticData.Truck)
                {
                    TList.Add(tk.GateWaitTime);
                }

                #endregion

                #region 队列长度

                int SumLenTime = 0;//长度*时间
                int MaxLen = 0;
                for (int i = 0; i < QSList.Count; i++)
                {
                    QueueState QS = QSList[i];
                    SumLenTime += (QS.EndTime - QS.Time) * QS.QueueLength;
                    if (QS.QueueLength > MaxLen)
                    {
                        MaxLen = QS.QueueLength;
                    }
                }
                double MeanQLength = (double)SumLenTime / (double)(QS_end.Time - QS_first.Time);
                MeanWaitLenList.Add(MeanQLength);
                MaxWaitLenList.Add(MaxLen);
                
                #endregion

                #region 门区利用率

                int TotalBusyTime = 0;//门区总工作时间（分子）
                int Gate_begin = StaticData.GateTimeWindowStart * 3600;
                int Gate_end = StaticData.GateTimeWindowEnd * 3600;
                foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                {
                    foreach (GateState gs in Gate.GSList)
                    {
                        if (gs.BeginTime < Gate_end && gs.EndTime <= Gate_end)
                        { TotalBusyTime += gs.EndTime - gs.BeginTime; }
                        if (gs.BeginTime < Gate_end && gs.EndTime > Gate_end)
                        { TotalBusyTime += Gate_end - gs.BeginTime; }
                    }
                }
                double MeanGateUseRatio = (double)TotalBusyTime / (double)TotalGateTime;
                MeanGateUseRatioList.Add(MeanGateUseRatio);
                
                #endregion

                #endregion

                #region 分时间段统计

                for (int i = 0; i < TimeIntervelNum; i++)
                {
                    int BeginTime = (Convert.ToInt32(GBeginTime_Tbox.Text) * 60 + i * StaticData.TimePeriodUnit) * 60;//单位：秒
                    int EndTime = BeginTime + StaticData.TimePeriodUnit * 60;//单位：秒
                    int TotalGateTime_TimeUnit = 0;//该时间段门区总开放时间（分母）

                    foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                    {
                        //判断门区在该时间段内是否Open
                        if (BeginTime <= Gate.OpenPeriod_StartTime && EndTime > Gate.OpenPeriod_StartTime)
                        {
                            if (EndTime <= Gate.OpenPeriod_EndTime)
                            {
                                TotalGateTime_TimeUnit += EndTime - Gate.OpenPeriod_StartTime;
                            }
                            if (EndTime > Gate.OpenPeriod_EndTime)
                            {
                                TotalGateTime_TimeUnit += Gate.OpenPeriod_EndTime - Gate.OpenPeriod_StartTime;
                            }
                        }
                        if (BeginTime > Gate.OpenPeriod_StartTime && BeginTime <= Gate.OpenPeriod_EndTime)
                        {
                            if (EndTime <= Gate.OpenPeriod_EndTime)
                            {
                                TotalGateTime_TimeUnit += EndTime - BeginTime;
                            }
                            if (EndTime > Gate.OpenPeriod_EndTime)
                            {
                                TotalGateTime_TimeUnit += Gate.OpenPeriod_EndTime - BeginTime;
                            }
                        }
                    }

                    #region 等待时间

                    List<double> WaitTimeList_ThisUnit = new List<double>();
                    foreach (Trucks tk in StaticData.Truck)
                    {
                        if (tk.OuccerTime >= BeginTime && tk.OuccerTime < EndTime)
                        {
                            WaitTimeList_ThisUnit.Add(tk.GateWaitTime);
                            //MeanWaitTimeList_TimeUnit[i].Add(tk.GateWaitTime);
                            if (tk.OuccerTime > EndTime)
                            { break; }
                        }
                    }

                    double Mean_WaitTimeList_ThisUnit = 0;//该时段平均等待时间
                    if (WaitTimeList_ThisUnit.Count == 0)//该时段没有卡车到达
                    {
                        Mean_WaitTimeList_ThisUnit = 0;
                    }
                    else
                    {
                        Mean_WaitTimeList_ThisUnit=MeanList(WaitTimeList_ThisUnit);
                    }

                    MeanWaitTimeList_TimeUnit[i].Add(Mean_WaitTimeList_ThisUnit);

                    #endregion

                    #region 队列长度

                    double sumlentime = 0;//工作时间
                    List<double> WaitLenList_ThisUnit = new List<double>();
                    for (int j = 0; j < QSList.Count; j++)
                    {
                        QueueState QS = QSList[j];
                        //跳出循环设置
                        if (QS.Time > EndTime)
                        { break; }
                        //记录长度*时间
                        if (QS.Time <= BeginTime && QS.EndTime > BeginTime)
                        {
                            if (QS.EndTime <= EndTime)
                            {
                                sumlentime = QS.QueueLength * (QS.EndTime - BeginTime);
                                WaitLenList_ThisUnit.Add(sumlentime);
                                //MeanWaitLenList_TimeUnit[i].Add(sumlentime);
                            }
                            if (QS.EndTime > EndTime)
                            {
                                sumlentime = QS.QueueLength * (EndTime - BeginTime);
                                WaitLenList_ThisUnit.Add(sumlentime);
                                //MeanWaitLenList_TimeUnit[i].Add(sumlentime);
                            }
                        }
                        if (QS.Time > BeginTime && QS.Time <= EndTime)
                        {
                            if (QS.EndTime <= EndTime)
                            {
                                sumlentime = QS.QueueLength * (QS.EndTime - QS.Time);
                                WaitLenList_ThisUnit.Add(sumlentime);
                                //MeanWaitLenList_TimeUnit[i].Add(sumlentime);
                            }
                            if (QS.EndTime > EndTime)
                            {
                                sumlentime = QS.QueueLength * (EndTime - QS.Time);
                                WaitLenList_ThisUnit.Add(sumlentime);
                                //MeanWaitLenList_TimeUnit[i].Add(sumlentime);
                            }
                        }
                    }
                    double WaitLen_ThisUnit = SumList(WaitLenList_ThisUnit) / (StaticData.TimePeriodUnit * 60);
                    MeanWaitLenList_TimeUnit[i].Add(WaitLen_ThisUnit);

                    #endregion

                    #region 门区利用率

                    int TotalBusyTime_TimeUnit = 0;//该时间段门区总工作时间（分子）
                    foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                    {
                        for (int j = 0; j < Gate.GSList.Count; j++)
                        {
                            GateState gs = Gate.GSList[j];
                            //跳出循环判断
                            if (gs.BeginTime > EndTime)
                            { break; }
                            if (gs.BeginTime <= BeginTime && gs.EndTime > BeginTime)
                            {
                                if (gs.EndTime <= EndTime)
                                { TotalBusyTime_TimeUnit += gs.EndTime - BeginTime; }
                                if (gs.EndTime > EndTime)
                                { TotalBusyTime_TimeUnit += EndTime - BeginTime; }
                            }
                            if (gs.BeginTime > BeginTime && gs.BeginTime <= EndTime)
                            {
                                if (gs.EndTime <= EndTime)
                                { TotalBusyTime_TimeUnit += gs.EndTime - gs.BeginTime; }
                                if (gs.EndTime > EndTime)
                                { TotalBusyTime_TimeUnit += EndTime - gs.BeginTime; }
                            }
                        }
                    }
                    double MeanUseRatio = (double)TotalBusyTime_TimeUnit / (double)TotalGateTime_TimeUnit;
                    MeanUseRatioList_TimeUnit[i].Add(MeanUseRatio);

                    #endregion

                } 

                #endregion

                #endregion

                GC.Collect();
            }

            #endregion//仿真循环结束

            //结束时间统计
            sw.Stop();
            label8.Text = "TimeCost: " + sw.Elapsed.TotalSeconds.ToString();
            double SingleTime = Convert.ToDouble(sw.Elapsed.TotalSeconds) / (double)StaticData.SimRepeatNum;
            label10.Text = "SingleRepeatTime：" + SingleTime.ToString();

            #region//整体统计

            //求95置信水平的仿真平均值、标准差、最大最小值
            MeanWaitTimeList = SortQueue(MeanWaitTimeList);
            ListConSelect(MeanWaitTimeList, 0.95);
            double MEAN_WT = MeanList(MeanWaitTimeList);
            double ST_WT = StList(MeanWaitTimeList);
            double Max_WT = MaxList(MeanWaitTimeList);
            double MIN_WT = MinList(MeanWaitTimeList);

            ListConSelect(MeanWaitLenList, 0.95);
            double MEAN_WL = MeanList(MeanWaitLenList);
            double ST_WL = StList(MeanWaitLenList);
            double Max_WL = MaxList(MeanWaitLenList);
            double MIN_WL = MinList(MeanWaitLenList);
            
            //最大队列长度
            ListConSelect(MaxWaitLenList, 0.95);
            double Max_QLen = MeanList(MaxWaitLenList);

            //95置信水平的门区平均利用率
            ListConSelect(MeanGateUseRatioList, 0.95);
            double MEAN_GATE_UR = MeanList(MeanGateUseRatioList);
            double ST_GATE_UR = StList(MeanGateUseRatioList);
            double MAX_GATE_UR = MaxList(MeanGateUseRatioList);
            double MIN_GATE_UR = MinList(MeanGateUseRatioList);

            //等待时间时间分布
            List<double> Mean_WT_List = new List<double>();
            for (int i = 0; i < TimeIntervelNum; i++)
            {
                ListConSelect(MeanWaitTimeList_TimeUnit[i], 0.95);
                double mean_wt_timeunit = MeanList(MeanWaitTimeList_TimeUnit[i]);
                Mean_WT_List.Add(mean_wt_timeunit);
            }
            //高峰/平峰 卡车平均等待时间
            ListConSelect(MeanWaitTimeList_Peak, 0.95);
            ListConSelect(MeanWaitTimeList_OffPeak, 0.95);
            double MEAN_QT_Peak = MeanList(MeanWaitTimeList_Peak);
            double MEAN_QT_OffPeak = MeanList(MeanWaitTimeList_OffPeak);

            //队列长度 时间分布
            List<double> Mean_WL_List = new List<double>();
            List<double> QL_Peak_List = new List<double>();
            List<double> QL_Offpeak_List = new List<double>();
            for (int i = 0; i < TimeIntervelNum; i++)
            {
                //double mean_sum_ql = SumList(MeanWaitLenList_TimeUnit[i]) / StaticData.SimRepeatNum;//平均 长度*时间
                //double mean_ql = mean_sum_ql / (StaticData.TimePeriodUnit * 60);//平均队列长度
                
                ListConSelect(MeanWaitLenList_TimeUnit[i], 0.95);
                double mean_ql = MeanList(MeanWaitLenList_TimeUnit[i]);
                Mean_WL_List.Add(mean_ql);

                //记录高峰/平峰 分时段平均队列长度
                int TimeNow = StaticData.GateTimeWindowStart * 60 + i * StaticData.TimePeriodUnit;//单位：分钟
                if (TimeNow >= StaticData.PeakBegin * 60 && TimeNow < StaticData.PeakEnd * 60)
                {
                    QL_Peak_List.Add(mean_ql);
                }
                else
                {
                    QL_Offpeak_List.Add(mean_ql);
                }
            }
            //高峰/平峰 队列长度
            double MEAN_QL_Peak = MeanList(QL_Peak_List);
            double MEAN_QL_OffPeak = MeanList(QL_Offpeak_List);

            //门区利用率 时间分布
            List<double> Mean_UR_List = new List<double>();
            for (int i = 0; i < TimeIntervelNum; i++)
            {
                ListConSelect(MeanUseRatioList_TimeUnit[i], 0.95);
                double mean_useratio = MeanList(MeanUseRatioList_TimeUnit[i]);
                Mean_UR_List.Add(mean_useratio);
            }

            #endregion

            #region//显示

            for (int i = 0; i < 3; i++)
            {
                DataRow r = Pooled_ResultDT.NewRow();
                if (i == 1)
                {
                    r[0] = "：到达率倍数" + ArrivalRate_Rate.ToString();
                }
                if (i == 2)
                {
                    r[1] = "Mean"; r[2] = "St"; r[3] = "Max"; r[4] = "Min";
                }
                Pooled_ResultDT.Rows.Add(r);
            }
            DataRow r_wt = Pooled_ResultDT.NewRow();
            r_wt[0] = "WaitTime";
            r_wt[1] = MEAN_WT.ToString("f2");
            r_wt[2] = ST_WT.ToString("f2");
            r_wt[3] = Max_WT.ToString("f2");
            r_wt[4] = MIN_WT.ToString("f2");
            Pooled_ResultDT.Rows.Add(r_wt);

            DataRow r_wl = Pooled_ResultDT.NewRow();
            r_wl[0] = "QueueLength";
            r_wl[1] = MEAN_WL.ToString("f2");
            r_wl[2] = ST_WL.ToString("f2");
            r_wl[3] = Max_WL.ToString("f2");
            r_wl[4] = MIN_WL.ToString("f2");
            Pooled_ResultDT.Rows.Add(r_wl);

            DataRow r_gur = Pooled_ResultDT.NewRow();
            r_gur[0] = "GateUseRatio";
            r_gur[1] = MEAN_GATE_UR.ToString("P");
            Pooled_ResultDT.Rows.Add(r_gur);

            DataRow r_wl_max = Pooled_ResultDT.NewRow();
            r_wl_max[0] = "Max QueueLength";
            r_wl_max[1] = Max_QLen.ToString("f2");
            Pooled_ResultDT.Rows.Add(r_wl_max);

            //QT QL 平峰/高峰 统计
            for (int i = 0; i < 3; i++)
            {
                DataRow r = Pooled_ResultDT.NewRow();
                if (i == 0)
                    r[1] = "Peak"; r[2] = "Off-Peak";
                if (i == 1)
                {
                    r[0] = "Queuing Time";
                    r[1] = MEAN_QT_Peak.ToString("f2");
                    r[2] = MEAN_QT_OffPeak.ToString("f2");
                }
                if (i == 2)
                {
                    r[0] = "Queue Length";
                    r[1] = MEAN_QL_Peak.ToString("f2");
                    r[2] = MEAN_QL_OffPeak.ToString("f2");
                }
                Pooled_ResultDT.Rows.Add(r);
            }

            //等待时间/队列长度/门区利用率 时间分布
            DataRow r_blank = Pooled_ResultDT.NewRow();
            r_blank[0] = "时间";
            r_blank[1] = "等待时间";
            r_blank[2] = "队列长度";
            r_blank[3] = "门区利用率";
            Pooled_ResultDT.Rows.Add(r_blank);

            for (int i = 0; i < TimeIntervelNum; i++)
            {
                DataRow r = Pooled_ResultDT.NewRow();
                r[0] = (Convert.ToInt32(GBeginTime_Tbox.Text) * 60 + StaticData.TimePeriodUnit * i).ToString();

                r[1] = (Mean_WT_List[i] / 60).ToString();//单位：分钟
                r[2] = Mean_WL_List[i].ToString();
                r[3] = Mean_UR_List[i].ToString("P");
                Pooled_ResultDT.Rows.Add(r);
            }
            PoolSim_Result_DataGrid.DataSource = Pooled_ResultDT;
            //导出结果
            ExportExcel(Pooled_ResultDT);

            #endregion
        }

        //NonPool_Sim
        private void Sim_NonP_Click(object sender, EventArgs e)//每个门区有一个队列
        {
            //显示窗口重置
            NPooled_ResultDT.Rows.Clear();
            //仿真循环次数
            StaticData.SimRepeatNum = Convert.ToInt32(RepeatNum_TBox.Text);
            //时间段单位
            StaticData.TimePeriodUnit = Convert.ToInt32(TimeUnit_TBox.Text);

            #region//参数获取

            //卡车类型比例
            List<int> TkTypeRatioList = new List<int>();
            List<int> TkShapeKList = new List<int>();
            List<double> TkLambdaList = new List<double>();
            int TkTypeNum = Convert.ToInt32(TypeNum_Tbox.Text);//类型数量
            double SumR = 0;//总和
            for (int i = 0; i < TkTypeNum; i++)
            {
                SumR += Convert.ToDouble(TypeRatioTBoxList[i].Text);
                TkShapeKList.Add(Convert.ToInt32(ShapeKTBoxList[i].Text));
                TkLambdaList.Add(Convert.ToDouble(LambdaTBoxList[i].Text));
            }
            for (int i = 0; i < TkTypeNum; i++)//累积百分比
            {
                int TypeR = Convert.ToInt32(10000 * Convert.ToDouble(TypeRatioTBoxList[i].Text) / SumR);
                if (i == 0)
                { TkTypeRatioList.Add(TypeR); }
                else
                { TkTypeRatioList.Add(TypeR + TkTypeRatioList[i - 1]); }
            }
            //卡车时间段及分布
            double ArrivalRate_Rate = Convert.ToDouble(textBox1.Text);//到达率敏感性分析系数
            int TPNum = Convert.ToInt32(TPNum_Tbox.Text);
            List<List<string>> TP_ParaList = new List<List<string>>();//时间段分布列表
            for (int i = 0; i < TimePeriodLabelList.Count; i++)//提取数据，转换格式
            {
                List<string> TPList = new List<string>();
                TPList.Add(TimePeriodBeginTBoxList[i].Text);
                TPList.Add(TimePeriodEndTBoxList[i].Text);
                TPList.Add(DisTypeComBoxList[i].Text);
                //到达率
                double ArrivalRate = Convert.ToDouble(DisValueTboxList[i].Text);
                ArrivalRate *= ArrivalRate_Rate;
                TPList.Add(ArrivalRate.ToString());//到达率

                TP_ParaList.Add(TPList);
            }
            //门区参数
            StaticData.EnterGateCheckNum = Convert.ToInt32(GateNum_Tbox.Text);
            StaticData.GateTimeWindowStart = Convert.ToInt32(GBeginTime_Tbox.Text);//开放时间
            StaticData.GateTimeWindowEnd = Convert.ToInt32(GEndTime_Tbox.Text);
            //其它参数
            StaticData.Stop_Go_Time = Convert.ToInt32(textBox2.Text);
            StaticData.TruckFailCheckRatio = Convert.ToDouble(textBox3.Text);


            #endregion

            #region//统计指标记录list声明
            //排队时间
            List<double> MeanWaitTimeList = new List<double>();
            List<double> MeanWaitTimeList_Peak = new List<double>();
            List<double> MeanWaitTimeList_OffPeak = new List<double>();

            //队列长度
            List<double> MeanWaitLenList = new List<double>();
            List<double> MeanWaitLenList_Peak = new List<double>();
            List<double> MeanWaitLenList_OffPeak = new List<double>();
            //门区利用率
            List<double> MeanGateUseRatioList = new List<double>();//门区利用率
            List<double> MeanGateUseRatioList_Peak = new List<double>();//门区利用率
            List<double> MeanGateUseRatioList_OffPeak = new List<double>();//门区利用率

            //等待时间、队列长度、门区利用率 时间分布指标记录
            int TotalTime = (Convert.ToInt32(GEndTime_Tbox.Text) - Convert.ToInt32(GBeginTime_Tbox.Text)) * 60;
            int TimeIntervelNum = TotalTime / StaticData.TimePeriodUnit;//时间段数量
            List<List<double>> MeanWaitTimeList_TimeUnit = new List<List<double>>();//时间
            List<List<double>> MeanQLengthList_TimeUnit = new List<List<double>>();
            List<List<double>> MeanUseRatioList_TimeUnit = new List<List<double>>();//门区利用率
            for (int i = 0; i < TimeIntervelNum; i++)//初始化
            {
                List<double> WaitTimeList = new List<double>();
                MeanWaitTimeList_TimeUnit.Add(WaitTimeList);
                List<double> WaitLenList = new List<double>();
                MeanQLengthList_TimeUnit.Add(WaitLenList);
                List<double> UseRatioList = new List<double>();
                MeanUseRatioList_TimeUnit.Add(UseRatioList);
            }
            #endregion

            //时间统计
            Stopwatch sw = new Stopwatch();
            sw.Start();//开始统计

            #region 仿真循环
            for (int S = 0; S < StaticData.SimRepeatNum; S++)
            {

                #region  //初始化

                StaticData.Truck.Clear();
                StaticData.EnterGateCheckList.Clear();
                //生成大门
                for (int i = 0; i < StaticData.EnterGateCheckNum; i++)
                {
                    GateCheckPlat Gate = new GateCheckPlat();
                    Gate.Code = i + 1;
                    Gate.IsBusy = false;
                    Gate.IsClosed = false;
                    Gate.IsAboutClose = false;
                    StaticData.EnterGateCheckList.Add(Gate);
                }
                //产生卡车
                Trucks TK = new Trucks();
                for (int i = 0; i < TPNum; i++)//每个时间段分别产生卡车
                {
                    TK.TruckCreat(TP_ParaList[i], TkTypeRatioList, S,TkShapeKList,TkLambdaList);
                }
                //建立初始事件表
                foreach (Trucks tk in StaticData.Truck)//初始事件表
                {
                    EventPointer EP = new EventPointer();
                    EP.Time = tk.OuccerTime;
                    EP.Type = "GateCheck";
                    EP.Truck = tk;
                    EP.TypeNum = 1;//非解锁事件
                    StaticData.EventList.Add(EP);
                }
                //大门对应卡车队列及大门开放时间 初始赋值
                int SimBeginTime = StaticData.GateTimeWindowStart * 60 * 60;
                foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                {
                    QueueState QS = new QueueState();
                    QS.Time = SimBeginTime;//初始队列开始时刻==大门开始运营时间
                    QS.QueueLength = 0;
                    Gate.QSList.Add(QS);
                    //门区开放区间 起始、终止 时间赋初值
                    Gate.OpenPeriod_StartTime = StaticData.GateTimeWindowStart * 3600;
                    Gate.OpenPeriod_EndTime = StaticData.GateTimeWindowEnd * 3600;
                }

                #endregion

                #region//关闭大门事件
                int CloseNum1 = 0;//关闭通道数量
                for (int i = 0; i < CloseNum1; i++)
                {
                    EventPointer EP = new EventPointer();
                    EP.Time = Convert.ToInt32(TimePeriodBeginTBoxList[0].Text) * 3600;
                    EP.Gate = StaticData.EnterGateCheckList[i];
                    EP.TypeNum = 0;
                    EP.Type = "GateClose";
                    //插入事件并排序
                    SortClass.InsertSortEP(StaticData.EventList, EP);
                }
                //开放初始事件
                int OpenNum = CloseNum1;
                for (int i = 0; i < OpenNum; i++)
                {
                    EventPointer EP = new EventPointer();
                    EP.Time = Convert.ToInt32(TimePeriodBeginTBoxList[1].Text) * 3600;
                    EP.Gate = StaticData.EnterGateCheckList[i];
                    EP.TypeNum = 0;
                    EP.Type = "GateOpen";
                    //插入事件并排序
                    SortClass.InsertSortEP(StaticData.EventList, EP);
                }
                //关闭门区事件
                int CloseNum2 = 0;
                for (int i = 0; i < CloseNum2; i++)
                {
                    EventPointer EP = new EventPointer();
                    EP.Time = Convert.ToInt32(TimePeriodBeginTBoxList[2].Text) * 3600;
                    EP.Gate = StaticData.EnterGateCheckList[i];
                    EP.TypeNum = 0;
                    EP.Type = "GateClose";
                    //插入事件并排序
                    SortClass.InsertSortEP(StaticData.EventList, EP);
                }
                #endregion

                do
                {
                    EventPointer EP = StaticData.EventList[0];
                    if (EP.Type == "GateCheck" || EP.Type == "GateCheck_Wait")
                    { GateCheck_NP(EP, S); }
                    if (EP.Type == "Unlock")
                    { GateUnlock_NP(EP); }
                    if (EP.Type == "GateClose")
                    { GateClose(EP); }
                    if (EP.Type == "GateOpen")
                    { GateOpen(EP); }

                    StaticData.EventList.Remove(EP);
                }
                while (StaticData.EventList.Count != 0);

                //仿真结束时间
                int SimEndTime = StaticData.Truck[0].Leave_GateEnter_Time;
                foreach (Trucks tk in StaticData.Truck)
                {
                    if (tk.Leave_GateEnter_Time > SimEndTime)
                    { SimEndTime = tk.Leave_GateEnter_Time; }
                }
                //门区各队列 末尾处理
                foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                {
                    QueueState QS = new QueueState();
                    QS.Time = SimEndTime;//仿真结束时间
                    QS.QueueLength = 0;
                    Gate.QSList.Add(QS);
                    //为每个队列状态添加结束时刻
                    for (int i = 0; i < Gate.QSList.Count; i++)
                    {
                        if (i != Gate.QSList.Count - 1)
                        {
                            Gate.QSList[i].EndTime = Gate.QSList[i + 1].Time;
                        }
                        else
                        {
                            Gate.QSList[i].EndTime = SimEndTime;
                        }
                    }
                }
                #region 单次仿真统计

                #region 全过程统计

                //总门区开放时间（分母）
                int TotalGateOpenTime = 0;
                foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                {
                    TotalGateOpenTime += Gate.OpenPeriod_EndTime - Gate.OpenPeriod_StartTime;
                }

                #region 等待时间

                int SumWaitTime = 0;
                int SumWaitTime_Peak = 0; int PeakNum = 0;
                int SumWaitTime_OffPeak = 0; int OffPeakNum = 0;
                foreach (Trucks tk in StaticData.Truck)
                {
                    SumWaitTime += tk.GateWaitTime;
                    if (tk.OuccerTime > StaticData.PeakBegin * 60 * 60 && tk.OuccerTime <= StaticData.PeakEnd * 60 * 60)
                    {
                        SumWaitTime_Peak += tk.GateWaitTime;
                        PeakNum++;
                    }
                    else
                    {
                        SumWaitTime_OffPeak += tk.GateWaitTime;
                        OffPeakNum++;
                    }
                }
                double MeanWaitTime = (double)SumWaitTime / (double)StaticData.Truck.Count;
                //test
                List<int> TList = new List<int>();
                foreach (Trucks tk in StaticData.Truck)
                {
                    TList.Add(tk.GateWaitTime);
                }

                double MeanWaitTime_Peak = (double)SumWaitTime_Peak / (double)PeakNum;
                double MeanWaitTime_OffPeak = (double)SumWaitTime_OffPeak / (double)OffPeakNum;
                MeanWaitTimeList.Add(MeanWaitTime / 60);
                MeanWaitTimeList_Peak.Add(MeanWaitTime_Peak / 60);
                MeanWaitTimeList_OffPeak.Add(MeanWaitTime_OffPeak / 60);

                #endregion

                #region 队列长度

                int SumLenTime = 0;//队列时长（分子）
                foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                {
                    int LenTime = 0;//长度*时间
                    for (int i = 0; i < Gate.QSList.Count; i++)
                    {
                        QueueState QS = Gate.QSList[i];
                        LenTime += (QS.EndTime - QS.Time) * QS.QueueLength;
                    }
                    SumLenTime += LenTime;
                }
                double MeanQLength = (double)SumLenTime / (double)TotalGateOpenTime;
                MeanWaitLenList.Add(MeanQLength);

                #endregion

                #region 门区利用率

                int TotalBusyTime = 0;//总工作时间（分子）
                int Gate_begin = StaticData.GateTimeWindowStart * 3600;
                int Gate_end = StaticData.GateTimeWindowEnd * 3600;
                foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                {
                    foreach (GateState gs in Gate.GSList)
                    {
                        if (gs.BeginTime < Gate_end && gs.EndTime <= Gate_end)
                        { TotalBusyTime += gs.EndTime - gs.BeginTime; }
                        if (gs.BeginTime < Gate_end && gs.EndTime > Gate_end)
                        { TotalBusyTime += Gate_end - gs.BeginTime; }
                    }
                }
                double MeanGateUseRatio = (double)TotalBusyTime / (double)TotalGateOpenTime;
                MeanGateUseRatioList.Add(MeanGateUseRatio);

                #endregion

                #endregion

                #region 分时间段统计

                //统计记录声明
                List<double> QLengthList_Period = new List<double>();

                for (int i = 0; i < TimeIntervelNum; i++)
                {
                    int BeginTime = (Convert.ToInt32(GBeginTime_Tbox.Text) * 60 + i * StaticData.TimePeriodUnit) * 60;//单位：秒
                    int EndTime = BeginTime + StaticData.TimePeriodUnit * 60;//单位：秒

                    //该时间段 门区总开放时间（分母）
                    int TotalGateOpenTime_Period = 0;
                    foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                    {
                        //判断门区在该时间段内是否Open
                        if (BeginTime <= Gate.OpenPeriod_StartTime && EndTime > Gate.OpenPeriod_StartTime)
                        {
                            if (EndTime <= Gate.OpenPeriod_EndTime)
                            {
                                TotalGateOpenTime_Period += EndTime - Gate.OpenPeriod_StartTime;
                            }
                            if (EndTime > Gate.OpenPeriod_EndTime)
                            {
                                TotalGateOpenTime_Period += Gate.OpenPeriod_EndTime - Gate.OpenPeriod_StartTime;
                            }
                        }
                        if (BeginTime > Gate.OpenPeriod_StartTime && BeginTime <= Gate.OpenPeriod_EndTime)
                        {
                            if (EndTime <= Gate.OpenPeriod_EndTime)
                            {
                                TotalGateOpenTime_Period += EndTime - BeginTime;
                            }
                            if (EndTime > Gate.OpenPeriod_EndTime)
                            {
                                TotalGateOpenTime_Period += Gate.OpenPeriod_EndTime - BeginTime;
                            }
                        }
                    }

                    #region 等待时间

                    List<double> WaitTimeList_ThisUnit = new List<double>();
                    foreach (Trucks tk in StaticData.Truck)
                    {
                        if (tk.OuccerTime >= BeginTime && tk.OuccerTime < EndTime)
                        {
                            WaitTimeList_ThisUnit.Add(tk.GateWaitTime);
                            //MeanWaitTimeList_TimeUnit[i].Add(tk.GateWaitTime);
                        }
                        if (tk.OuccerTime > EndTime)
                        { break; }
                    }

                    double Mean_WaitTimeList_ThisUnit = 0;//该时段平均等待时间
                    if (WaitTimeList_ThisUnit.Count == 0)//该时段没有卡车到达
                    {
                        Mean_WaitTimeList_ThisUnit = 0;
                    }
                    else
                    {
                        Mean_WaitTimeList_ThisUnit = MeanList(WaitTimeList_ThisUnit);
                    }
                    MeanWaitTimeList_TimeUnit[i].Add(Mean_WaitTimeList_ThisUnit);

                    #endregion

                    #region 队列长度

                    double SumLenTime_Period = 0;//该时段 队列时间（分子）
                    double LenTime = 0;
                    foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                    {
                        for (int j = 0; j < Gate.QSList.Count; j++)
                        {
                            QueueState QS = Gate.QSList[j];
                            //跳出循环设置
                            if (QS.Time > EndTime)
                            { break; }
                            //记录长度*时间
                            if (QS.Time <= BeginTime && QS.EndTime > BeginTime)
                            {
                                if (QS.EndTime <= EndTime)
                                {
                                    LenTime = QS.QueueLength * (QS.EndTime - BeginTime);
                                }
                                if (QS.EndTime > EndTime)
                                {
                                    LenTime = QS.QueueLength * (EndTime - BeginTime);
                                }
                                SumLenTime_Period += LenTime;
                            }
                            if (QS.Time > BeginTime && QS.Time <= EndTime)
                            {
                                if (QS.EndTime <= EndTime)
                                {
                                    LenTime = QS.QueueLength * (QS.EndTime - QS.Time);
                                }
                                if (QS.EndTime > EndTime)
                                {
                                    LenTime = QS.QueueLength * (EndTime - QS.Time);
                                }
                                SumLenTime_Period += LenTime;
                            }
                        }
                    }
                    //该时间段 平均队列长度
                    double MeanQLength_Period = (double)SumLenTime_Period / (double)TotalGateOpenTime_Period;
                    MeanQLengthList_TimeUnit[i].Add(MeanQLength_Period);
                    //QLengthList_Period.Add(MeanQLength_Period);//该时段 队列长度 记录

                    #endregion

                    #region 门区利用率

                    int TotalBusyTime_TimeUnit = 0;//总工作时间（分子）
                    foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                    {
                        for (int j = 0; j < Gate.GSList.Count; j++)
                        {
                            GateState gs = Gate.GSList[j];
                            //跳出循环判断
                            if (gs.BeginTime > EndTime)
                            { break; }
                            if (gs.BeginTime <= BeginTime && gs.EndTime > BeginTime)
                            {
                                if (gs.EndTime <= EndTime)
                                { TotalBusyTime_TimeUnit += gs.EndTime - BeginTime; }
                                if (gs.EndTime > EndTime)
                                { TotalBusyTime_TimeUnit += EndTime - BeginTime; }
                            }
                            if (gs.BeginTime > BeginTime && gs.BeginTime <= EndTime)
                            {
                                if (gs.EndTime <= EndTime)
                                { TotalBusyTime_TimeUnit += gs.EndTime - gs.BeginTime; }
                                if (gs.EndTime > EndTime)
                                { TotalBusyTime_TimeUnit += EndTime - gs.BeginTime; }
                            }
                        }
                    }
                    double MeanUseRatio = (double)TotalBusyTime_TimeUnit / StaticData.EnterGateCheckNum / (StaticData.TimePeriodUnit * 60);
                    MeanUseRatio = (double)TotalBusyTime_TimeUnit / (double)TotalGateOpenTime_Period;
                    MeanUseRatioList_TimeUnit[i].Add(MeanUseRatio);

                    #endregion
                }
                //单次循环 队列长度 记录
                //MeanQLengthList_TimeUnit.Add(QLengthList_Period);


                #endregion

                #endregion

                GC.Collect();
            }

            #endregion//仿真循环结束

            //结束时间统计
            sw.Stop();
            label9.Text = "TimeCost: " + sw.Elapsed.TotalSeconds.ToString();
            double SingleTime = Convert.ToDouble(sw.Elapsed.TotalSeconds) / (double)StaticData.SimRepeatNum;
            label13.Text = "SingleRepeatTime：" + SingleTime.ToString();

            #region 整体统计

            //求95置信水平的仿真平均值、标准差、最大最小值
            //等待时间
            MeanWaitTimeList = SortQueue(MeanWaitTimeList);
            ListConSelect(MeanWaitTimeList, 0.95);
            double MEAN_WT = MeanList(MeanWaitTimeList);
            double ST_WT = StList(MeanWaitTimeList);
            double Max_WT = MaxList(MeanWaitTimeList);
            double MIN_WT = MinList(MeanWaitTimeList);
            //队列长度
            ListConSelect(MeanWaitLenList, 0.95);
            double MEAN_WL = MeanList(MeanWaitLenList);
            double ST_WL = StList(MeanWaitLenList);
            double Max_WL = MaxList(MeanWaitLenList);
            double MIN_WL = MinList(MeanWaitLenList);
            //门区利用率
            ListConSelect(MeanGateUseRatioList, 0.95);
            double MEAN_GATE_UR = MeanList(MeanGateUseRatioList);
            double ST_UR = StList(MeanGateUseRatioList);
            double Max_UR = MaxList(MeanGateUseRatioList);
            double MIN_UR = MinList(MeanGateUseRatioList);

            //等待时间 时间分布
            List<double> Mean_WT_List = new List<double>();
            for (int i = 0; i < TimeIntervelNum; i++)
            {
                ListConSelect(MeanWaitTimeList_TimeUnit[i], 0.95);
                double mean_wt_timeunit = MeanList(MeanWaitTimeList_TimeUnit[i]);
                Mean_WT_List.Add(mean_wt_timeunit);
            }
            //高峰/平峰 卡车平均等待时间
            ListConSelect(MeanWaitTimeList_Peak, 0.95);
            ListConSelect(MeanWaitTimeList_OffPeak, 0.95);
            double MEAN_QT_Peak = MeanList(MeanWaitTimeList_Peak);
            double MEAN_QT_OffPeak = MeanList(MeanWaitTimeList_OffPeak);

            //队列长度 时间分布
            List<double> Mean_QL_List = new List<double>();
            for (int i = 0; i < TimeIntervelNum; i++)
            {
                //List<double> QLengthList = new List<double>();
                //for (int j = 0; j < StaticData.SimRepeatNum; j++)
                //{
                //    QLengthList.Add(MeanQLengthList_TimeUnit[j][i]);
                //}
                ListConSelect(MeanQLengthList_TimeUnit[i], 0.95);
                double MeanQLength_Period = MeanList(MeanQLengthList_TimeUnit[i]);//该时间段平均 队列长度
                Mean_QL_List.Add(MeanQLength_Period);//记录
            }
            //高峰、平峰
            List<double> QL_Peak_List = new List<double>();
            List<double> QL_Offpeak_List = new List<double>();
            for (int i = 0; i < TimeIntervelNum; i++)
            {
                //记录高峰/平峰 分时段平均队列长度
                int TimeNow = StaticData.GateTimeWindowStart * 60 + i * StaticData.TimePeriodUnit;//单位：分钟
                if (TimeNow >= StaticData.PeakBegin * 60 && TimeNow < StaticData.PeakEnd * 60)
                {
                    QL_Peak_List.Add(Mean_QL_List[i]);
                }
                else
                {
                    QL_Offpeak_List.Add(Mean_QL_List[i]);
                }
            }
            //高峰/平峰 队列长度
            double MEAN_QL_Peak = MeanList(QL_Peak_List);
            double MEAN_QL_OffPeak = MeanList(QL_Offpeak_List);

            //门区利用率 时间分布
            List<double> Mean_UR_List = new List<double>();
            for (int i = 0; i < TimeIntervelNum; i++)
            {
                ListConSelect(MeanUseRatioList_TimeUnit[i], 0.95);
                double mean_useratio = MeanList(MeanUseRatioList_TimeUnit[i]);
                Mean_UR_List.Add(mean_useratio);
            }

            #endregion

            #region 显示
            for (int i = 0; i < 3; i++)
            {
                DataRow r = NPooled_ResultDT.NewRow();
                if (i == 1)
                {
                    //r[0] = "大门数量：" + StaticData.EnterGateCheckNum;
                    r[0] = "：到达率倍数" + ArrivalRate_Rate.ToString();
                }
                if (i == 2)
                {
                    r[1] = "Mean"; r[2] = "St"; r[3] = "Max"; r[4] = "Min";
                }
                NPooled_ResultDT.Rows.Add(r);
            }
            DataRow r_wt = NPooled_ResultDT.NewRow();
            r_wt[0] = "WaitTime";
            r_wt[1] = MEAN_WT.ToString("f2");
            r_wt[2] = ST_WT.ToString("f2");
            r_wt[3] = Max_WT.ToString("f2");
            r_wt[4] = MIN_WT.ToString("f2");
            NPooled_ResultDT.Rows.Add(r_wt);

            DataRow r_wl = NPooled_ResultDT.NewRow();
            r_wl[0] = "QueueLength";
            r_wl[1] = MEAN_WL.ToString("f2");
            r_wl[2] = ST_WL.ToString("f2");
            r_wl[3] = Max_WL.ToString("f2");
            r_wl[4] = MIN_WL.ToString("f2");
            NPooled_ResultDT.Rows.Add(r_wl);

            DataRow r_gur = NPooled_ResultDT.NewRow();
            r_gur[0] = "GateUseRatio";
            r_gur[1] = MEAN_GATE_UR.ToString("P");
            r_gur[2] = ST_UR.ToString("f2");
            r_gur[3] = Max_UR.ToString("f2");
            r_gur[4] = MIN_UR.ToString("f2");
            NPooled_ResultDT.Rows.Add(r_gur);

            for (int i = 0; i < 3; i++)
            {
                DataRow r = NPooled_ResultDT.NewRow();

                if (i == 0)
                    r[1] = "Peak"; r[2] = "Off-Peak";
                if (i == 1)
                {
                    r[0] = "Queuing Time";
                    r[1] = MEAN_QT_Peak.ToString("f2");
                    r[2] = MEAN_QT_OffPeak.ToString("f2");
                }
                if (i == 2)
                {
                    r[0] = "Queue Length";
                    r[1] = MEAN_QL_Peak.ToString("f2");
                    r[2] = MEAN_QL_OffPeak.ToString("f2");
                }
                NPooled_ResultDT.Rows.Add(r);
            }

            //等待时间/队列长度 时间分布
            DataRow r_blank = NPooled_ResultDT.NewRow();
            r_blank[0] = "时间";
            r_blank[1] = "等待时间";
            r_blank[2] = "队列长度";
            r_blank[3] = "门区利用率";

            NPooled_ResultDT.Rows.Add(r_blank);

            for (int i = 0; i < TimeIntervelNum; i++)
            {
                DataRow r = NPooled_ResultDT.NewRow();
                r[0] = (Convert.ToInt32(GBeginTime_Tbox.Text) * 60 + StaticData.TimePeriodUnit * i).ToString();
                r[1] = (Mean_WT_List[i] / 60).ToString();//单位：分钟
                r[2] = Mean_QL_List[i].ToString();
                r[3] = Mean_UR_List[i].ToString("P");
                NPooled_ResultDT.Rows.Add(r);
            }

            NPoolSim_Result_DataGrid.DataSource = NPooled_ResultDT;
            #endregion

            //导出结果
            ExportExcel(NPooled_ResultDT);
        }

        #region 事件函数

        public void GateCheck_P(EventPointer EP,int S)//Pooled
        {
            bool IsIdleGate = true;
            foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
            {
                if (Gate.IsBusy == false && Gate.IsClosed == false)//两个条件
                {
                    //产生服从爱尔朗分布的服务时间
                    RandomGenerator RG = new RandomGenerator();

                    //int servetime = Convert.ToInt32(RG.ErlangIndex(StaticData.Gate_k, StaticData.Gate_lam, EP.Time) * 60);//平均1/0.2=5分钟
                    ////int servetime = 300;
                    ////检查出现问题，服务时间增加一倍
                    //if (EP.Truck.IsFailCheck == true)
                    //{
                    //    servetime *= 2;
                    //}

                    int servetime = ServeTimeCal(EP,S);//门区检查时间
                    EP.Truck.GateCheckTime = servetime;//门区完毕检查时间

                    int go_stop_time = 0;//起停附加时间 
                    if (EP.Type == "GateCheck_Wait")//若有等待过程，需附加起停时间
                    {
                        go_stop_time = StaticData.Stop_Go_Time;//仿真时间
                    }
                    EP.Truck.Leave_GateEnter_Time = EP.Time + go_stop_time + servetime;//检查完毕时间
                    EP.Truck.GateWaitTime = EP.Truck.Leave_GateEnter_Time - EP.Truck.OuccerTime - EP.Truck.GateCheckTime;

                    //门区状态更新
                    Gate.Truck = EP.Truck;
                    Gate.IsBusy = true;
                    Gate.BusyTimeStart = EP.Time;
                    Gate.BusyTimeEnd = EP.Truck.Leave_GateEnter_Time;
                    //门区占用状态记录
                    GateState gs = new GateState();
                    gs.BeginTime = Gate.BusyTimeStart + go_stop_time;//占用时间不包括go-and-stop time
                    gs.EndTime = Gate.BusyTimeEnd;
                    Gate.GSList.Add(gs);

                    //产生解锁事件
                    EventPointer EP_F = new EventPointer();
                    EP_F.Time = Gate.BusyTimeEnd;//解锁时间
                    EP_F.Gate = Gate;
                    EP_F.Type = "Unlock";
                    EP_F.TypeNum = 0;//解锁事件
                    //插入事件并排序
                    SortClass.InsertSortEP(StaticData.EventList, EP_F);

                    //队列减少
                    if (EP.Type == "GateCheck_Wait")
                    {
                        QueueState QS = new QueueState();
                        QS.Time = EP.Time;
                        QS.QueueLength = QSList[QSList.Count - 1].QueueLength - 1;
                        QSList.Add(QS);
                    }

                    IsIdleGate = true;
                    break;
                }
                else
                {
                    IsIdleGate = false; 
                }
            }
            if (IsIdleGate == false)//无空闲Gate,加入排队队列
            {
                GateCheck_EPQueue.Add(EP);

                //队列增加
                QueueState QS = new QueueState();
                QS.Time = EP.Time;
                QS.QueueLength = QSList[QSList.Count - 1].QueueLength + 1;
                QSList.Add(QS);

                //EventPointer EP_F = new EventPointer();
                //EP_F.Type = "GateCheck_Wait";
                //EP_F.Truck = EP.Truck;
                //EP_F.TypeNum = 1;//非解锁事件
                ////最早解锁Gate时间=GateCheck_Wait开始时间
                //int MinUnlockTime = 240*3600;//大M值
                //foreach (GateCheckPlat Gate in StaticData.EnterGateCheckList)
                //{
                //    if (Gate.IsClosed == false)
                //    {
                //        if (Gate.BusyTimeEnd < MinUnlockTime)
                //        { MinUnlockTime = Gate.BusyTimeEnd; }
                //    }
                //}
                //EP_F.Time = MinUnlockTime;

                ////插入事件并排序
                //SortClass.InsertSortEP(StaticData.EventList, EP_F);

                //if (EP.Type == "GateCheck_Wait")//二次延后事件，队列不增加
                //{

                //}
                //if (EP.Type == "GateCheck")//初次延后事件，队列增加
                //{
                //    //队列增加
                //    QueueState QS = new QueueState();
                //    QS.Time = EP.Time;
                //    QS.QueueLength = QSList[QSList.Count - 1].QueueLength + 1;
                //    QSList.Add(QS);
                //}
            }
        }

        public void GateUnlock_P(EventPointer EP)
        {
            EP.Gate.IsBusy = false;
            EP.Gate.Truck = null;

            //添加GateClose 事件 & GateCheck_Wait事件，GateClose 优先于 GateCheck_Wait 
            /*未来事件表：添加GateClose事件*/
            if (GateClose_EPQueue.Count() != 0)
            {
                EventPointer EP_New = GateClose_EPQueue[0];
                EP_New.Time = EP.Gate.BusyTimeEnd;
                SortClass.InsertSortEP(StaticData.EventList, EP_New);

                GateClose_EPQueue.Remove(EP_New);
            }
            else
            {
                /*未来事件表：添加GateCheck事件*/
                if (GateCheck_EPQueue.Count() != 0)
                {
                    EventPointer EP_New = GateCheck_EPQueue[0];
                    EP_New.Time = EP.Gate.BusyTimeEnd;
                    EP_New.Type = "GateCheck_Wait";
                    SortClass.InsertSortEP(StaticData.EventList, EP_New);

                    GateCheck_EPQueue.Remove(EP_New);
                }
            }
        }

        public void GateCheck_NP(EventPointer EP,int S)//Non Pooled
        {
            //新到达货卡选择门区(排队中货卡不改变门区): 选择当前门区排队数量最小的且不是即将关闭的门区
            if (EP.Type == "GateCheck")
            {
                int GateIndex = 0;//最小等待队列门区的索引
                int TruckQueue_GateNum = 100000000;//默认队列长度初值
                for (int i = 0; i < StaticData.EnterGateCheckList.Count(); i++)
                {
                    GateCheckPlat Gate=StaticData.EnterGateCheckList[i];
                    if (Gate.IsAboutClose == false)//门区不关闭
                    {
                        if (Gate.GateCheck_EP_Queue.Count() < TruckQueue_GateNum)
                        {
                            TruckQueue_GateNum = Gate.GateCheck_EP_Queue.Count();
                            GateIndex = i;
                        }
                    }
                }
                EP.Truck.Gate = StaticData.EnterGateCheckList[GateIndex];
            }

            //首先判断预设定门区通道是否关闭,如关闭则更换门区通道
            if (EP.Truck.Gate.IsClosed == true)
            {
                int gate_index = 0;
                do
                {
                    Random ran = new Random();
                    gate_index = ran.Next(0, StaticData.EnterGateCheckNum);
                }
                while (StaticData.EnterGateCheckList[gate_index].IsClosed == true);
                EP.Truck.Gate = StaticData.EnterGateCheckList[gate_index];
            }
            //
            if (EP.Truck.Gate.IsBusy == false)//门区空闲
            {
                //产生服从爱尔朗分布的服务时间
                RandomGenerator RG = new RandomGenerator();
                //int servetime = Convert.ToInt32(RG.ErlangIndex(StaticData.Gate_k, StaticData.Gate_lam, EP.Time) * 60);//平均1/0.2=5分钟
                ////检查出现问题，服务时间增加一倍
                //if (EP.Truck.IsFailCheck == true)
                //{
                //    servetime *= 2;
                //}

                int servetime = ServeTimeCal(EP,S);//门区检查时间
                EP.Truck.GateCheckTime = servetime;//门区检查完毕时间

                int go_stop_time = 0;//起停附加时间
                if (EP.Type == "GateCheck_Wait")//若有等待过程，需附加起停时间
                {
                    go_stop_time = StaticData.Stop_Go_Time;//仿真时间
                }
                EP.Truck.Leave_GateEnter_Time = EP.Time + go_stop_time + servetime;//检查完毕时间
                EP.Truck.GateWaitTime = EP.Truck.Leave_GateEnter_Time - EP.Truck.OuccerTime - EP.Truck.GateCheckTime;//门区等待时间

                EP.Truck.Gate.Truck = EP.Truck;
                EP.Truck.Gate.IsBusy = true;
                EP.Truck.Gate.BusyTimeStart = EP.Time;
                EP.Truck.Gate.BusyTimeEnd = EP.Truck.Leave_GateEnter_Time;
                //门区占用状态记录
                GateState gs = new GateState();
                gs.BeginTime = EP.Truck.Gate.BusyTimeStart + go_stop_time;//占用时间不包括go-and-stop time
                gs.EndTime = EP.Truck.Gate.BusyTimeEnd;
                EP.Truck.Gate.GSList.Add(gs);

                //产生解锁事件
                EventPointer EP_F = new EventPointer();
                EP_F.Time = EP.Truck.Leave_GateEnter_Time;//解锁时间
                EP_F.Gate = EP.Truck.Gate;
                EP_F.Type = "Unlock";
                EP_F.TypeNum = 0;//解锁事件
                //插入事件并排序
                SortClass.InsertSortEP(StaticData.EventList, EP_F);

                //队列减少
                if (EP.Type == "GateCheck_Wait")
                {
                    QueueState QS = new QueueState();
                    QS.Time = EP.Time;
                    int QSListCount = EP.Truck.Gate.QSList.Count();
                    QS.QueueLength = EP.Truck.Gate.QSList[QSListCount - 1].QueueLength - 1;
                    EP.Truck.Gate.QSList.Add(QS);
                }
            }
            else
            {
                EP.Truck.Gate.GateCheck_EP_Queue.Add(EP);//检查事件加入对应门区的排队队列
                //队列增加
                QueueState QS = new QueueState();
                QS.Time = EP.Time;
                int QSListCount = EP.Truck.Gate.QSList.Count();
                QS.QueueLength = EP.Truck.Gate.QSList[QSListCount - 1].QueueLength + 1;
                EP.Truck.Gate.QSList.Add(QS);

                if (EP.Type == "GateCheck_Wait")
                {
                    int test = 0;
                }
            }
        }

        public void GateUnlock_NP(EventPointer EP)
        {
            EP.Gate.IsBusy = false;
            EP.Gate.Truck = null;

            //添加GateCheck_Wait事件 & GateClose 事件， GateCheck_Wait 优先于 GateClose
            /*未来事件表：添加GateCheck_Wait事件*/
            if (EP.Gate.GateCheck_EP_Queue.Count() != 0)
            {
                EventPointer EP_New = EP.Gate.GateCheck_EP_Queue[0];
                EP_New.Time = EP.Gate.BusyTimeEnd;
                EP_New.Type = "GateCheck_Wait";
                SortClass.InsertSortEP(StaticData.EventList, EP_New);

                EP.Gate.GateCheck_EP_Queue.Remove(EP_New);
            }
            else
            {
                /*未来事件表：添加GateClose事件*/
                if (GateClose_EPQueue.Count() != 0)
                {
                    EventPointer EP_New = GateClose_EPQueue[0];
                    EP_New.Time = EP.Gate.BusyTimeEnd;
                    SortClass.InsertSortEP(StaticData.EventList, EP_New);

                    GateClose_EPQueue.Remove(EP_New);
                }
            }
        }

        public void GateClose(EventPointer EP)
        {
            GateCheckPlat Gate = EP.Gate;
            if (Gate.IsBusy == false && Gate.GateCheck_EP_Queue.Count()==0)//当前作业及排队车辆，可以关闭
            {
                Gate.IsClosed = true;
                Gate.IsAboutClose = true;
                Gate.Truck = null;
                //门区开放区起点间赋值
                Gate.OpenPeriod_EndTime = EP.Time;
            }
            else//当前无法关闭
            {
                Gate.IsAboutClose = true;//标志：该门区即将被关闭，新到达货卡不再接入
                //门区关闭时间加入排队队列
                GateClose_EPQueue.Add(EP);
            }
        }
        //门区通道开放
        public void GateOpen(EventPointer EP)
        {
            EP.Gate.IsClosed = false;
            EP.Gate.IsAboutClose = false;
            EP.Gate.Truck = null;
            //门区开放区起点间赋值
            EP.Gate.OpenPeriod_StartTime = EP.Time;
            //门区关闭时间赋默认值，默认为最终时间
            EP.Gate.OpenPeriod_EndTime = StaticData.GateTimeWindowEnd * 3600;
        }

        #endregion

        #region 辅助方法
        //计算大门服务时间
        public int ServeTimeCal(EventPointer EP,int S)
        {
            Trucks tk = EP.Truck;
            int servetime = 0;
            RandomGenerator RG = new RandomGenerator();
            int RandomSeed = EP.Time + S;

            if (tk.IsFailCheck == false)//正常
            {
                servetime = Convert.ToInt32(RG.ErlangIndex(tk.ShapeK, tk.Lambda, RandomSeed) * 60);//mean=5*1/1.103=4.533
            }
            else//trouble transaction  
            {
                servetime = 2 * Convert.ToInt32(RG.ErlangIndex(tk.ShapeK, tk.Lambda, RandomSeed) * 60);//mean=5*1/1.103=4.533
            }
            return servetime;
        }

        //获得一定置信水平范围的List
        public void ListConSelect(List<double> RList, double rate)//rate:置信水平
        {
            int DeleteNum = RList.Count - Convert.ToInt32(RList.Count * rate);
            for (int i = 0; i < DeleteNum; i++)
            {
                double value = RList[0];
                if (i % 2 == 0)//先删除一个最大的
                {
                    for (int j = 0; j < RList.Count; j++)
                    {
                        if (value <= RList[j])
                        {
                            value = RList[j];
                        }
                    }
                }
                else//再删除一个最小的
                {
                    for (int j = 0; j < RList.Count; j++)
                    {
                        if (value >= RList[j])
                        {
                            value = RList[j];
                        }
                    }
                }
                //删除该元素
                RList.Remove(value);
            }
        }
        //求和
        public double SumList(List<double> RList)
        {
            double sumv = 0;
            foreach (double v in RList)
            {
                sumv += v;
            }
            return sumv;
        }
        //求平均值
        public double MeanList(List<double> RList)
        {
            double sumv = 0;
            foreach (double v in RList)
            {
                sumv += v;
            }
            double mean = sumv / (double)RList.Count;
            return mean;
        }
        //求标准差
        public double StList(List<double> RList)
        {
            double mean = MeanList(RList);
            double sumva = 0;
            foreach (double v in RList)
            {
                sumva += (v - mean) * (v - mean);
            }
            double st = Math.Sqrt(sumva / (double)RList.Count);
            return st;
        }
        //求最大值
        public double MaxList(List<double> RList)
        {
            double maxv = 0;
            foreach (double v in RList)
            {
                if (maxv <= v)
                    maxv = v;
            }
            return maxv;
        }
        //求最小值
        public double MinList(List<double> RList)
        {
            double minv = RList[0];
            foreach (double v in RList)
            {
                if (minv >= v)
                    minv = v;
            }
            return minv;
        }
        //升序 排序
        public List<double> SortQueue(List<double> RList)
        {
            List<double> TemRList=new List<double>();
            do
            {
                double d = RList[0];
                for (int i = 1; i < RList.Count(); i++)
                {
                    if (d > RList[i])
                    {
                        d = RList[i];
                    }
                }
                RList.Remove(d);
                TemRList.Add(d);
            }
            while (RList.Count() != 0);
            return TemRList;
        }
        //保存DT到Excel
        protected void ExportExcel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return;
            }
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                switch (dt.Columns[i].ColumnName)
                {
                    case "NAME":
                        worksheet.Cells[1, i + 1] = "姓名";
                        range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                        range.Interior.ColorIndex = 15;
                        range.Font.Bold = true;
                        break;
                    case "REMARK":
                        worksheet.Cells[1, i + 1] = "备注";
                        range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                        range.Interior.ColorIndex = 15;
                        range.Font.Bold = true;
                        break;
                }
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            xlApp.Visible = true;
        }
        
        #endregion
    }

    //队列状态类
    public class QueueState
    {
        public int Time;//队列变化时刻
        public int EndTime;//该队列状态的结束时刻
        public int QueueLength;//队列长度
    }
}
