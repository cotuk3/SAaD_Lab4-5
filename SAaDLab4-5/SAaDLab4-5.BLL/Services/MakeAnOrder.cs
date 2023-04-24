using SAaDLab4_5.BLL.DTO;
using SAaDLab4_5.BLL.FieldValidator;
using SAaDLab4_5.BLL.Interfaces;

namespace SAaDLab4_5.BLL.Services;
public class MakeAnOrder : IMakeOrder
{
    public OrderDTO OrderDTO
    {
        get => default;
        set
        {
        }
    }

    public OrderDTO MakeOrder(string[] fields)
    {
        OrderDTO order = new()
        {
            Date = DateTime.Parse(fields[(int)FieldConstants.OrderField.Date]),
            QuestId = Int32.Parse(fields[(int)FieldConstants.OrderField.QuestId]),
            GiftCertificate = Boolean.Parse(fields[(int)FieldConstants.OrderField.GiftCertificate]),
        };

        return order;
    }
}
