using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MikiDB.Core;
using MikiDB.API;
using MikiDB.Exceptions;

namespace MikiDB_GUI{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        private static DbManager _manager = DbManager.Instance;
        private static Executor _executor = new Executor(_manager);
        private static readonly string _lb = Environment.NewLine;
        private ObservableCollection<string> _dbList = new ObservableCollection<string>();
        private ObservableCollection<string> _tbList = new ObservableCollection<string>();
        private List<string> _history = new List<string>();
        private string _cached = "";
        private int _histCursor = 0;
        private string _log = "";
        private string _input = "";
        private string _curDbName;
        private string _curTbName;

        public string Log {
            get { return _log; }
            set {
                if(_log != value) {
                    _log = value;
                    NotifyPropertyChanged("Log");
                }
            }
        }
        
        public string Input {
            get { return _input; }
            set {
                if(_input != value) {
                    _input = value;
                    NotifyPropertyChanged("Input");
                }
            }
        }

        public string CurDbName {
            get {
                if (dbList.SelectedItem != null) {
                    _curDbName = dbList.SelectedItem as string;
                }
                return _curDbName;
            }
        }

        public string CurTbName {
            get {
                if (tableList.SelectedItem != null) {
                    _curTbName = tableList.SelectedItem as string;
                }
                return _curTbName;
            }
        }

        public MikiDB.Core.Table Table {
            get {
                try {
                    return _manager.GetDbByName(CurDbName).GetTableByName(CurTbName); 
                }
                catch (BaseDbExc) {
                    return null;
                }
            }
        }

        public MainWindow() {
            InitializeComponent();
            log.DataContext = this;
            input.DataContext = this;
            UpdateDbList();
            input.Focus();
            dbList.ItemsSource = _dbList;
            tableList.ItemsSource = _tbList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string pName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pName));
        }


        private void PrintLine(LogLevel lv, string str) {
            Log += (lv.GetStr() + str + _lb);
        }

        private void PrintLine(string str) {
            PrintLine(LogLevel.INFO, str);
            log.ScrollToEnd();
        }

        private void RunSQL_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (Input != "") {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
		}

		private void RunSQL_Executed(object sender, ExecutedRoutedEventArgs e) {
            PrintLine(LogLevel.ACCEPTED, Input);
            PrintLine(_executor.ParseAndRun(Input).ToString());
            _history.Add(Input);
            _histCursor = 0;
            Input = "";
            Refresh();
        }

        private void GetHistBack_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (_history.Count > _histCursor) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
		}

		private void GetHistBack_Executed(object sender, ExecutedRoutedEventArgs e) {
            if(_histCursor == 0) {
                _cached = Input;
            }
            Input = _history[_history.Count - 1 - _histCursor];
            _histCursor++;
		}

        private void GetHistForw_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if(_histCursor > 0) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
		}

		private void GetHistForw_Executed(object sender, ExecutedRoutedEventArgs e) {
            _histCursor--;
            if(_histCursor == 0) {
                Input = _cached;
            }
            else {
                Input = _history[_history.Count - _histCursor];
            }
		}

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            //todo
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e) {
            //todo
		}

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            //todo
		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e) {
            //todo
		}

        private void Refresh_Click(object sender, RoutedEventArgs e) {
            Refresh();
        }

        private void UpdateDbList() {
            _dbList.ToList().ForEach(s => _dbList.Remove(s));
            _manager.GetDbNames().ForEach(s => _dbList.Add(s));
        } 

        private void UpdateTbList() {
            try {
                _tbList.ToList().ForEach(s => _tbList.Remove(s));
                var names = _manager.GetDbByName(CurDbName).GetTableNames();
                names.ForEach(s => _tbList.Add(s));
            }
            catch(BaseDbExc e) {

            }
        }

        private void UpdateTbData() {
            if (Table == null) {
                return;
            }
            tableGrid.AllowsColumnReorder = true;
            tableGrid.Columns.Clear();
            tableData.DataContext = Table.CopyRows();
            Table.CopyColDefs().Select(kv => {
                var col = new GridViewColumn();
                col.Header = kv.Key;
                col.DisplayMemberBinding = new Binding($"DataCopy[{kv.Key}]");
                return col;
            }).ToList().ForEach(col => tableGrid.Columns.Add(col));
            tableData.ItemsSource = new ObservableCollection<TbRow>(Table.CopyRows());
        }

        private void Refresh() {
            UpdateTbData();
            UpdateTbList();
            UpdateDbList();
        }

        private void dbList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateTbList();
        }

        private void tableList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateTbData();
        }
    }
}
