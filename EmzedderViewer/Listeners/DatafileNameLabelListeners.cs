using EmzedderViewer.ModelViews;
using System.ComponentModel;
using Label = System.Windows.Controls.Label;

namespace EmzedderViewer.Listeners
{
    /// <summary>
    /// Listeners to update state of DatafileName Label in main window
    /// </summary>
    public class DatafileNameLabelListeners
    {
        private Label _datafileNameLabel;

        public DatafileNameLabelListeners(Label datafileNameLabel, ChromatogramViewModel chromVM)
        {
            _datafileNameLabel = datafileNameLabel;
            chromVM.PropertyChanged += ChromVM_ChromatogramChanged;
        }
        /// <summary>
        /// Detected changes in data file name and updates DatafileLabel text
        /// </summary>
        private void ChromVM_ChromatogramChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = sender as ChromatogramViewModel;
            if (vm == null) return;
            //TODO this moves to its own listener page
            if (e.PropertyName == nameof(ChromatogramViewModel.Datafile))
            {
                _datafileNameLabel.Content = vm.GetDatafileName();
            }
        }

    }
}
