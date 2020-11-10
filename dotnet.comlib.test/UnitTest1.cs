using System.Diagnostics;

using Comlib;

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
                Name = "123",
                CodeEx = "111111111",
                DataId = "12312xx",
                DataType = 1,
                Id = 1,
                NameEx = "123123",
            };
            //ReflectionUtils.GetProperties(typeof(DTOWXTest), x => {
            //});

            //string props = "Name";
            //var propsX = ReflectionUtils.GetPropertiesAsMap(obj, null);

            SaveDocumentData(obj);
            SaveDocumentDataEx(obj);
        }

        /// <summary>
        /// ���������json����
        /// </summary>
        /// <param name="document"></param>
        public static void SaveDocumentData(DTOShopDiyBase obj)
        {
            var str = obj.ToJson();

            Debug.WriteLine("none{0}", str);
        }

        /// <summary>
        /// ���������json����
        /// </summary>
        /// <param name="document"></param>
        public static void SaveDocumentDataEx(DTOWXTest obj)
        {
            var str = obj.ToJson();
            Debug.WriteLine("Ex{0}", str);
        }
    }
}