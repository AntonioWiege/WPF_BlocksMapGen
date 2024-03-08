using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System;
using System.Windows;
namespace WPFGettingInto
{
    public class KeyboardListener : IDisposable
    {
        private readonly Task keyboardTask;

        //Here you can put those keys that you want to capture
        private readonly List<KeyState> numericKeys = new List<KeyState>
    {
        new KeyState(Key.Q),
        new KeyState(Key.W),
        new KeyState(Key.E),
        new KeyState(Key.A),
        new KeyState(Key.S),
        new KeyState(Key.D)
    };

        private bool isRunning = true;

        public KeyboardListener()
        {
            keyboardTask = new Task(StartKeyboardListener);
            keyboardTask.Start();
        }

        private void StartKeyboardListener()
        {
            while (isRunning)
            {
                Thread.Sleep(15);
                if (Application.Current != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Application.Current.Windows.Count > 0)
                        {
                            foreach (var keyState in numericKeys)
                            {
                                if (Keyboard.IsKeyDown(keyState.Key))
                                {
                                    keyState.IsPressed = true;
                                }

                                if (Keyboard.IsKeyToggled(keyState.Key) && !keyState.IsPressed) //
                                {
                                    KeyboardDownEvent?.Invoke(
                                        null, new KeyEventArgs(Keyboard.PrimaryDevice,
                                        PresentationSource.FromDependencyObject(Application.Current.Windows[0]),
                                        0, keyState.Key));
                                }

                                if (Keyboard.IsKeyUp(keyState.Key))
                                {
                                    keyState.IsPressed = false;
                                }
                            }
                        }
                    });
                }
            }
        }

        public event KeyEventHandler KeyboardDownEvent;

        /// <summary>
        /// Состояние клавиши
        /// </summary>
        private class KeyState
        {
            public KeyState(Key key)
            {
                this.Key = key;
            }

            public Key Key { get; }
            public bool IsPressed { get; set; }
        }

        public void Dispose()
        {
            isRunning = false;
            keyboardTask.Dispose();
        }
    }
}