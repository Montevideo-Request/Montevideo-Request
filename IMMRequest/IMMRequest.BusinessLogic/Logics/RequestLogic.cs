using IMMRequest.BusinessLogic.Interface;
using IMMRequest.DataAccess.Interface;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Net.Mail;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic
{
    public class RequestLogic : IRequestLogic<Request, TypeEntity>
    {
        protected IRequestRepository<Request, TypeEntity> repository;
		public RequestLogic(IRequestRepository<Request, TypeEntity> requestRepository) 
        {
            this.repository = requestRepository;
        }

        public RequestLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new RequestRepository(IMMRequestContext);
		}

        public Request Create(Request entity)
        {
            IsValid(entity);
            this.repository.Add(entity);
            this.repository.Save();
            return entity;
        }

        public Request Get(Guid id)
        {
            EntityExists(id);
            return this.repository.Get(id);
        }

        public IEnumerable<Request> GetAll()
        {
            IEnumerable<Request> entities = this.repository.GetAll();
            
            if (entities.ToList().Count() == 0) 
            {
                throw new ExceptionController(LogicExceptions.GENERIC_NO_ELEMENTS);
            }

            return entities;
		}

        public void Update(Request entity)
        {
            try
            {
                Request requestToUpdate = this.repository.Get(entity.Id);
                requestToUpdate.RequestorsEmail = entity.RequestorsEmail;
                requestToUpdate.RequestorsName = entity.RequestorsName;
                requestToUpdate.RequestorsPhone = entity.RequestorsPhone;
                this.repository.Update(requestToUpdate);
                this.repository.Save();
            }
            catch
            {
                throw new ExceptionController(LogicExceptions.INVALID_ID_REQUEST);
            } 
        }

        public void Save()
        {
            this.repository.Save();
        }

        public TypeEntity GetTypeWithFields(Guid id)
        {
            return this.repository.GetTypeWithFields(id);
        }

        public void IsValid(Request request)
        { 
            if(request.RequestorsEmail.Length > 0)
            {
                ValidEmailFormat(request.RequestorsEmail);
            }
            if(request.RequestorsPhone.Length > 0)
            {
                ValidPhoneFormat(request.RequestorsPhone);
            }
            TypeEntity selectedType = GetTypeWithFields(request.TypeId);
            if(selectedType == null)
            {
                throw new ExceptionController(LogicExceptions.INVALID_TYPE_NOT_EXIST);
            }
            foreach(AdditionalFieldValue additionalFieldValue in request.AdditionalFieldValues)
            {
                if(additionalFieldValue.RequestId != request.Id)
                {
                    throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD_REQUEST_ID);
                }
                AdditionalField selectedAdditionalField = selectedType.AdditionalFields.FirstOrDefault(a => a.Id == additionalFieldValue.AdditionalFieldId);
                FieldRange dummyFieldRange = new FieldRange();
                dummyFieldRange.Range = additionalFieldValue.Value;
                if(!selectedAdditionalField.Ranges.Contains(dummyFieldRange))
                {
                    throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD_RANGES);
                }
            }
        }

        private void ValidEmailFormat(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch
            {
                throw new ExceptionController(LogicExceptions.INVALID_EMAIL_FORMAT);
            }
        }

        private void ValidPhoneFormat(string phoneNumber)
        {
            phoneNumber.Trim()
                    .Replace(" ", "")
                    .Replace(".", "")
                    .Replace("-", "")
                    .Replace("(", "")
                    .Replace(")", "");
            if(!Regex.Match(phoneNumber, @"^[0-9-]*$").Success)
            {
                throw new ExceptionController(LogicExceptions.INVALID_PHONE_FORMAT);
            }
        }

        public void EntityExists(Guid id)
        {
            return ;
        }
    }
}
