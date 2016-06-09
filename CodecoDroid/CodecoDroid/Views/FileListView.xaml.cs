using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CodecoDroid.Views
{
    public partial class FileListView : ContentPage, INotifyPropertyChanged
    {
        private ObservableCollection<SavedKeyDictionary> _filesCollection;
        public ObservableCollection<SavedKeyDictionary> FilesCollection
        {
            get { return _filesCollection; }
            set
            {
                if(value != _filesCollection)
                {
                    _filesCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public FileListView ()
        {
            InitializeComponent ();
            this.BindingContext = this;
        }


        public new event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

    }
}
