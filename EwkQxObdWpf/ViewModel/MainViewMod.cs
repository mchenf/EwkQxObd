using EwkQxObd.Core.Model;
using EwkQxObd.Data.TableContract;
using EwkQxObdWpf.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EwkQxObdWpf.ViewModel
{
    public class MainViewMod
    {
        public MainViewMod()
        {
            using EqoSelectTblContract store = new();

            Contracts = store.SelectAllAsContracts();
        }

        public List<EqoContract> Contracts { get; set; }


        public ICommand CheckContractDetailCommand { get; set; } = new CheckContractDetailCommand();
    }
}
