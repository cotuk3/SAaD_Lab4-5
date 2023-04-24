using SAaDLab4_5.BLL.DTO;
using SAaDLab4_5.BLL.FieldValidator;
using SAaDLab4_5.BLL.Interfaces;

namespace SAaDLab4_5.BLL.Services;
public class RegisterCustomer : IRegister
{
    public CustomerDTO CustomerDTO
    {
        get => default;
        set
        {
        }
    }

    public CustomerDTO Register(string[] fields)
    {
        CustomerDTO customer = new()
        {
            FirstName = fields[(int)FieldConstants.CustomerField.FirstName],
            LastName = fields[(int)FieldConstants.CustomerField.LastName],
            EmailAddress = fields[(int)FieldConstants.CustomerField.EmailAddress],
            PhoneNumber = fields[(int)FieldConstants.CustomerField.PhoneNumber],
        };

        return customer;
    }
}
