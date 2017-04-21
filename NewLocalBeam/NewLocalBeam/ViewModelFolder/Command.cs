using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLocalBeam.ModelFolder;
using NewLocalBeam.ViewModelFolder;
using System.Windows.Input;

namespace NewLocalBeam
{
    class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ViewModelBase ViewModel { get; set; }

        public Command(ViewModelBase viewModelBase)
        {
            this.ViewModel = viewModelBase;

        }

        public bool CanExecute(object parameter)
        {

            return true;
            //throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {

            this.ViewModel.RunMethod();
            //throw new NotImplementedException();
        }
    }
}
