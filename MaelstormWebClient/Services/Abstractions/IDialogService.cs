using MaelstormApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaelstormWebClient.Services.Abstractions
{
    public interface IDialogService
    {
        Dialog OpenedDialog { get; }
        IDisposable Subscribe(IObserver<Dialog> observer);
        Task<bool> OpenDialogByInterlocutorIdAsync(long interlocutorId);
    }
}
