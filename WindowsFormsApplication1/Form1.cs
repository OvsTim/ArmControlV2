using System.IO.Ports;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback(string text);
        public Form1()
        {
            InitializeComponent();
            port.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
            port.Open();

        }    
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SetText(port.ReadLine());
        }   
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            port.Close();
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }
    }
}
