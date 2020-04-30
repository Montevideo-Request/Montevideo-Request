using IMMRequest.DataAccess.Interface;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using IMMRequest.Exceptions;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace IMMRequest.BusinessLogic
{
    public class RequestLogic : BaseLogic<Request, Request>
    {
		public RequestLogic(IRepository<Request, Request> requestRepository) 
        {
            this.repository = requestRepository;
        }

        public RequestLogic() 
        {
			IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
			this.repository = new RequestRepository(IMMRequestContext);
		}

        public override void Update(Request entity)
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
                throw new ExceptionController(LogicExceptions.INVALID_ID);
            } 
        }

        public override void IsValid(Request request)
        { 
            if(request.RequestorsEmail.Length > 0)
            {
                ValidEmailFormat(request.RequestorsEmail);
            }
            if(request.RequestorsPhone.Length > 0)
            {
                ValidPhoneFormat(request.RequestorsPhone);
            }

            // falta validar el type aqui 

            foreach(AdditionalFieldValue additionalFieldValue in request.AdditionalFieldValues)
            {
                if(additionalFieldValue.AdditionalField.TypeId != request.TypeId)
                {
                    throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD);
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
            //if(!Regex.Match(phoneNumber, @"^\+\d{5,15}$").Success)
            if(!Regex.IsMatch(phoneNumber, @"^\+\d{5,15}$"))
            {
                throw new ExceptionController(LogicExceptions.INVALID_PHONE_FORMAT);
            }
        }

        public override void EntityExists(Guid id)
        {
            return ;
        }
    }
}
