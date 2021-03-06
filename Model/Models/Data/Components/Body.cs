﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Data.Components
{
    [Serializable]
    public class Body
    {
        public Body()
        {
            ID = 0;
            Title = string.Empty;
            Company = string.Empty;
            Formfactor = new List<string>();
            Type = string.Empty;
            BuildInCharger = false;
            ChargerPower = 0;
            Color = string.Empty;
            USB2Ports = 0;
            USB3Ports = 0;
            Additions = string.Empty;
            BackLightColor = string.Empty;
			Compatible = true;
			Incompatible = new Dictionary<string, string>();
        }

        public Body(int iD, string title, string company, IEnumerable<string> formfactor, string type, bool buildInCharger, int chargerPower, string color, int uSB2Ports, int uSB3Ports, string additions, string backLightColor)
        {
            ID = iD;
            Title = title;
            Company = company;
            Formfactor = formfactor.ToList();
            Type = type;
            BuildInCharger = buildInCharger;
            ChargerPower = chargerPower;
            Color = color;
            USB2Ports = uSB2Ports;
            USB3Ports = uSB3Ports;
            Additions = additions;
            BackLightColor = backLightColor;
        }

        public int							ID                  { get; set; }
        public string						Title               { get; set; }
        public string						Company             { get; set; }
        public List<string>					Formfactor          { get; set; }
        public string						Type                { get; set; }
        public bool							BuildInCharger      { get; set; }
        public int							ChargerPower        { get; set; }
        public string						Color               { get; set; }
		public int							USB2Ports           { get; set; }
        public int							USB3Ports           { get; set; }
        public string						Additions           { get; set; }
        public string						BackLightColor      { get; set; }
		public bool							Compatible			{ get; set; }
		public Dictionary<string, string>	Incompatible		{ get; set; }
	}
}
