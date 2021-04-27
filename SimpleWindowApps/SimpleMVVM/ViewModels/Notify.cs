using SimpleMVVM.ViewModels.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVVM.ViewModels
{
    class Notify : INotifyPropertyChanged
    {
        public CopyCommand CopyMessageCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null) 
        {
            if (!String.IsNullOrEmpty(name))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public Notify() 
        {
            CopyMessageCommand = new CopyCommand(CopyText);
        }
        public void CopyText()
        {
            TargetText = OriginText;
        }

        private string _OriginText;
        public string OriginText
        {
            get { return _OriginText; }
            set 
            {
                _OriginText = value;
                OnPropertyChanged();
            }
        }

        private string _TargetText;
        public string TargetText
        {
            get { return _TargetText; }
            set
            {
                _TargetText = value;
                OnPropertyChanged();
            }
        }
    }
}
