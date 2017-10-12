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

namespace App.Components {
	public enum AnimationStyle {
		None,
		Fast,
		Slow
	}
	public partial class Accordion: Grid {

		public AnimationStyle AnimationStyle {
			get { return _animationStyle; }
			set { _animationStyle = value; }
		}
		private AnimationStyle _animationStyle;

		public int DefaultExpandedIndex {
			get { return _defaultExpandedIndex; }
			set { _defaultExpandedIndex = value; }
		}
		private int _defaultExpandedIndex = 0;

		public Accordion () {
			InitializeComponent();


		}
	}
}
