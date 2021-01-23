using System;
using System.Collections.Generic;
using MaelstormWebClient.Models;

namespace MaelstormWebClient.Services.Abstractions
{
    interface IMessagesPanelsService
    {
        IReadOnlyList<MessagesPanel> MessagesPanels { get; }
        MessagesPanel ActiveMessagesPanel { get; }
        event Action<MessagesPanel> OnMessagesPanelChanged;
    }
}