using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using IMMRequest.DataAccess.Interface;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public abstract class BaseLogicTest<T, X> where T : class where X : class
    {
        public abstract T CreateEntity();
        
        public abstract BaseLogic<T, X> CreateBaseLogic(IRepository<T, X> obj);

        public abstract Guid GetId(T entity); 

        public abstract T ModifyEntity(T Entity);

        [TestMethod]
        public void SaveCorrect() 
        {
            var mock = new Mock<IRepository<T, X>>(MockBehavior.Strict);
            mock.Setup(m => m.Save());
            var controller = CreateBaseLogic(mock.Object);

            controller.Save();
            mock.VerifyAll();
        }
    }
}