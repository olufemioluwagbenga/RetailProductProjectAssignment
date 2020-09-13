using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Logic.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }
}
