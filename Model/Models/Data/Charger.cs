using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Charger
    {
        public Charger()
        {
            ID = 0;
            Title = null;
            Company = null;
            Series = null;
            Power = 0;
            Sertificate = null;
            VideoConnectorsAmount = 0;
            ConnectorType = null;
            SATAAmount = 0;
            IDEAmount = 0;
            MotherboardConnector = null;
            Addition = null;
        }

        public Charger(int iD, string title, string company, string series, int power, string sertificate, int videoConnectorsAmount, string connectorType, int sATAAmount, int iDEAmount, string motherboardConnector, string addition)
        {
            ID = iD;
            Title = title;
            Company = company;
            Series = series;
            Power = power;
            Sertificate = sertificate;
            VideoConnectorsAmount = videoConnectorsAmount;
            ConnectorType = connectorType;
            SATAAmount = sATAAmount;
            IDEAmount = iDEAmount;
            MotherboardConnector = motherboardConnector;
            Addition = addition;
        }

        public int      ID                      { get; set; }
        public string   Title                   { get; set; }
        public string   Company                 { get; set; }
        public string   Series                  { get; set; }
        public int      Power                   { get; set; }
        public string   Sertificate             { get; set; }
        public int      VideoConnectorsAmount   { get; set; }
        public string   ConnectorType           { get; set; }
	    public int      SATAAmount              { get; set; }
        public int      IDEAmount               { get; set; }
        public string   MotherboardConnector    { get; set; }
	    public string   Addition                { get; set; }
    }
}
