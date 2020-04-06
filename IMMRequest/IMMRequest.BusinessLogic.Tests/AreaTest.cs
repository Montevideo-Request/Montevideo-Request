using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Tests 
{
    [TestClass]
    public class AreaTest 
    {
        public AreaLogic areaLogic;

        public AreaTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.areaLogic = new AreaLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.areaLogic = new AreaLogic();
        }
    }
}