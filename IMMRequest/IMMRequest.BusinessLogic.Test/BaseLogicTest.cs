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
        public void RemoveCorrect() 
        {
	        T entity = CreateEntity();

            var mock = new Mock<IRepository<T, X>>(MockBehavior.Strict);
            mock.Setup(m => m.Remove(entity));
            mock.Setup(m => m.Save());
            var controller = CreateBaseLogic(mock.Object);

            controller.Remove(entity); 
            mock.VerifyAll();
        }

        [TestMethod]
        public void RemoveInvalid() 
        {
            T entity = CreateEntity();
            var mock = new Mock<IRepository<T, X>>(MockBehavior.Strict);
            mock.Setup(m => m.Remove(entity)).Throws(new ArgumentException());
            var controller = CreateBaseLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Remove(entity)) ;
            mock.VerifyAll();
        }

        /*[TestMethod]
        public void UpdateCorrect() 
        {
	        T entity = CreateEntity();
            var mock = new Mock<IRepository<T>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(GetId(entity))).Returns(entity);
            mock.Setup(m => m.Update(ModifyEntity(entity)));
            mock.Setup(m => m.Save());
            var controller = CreateBaseLogic(mock.Object);

            controller.Update(entity);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateInvalid() 
        {
            T entity = CreateEntity();
            Guid entityGuid = GetId(entity);
            var mock = new Mock<IRepository<T>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(entityGuid)).Throws(new ArgumentException());
            var controller = CreateBaseLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.Update(entity));
            mock.VerifyAll();
        }*/

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