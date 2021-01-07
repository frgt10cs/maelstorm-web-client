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
    public class DialogService:IDialogService
    {
        private List<IObserver<Dialog>> Observers;
        public IObservable<Dialog> Observable;
        private Dialog openedDialog;        
        public Dialog OpenedDialog => openedDialog;

        public DialogService()
        {
            Observers = new List<IObserver<Dialog>>();
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

        public async Task<bool> OpenDialogByInterlocutorIdAsync(long interlocutorId)
        {
            if (openedDialog != null && openedDialog.dialog.InterlocutorId == interlocutorId) return true;
            var dialog = await ApiClient.Instance.Dialogs.GetDialogAsync(interlocutorId);
            if (dialog != null)
            {
                openedDialog = dialog;
                foreach (var observer in Observers)
                    observer.OnNext(openedDialog);
                return true;
            }
            return false;
        }

        public IDisposable Subscribe(IObserver<Dialog> observer)
        {
            Observers.Add(observer);
            return Disposable.Empty;
        }
    }
}
