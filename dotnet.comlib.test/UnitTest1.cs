using System;

using Comlib.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotnet.comlib.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var obj = new DTOWXTest() {
                Code = DateTime.Now.ToString(),
                Name = "123",
            };
            //ReflectionUtils.GetProperties(typeof(DTOWXTest), x => {
            //});

            string props = "Name";
            var propsX = ReflectionUtils.GetPropertiesAsMap(obj, null);
        }
    }
}