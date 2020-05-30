namespace IMMRequest.Exceptions
{
    public class DataAccessExceptions
    {
        public const string GENERIC_ELEMENT_ALREADY_EXISTS = "An entity with the same specifications already exists in the system.";
        public const string NOT_FOUND_PARENT_ADMINISTRATOR = "A parent administrator for the given administrator was not found.";
        public const string NOT_FOUND_PARENT_REQUEST = "A parent request for the given request was not found.";
        public const string NOT_FOUND_PARENT_TOPIC = "A parent topic for the given type was not found.";
        public const string NOT_FOUND_PARENT_TYPE = "A parent type for the given additional field was not found.";
        public const string NOT_FOUND_ADMINISTRATOR = "A parent administrator for the given administrator was not found.";
        public const string NOT_FOUND_REQUEST = "A request that meets the specifications was not found.";
        public const string NOT_FOUND_AREA = "An area that meets the specifications was not found.";
        public const string NOT_FOUND_TOPIC = "A topic that meets the specifications was not found.";
        public const string NOT_FOUND_TYPE = "A type that meets the specifications was not found.";
        public const string NOT_FOUND_ADDITIONAL_FIELD = "An additional field that meets the specifications was not found.";
    }
}