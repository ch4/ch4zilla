using System;
using System.Windows.Media;
using ch4zilla_gui.Helpers;

namespace ch4zilla_gui.Models {
    public class FileModel : NotificationObject {
        public FileModel(string name, string path, UInt64 size) {
            Name = name;
            Path = path;
            Size = size;
        }

        private string _name;
        public string Name {
            get { return _name; }
            set {
                if (_name != value) {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        private string _path;
        public string Path {
            get { return _path; }
            set {
                if (_path != value) {
                    _path = value;
                    RaisePropertyChanged(() => Path);
                }
            }
        }

        private UInt64 _size;
        public UInt64 Size {
            get { return _size; }
            set {
                if (_size != value) {
                    _size = value;
                    RaisePropertyChanged(() => Size);
                }
            }
        }

        public string ToString() {
            return this._name;
        }
    }
}
