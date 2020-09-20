using System;
using NUnit.Framework;
using ComputerComplectorWebAPI;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Add;
using ComputerComplectorWebAPI.Models.Exceptions;

namespace ComputerComplectorWebAPITests.Models.Requests.Add
{
    public class AddChargerRequestUnitTests
    {
		[Test]
		[TestCase("", "", -1, "", -1, -1, "", "", "", "", -1)]
		public void AddChargerRequestUnitTests_AddChargerRequest_IncorrectParameters_ExpectedValidationException(
			string company, string connectionType, int ideAmount, string motherboardConnector, int power, int sataAmount,
			string series, string sertificate, string title, string videocardConnector, int videoConnectorsAmount)
		{
			Charger charger = new Charger()
			{
				Company = company,
				ConnectorType = connectionType,
				IDEAmount = ideAmount,
				MotherboardConnector = motherboardConnector,
				Power = power,
				SATAAmount = sataAmount,
				Series = series,
				Sertificate80Plus = sertificate,
				Title = title,
				VideocardConnector = videocardConnector,
				VideoConnectorsAmount = videoConnectorsAmount
			};

			Assert.Throws(typeof(ValidationException), () => { new AddChargerRequest(charger); });
		}

		[Test]
		[TestCase("c", "c", 1, "c", 1, 1, "c", "c", "c", "c", 1)]
		[TestCase("cd", "sdvc", 1, "vdc", 2, 4, "cgf", "crd", "cnh", "cfgn", 10)]
		public void AddChargerRequestUnitTests_AddChargerRequest_CorrectParameters_ExpectedNotThrowException(
			string company, string connectionType, int ideAmount, string motherboardConnector, int power, int sataAmount,
			string series, string sertificate, string title, string videocardConnector, int videoConnectorsAmount)
		{
			Charger charger = new Charger()
			{
				Company = company,
				ConnectorType = connectionType,
				IDEAmount = ideAmount,
				MotherboardConnector = motherboardConnector,
				Power = power,
				SATAAmount = sataAmount,
				Series = series,
				Sertificate80Plus = sertificate,
				Title = title,
				VideocardConnector = videocardConnector,
				VideoConnectorsAmount = videoConnectorsAmount
			};

			Assert.DoesNotThrow(() => { new AddChargerRequest(charger); });
		}

		[Test]
		public void AddChargerRequestUnitTests_AddChargerRequest_CorrectParameters_ExpectedParametersConatinsValues()
		{
			Charger charger = new Charger()
			{
				Company = "C!",
				ConnectorType = "CON1",
				IDEAmount = 1,
				MotherboardConnector = "128",
				Power = 12,
				SATAAmount = 2,
				Series = "S",
				Sertificate80Plus = "GOLD",
				Title = "CH1",
				VideocardConnector = "24",
				VideoConnectorsAmount = 2
			};

			AddChargerRequest request = new AddChargerRequest(charger);

			foreach (var param in request.Parameters)
			{
				switch (param.ParameterName)
				{
					case "@company":
					{
						Assert.AreEqual(charger.Company, param.Value);
						break;
					}
					//case "@title":
					//{
					//	Assert.AreEqual(body.Title, param.Value);
					//	break;
					//}
					//case "@company":
					//{
					//	Assert.AreEqual(body.Company, param.Value);
					//	break;
					//}
					//case "@formfactor":
					//{
					//	Assert.AreEqual(body.Formfactor, param.Value);
					//	break;
					//}
					//case "@type":
					//{
					//	Assert.AreEqual(body.Type, param.Value);
					//	break;
					//}
					//case "@buildInCharger":
					//{
					//	Assert.AreEqual(body.BuildInCharger ? 1 : 0, param.Value);
					//	break;
					//}
					//case "@chargerPower":
					//{
					//	Assert.AreEqual(body.ChargerPower, param.Value);
					//	break;
					//}
					//case "@color":
					//{
					//	Assert.AreEqual(body.Color, param.Value);
					//	break;
					//}
					//case "@usb3Amount":
					//{
					//	Assert.AreEqual(body.USB3Ports, param.Value);
					//	break;
					//}
					//case "@usb2Amount":
					//{
					//	Assert.AreEqual(body.USB2Ports, param.Value);
					//	break;
					//}
					//case "@vidoecardMaxLength":
					//{
					//	Assert.AreEqual(body.VideocardMaxLength, param.Value);
					//	break;
					//}
				}
			}
		}
	}
}
