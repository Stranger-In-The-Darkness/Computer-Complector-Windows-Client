using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Model.Models.Data.Components;

namespace Model
{
	[Serializable]
	[XmlRoot]
	public class Selected
	{
		public Body SelectedBody { get; set; }
		public Cooler SelectedCooler { get; set; }
		public Charger SelectedCharger { get; set; }
		public CPU SelectedCPU { get; set; }
		public HDD SelectedHDD { get; set; }
		public Motherboard SelectedMotherboard { get; set; }
		public RAM SelectedRAM { get; set; }
		public SSD SelectedSSD { get; set; }
		public Videocard SelectedVideocard { get; set; }
	}
}
