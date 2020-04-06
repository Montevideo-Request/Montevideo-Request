using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Tests 
{
    [TestClass]
    public class TypeTest 
    {
        public TypeLogic typeLogic;

        public TypeTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.typeLogic = new TypeLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.typeLogic = new TypeLogic();
        }
    }
}