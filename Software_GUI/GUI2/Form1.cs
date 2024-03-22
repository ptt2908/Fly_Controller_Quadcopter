using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace GUI2
{
    public partial class Form1 : Form
    {
        float Kp_AngleRoll = 0, Ki_AngleRoll = 0, Kd_AngleRoll = 0,
              Kp_AnglePitch = 0, Ki_AnglePitch = 0, Kd_AnglePitch = 0,

              Kp_RateRoll = 0, Ki_RateRoll = 0, Kd_RateRoll = 0,
              Kp_RatePitch = 0, Ki_RatePitch = 0, Kd_RatePitch = 0,
              Kp_RateYaw = 0, Ki_RateYaw = 0, Kd_RateYaw = 0;

        float float_AngleRoll = 0, float_AnglePitch = 0, float_AngleRoll_SetPoint = 0, float_AnglePitch_SetPoint = 0;
        int status = 0;
        int time = 0;
        int flag_time = 0;
        string rcv_data;

        private void Data_Listview(float time, float datas)
        {
            if (status == 0)
                return;
            else
            {
                ListViewItem item = new ListViewItem(time.ToString()); // Gán biến realtime vào cột đầu tiên của ListView
                item.SubItems.Add(datas.ToString());
                listView1.Items.Add(item); // Gán biến datas vào cột tiếp theo của ListView
                listView1.Items[listView1.Items.Count - 1].EnsureVisible(); // Hiện thị dòng được gán gần nhất ở ListView, tức là mình cuộn ListView theo dữ liệu gần nhất đó
            }
        }
        private void txt_Ki_RateRoll_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kd_RateRoll_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kp_RatePitch_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Ki_RatePitch_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kd_RatePitch_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kp_RateYaw_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void btnplot_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) serialPort1.Open();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);


            byte[] byteHeader = { 0xCC, 0xDD };
            byte[] byteData = { 0x01 };
            byte[] combinedArray = byteHeader.Concat(byteData).ToArray();

            serialPort1.Write(combinedArray, 0, combinedArray.Length);
        }

        private void btnload_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) serialPort1.Open();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);


            if (string.IsNullOrEmpty(txt_Kp_AngleRoll.Text)) txt_Kp_AngleRoll.Text = "0";
            if (string.IsNullOrEmpty(txt_Ki_AngleRoll.Text)) txt_Ki_AngleRoll.Text = "0";
            if (string.IsNullOrEmpty(txt_Kd_AngleRoll.Text)) txt_Kd_AngleRoll.Text = "0";

            if (string.IsNullOrEmpty(txt_Kp_AnglePitch.Text)) txt_Kp_AnglePitch.Text = "0";
            if (string.IsNullOrEmpty(txt_Ki_AnglePitch.Text)) txt_Ki_AnglePitch.Text = "0";
            if (string.IsNullOrEmpty(txt_Kd_AnglePitch.Text)) txt_Kd_AnglePitch.Text = "0";

            if (string.IsNullOrEmpty(txt_Kp_RateRoll.Text)) txt_Kp_RateRoll.Text = "0";
            if (string.IsNullOrEmpty(txt_Ki_RateRoll.Text)) txt_Ki_RateRoll.Text = "0";
            if (string.IsNullOrEmpty(txt_Kd_RateRoll.Text)) txt_Kd_RateRoll.Text = "0";

            if (string.IsNullOrEmpty(txt_Kp_RatePitch.Text)) txt_Kp_RatePitch.Text = "0";
            if (string.IsNullOrEmpty(txt_Ki_RatePitch.Text)) txt_Ki_RatePitch.Text = "0";
            if (string.IsNullOrEmpty(txt_Kd_RatePitch.Text)) txt_Kd_RatePitch.Text = "0";

            if (string.IsNullOrEmpty(txt_Kp_RateYaw.Text)) txt_Kp_RateYaw.Text = "0";
            if (string.IsNullOrEmpty(txt_Ki_RateYaw.Text)) txt_Ki_RateYaw.Text = "0";
            if (string.IsNullOrEmpty(txt_Kd_RateYaw.Text)) txt_Kd_RateYaw.Text = "0";


            byte[] byteHeader = { 0xAA, 0xFF };
            byte[] byteLength = { 0x3F };

            Kp_AngleRoll = float.Parse(txt_Kp_AngleRoll.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kp_AngleRoll = BitConverter.GetBytes(Kp_AngleRoll);
            Ki_AngleRoll = float.Parse(txt_Ki_AngleRoll.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Ki_AngleRoll = BitConverter.GetBytes(Ki_AngleRoll);
            Kd_AngleRoll = float.Parse(txt_Kd_AngleRoll.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kd_AngleRoll = BitConverter.GetBytes(Kd_AngleRoll);

            Kp_AnglePitch = float.Parse(txt_Kp_AnglePitch.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kp_AnglePitch = BitConverter.GetBytes(Kp_AnglePitch);
            Ki_AnglePitch = float.Parse(txt_Ki_AnglePitch.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Ki_AnglePitch = BitConverter.GetBytes(Ki_AnglePitch);
            Kd_AnglePitch = float.Parse(txt_Kd_AnglePitch.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kd_AnglePitch = BitConverter.GetBytes(Kd_AnglePitch);

            Kp_RateRoll = float.Parse(txt_Kp_RateRoll.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kp_RateRoll = BitConverter.GetBytes(Kp_RateRoll);
            Ki_RateRoll = float.Parse(txt_Ki_RateRoll.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Ki_RateRoll = BitConverter.GetBytes(Ki_RateRoll);
            Kd_RateRoll = float.Parse(txt_Kd_RateRoll.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kd_RateRoll = BitConverter.GetBytes(Kd_RateRoll);

            Kp_RatePitch = float.Parse(txt_Kp_RatePitch.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kp_RatePitch = BitConverter.GetBytes(Kp_RatePitch);
            Ki_RatePitch = float.Parse(txt_Ki_RatePitch.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Ki_RatePitch = BitConverter.GetBytes(Ki_RatePitch);
            Kd_RatePitch = float.Parse(txt_Kd_RatePitch.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kd_RatePitch = BitConverter.GetBytes(Kd_RatePitch);

            Kp_RateYaw = float.Parse(txt_Kp_RateYaw.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kp_RateYaw = BitConverter.GetBytes(Kp_RateYaw);
            Ki_RateYaw = float.Parse(txt_Ki_RateYaw.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Ki_RateYaw = BitConverter.GetBytes(Ki_RateYaw);
            Kd_RateYaw = float.Parse(txt_Kd_RateYaw.Text, CultureInfo.InvariantCulture);
            byte[] byteArray_Kd_RateYaw = BitConverter.GetBytes(Kd_RateYaw);

            byte[] combinedArray = byteHeader.Concat(byteLength)
                                   .Concat(byteArray_Kp_AngleRoll).Concat(byteArray_Ki_AngleRoll).Concat(byteArray_Kd_AngleRoll)
                                   .Concat(byteArray_Kp_AnglePitch).Concat(byteArray_Ki_AnglePitch).Concat(byteArray_Kd_AnglePitch)
                                   .Concat(byteArray_Kp_RateRoll).Concat(byteArray_Ki_RateRoll).Concat(byteArray_Kd_RateRoll)
                                   .Concat(byteArray_Kp_RatePitch).Concat(byteArray_Ki_RatePitch).Concat(byteArray_Kd_RatePitch)
                                   .Concat(byteArray_Kp_RateYaw).Concat(byteArray_Ki_RateYaw).Concat(byteArray_Kd_RateYaw)
                                   .ToArray();
            Thread.Sleep(50);
            serialPort1.Write(combinedArray, 0, combinedArray.Length);
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) serialPort1.Open();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);


            byte[] byteHeader = { 0xCC, 0xDD };
            byte[] byteData = { 0x00 };
            byte[] combinedArray = byteHeader.Concat(byteData).ToArray();

            serialPort1.Write(combinedArray, 0, combinedArray.Length);
        }

        private void txt_Ki_RateYaw_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kd_RateYaw_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kp_RateRoll_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kd_AnglePitch_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Ki_AnglePitch_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kp_AnglePitch_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kd_AngleRoll_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag_time == 1)
            {
                time += 125;
                //Draw();
                //zedGraphControl1.AxisChange();
                //zedGraphControl1.Invalidate();
                //zedGraphControl1.Refresh();
            }
            else time = 0;
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            time = 0;
            ClearZedGraph();//Xóa đường trong đồ thị
            listView1.Items.Clear(); // Xóa listview
        }

        private void txt_Ki_AngleRoll_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private void txt_Kp_AngleRoll_TextChanged(object sender, EventArgs e)
        {
            label_load.Text = "NOT LOADED";
        }

        private const int FrameLength = 20;
        private byte[] receivedDataBuffer = new byte[FrameLength];
        private int receivedDataIndex = 0;

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytesRead = serialPort1.BytesToRead;
            byte[] buffer = new byte[bytesRead];
            serialPort1.Read(buffer, 0, bytesRead);


            foreach (byte b in buffer)
            {
                if (receivedDataIndex < receivedDataBuffer.Length)
                {
                    receivedDataBuffer[receivedDataIndex] = b;
                    receivedDataIndex++;

                    if ((receivedDataIndex == FrameLength))
                    {
                        if (receivedDataBuffer[0] == 0x44)
                        {
                            // Nếu đã nhận đủ số byte cần thiết, xử lý dữ liệu và chuẩn bị nhận tiếp
                            float_AngleRoll = BitConverter.ToSingle(receivedDataBuffer, 2);
                            float_AnglePitch = BitConverter.ToSingle(receivedDataBuffer, 6);
                            float_AngleRoll_SetPoint = BitConverter.ToSingle(receivedDataBuffer, 10);
                            float_AnglePitch_SetPoint = BitConverter.ToSingle(receivedDataBuffer, 14);
                            this.BeginInvoke(new Action<float, float>(AddFloatToListView), float_AngleRoll, float_AnglePitch);
                        }
                        else if (receivedDataBuffer[0] == 0x42)
                        {
                            this.Invoke(new Action(() => CheckAndUpdateLoad(receivedDataBuffer[0])));
                        }
                        else if (receivedDataBuffer[0] == 0x50)
                        {
                            this.Invoke(new Action(() => CheckAndUpdateStart(receivedDataBuffer[0])));
                        }
                        else if (receivedDataBuffer[0] == 0x53)
                        {
                            this.Invoke(new Action(() => CheckAndUpdateStop(receivedDataBuffer[0])));
                        }
                        // Reset index và chuẩn bị nhận dữ liệu mới
                        receivedDataIndex = 0;
                    }
                }
            }
        }

        private void AddFloatToListView(float value1, float value2)
        {
            flag_time = 1;
            // Hiển thị số float trong ListView và xuống dòng mỗi khi nhận được dữ liệu mới
            ListViewItem item = new ListViewItem(value1.ToString());
            item.SubItems.Add(value2.ToString());
            listView1.Items.Add(item);
            listView1.Items[listView1.Items.Count - 1].EnsureVisible();
            Draw();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();

            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
            zedGraphControl2.Refresh();
        }

        private void CheckAndUpdateLoad(byte value)
        {
            if (value == 0x42)
            {
                // Cập nhật UI
                label_load.Text = "LOADED";
            }
        }
        private void CheckAndUpdateStart(byte value)
        {
            if (value == 0x50)
            {
                // Cập nhật UI
                label_Start.Text = "STARTED";
                timer1.Start();
            }
        }
        private void CheckAndUpdateStop(byte value)
        {
            if (value == 0x53)
            {
                // Cập nhật UI
                flag_time = 0;
                label_Start.Text = "STOPPED";
                timer1.Stop();
            }
        }
        private void btndisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                serialPort1.Close();
                lb_connect.Text = "Disconnected";
                btnconnect.Enabled = true;
                btndisconnect.Enabled = false;
                btnload.Enabled = false;
                btnplot.Enabled = false;
                btnstop.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = selectCOM.Text;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                //serialPort1.Open();
                progressBar1.Value = 100;
                lb_connect.Text = "Connected";
                btnconnect.Enabled = false;
                btndisconnect.Enabled = true;
                btnload.Enabled = true;
                btnplot.Enabled = true;
                btnstop.Enabled = true;
                btnclear.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphPane char_Roll = zedGraphControl1.GraphPane;
            char_Roll.Title.Text = "AngleRoll";
            char_Roll.XAxis.Title.Text = "Time(ms)";
            char_Roll.YAxis.Title.Text = "degree";
            RollingPointPairList list1 = new RollingPointPairList(60000);
            RollingPointPairList list2 = new RollingPointPairList(60000);
            LineItem duongline1 = char_Roll.AddCurve("AngleRoll", list1, Color.Blue, SymbolType.None);
            LineItem duongline2 = char_Roll.AddCurve("AngleRoll_SetPoint", list2, Color.Red, SymbolType.None);

            char_Roll.XAxis.Scale.Min = 0;
            char_Roll.XAxis.Scale.Max = 20000;
            char_Roll.XAxis.Scale.MinorStep = 2000;
            char_Roll.XAxis.Scale.MajorStep = 2000;

            char_Roll.YAxis.Scale.Min = -55;
            char_Roll.YAxis.Scale.Max = 55;
            char_Roll.YAxis.Scale.MinorStep = 5;
            char_Roll.YAxis.Scale.MajorStep = 5;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            GraphPane char_Pitch = zedGraphControl2.GraphPane;
            char_Pitch.Title.Text = "AnglePitch";
            char_Pitch.XAxis.Title.Text = "Time(ms)";
            char_Pitch.YAxis.Title.Text = "degree";
            RollingPointPairList list3 = new RollingPointPairList(60000);
            RollingPointPairList list4 = new RollingPointPairList(60000);
            LineItem duongline3 = char_Pitch.AddCurve("AnglePitch", list3, Color.Blue, SymbolType.None);
            LineItem duongline4 = char_Pitch.AddCurve("AnglePitch_SetPoint", list4, Color.Red, SymbolType.None);

            char_Pitch.XAxis.Scale.Min = 0;
            char_Pitch.XAxis.Scale.Max = 20000;
            char_Pitch.XAxis.Scale.MinorStep = 2000;
            char_Pitch.XAxis.Scale.MajorStep = 2000;

            char_Pitch.YAxis.Scale.Min = -55;
            char_Pitch.YAxis.Scale.Max = 55;
            char_Pitch.YAxis.Scale.MinorStep = 5;
            char_Pitch.YAxis.Scale.MajorStep = 5;

            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();


            string[] myport = SerialPort.GetPortNames();
            selectCOM.Items.AddRange(myport);
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = System.IO.Ports.Parity.None;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = System.IO.Ports.StopBits.One;

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        private void Draw()
        {
            if (zedGraphControl1.GraphPane.CurveList.Count <= 0) return;

            LineItem duongline1 = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            LineItem duongline2 = zedGraphControl1.GraphPane.CurveList[1] as LineItem;
            if (duongline1 == null)
                return;
            if (duongline2 == null)
                return;
            IPointListEdit list1 = duongline1.Points as IPointListEdit;
            IPointListEdit list2 = duongline2.Points as IPointListEdit;
            if (list1 == null)
                return;
            if (list2 == null)
                return;
            list1.Add(time, float_AngleRoll); // Thêm điểm trên đồ thị
            list2.Add(time, float_AngleRoll_SetPoint);
            Scale xScale1 = zedGraphControl1.GraphPane.XAxis.Scale;
            Scale yScale1 = zedGraphControl1.GraphPane.YAxis.Scale;
            // Tự động Scale theo trục x
            if (time > xScale1.Max /*- xScale.MajorStep*/)
            {
                xScale1.Max = time + xScale1.MajorStep;
                xScale1.Min = xScale1.Min + xScale1.MajorStep;
            }

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
            //---------------------------------------------------------------------
            if (zedGraphControl2.GraphPane.CurveList.Count <= 0) return;

            LineItem duongline3 = zedGraphControl2.GraphPane.CurveList[0] as LineItem;
            LineItem duongline4 = zedGraphControl2.GraphPane.CurveList[1] as LineItem;
            if (duongline3 == null)
                return;
            if (duongline4 == null)
                return;
            IPointListEdit list3 = duongline3.Points as IPointListEdit;
            IPointListEdit list4 = duongline4.Points as IPointListEdit;
            if (list3 == null)
                return;
            if (list4 == null)
                return;
            list3.Add(time, float_AnglePitch); // Thêm điểm trên đồ thị          
            list4.Add(time, float_AnglePitch_SetPoint);
            Scale xScale2 = zedGraphControl2.GraphPane.XAxis.Scale;
            Scale yScale2 = zedGraphControl2.GraphPane.YAxis.Scale;
            // Tự động Scale theo trục x
            if (time > xScale2.Max /*- xScale.MajorStep*/)
            {
                xScale2.Max = time + xScale2.MajorStep;
                xScale2.Min = xScale2.Min + xScale2.MajorStep;
            }

            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
            zedGraphControl2.Refresh();

        }

        private void ClearZedGraph()
        {
            zedGraphControl1.GraphPane.CurveList.Clear(); // Xóa đường
            zedGraphControl1.GraphPane.GraphObjList.Clear(); // Xóa đối tượng
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            GraphPane char_Roll = zedGraphControl1.GraphPane;
            char_Roll.Title.Text = "AngleRoll";
            char_Roll.XAxis.Title.Text = "Time(ms)";
            char_Roll.YAxis.Title.Text = "degree";
            RollingPointPairList list1 = new RollingPointPairList(60000);
            RollingPointPairList list2 = new RollingPointPairList(60000);
            LineItem duongline1 = char_Roll.AddCurve("AngleRoll", list1, Color.Blue, SymbolType.None);
            LineItem duongline2 = char_Roll.AddCurve("AngleRoll_SetPoint", list2, Color.Red, SymbolType.None);

            char_Roll.XAxis.Scale.Min = 0;
            char_Roll.XAxis.Scale.Max = 20000;
            char_Roll.XAxis.Scale.MinorStep = 2000;
            char_Roll.XAxis.Scale.MajorStep = 2000;

            char_Roll.YAxis.Scale.Min = -55;
            char_Roll.YAxis.Scale.Max = 55;
            char_Roll.YAxis.Scale.MinorStep = 5;
            char_Roll.YAxis.Scale.MajorStep = 5;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
            //-----------------------------------------------
            zedGraphControl2.GraphPane.CurveList.Clear(); // Xóa đường
            zedGraphControl2.GraphPane.GraphObjList.Clear(); // Xóa đối tượng
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
            GraphPane char_Pitch = zedGraphControl2.GraphPane;
            char_Pitch.Title.Text = "AnglePitch";
            char_Pitch.XAxis.Title.Text = "Time(ms)";
            char_Pitch.YAxis.Title.Text = "degree";
            RollingPointPairList list3 = new RollingPointPairList(60000);
            RollingPointPairList list4 = new RollingPointPairList(60000);
            LineItem duongline3 = char_Pitch.AddCurve("AnglePitch", list3, Color.Blue, SymbolType.None);
            LineItem duongline4 = char_Pitch.AddCurve("AnglePitch_SetPoint", list4, Color.Red, SymbolType.None);

            char_Pitch.XAxis.Scale.Min = 0;
            char_Pitch.XAxis.Scale.Max = 20000;
            char_Pitch.XAxis.Scale.MinorStep = 2000;
            char_Pitch.XAxis.Scale.MajorStep = 2000;

            char_Pitch.YAxis.Scale.Min = -55;
            char_Pitch.YAxis.Scale.Max = 55;
            char_Pitch.YAxis.Scale.MinorStep = 5;
            char_Pitch.YAxis.Scale.MajorStep = 5;

            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
            zedGraphControl2.Refresh();
        }
    }
}
