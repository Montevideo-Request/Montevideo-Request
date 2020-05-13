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
        List<string> validStates = new List<string>(){ "Creada", "En Revision", "Aceptada", "Denegada",  "Finalizada" };
        Dictionary<string, HashSet<string>> states = new Dictionary<string, HashSet<string>>();

        public RequestLogic(IRequestRepository<Request, TypeEntity> requestRepository)
        {
            this.repository = requestRepository;

            /* Valid States HashSet */
            states.Add("Creada",new HashSet<string>{"Creada", "En Revision"});
            states.Add("En Revision",new HashSet<string>{"Creada", "En Revision", "Aceptada", "Denegada"});
            states.Add("Aceptada",new HashSet<string>{"En Revision", "Aceptada", "Denegada", "Finalizada"});
            states.Add("Denegada",new HashSet<string>{"En Revision", "Aceptada", "Denegada", "Finalizada"});
            states.Add("Finalizada",new HashSet<string>{"Aceptada", "Denegada"});
        }

        public RequestLogic()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            this.repository = new RequestRepository(IMMRequestContext);
        }

        public Request Create(Request entity)
        {
            IsValid(entity);
            entity.State = validStates[0]; //Primer valor valido se setea por defecto al crearse.

            this.repository.Add(entity);
            this.repository.Save();
            return entity;
        }

        public Request Get(Guid id)
        {
            NotExist(id);
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

        public Request Update(Request entity)
        {
            Request requestToUpdate = this.repository.Get(entity.Id);
            
            IsValidToUpdate(entity, requestToUpdate);
            requestToUpdate.State = entity.State;
            requestToUpdate.Description = entity.Description;
            
            this.repository.Update(requestToUpdate);
            this.repository.Save();

            return requestToUpdate;   
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
            if (request.RequestorsEmail != null && request.RequestorsEmail.Length > 0)
            {
                ValidEmailFormat(request.RequestorsEmail);
            } 
            else 
            {
                throw new ExceptionController(LogicExceptions.INVALID_EMAIL_FORMAT);
            }

            if (request.RequestorsPhone != null && request.RequestorsPhone.Length > 0)
            {
                ValidPhoneFormat(request.RequestorsPhone);
            }
            else
            {
                throw new ExceptionController(LogicExceptions.INVALID_PHONE_FORMAT);
            }
            if ( request.Description != null && request.Description.Length > 0 )
            {
                throw new ExceptionController(LogicExceptions.NO_PERMISSION_DESCRIPTION_FIELD);
            }
            if (request.State != null && request.State.Length > 0)
            {
                throw new ExceptionController(LogicExceptions.NO_PERMISSION_STATE_FIELD);
            }

            ValidateAdditionalFields(request);
        }

        private void IsValidToUpdate(Request request, Request requestToUpdate)
        {
            if (!validStates.Contains(request.State) || (request.State != null && request.State.Length == 0) || request.State == null )
            {
                throw new ExceptionController(LogicExceptions.INVALID_STATE);
            }

            if (!states[requestToUpdate.State].Contains(request.State))
            {
                throw new ExceptionController(LogicExceptions.INVALID_STATE_PROGRESSION);
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
            if (!Regex.Match(phoneNumber, @"^[0-9-]*$").Success)
            {
                throw new ExceptionController(LogicExceptions.INVALID_PHONE_FORMAT);
            }
        }

        private void ValidateAdditionalFields(Request request)
        {
            TypeEntity selectedType = GetTypeWithFields(request.TypeId);

            if (selectedType == null)
            {
                throw new ExceptionController(LogicExceptions.INVALID_TYPE_NOT_EXIST);
            }

            foreach (AdditionalFieldValue additionalFieldValue in request.AdditionalFieldValues)
            {
                if (additionalFieldValue.RequestId != request.Id)
                {
                    throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD_REQUEST_ID);
                }
                AdditionalField selectedAdditionalField = selectedType.AdditionalFields.FirstOrDefault(a => a.Id == additionalFieldValue.AdditionalFieldId);

                if (selectedAdditionalField.Ranges.Count > 0)
                {
                    FieldRange dummyFieldRange = new FieldRange();
                    dummyFieldRange.Range = additionalFieldValue.Value;
                    if (!selectedAdditionalField.Ranges.Contains(dummyFieldRange))
                    {
                        throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD_RANGES);
                    }
                }
            }
        }

        private void NotExist(Guid id)
        {
            Request dummyRequest = new Request();
            dummyRequest.Id = id;
            if (!this.repository.Exist(dummyRequest))
            {
                throw new ExceptionController(LogicExceptions.INVALID_ID_AREA);
            }
        }
    }
}
