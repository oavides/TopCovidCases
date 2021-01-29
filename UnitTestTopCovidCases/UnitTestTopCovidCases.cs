using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestTopCovidCases
{
    [TestClass]
    public class UnitTestTopCovidCases
    {
        [TestMethod]
        public void ListRegionCheckResponseIsNotNull()
        {
            // Arrage
            var dataAccess = new TopCovidCases.Models.DataAccess();

            // Act
            var result =  dataAccess.ListRegion();

            // Assert
            Assert.IsNotNull(result);

        }



        [TestMethod]
        public void ListCasesCheckResponseIsNotNull()
        {
            // Arrage
            var dataAccess = new TopCovidCases.Models.DataAccess();
            var iso = "USA";

            // Act
            var result = dataAccess.ListCases(iso);

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
