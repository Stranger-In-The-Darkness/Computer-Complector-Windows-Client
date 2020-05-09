using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class CPU
    {
        public CPU()
        {
            ID = 0;
            Title = null;
            Company = null;
            Series = null;
            Socket = null;
            Frequency = 0;
            CoresAmount = 0;
            ThreadsAmount = 0;
            IntegratedGraphics = false;
            Core = null;
            DeliveryType = null;
            Overcloacking = false;
			Compatible = true;
			Incompatible = new Dictionary<string, string>();
		}

        public CPU(int iD, string title, string company, string series, string socket, double frequency, int coresAmount, int threadsAmount, bool integratedGraphics, string core, string deliveryType, bool overcloacking)
        {
            ID = iD;
            Title = title;
            Company = company;
            Series = series;
            Socket = socket;
            Frequency = frequency;
            CoresAmount = coresAmount;
            ThreadsAmount = threadsAmount;
            IntegratedGraphics = integratedGraphics;
            Core = core;
            DeliveryType = deliveryType;
            Overcloacking = overcloacking;
        }

        public int							ID                  { get; set; }
        public string						Title               { get; set; }
        public string						Company             { get; set; }
        public string						Series              { get; set; }
        public string						Socket              { get; set; }
        public double						Frequency           { get; set; }
        public int							CoresAmount         { get; set; }
        public int							ThreadsAmount       { get; set; }
        public bool							IntegratedGraphics  { get; set; }
        public string						Core                { get; set; }
        public string						DeliveryType        { get; set; }
        public bool							Overcloacking       { get; set; }
		public bool							Compatible			{ get; set; }
		public Dictionary<string, string>	Incompatible		{ get; set; }
	}
}
