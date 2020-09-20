using System.Linq;
using NUnit.Framework;
using ComputerComplectorWebAPI;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Add;
using ComputerComplectorWebAPI.Models.Exceptions;

namespace ComputerComplectorWebAPITests.Models.Requests.Add
{
    public class AddBodyRequestUnitTests
    {
        [Test]
        [TestCase("", false, 0, "", "", "", "", "", 0, 0, 0)]
        [TestCase("d", false, -1, "", "", "", "", "", 0, 0, 0)]
        [TestCase("d", false, 1, "", "", "", "", "", 0, 0, 0)]
        [TestCase("d", false, 1, "d", "", "", "", "", 0, 0, 0)]
        [TestCase("d", false, 1, "d", "d", "", "", "", 0, 0, 0)]
        [TestCase("d", false, 1, "d", "d", "d", "", "", 0, 0, 0)]
        [TestCase("d", false, 1, "d", "d", "d", "d", "", 0, 0, 0)]
        [TestCase("d", false, 1, "d", "d", "d", "d", "d", -1, 0, 0)]
        [TestCase("d", false, 1, "d", "d", "d", "d", "d", 1, -1, 0)]
        [TestCase("d", false, 1, "d", "d", "d", "d", "d", 1, 1, -1)]
        public void AddBodyRequestUnitTests_AddBodyRequest_IncorrectParameters_ExpectedValidationException(
            string additions, bool charger, int power, string color, string company, string formfactor, string title, string type,
            int usb2, int usb3, int length)
        {
            Body body = new Body()
            {
                Additions = additions,
                BuildInCharger = charger,
                ChargerPower = power,
                Color = color,
                Company = company,
                Title = title,
                Type = type,
                USB2Amount = usb2,
                USB3Amount = usb3,
                VideocardMaxLength = length
            };

            Assert.Throws(typeof(ValidationException), ()=> { new AddBodyRequest(body); });
        }

        [Test]
        [TestCase("dd", false, 12, "dbs", "dgd", "dfh", "dfnht", "ddfg", 134, 146, 14)]
        [TestCase("tf", false, 10, "dd", "dvd", "ddgdf", "gnbfd", "dntd", 2, 15, 12)]
        public void AddBodyRequestUnitTests_AddBodyRequest_CorrectParameters_ExpectedNotThrowException(
            string additions, bool charger, int power, string color, string company, string formfactor, string title, string type,
            int usb2, int usb3, int length)
        {
            Body body = new Body()
            {
                Additions = additions,
                BuildInCharger = charger,
                ChargerPower = power,
                Color = color,
                Company = company,
                Title = title,
                Type = type,
                USB2Amount = usb2,
                USB3Amount = usb3,
                VideocardMaxLength = length
            };

            Assert.DoesNotThrow(() => { new AddBodyRequest(body); });
        }

        [Test]
        public void AddBodyRequestUnitTests_AddBodyRequest_CorrectParameters_ExpectedParametersConatinsValues()
        {
            Body body = new Body()
            {
                Additions = "",
                BuildInCharger = true,
                ChargerPower = 120,
                Color = "White",
                Company = "Comp",
                Title = "ADX 1020",
                Type = "Tower",
                USB2Amount = 2,
                USB3Amount = 3,
                VideocardMaxLength = 150
            };

            AddBodyRequest request = new AddBodyRequest(body);

            foreach (var param in request.Parameters)
            {
                switch (param.ParameterName)
                {
                    case "@addition":
                    {
                        Assert.AreEqual(body.Additions, param.Value);
                        break;
                    }
                    case "@title":
                    {
                        Assert.AreEqual(body.Title, param.Value);
                        break;
                    }
                    case "@company":
                    {
                        Assert.AreEqual(body.Company, param.Value);
                        break;
                    }
                    case "@formfactor":
                    {
                        Assert.AreEqual(body.Formfactor, param.Value);
                        break;
                    }
                    case "@type":
                    {
                        Assert.AreEqual(body.Type, param.Value);
                        break;
                    }
                    case "@buildInCharger":
                    {
                        Assert.AreEqual(body.BuildInCharger ? 1 : 0, param.Value);
                        break;
                    }
                    case "@chargerPower":
                    {
                        Assert.AreEqual(body.ChargerPower, param.Value);
                        break;
                    }
                    case "@color":
                    {
                        Assert.AreEqual(body.Color, param.Value);
                        break;
                    }
                    case "@usb3Amount":
                    {
                        Assert.AreEqual(body.USB3Amount, param.Value);
                        break;
                    }
                    case "@usb2Amount":
                    {
                        Assert.AreEqual(body.USB2Amount, param.Value);
                        break;
                    }
                    case "@vidoecardMaxLength":
                    {
                        Assert.AreEqual(body.VideocardMaxLength, param.Value);
                        break;
                    }
                }
            }
        }
    }
}