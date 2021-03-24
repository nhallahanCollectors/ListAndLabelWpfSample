using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListAndLabelWpfSample
{
    public class ViewModel : INotifyPropertyChanged
    {
        private PrinterService _printerService;
        private string filePath;

        public ViewModel()
        {
            _printerService = new PrinterService();


            Templates = new ObservableCollection<string>( _printerService.GetLabels()?.ToList());
            Printers = new ObservableCollection<string>(_printerService.GetPrinters()?.ToList());

            if(Templates?.Count > 0)
                SelectedTemplate = Templates[0];

            if (Printers?.Count > 0)
                SelectedPrinter = Printers[0];

            filePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase),"LabelTemplates\\").Replace("file:\\","");
        }

        #region props
        private ObservableCollection<string> _templates;
        public ObservableCollection<string> Templates
        {
            get => _templates;
            set
            {
                _templates = value;
                NotifyPropertyChanged(nameof(Templates));
            }
        }
        private string _selectedTemplate;
        public string SelectedTemplate
        {
            get => _selectedTemplate;
            set
            {
                _selectedTemplate = value;
                NotifyPropertyChanged(nameof(SelectedTemplate));
            }
        }

        private ObservableCollection<string> _printers;
        public ObservableCollection<string> Printers
        {
            get => _printers;
            set
            {
                _printers = value;
                NotifyPropertyChanged(nameof(Printers));
            }
        }

        private string _selectedPrinter;
        public string SelectedPrinter
        {
            get => _selectedPrinter;
            set
            {
                _selectedPrinter = value;
                NotifyPropertyChanged(nameof(SelectedPrinter));
            }
        }

        private bool _printWithOrderLabel = true;
        public bool PrintWithOrderLabel
        {
            get => _printWithOrderLabel;
            set
            {
                _printWithOrderLabel = value;
                NotifyPropertyChanged(nameof(PrintWithOrderLabel));
            }
        }

        //private ObservableCollection<FlipDataSource> _printItems;
        //public  ObservableCollection<FlipDataSource> PrintItems
        //{
        //    get => _printItems;
        //    set
        //    {
        //        _printItems = value;
        //        NotifyPropertyChanged(nameof(PrintItems));
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region commands
        private ICommand _printCommand;
        public ICommand PrintCommand => _printCommand ?? (_printCommand = new CommandHandler(() => Print(), true));

        private ICommand _designCommand;
        public ICommand DesignCommand => _designCommand ?? (_designCommand = new CommandHandler(() => Design(), true));
        #endregion

        #region methods

        private void Print()
        {
            _printerService.PrintListAndLabel(GenerateItems(PrintWithOrderLabel), filePath, SelectedTemplate, SelectedPrinter);
        }

        private void Design()
        {
            _printerService.DesignListAndLabel(GenerateItems(PrintWithOrderLabel), filePath, SelectedTemplate, SelectedPrinter);
        }

        private List<FlipDataSource> GenerateItems(bool withOrderFlip)
        {
            if (withOrderFlip)
            {

                return new List<FlipDataSource>()
                {
                    new FlipDataSource //Order Info / Box flip
                    {
                        Line1 = "21360313/15d",
                        Line2 = "3",
                        Line3 = "R075 -S 04/06/21",
                        Line4 = "Raw+40-REG++",
                        Line5 = "65465456 \\ 075",
                        Line6 = "0.1000",
                        Line7 = "Domestic Shipping",
                        Line0 = "21360313",//barcode
                        MintError = string.Empty,
                        Holdered = true,
                        FlipTypeEnum = FlipType.Box
                    },
                    new FlipDataSource //item flip
                    {
                        Line1 = "2006",
                        MintError = string.Empty,
                        Line2 = "$50",
                        Line3 = "PCGS MS65",
                        Line4 = "American Buffalo",
                        Line5 = ".9999 Fine Gold",
                        Line6 = string.Empty,
                        Line7 = "9999.65/34025610",
                        Line0 = "9999.65/34025610",
                        Holdered = true,
                        FlipTypeEnum = FlipType.ItemFlip
                    },
                    new FlipDataSource //item flip
                    {
                        Line1 = "2006",
                        MintError = string.Empty,
                        Line2 = "$50",
                        Line3 = "PCGS MS65",
                        Line4 = "American Buffalo",
                        Line5 = ".9999 Fine Gold",
                        Line6 = string.Empty,
                        Line7 = "9999.65/34025611",
                        Line0 = "9999.65/34025611",
                        Holdered = true,
                        FlipTypeEnum = FlipType.ItemFlip
                    },
                    new FlipDataSource
                    {
                        Line1 = "21360313/15d",
                        Line2 = "3",
                        Line3 = string.Empty,
                        Line4 = "Sealers Flip",
                        Line5 = "5646546",
                        Line6 = "075",
                        Line7 = string.Empty,
                        MintError = string.Empty,
                        Holdered = true,
                        Line0 = "21360313"
                    }
                };
            }
            else
            {
                return new List<FlipDataSource>()
                {
                    new FlipDataSource //Order Info / Box flip
                    {
                        Line1 = "Ignore this item",
                        Line2 = "3",
                        Line3 = "Data changed to",
                        Line4 = "cause FlipType = 6Line",
                        Line5 = "65465456 \\ 075",
                        Line6 = string.Empty,
                        Line7 = "Domestic Shipping",
                        Line0 = "21360313",//barcode
                        MintError = string.Empty,
                        Holdered = true,
                        FlipTypeEnum = FlipType.ItemFlip
                    },

                    new FlipDataSource //item flip
                    {
                        Line1 = "2006",
                        MintError = string.Empty,
                        Line2 = "$50",
                        Line3 = "PCGS MS65",
                        Line4 = "American Buffalo",
                        Line5 = ".9999 Fine Gold",
                        Line6 = string.Empty,
                        Line7 = "9999.65/34025610",
                        Line0 = "9999.65/34025610",
                        Holdered = true,
                        FlipTypeEnum = FlipType.ItemFlip
                    },
                    new FlipDataSource //item flip
                    {
                        Line1 = "2006",
                        MintError = string.Empty,
                        Line2 = "$50",
                        Line3 = "PCGS MS65",
                        Line4 = "American Buffalo",
                        Line5 = ".9999 Fine Gold",
                        Line6 = string.Empty,
                        Line7 = "9999.65/34025611",
                        Line0 = "9999.65/34025611",
                        Holdered = true,
                        FlipTypeEnum = FlipType.ItemFlip
                    },
                    new FlipDataSource
                    {
                        Line1 = "21360313/15d",
                        Line2 = "3",
                        Line3 = string.Empty,
                        Line4 = "Sealers Flip",
                        Line5 = "5646546",
                        Line6 = "075",
                        Line7 = string.Empty,
                        MintError = string.Empty,
                        Holdered = true,
                        Line0 = "21360313"
                    }

                };
            }
        }
        #endregion
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
