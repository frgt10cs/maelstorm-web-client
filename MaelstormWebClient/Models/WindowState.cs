using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

namespace MaelstormWebClient.Models
{
    public enum Window
    {
        None,
        Dialogs,
        CurrentDialog,        
        Users,
        Settings
    }

    public class WindowState : IObservable<Window>
    {
        private List<IObserver<Window>> Observers;
        public IObservable<Window> Observable;
        private Window openedWindow;
        private Window prevOpenedWindow;
        public Window OpenedWindow => openedWindow;
        
        public WindowState()
        {
            Observers = new List<IObserver<Window>>();
        }

        public bool IsOpened(Window window)
            => openedWindow == window;        

        public void Back()
        {
            OpenWindow(prevOpenedWindow);
            prevOpenedWindow = Window.None;
        }

        public void OpenWindow(Window window)
        {
            if (window == openedWindow) return;
            prevOpenedWindow = openedWindow;
            openedWindow = window;
            foreach (var observer in Observers)
                observer.OnNext(window);
        }

        public void CloseAll()
        {
            OpenWindow(Window.None);
        }

        public IDisposable Subscribe(IObserver<Window> observer)
        {            
            Observers.Add(observer);
            return Disposable.Empty;
        }
    }
}
