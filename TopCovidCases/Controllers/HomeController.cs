using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TopCovidCases.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Xml.Linq;

namespace TopCovidCases.Controllers
{
    public class HomeController : Controller
    {
        private IDataAccess dataAccess;

        public HomeController(IDataAccess _dataAccess)
        {
            dataAccess = _dataAccess;
        }




        public IActionResult Index()
        {
            return View();
        }




        public async Task<IActionResult> GetRegions()
        {
            return Json(await dataAccess.ListRegion());
        }




        public async Task<IActionResult> GetReport(String iso) {
            return Json(await dataAccess.ListCases(iso));
        }



        
        public async Task<IActionResult> GetCSV(String iso)
        {
            StringBuilder sb = null;
            try
            {
                IList<Cases> cases = await dataAccess.ListCases(iso);

                sb = new StringBuilder();
                sb.Append(iso != null ? "PROVINCE" : "REGION" + ';' + "CASES" + ';' + "DEATHS" + ';');
                sb.Append("\r\n");
                foreach (Cases c in cases)
                {
                    sb.Append((iso != null ? c.region.province.Equals("") ? c.region.name : c.region.province : c.region.name) + ';' + c.confirmed + ';' + c.deaths + ';');
                    sb.Append("\r\n");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "TopcovidCases.csv");
        }




     
        public async Task<IActionResult> GetXML(String iso)
        {
            XElement xEle = null;
            try
            {
                IList<Cases> cases = await dataAccess.ListCases(iso);

                xEle = new XElement("root",
                    from c in cases
                    select new XElement("cases",
                        new XElement(iso != null ? "province" : "region", iso != null ? c.region.province.Equals("") ? c.region.name : c.region.province : c.region.name),
                        new XElement("confirmed", c.confirmed),
                        new XElement("deaths", c.deaths)
                    ));
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return File(Encoding.UTF8.GetBytes(xEle.ToString()), "text/xml", "TopcovidCases.xml");
        }



        

        public async Task<IActionResult> GetJson(String iso)
        {
            JObject json = null;
            try
            {
                IList<Cases> cases = await dataAccess.ListCases(iso);

                json = new JObject(
                        new JProperty("cases",
                            new JArray(
                                from c in cases
                                select new JObject(
                                    new JProperty(iso != null ? "province" : "region", iso != null ? c.region.province.Equals("") ? c.region.name : c.region.province : c.region.name),
                                    new JProperty("confirmed", c.confirmed),
                                    new JProperty("deaths", c.deaths)))));
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return File(Encoding.UTF8.GetBytes(json.ToString()), "application/json", "TopcovidCases.json");
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
