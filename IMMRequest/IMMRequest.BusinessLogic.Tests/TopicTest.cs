using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Tests 
{
    [TestClass]
    public class TopicTest 
    {
        public TopicLogic topicLogic;

        public AdditionalFieldTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.topicLogic = new TopicLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.topicLogic = new topicLogic();
        }
    }
}