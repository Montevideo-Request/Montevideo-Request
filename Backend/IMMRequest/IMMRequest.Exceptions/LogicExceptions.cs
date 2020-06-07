namespace IMMRequest.Exceptions
{
    public class LogicExceptions
    {
        public const string NOT_IMPLEMENTED = "Method not implemented.";
        public const string GENERIC_INVALID_ID = "No items with the indicated id were found.";
        public const string GENERIC_NO_ELEMENTS = "No items found.";
        public const string INVALID_ID_AREA = "The id of the indicated area is incorrect.";
        public const string INVALID_AREA_TOPIC_COMBINATION = "The Area indicated for that given Topic was not found.";
        public const string INVALID_TOPIC_TYPE_COMBINATION = "The Topic indicated for that given Type was not found.";
        public const string INVALID_TYPE_FIELD_COMBINATION = "The Type indicated for that given Additional Field was not found.";
        public const string INVALID_ID_TOPIC = "The id of the indicated topic is incorrect.";
        public const string INVALID_ID_TYPE = "The id of the indicated type is incorrect.";
        public const string INVALID_ADMINISTRATOR = "That administrator does not exist.";
        public const string INVALID_ID_ADDITIONAL_FIELD = "The id of the indicated additional field is incorrect.";
        public const string INVALID_ID_REQUEST = "The id of the indicated request is incorrect.";
        public const string INVALID_EMAIL_FORMAT = "the format of the given email is incorrect, it must be of the format (example@example.eg).";
        public const string INVALID_EMAIL_IN_USE = "The email given is already registered in the system.";
        public const string INVALID_CREDENTIALS = "The credentials provided are invalid.";
        public const string INVALID_PHONE_FORMAT = "The format of the given phone is invalid.";
        public const string INVALID_ADDITIONAL_FIELD = "The additional fields given are invalid, one or more of them does not belong to the indicated type.";
        public const string INVALID_BOOLEAN = "The value provided for this field is not a Boolean.";
        public const string INVALID_BOOLEAN_RANGE = "Boolean Field Types cannot contain Range Values.";
        public const string INVALID_LENGTH = "One of the fields was not entered.";
        public const string NO_PERMISSION_DESCRIPTION_FIELD = "You are not allowed to set the DESCRIPTION field of a Request.";
        public const string NO_PERMISSION_STATE_FIELD = "You are not allowed to set the STATE field of a Request.";
        public const string INVALID_FIELD_TYPE = "The Field Type is invalid and it is required for this entity.";
        public const string WRONG_DATE_FORMAT = "The given date format is invalid, must be 'MM/dd/yyyy'.";
        public const string DATE_OUT_OF_RANGE = "The given date is out of range.";
        public const string NUMBER_OUT_OF_RANGE = "The selected number is out of range.";
        public const string INVALID_DATE_RANGE = "The given date range is invalid, it must comply with the format Date:From - Date:To.";
        public const string INVALID_NUMBER_RANGE = "The given range is invalid, the secuence should be incremental [smaller > bigger].";
        public const string WRONG_INTEGER_FORMAT = "The given integer is invalid.";
        public const string ALREADY_EXISTS_ADDITIONAL_FIELD = "There is already an additional field with the same name within the indicated type.";
        public const string ALREADY_EXISTS_TYPE = "There is already a type with the same name within the indicated topic.";
        public const string ALREADY_EXISTS_AREA = "There is already an area with the given name.";
        public const string ALREADY_EXISTS_TOPIC = "There is already a topic with the same name within the indicated area.";
        public const string ALREADY_EXISTS_ADMIN = "That email is already taken.";
        public const string RANGE_NOT_LISTED = "The additional field range does not correspond to the given specifications.";
        public const string INVALID_TYPE_NOT_EXIST = "Type not found.";
        public const string MISSING_REQUIRED_ADDITIONAL_FIELD = "Please Fill in all the required additional Fields for this request.";
        public const string INVALID_NAME = "The Name is invalid and it is required for this entity.";
        public const string INVALID_ADDITIONAL_FIELD_REQUEST_ID = "The request id of the additional field does not map to the request id itself.";
        public const string INVALID_ADDITIONAL_FIELD_RANGES = "The selection is not within the avaliable ranges.";
        public const string INVALID_MULTISELECTION = "This field does not support multiple selections, please use only one possible value.";
        public const string INVALID_SELECTION = "The selection is null or invalid. Use selections within the given ranges";
        public const string REPEATED_SELECTION = "There are repeated values on the multiple selection";
        public const string INVALID_MULTISELECT_RANGES = "There are no given ranges for the multiselect field. Add possible values and try again.";
        public const string EMPTY_EMAIL_INPUT = "The administrator's email was not entered.";
        public const string EMPTY_NAME_INPUT = "The administrator's name was not entered.";
        public const string EMPTY_PASSWORD_INPUT = "The administrator's password was not entered.";
        public const string INVALID_ID_ADDITIONAL_FIELD_IN_RANGE = "The id of the additional field in the given range does not correspond to the id of the parent additional field.";
        public const string EMPTY_RANGE_INPUT = "The additional field's range was not entered.";
        public const string RANGE_REPEATED_IN_LIST = "Two fields with the same value have been indicated in the input.";
        public const string INVALID_STATE = "The State introduced is invalid.";
        public const string INVALID_STATE_PROGRESSION = "The State introduced does not follow the State Progression Flow.";
    }
}
