using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoMusic
{
    public partial class Form1 : Form
    {
        byte[] HexBytes;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (HexBytes != null)
            {
                serialPort1.Write(HexBytes, 0, HexBytes.Length);
            }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                string msg = serialPort1.ReadExisting();
                MessageBox.Show(msg);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            string[] baud = { "4800", "9600", "19200", "38400", "57600", "115200", "230400" , "250000" };
            cmbPorts.Items.AddRange(ports);
            cmbBaud.Items.AddRange(baud);
            cmbPorts.SelectedIndex = 0;
            cmbBaud.SelectedIndex = 0;
            btnSend.Enabled = false;

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            try
            {
                serialPort1.PortName = cmbPorts.Text;
                serialPort1.BaudRate = Convert.ToInt32(cmbBaud.Text);
                serialPort1.Open();
                btnSend.Enabled = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "d:\\";
            openFileDialog1.Filter = "Wav files (*.wav)|*.wav|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string directory = openFileDialog1.FileName;
                    HexBytes = File.ReadAllBytes(directory);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
        }
    }
}
