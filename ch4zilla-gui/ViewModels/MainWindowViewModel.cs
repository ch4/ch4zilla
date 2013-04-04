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

        #endregion

        #region Commands

        public ICommand ConnectCommand { get { return new DelegateCommand(OnConnect); } }

        #endregion

        #region Ctor
        public MainWindowViewModel() {
        }
        #endregion

        #region Command Handlers
        private void OnConnect() {
        }
        #endregion
    }
}