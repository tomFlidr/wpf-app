using Models.Objects;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App.Forms {
	/// <summary>
	/// Interaction logic for Dealerform.xaml
	/// </summary>
	public partial class DealerForm: UserControl {
		public DealerForm () {
			InitializeComponent();
		}
		public DealerForm (Dealer d) {
			InitializeComponent();

			this.DataContext = d;

			this.submitBtn.Click += (o, args) => {
				var dealer = (this.DataContext as Dealer);
				Desharp.Debug.Dump(dealer);
				dealer.Save();
			};
		}

	}
}
