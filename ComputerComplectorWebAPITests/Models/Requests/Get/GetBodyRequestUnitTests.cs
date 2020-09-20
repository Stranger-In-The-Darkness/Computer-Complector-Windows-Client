using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using ComputerComplectorWebAPI;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Get;
using ComputerComplectorWebAPI.Models.Exceptions;

namespace ComputerComplectorWebAPITests.Models.Requests.Get
{
    public class GetBodyRequestUnitTests
    {
		[Test]
		public void GetBodyRequestUnitTests_EmptyRequest_ExpectedSelectAllExpression()
		{
			//GetBodiesRequest request = new GetBodiesRequest();

			var exp = "SELECT * FROM BODY";
			//var act = request.Expression;

			//Assert.AreEqual(act, exp);
		}

		[Test]
		[TestCase(null, null, null, null, null, null, null, null, 
			"SELECT * FROM BODY")]
		[TestCase(new string[] { "C1" }, null, null, null, null, null, null, null, 
			"SELECT * FROM BODY WHERE Company=@company0")]
		[TestCase(new string[] { "C1", "C2" }, null, null, null, null, null, null, null, 
			"SELECT * FROM BODY WHERE Company=@company0 OR Company=@company1")]
		[TestCase(null, new string[] { "F1" }, null, null, null, null, null, null, 
			"SELECT * FROM BODY WHERE Formfactor=@formfactor0")]
		[TestCase(null, new string[] { "F1", "F2" }, null, null, null, null, null, null, 
			"SELECT * FROM BODY WHERE Formfactor=@formfactor0 OR Formfactor=@formfactor1")]
		[TestCase(null, null, new string[] { "T1" }, null, null, null, null, null, 
			"SELECT * FROM BODY WHERE Type=@type0")]
		[TestCase(null, null, new string[] {"T1", "T2" }, null, null, null, null, null, 
			"SELECT * FROM BODY WHERE Type=@type0 OR Type=@type1")]
		[TestCase(null, null, null, new bool[] { true }, null, null, null, null, 
			"SELECT * FROM BODY WHERE [Build-inCharger]=@buildInCharger0")]
		[TestCase(null, null, null, new bool[] { true, false }, null, null, null, null, 
			"SELECT * FROM BODY WHERE [Build-inCharger]=@buildInCharger0 OR [Build-inCharger]=@buildInCharger1")]
		[TestCase(null, null, null, null, new string[] { "120-240" }, null, null, null, 
			"SELECT * FROM BODY WHERE (ChargerPower>=@chargerPower10 AND ChargerPower<=@chargerPower20)")]
		[TestCase(null, null, null, null, new string[] { "120-180", "240-460" }, null, null, null, 
			"SELECT * FROM BODY WHERE (ChargerPower>=@chargerPower10 AND ChargerPower<=@chargerPower20) OR (ChargerPower>=@chargerPower11 AND ChargerPower<=@chargerPower21)")]
		[TestCase(null, null, null, null, null, new string[] { "1" }, null, null, 
			"SELECT * FROM BODY WHERE [USB3.0Amount]=@usbPorts0")]
		[TestCase(null, null, null, null, null, new string[] { "1", "4" }, null, null, 
			"SELECT * FROM BODY WHERE [USB3.0Amount]=@usbPorts0 OR [USB3.0Amount]=@usbPorts1")]
		[TestCase(null, null, null, null, null, null, 1, null, 
			"SELECT * FROM BODY WHERE Formfactor = (SELECT TOP 1 Formfactor FROM MOTHERBOARD WHERE ID = @id)")]
		[TestCase(null, null, null, null, null, null, null, 1, 
			"SELECT * FROM BODY WHERE VideocardMaxLength >= (SELECT TOP 1 Length FROM VIDEOCARD WHERE ID = @vID)")]
		[TestCase(
			new string[] { "C1" }, new string[] { "F1" }, new string[] { "T1" }, new bool[] { true }, 
			new string[] { "120-240" }, new string[] { "1" }, 1, 1,
			"SELECT * FROM BODY WHERE " +
			"Company=@company0 AND " +
			"Formfactor=@formfactor0 AND " +
			"Type=@type0 AND " +
			"[Build-inCharger]=@buildInCharger0 AND " +
			"(ChargerPower>=@chargerPower10 AND ChargerPower<=@chargerPower20) AND " +
			"[USB3.0Amount]=@usbPorts0 AND " +
			"Formfactor = (SELECT TOP 1 Formfactor FROM MOTHERBOARD WHERE ID = @id) AND " +
			"VideocardMaxLength >= (SELECT TOP 1 Length FROM VIDEOCARD WHERE ID = @vID)")]
		public void GetBodyRequestUnitTests_ParametrizedRequest_ExpectedSelectAllExpressionWithConditions(
			string[] company, string[] format, string[] type, bool[] buildInCharger, string[] chargerPower, 
			string[] usbPorts, int? selectedMotherboard, int? selectedVideocard, string exp)
		{
			GetBodiesRequest request = new GetBodiesRequest(
				company, format, type, buildInCharger, chargerPower, usbPorts, 
				selectedMotherboard, selectedVideocard);

			var act = request.Expression;

			Assert.AreEqual(exp, act);
		}
	}
}