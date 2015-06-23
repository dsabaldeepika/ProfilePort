using ProfilePort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilePort.Adapters.DataAdapters
{
    public class Messages:IMessages
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        List<MessageVM> IMessages.GetMessages(string DashboardId)
        {
            var q = db.Messages.Where(m =>m.DashboardId==DashboardId )
                    .Select(m => new MessageVM
                    {
                        MessageId = m.MessageId,
                        FromId = m.FromId,
                        ToId = m.ToId,
                        Subject = m.Subject,
                        Content = m.Content,
                        DateCreated = m.DateCreated,
                        DateRead = m.DateRead,
                        From= m.From,
                        To= m.To
                    });

            var result = q.ToList();
            var count = result.Count;
            return result;
        }

        MessageVM IMessages.GetMessage(int id)
        {
            var q = db.Messages.Where(m => m.MessageId == id)
                    .Select(m => new MessageVM
                    {
                        MessageId = m.MessageId,
                        FromId = m.FromId,
                        ToId = m.ToId,
                        Subject = m.Subject,
                        Content = m.Content,
                        DateCreated = m.DateCreated,
                        DateRead = m.DateRead,
                        From = m.From,
                        To = m.To
                    });
            return q.FirstOrDefault();
        }

        DataModel.Message IMessages.PostNewMessage(MessageVM m, string DashboardId)
        {
            Message message = new Message();
            //User user = db.Users.Where(u => u.Id == DashboardId).FirstOrDefault();
                
                 message.DashboardId = DashboardId;
                 message.FromId = m.FromId;
                 message.ToId = m.ToId;
                 message.Subject = m.Subject;
                 message.Content = m.Content;
                 message.DateCreated = m.DateCreated;
                 message.DateRead = m.DateRead;
                 message.From = m.From;
                 message.To = m.To;
                 db.SaveChanges();
                 return message;
        }

        DataModel.Message IMessages.deleteMessage(int id)
        {
            Message message = new Message();
            message = db.Messages.Where(u => u.MessageId == id).FirstOrDefault();
            db.Messages.Remove(message);
            db.SaveChanges();
            return message;
        }
    }
}