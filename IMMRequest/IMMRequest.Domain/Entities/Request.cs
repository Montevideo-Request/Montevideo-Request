using System;

namespace IMMRequest.Domain
{
    public class Request
    {
        public Guid Id { get; set; }
        
        public string RequestorsName { get; set; }

        public string RequestorsEmail { get; set; }

        public string RequestorsPhone { get; set; }

        public Area Area { get; set; }

        public Topic Topic { get; set; }

        public Type Type { get; set; }

        public string State { get; set; }

        private string Description { get; set; }

        public Request(string RequestorsName, string RequestorsEmail, string RequestorsPhone, 
                        Area Area, Topic Topic, Type Type, string State, string Description) 
        {
            this.Id = Guid.NewGuid();
            this.RequestorsName = RequestorsName;
            this.RequestorsEmail = RequestorsEmail;
            this.RequestorsPhone = RequestorsPhone;
            this.Area = Area;
            this.Topic = Topic;
            this.Type = Type;
            this.State = State;
            this.Description = Description;
        }

        public bool IsValid() 
        {
            return true;
        }

        public override bool Equals(Object obj) 
        {
			Request request = obj as Request;
			bool equals = false;
			if (obj == null) {
				equals = false;
			}
			else {
				equals = this.RequestorsName == request.RequestorsName 
                && this.RequestorsEmail == request.RequestorsEmail
                && this.RequestorsPhone == request.RequestorsPhone
                && this.Area == request.Area
                && this.Topic == request.Topic
                && this.Type == request.Type
                && this.State == request.State
                && this.Description == request.Description;
			}
			return equals;
		}
    }
}
