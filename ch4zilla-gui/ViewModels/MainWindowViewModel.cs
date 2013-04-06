using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ch4zilla_gui.Helpers;
using ch4zilla_gui.Models;

namespace ch4zilla_gui.ViewModels {
    public class MainWindowViewModel : BaseViewModel {
        #region Properties
		
		private string serverhostname;
		public string ServerHostName { 
			get { return serverhostname; }
            set {
                if (serverhostname != value) {
                    serverhostname = value;
                    RaisePropertyChanged(() => ServerHostName);
                }
            }
		}
		
		private string username;
		public string Username { 
			get { return username; }
            set {
                if (username != value) {
                    username = value;
                    RaisePropertyChanged(() => Username);
                }
            }
		}
		private string password;
		public string Password { 
			get { return password; }
            set {
                if (password != value) {
                    password = value;
                    RaisePropertyChanged(() => Password);
                }
            }
		}

        private ObservableCollection<FileModel> remoteFiles;
        public ObservableCollection<FileModel> RemoteFiles {
            get { return remoteFiles; }
            set {
                if (remoteFiles != value) {
                    remoteFiles = value;
                    RaisePropertyChanged(() => RemoteFiles);
                }
            }
        }

        private ObservableCollection<FileModel> localFiles;
        public ObservableCollection<FileModel> LocalFiles {
            get { return localFiles; }
            set {
                if (localFiles != value) {
                    localFiles = value;
                    RaisePropertyChanged(() => LocalFiles);
                }
            }
        }

        #endregion

        #region Commands

        public ICommand ConnectCommand { get { return new DelegateCommand(OnConnect); } }

        #endregion

        #region Ctor
        public MainWindowViewModel() {
            remoteFiles = new ObservableCollection<FileModel>();
            remoteFiles.Add(new FileModel("nothing here!", "", 0));
        }
        #endregion

        #region Command Handlers
        private void OnConnect() {

        }
        #endregion
    }
}