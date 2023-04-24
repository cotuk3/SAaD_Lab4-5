using SAaDLab4_5.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAaDLab4_5.BLL.Interfaces;
public interface IRegister
{
    CustomerDTO Register(string[] fields);
}
