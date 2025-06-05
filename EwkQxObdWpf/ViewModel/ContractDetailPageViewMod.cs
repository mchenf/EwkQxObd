using EwkQxObd.Core.Model;
using EwkQxObdWpf.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EwkQxObdWpf.ViewModel
{
    public class ContractDetailPageViewMod
    {
        public ICommand NewContractCommand { get; set; }

        public ContractDetailPageViewMod()
        {
            NewContractCommand = new NewContractCommand();
            iqxContract.ContractNumber = 3322;
            iqxContract.Description = "aahdhdhhdhd";
        }


        private EqoContract iqxContract = new EqoContract();

        public EqoContract IqxContract
        {
            get { return iqxContract; }
            set { iqxContract = value; }
        }







    }
}
