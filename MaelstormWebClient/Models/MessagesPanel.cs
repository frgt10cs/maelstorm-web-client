using System.Collections.Generic;
using MaelstormDTO.Responses;

namespace MaelstormWebClient.Models
{
    class MessagesPanel
    {
        public int DialogId { get; set; }
        public List<Message> Messages { get; set; }
    }
}