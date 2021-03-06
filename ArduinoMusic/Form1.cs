﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoMusic
{
    public partial class Form1 : Form
    {
        byte[] HexBytes;
        string[] ports;
        public delegate void AddDataDelegate(string serialinput);
        public AddDataDelegate SerialDelegate;
        SerialClass MySerialPort = new SerialClass();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SerialDelegate = new AddDataDelegate(AddDataMethod);
            ports = SerialPort.GetPortNames();
            cmbPorts.Items.AddRange(ports);
            if (ports.Length != 0)
            {
                cmbPorts.SelectedIndex = 0;
                string[] baud = { "4800", "9600", "19200", "38400", "57600", "115200", "230400", "250000" };
                cmbBaud.Items.AddRange(baud);
                cmbBaud.SelectedIndex = 1;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            }
            btnSend.Enabled = false;
            btnClose.Enabled = false;

        }

        public void AddDataMethod(string serialinput)
        {
            txtboxReceive.AppendText(serialinput);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (HexBytes != null)
            {
                //serialPort1.Write(HexBytes, 0, HexBytes.Length);
                Thread WriteThread = new Thread(() => MySerialPort.SerialWrite(HexBytes));
                WriteThread.Start();
                //oSerialClass.SerialWrite(HexBytes);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (ports.Length != 0)
                {
                    btnOpen.Enabled = false;
                    MySerialPort.InitializeSerialPort(cmbPorts.Text, Convert.ToInt32(cmbBaud.Text));
                    /*
                    serialPort1.PortName = cmbPorts.Text;
                    serialPort1.BaudRate = Convert.ToInt32(cmbBaud.Text);
                    serialPort1.Open();
                    */
                    btnSend.Enabled = true;
                    btnClose.Enabled = true;
                }
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
            if (MySerialPort.PortStatus() == true)
            {
                MySerialPort.ClosePort();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MySerialPort.ClosePort();
            btnClose.Enabled = false;
            btnSend.Enabled = false;
            btnOpen.Enabled = true;
        }
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string s = sp.ReadExisting();
            txtboxReceive.Invoke(SerialDelegate, new Object[] { s });
        }
    }

    public class SerialClass
    {
        SerialPort ComPort = new SerialPort();
        //Form1 MainForm = new Form1();
        public void beta()
        {
            MessageBox.Show("Hello");
        }

        public void InitializeSerialPort(string portname, int baudrate)
        {
            ComPort.PortName = portname;
            ComPort.BaudRate = baudrate;
            ComPort.Open();
        }
        public bool PortStatus()
        {
            return ComPort.IsOpen;
        }
        public void ClosePort()
        {
            ComPort.Close();
        }
        public void SerialWrite(byte[] Audio)
        {
            ComPort.Write(Audio, 0, Audio.Length);
        }
    }
}
