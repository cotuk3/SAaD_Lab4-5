using SAaDLab4_5.BLL.DTO;

namespace SAaDLab4_5.BLL.Interfaces;
public interface IMakeOrder
{
    OrderDTO MakeOrder(string[] fields);
}
