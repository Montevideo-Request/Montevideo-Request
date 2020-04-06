using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Tests 
{
    [TestClass]
    public class RequestTest 
    {
        public RequestLogic requestLogic;

        public RequestTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.requestLogic = new RequestLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.requestLogic = new RequestLogic();
        }
    }
}