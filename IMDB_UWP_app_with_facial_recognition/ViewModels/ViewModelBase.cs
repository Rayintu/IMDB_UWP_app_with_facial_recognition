using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace IMDB_UWP_app_with_facial_recognition.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual CoreDispatcher DispatcherObject { get; protected set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected ViewModelBase()
        {
            DispatcherObject = CoreWindow.GetForCurrentThread().Dispatcher;
            SimpleCommandManager.AssignOnPropertyChanged(ref this.PropertyChanged);
            _commandsList = new List<RelayCommand>();
        }

        private List<RelayCommand> _commandsList;

        protected RelayCommand CreateCommand(Action execute)
        {
            return CreateCommand(execute, null);
        }

        protected RelayCommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            var tempCmd = new RelayCommand(execute, canExecute);
            if (_commandsList.Contains(tempCmd))
            {
                return _commandsList[_commandsList.IndexOf(tempCmd)];
            }
            else
            {
                _commandsList.Add(tempCmd);
                return tempCmd;
            }
        }

        public void RemoveCommands()
        {
            for (var i = 0; i < _commandsList.Count; i++)
            {
                _commandsList[i].RemoveCommand();
            }
        }

        protected void ChangeProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(property, value))
            {
                return;
            }
            else
            {
                property = value;

                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChangedEmpty()
        {
            OnPropertyChanged(String.Empty);
        }
    }
}
