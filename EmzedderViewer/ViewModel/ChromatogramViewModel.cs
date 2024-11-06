using EmzedderViewer.Commands;
using System.Windows.Input;
using Emzedder.Datafile;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace EmzedderViewer.ModelViews
{
    public class ChromatogramViewModel : INotifyPropertyChanged
    {
        public ICommand OpenDataFileCommand { get; }
        private ThermoDatafile _datafile;
        private ChromDatapoint[] _currentChrom;

        private ChromatogramType _selectedChromatogramType;
        public ObservableCollection<ChromatogramType> ChromatogramTypeOptions { get; } =
        [
            ChromatogramType.BPC,
            ChromatogramType.TIC,
            ChromatogramType.Unfiltered
        ];
        public ChromatogramViewModel()
        {
            OpenDataFileCommand = new RelayCommand(OpenDataFile);
            SelectedChromatogramType = ChromatogramType.BPC;
        }
        public int GetNearestScanNumber(double retentionTime)
        {
            return _currentChrom.OrderBy(d => Math.Abs(d.RetentionTime - retentionTime))
                                .First()
                                .Scan;
        }
        public ThermoDatafile Datafile
        {
            get => _datafile;
            set
            {
                _datafile = value;
                OnPropertyChanged(nameof(Datafile));
            }
        }
        public ChromatogramType SelectedChromatogramType
        {
            get => _selectedChromatogramType;
            set
            {
                _selectedChromatogramType = value;
                SetChromatogram();
                OnPropertyChanged(nameof(SelectedChromatogramType));
            }
        }
        public ChromDatapoint[] CurrentChrom
        {
            get => _currentChrom;
            set
            {
                _currentChrom = value;
                OnPropertyChanged(nameof(CurrentChrom));
            }
        }
        public double[] GetCurrentChromXData()
        {
            return CurrentChrom.Select(d => d.RetentionTime).ToArray();
        }
        public double[] GetCurrentChromYData()
        {
            return CurrentChrom.Select(d => d.Intensity).ToArray();
        }
        public void OpenDataFile()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                Datafile = new ThermoDatafile(filePath);
                SetChromatogram();

            }
        }
        private void SetChromatogram()
        {
            if (Datafile != null)
            {
                switch (_selectedChromatogramType)
                {
                    case ChromatogramType.BPC:
                        CurrentChrom = Datafile.GetBasePeakChromatogram();
                        break;
                    case ChromatogramType.TIC:
                        CurrentChrom = Datafile.GetUnfilteredChromatogram();
                        break;
                    case ChromatogramType.Unfiltered:
                        CurrentChrom = Datafile.GetUnfilteredChromatogram();
                        break;
                    default:
                        break;
                }
            }
        }
        public string GetDatafileName()
        {
            if (Datafile == null)
            {
                return "Choose a datafile...";
            }
            Console.WriteLine(Datafile.FilePath);
            return Datafile.FilePath!.Split(@"\").Last();
        }
        public MSDatapoint[] GetMsSpectrum(int scanNumber)
        {
            return _datafile.GetMassSpectrum(scanNumber);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
    public enum ChromatogramType
    {
        BPC,
        TIC,
        Unfiltered
    }

}
