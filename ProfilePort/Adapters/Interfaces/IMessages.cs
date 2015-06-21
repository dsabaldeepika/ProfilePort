using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
    public interface IMessages
    {
        List<MessageVM> GetMessages(string userID);
        MessageVM GetMessage(int id);
        Message PostNewMessage(MessageVM newMessage, string userID);
        Message deleteMessage(int id);
    }
}
