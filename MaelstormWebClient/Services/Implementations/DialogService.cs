using MaelstormApi;
using MaelstormApi.Models;
using MaelstormWebClient.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace MaelstormWebClient.Services.Implementations
{
    public class DialogService : IDialogService
    {
        private ApiClient api;
        private Dialog openedDialog;
        public Dialog OpenedDialog => openedDialog;

        public IReadOnlyList<Dialog> Dialogs => _dialogs.AsReadOnly();
        private List<Dialog> _dialogs;
        public int UploadCount { get; set; }

        public event Action OnDialogsUploaded;
        public event Action<Dialog> OnDialogOpened;
        public event Action<Dialog> OnDialogStateChanged;
        public event Action<MaelstormDTO.Responses.Message> OnNewMessage;

        public DialogService()
        {
            _dialogs = new List<Dialog>();
            api = ApiClient.Instance;
            api.MessageNotificationService.OnNewMessage += AddNewMessage;
            UploadCount = 20;
        }

        //public async Task<bool> OpenDialogAsync(long dialogId)
        //{
        //    if (openedDialog.dialog.Id == dialogId) return true;
        //    var dialog = await ApiClient.Instance.Dialogs.GetDialogAsync(dialogId);
        //    if (dialog != null)
        //    {
        //        openedDialog = dialog;
        //        foreach (var observer in Observers)
        //            observer.OnNext(openedDialog);
        //        return true;
        //    }            
        //    return false;
        //}

        private Task AddNewMessage(Message message)
        {
            var dialog = Dialogs.FirstOrDefault(d=>d.dialog.Id == message.DialogId);
            if(dialog == null){                
                // upload dialog
                dialog = await api.Dialogs.GetDialogAsync(message);
            }
            dialog.Messages.Add(message);
        }

        public async Task<bool> OpenDialogByInterlocutorIdAsync(long interlocutorId)
        {
            if (openedDialog != null && openedDialog.dialog.InterlocutorId == interlocutorId) return true;
            Dialog dialog = _dialogs.FirstOrDefault(d => d.dialog.InterlocutorId == interlocutorId);
            if (dialog == null) dialog = await api.Dialogs.GetDialogAsync(interlocutorId);
            if (dialog != null)
            {
                openedDialog = dialog;
                OnDialogOpened?.Invoke(dialog);
                return true;
            }
            return false;
        }

        public async Task UploadDialogsAsync(int offset, int count)
        {
            _dialogs.AddRange(await api.Dialogs.GetDialogsAsync(offset, count));
            OnDialogsUploaded?.Invoke();
        }

        public async Task UploadDialogsAsync()
        {
            await UploadDialogsAsync(_dialogs.Count(), UploadCount);
        }
    }
}
