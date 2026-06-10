using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Taller_3.Models
{
    public class HorarioGrupoItem : INotifyPropertyChanged
    {
        private bool _isExpanded;

        public HorarioGrupoItem(
            string titulo,
            string espacio,
            DateTime horaInicio,
            DateTime horaFin,
            IEnumerable<HorarioResponseDto> ocurrencias)
        {
            Titulo = titulo;
            Espacio = espacio;
            HoraInicio = horaInicio;
            HoraFin = horaFin;

            var ordenadas = ocurrencias.OrderBy(o => o.FechaInicio).ToList();
            Ocurrencias = new ObservableCollection<HorarioResponseDto>(ordenadas);

            var fechas = ordenadas.Select(o => o.FechaInicio.Date).ToList();
            var primera = fechas.First();
            var ultima = fechas.Last();

            RangoFechas = primera == ultima
                ? primera.ToString("dd/MM/yyyy")
                : $"{primera:dd/MM/yyyy} - {ultima:dd/MM/yyyy}";

            ProximaFecha = fechas.Where(f => f >= DateTime.Today).DefaultIfEmpty(primera).First();
        }

        public string Titulo { get; }
        public string Espacio { get; }
        public DateTime HoraInicio { get; }
        public DateTime HoraFin { get; }
        public ObservableCollection<HorarioResponseDto> Ocurrencias { get; }

        public bool EsRecurrente => Ocurrencias.Count > 1;
        public int CantidadOcurrencias => Ocurrencias.Count;
        public HorarioResponseDto PrimeraOcurrencia => Ocurrencias.FirstOrDefault();
        public string RangoFechas { get; }
        public DateTime ProximaFecha { get; }

        public string EtiquetaRecurrencia =>
            EsRecurrente ? $"Recurrente · {CantidadOcurrencias} fechas" : "Horario único";

        public string TextoExpandir =>
            IsExpanded ? "Ocultar fechas" : $"Ver fechas ({CantidadOcurrencias})";

        public string HorarioTexto =>
            $"{HoraInicio:HH:mm} - {HoraFin:HH:mm}";

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded == value)
                    return;

                _isExpanded = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TextoExpandir));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
