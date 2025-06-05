using EwkQxObdWpf.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EwkQxObdWpf.View
{
    /// <summary>
    /// Interaction logic for ContractDetails.xaml
    /// </summary>
    public partial class ContractDetails : Window
    {
        public ContractDetails()
        {
            InitializeComponent();
            ContractDetailPageViewMod mwMod = new ContractDetailPageViewMod();

            DataContext = mwMod;
        }
    }
}
