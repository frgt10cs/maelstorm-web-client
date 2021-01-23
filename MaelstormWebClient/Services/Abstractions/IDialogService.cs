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
        IReadOnlyList<Dialog> Dialogs { get; }
        event Action OnDialogsUploaded;
        event Action<Dialog> OnDialogOpened;
        event Action<Dialog> OnDialogStateChanged;
        int UploadCount { get; }
        Task<bool> OpenDialogByInterlocutorIdAsync(long interlocutorId);
        Task UploadDialogsAsync(int offset, int count);
        Task UploadDialogsAsync();
    }
}
