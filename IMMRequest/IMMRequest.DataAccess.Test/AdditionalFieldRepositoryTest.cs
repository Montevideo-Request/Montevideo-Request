using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class AdditionalFieldRepositoryTest : BaseRepositoryTest<AdditionalField>
    {
        public override AdditionalField CreateEntity()
        {
            AdditionalField Field = new AdditionalField();
            return Field;
        }

        public override AdditionalField ModifiedEntity(AdditionalField AdditionalField)
        {
            AdditionalField ModifiedField = AdditionalField;
            ModifiedField.Name = "Test";
            return ModifiedField;
        }

        public override AdditionalField GetSavedEntity(BaseRepository<AdditionalField> AdditionalFieldRepo, AdditionalField AdditionalField)
        {
            AdditionalField AdditionalFieldToReturn = AdditionalFieldRepo.Get(AdditionalField.Id);
            return AdditionalFieldToReturn;
        }

        public override BaseRepository<AdditionalField> CreateRepository()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdditionalFieldRepository AdditionalFieldRepo = new AdditionalFieldRepository(IMMRequestContext);

            return AdditionalFieldRepo;
        }

    }
}
