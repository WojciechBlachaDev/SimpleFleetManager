using System.Windows.Input;

namespace FMS.ViewModels.Common
{
    class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
#pragma warning disable IDE0052 // Usuń nieodczytywane składowe prywatne
        private readonly ICommand showStep2ButtonCommand;
#pragma warning restore IDE0052 // Usuń nieodczytywane składowe prywatne

        public Action AddItems { get; }

        public RelayCommand(Action<object> execute)
#pragma warning disable CS8625 // Nie można przekonwertować literału o wartości null na nienullowalny typ referencyjny.
            : this(execute, null)
#pragma warning restore CS8625 // Nie można przekonwertować literału o wartości null na nienullowalny typ referencyjny.
        {
        }

#pragma warning disable CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
#pragma warning restore CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

#pragma warning disable CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
        public RelayCommand(ICommand showStep2ButtonCommand)
#pragma warning restore CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
        {
            this.showStep2ButtonCommand = showStep2ButtonCommand;
        }

#pragma warning disable CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
        public RelayCommand(Action addItems)
#pragma warning restore CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
        {
            AddItems = addItems;
        }

#pragma warning disable CS8767 // Dopuszczanie wartości null dla typów referencyjnych w typie parametru nie jest zgodne z niejawnie zaimplementowaną składową (prawdopodobnie z powodu atrybutów dopuszczania wartości null).
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
#pragma warning restore CS8767 // Dopuszczanie wartości null dla typów referencyjnych w typie parametru nie jest zgodne z niejawnie zaimplementowaną składową (prawdopodobnie z powodu atrybutów dopuszczania wartości null).

#pragma warning disable CS8612 // Obsługa wartości null dla typów referencyjnych w typie jest niezgodna z niejawnie implementowaną składową.
        public event EventHandler CanExecuteChanged
#pragma warning restore CS8612 // Obsługa wartości null dla typów referencyjnych w typie jest niezgodna z niejawnie implementowaną składową.
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

#pragma warning disable CS8767 // Dopuszczanie wartości null dla typów referencyjnych w typie parametru nie jest zgodne z niejawnie zaimplementowaną składową (prawdopodobnie z powodu atrybutów dopuszczania wartości null).
        public void Execute(object parameter) => _execute(parameter);
#pragma warning restore CS8767 // Dopuszczanie wartości null dla typów referencyjnych w typie parametru nie jest zgodne z niejawnie zaimplementowaną składową (prawdopodobnie z powodu atrybutów dopuszczania wartości null).
    }
}
