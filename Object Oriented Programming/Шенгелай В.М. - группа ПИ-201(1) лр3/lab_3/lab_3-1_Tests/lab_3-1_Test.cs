using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lab_3_1_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NOD_2806and345_23returned()
        {
            //arrange
            int x = 2806;
            int y = 345;
            int expected = 23;

            //act
            Calculations c = new Calculations();
            int actual = c.NOD(x, y);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
