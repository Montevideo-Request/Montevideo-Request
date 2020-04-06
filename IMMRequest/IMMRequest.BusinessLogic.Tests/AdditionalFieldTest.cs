using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Tests 
{
    [TestClass]
    public class AdditionalFieldTest 
    {
        public AdditionalFieldLogic additionalFieldLogic;

        public AdditionalFieldTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.additionalFieldLogic = new AdditionalFieldLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.additionalFieldLogic = new AdditionalFieldLogic();
        }
    }
}