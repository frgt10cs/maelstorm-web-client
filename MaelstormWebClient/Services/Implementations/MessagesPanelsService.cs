using System;
using System.Collections.Generic;
using System.Linq;
using MaelstormDTO.Responses;
using MaelstormWebClient.Models;
using MaelstormWebClient.Services.Abstractions;

namespace MaelstormWebClient.Services.Implementations
{
    class MessagesPanelsService : IMessagesPanelsService
    {
        public IReadOnlyList<MessagesPanel> MessagesPanels => messagesPanels;
        private List<MessagesPanel> messagesPanels { get; set; }

        public MessagesPanel ActiveMessagesPanel { get; set; }

        public event Action<MessagesPanel> OnMessagesPanelChanged;

        public MessagesPanelsService()
        {
            messagesPanels = new List<MessagesPanel>();
        }

        public void ChangeMessagesPanel(int dialogId)
        {
            ActiveMessagesPanel = messagesPanels.FirstOrDefault(mp => mp.DialogId == dialogId);
        }

        public void AddMessagesPanel(int dialogId, List<Message> messages)
        {
            messagesPanels.Add(new MessagesPanel()
            {
                DialogId = dialogId,
                Messages = messages
            });
        }
    }
}