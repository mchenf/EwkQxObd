using EwkQxObd.Core.Model;
using EwkQxObdWpf.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EwkQxObdWpf.Command
{
    public class CheckContractDetailCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var contract = parameter as EqoContract;
            if (contract == null)
            {
                return;
            }

            var wnd = new ContractDetails(contract);
            wnd.Show();

        }
    }
}
