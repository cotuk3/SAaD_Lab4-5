using FieldValidatorAPI;
using SAaDLab4_5.BLL.Interfaces;
using SAaDLab4_5.DAL.Data.Interfaces;

namespace SAaDLab4_5.BLL.FieldValidator;
public class OrderValidator : IFieldValidator
{
    IUnitOfWork DataBase { get; set; }

    public OrderValidator(IUnitOfWork dataBase)
    {
        DataBase = dataBase;
    }

    delegate bool DateIsFreeDel(DateTime date, int questId);
    delegate bool ValueIsInt(string questId, out int result);
    delegate bool QuestIdValidatorDel(int questId);

    FieldValidatorDel _fieldValidatorDel = null;


    RequiredValidDel _requiredValidDel = null;
    DateValidDel _dateValidDel = null;
    PatternMatchValidDel _patternMatchValidDel = null;

    DateIsFreeDel _dateIsFreeDel = null;
    ValueIsInt _valueIsIntDel = null;
    QuestIdValidatorDel _questIdValidatorDel = null;

    string[] _fieldArray = null;

    //create array only 1 time
    public string[] FieldArray => _fieldArray ??= new string[Enum.GetValues
            (typeof(FieldConstants.OrderField)).Length];

    public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

    public void InitialiseValidatorDelegates()
    {
        _fieldValidatorDel = new(ValidField);

        _requiredValidDel = CommonFieldValidatorFunctions.RequiredFieldValidDel;
        _dateValidDel = CommonFieldValidatorFunctions.DateFieldValidDel;
        _patternMatchValidDel = CommonFieldValidatorFunctions.PatternMatchValidDel;

        _valueIsIntDel = new(IsValueInt);
        _dateIsFreeDel = new(IsDateFree);
        _questIdValidatorDel = new(IsQuestIdValid);
    }

    private bool IsDateFree(DateTime date, int questId)
    {
        TimeSpan toComplete = DataBase.Quests.GetOne(questId).TimeToComplete;

        var orders = from a in DataBase.Orders.GetAll()
                     where (a.Date <= date + toComplete && a.Date >= date) || (a.Date >= date - toComplete && a.Date < date)
                     && a.QuestId == questId
                     select a;

        return orders.Count() is 0;
    }

    private bool IsValueInt(string questId, out int result)
    {
        return Int32.TryParse(questId, out result);
    }

    private bool IsQuestIdValid(int questId)
    {
        int max = DataBase.Quests.GetAll().Count();

        return (questId >= 1 && questId <= max);
    }

    private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
    {

        fieldInvalidMessage = "";

        FieldConstants.OrderField userOrderField = (FieldConstants.OrderField)fieldIndex;

        switch(userOrderField)
        {
            case FieldConstants.OrderField.QuestId:
            fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.OrderField), userOrderField)}{Environment.NewLine}" : "";
            int res = 0;
            fieldInvalidMessage = (fieldInvalidMessage is "" && !_valueIsIntDel(fieldValue, out res)) ? $"You did not enter a valid value for quest id{Environment.NewLine}" : fieldInvalidMessage;
            fieldInvalidMessage = (fieldInvalidMessage is "" && !_questIdValidatorDel(res)) ? $"You did not enter valid number for quest id{Environment.NewLine}" : fieldInvalidMessage;
            break;
            case FieldConstants.OrderField.Date:
            fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.OrderField), userOrderField)}{Environment.NewLine}" : "";
            DateTime validDateTime = new();
            fieldInvalidMessage = (fieldInvalidMessage is "" && !_dateValidDel(fieldValue, out validDateTime)) ? $"You did not enter a valid date{Environment.NewLine}" : fieldInvalidMessage;
            int index = (int)FieldConstants.OrderField.QuestId;
            fieldInvalidMessage = (fieldInvalidMessage is "" && !_dateIsFreeDel(validDateTime, Int32.Parse(fieldArray[index]))) ? $"You date is not free{Environment.NewLine}" : fieldInvalidMessage;
            break;
            case FieldConstants.OrderField.GiftCertificate:
            fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.OrderField), userOrderField)}{Environment.NewLine}" : "";
            fieldInvalidMessage = (fieldInvalidMessage is "" && !_patternMatchValidDel(fieldValue, @"^(True|False)$")) ? $"You must enter \"True\" or \"False\"" : fieldInvalidMessage;
            break;
            default:
            throw new ArgumentException("This field does not exists");
        }

        return (fieldInvalidMessage is "");
    }

    public FieldConstants FieldConstants
    {
        get => default;
        set
        {
        }
    }
}
