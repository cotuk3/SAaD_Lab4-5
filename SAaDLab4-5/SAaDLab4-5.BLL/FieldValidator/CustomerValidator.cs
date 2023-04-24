using FieldValidatorAPI;
using SAaDLab4_5.BLL.Interfaces;

namespace SAaDLab4_5.BLL.FieldValidator;
public class CustomerValidator : IFieldValidator
{
    FieldValidatorDel _fieldValidatorDel = null;

    RequiredValidDel _requiredValidDel = null;
    PatternMatchValidDel _patternMatchValidDel = null;


    string[] _fieldArray = null;

    public string[] FieldArray => _fieldArray ??= new string[Enum.GetValues
            (typeof(FieldConstants.CustomerField)).Length];

    public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

    public FieldConstants FieldConstants
    {
        get => default;
        set
        {
        }
    }

    public void InitialiseValidatorDelegates()
    {
        _fieldValidatorDel = new FieldValidatorDel(ValidField);

        _requiredValidDel = CommonFieldValidatorFunctions.RequiredFieldValidDel;
        _patternMatchValidDel = CommonFieldValidatorFunctions.PatternMatchValidDel;
    }

    private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
    {

        fieldInvalidMessage = "";

        FieldConstants.CustomerField userOrderField = (FieldConstants.CustomerField)fieldIndex;

        switch(userOrderField)
        {
            case FieldConstants.CustomerField.EmailAddress:
            fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.CustomerField), userOrderField)}{Environment.NewLine}" : "";
            fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Email_Address_RegEx_Pattern)) ? $"You must enter a valid email address{Environment.NewLine}" : fieldInvalidMessage;
            break;
            case FieldConstants.CustomerField.FirstName:
            fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.CustomerField), userOrderField)}{Environment.NewLine}" : "";
            fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.FirstLastName_RegEx_Pattern)) ? $"You did not enter a valid name{Environment.NewLine}" : fieldInvalidMessage;
            break;
            case FieldConstants.CustomerField.LastName:
            fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.CustomerField), userOrderField)}{Environment.NewLine}" : "";
            fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.FirstLastName_RegEx_Pattern)) ? $"You did not enter a valid last name{Environment.NewLine}" : fieldInvalidMessage;
            break;
            case FieldConstants.CustomerField.PhoneNumber:
            fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.CustomerField), userOrderField)}{Environment.NewLine}" : "";
            fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.UA_PhoneNumber_RegEx_Pattern)) ? $"You did not enter a valid UK phone number{Environment.NewLine}" : fieldInvalidMessage;
            break;
            default:
            throw new ArgumentException("This field does not exists");
        }

        return (fieldInvalidMessage == "");

    }
}
