﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace CCCPMatrixCounterApi
{
    public class SerialWriter
    {
        private List<SerialPort> SerialPorts;

        public SerialWriter()
        {
            SetSerialPorts(9600);
        }

        public void Write(string text)
        {
            try
            {
                WriteToAllPorts(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw e;
            }
        }

        private void WriteToAllPorts(string text)
        {
            foreach(var port in this.SerialPorts)
            {
                port.Open();

                port.WriteLine(text);
                port.Close();
            }
        }

        private void SetSerialPorts(int baudRate)
        {
            Console.WriteLine("Creating serial ports");
            this.SerialPorts = new List<SerialPort>();
            
            var availablePorts = SerialPort.GetPortNames().Distinct();
            foreach (var port in availablePorts)
            {
                Console.WriteLine(port);
                this.SerialPorts.Add(new SerialPort(port, baudRate));
            }
        }
    }
}
