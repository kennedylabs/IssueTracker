using IssueTrackerModels;
using System.Security.Principal;

namespace IssueTrackerWeb.Infrastructure
{
    public class SimpleIdentity : GenericIdentity
    {
        public UserTicketModel Ticket { get; private set; }

        public SimpleIdentity(UserTicketModel ticket) : base(ticket.UserName)
        {
            Ticket = ticket;
        }
    }
}
