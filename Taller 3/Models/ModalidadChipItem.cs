using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Taller_3.Models
{
    public class ModalidadChipItem : INotifyPropertyChanged
    {
        private bool _isSeleccionado;

        public ModalidadChipItem(string nombre)
        {
            Nombre = nombre;
        }

        public string Nombre { get; }

        public bool IsSeleccionado
        {
            get => _isSeleccionado;
            set
            {
                if (_isSeleccionado == value)
                    return;

                _isSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
