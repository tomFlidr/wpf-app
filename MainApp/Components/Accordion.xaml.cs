using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
		private bool _fistTimeRender = false;
		private double _expanderHeight = 29;

		public Accordion () {
			this.InitializeComponent();
		}

		protected override void OnRender (DrawingContext dc) {
			base.OnRender(dc);
			if (this._fistTimeRender) return;

			RowDefinition rd;
			Expander ex;
			int i = 0;
			foreach (UIElement item in this.Children) {
				if (!(item is Expander)) throw new Exception("Položka není Expander.");
				
				rd = new RowDefinition();
				if (i == this._defaultExpandedIndex) {
					rd.Height = new GridLength(1, GridUnitType.Star);
				} else {
					rd.Height = new GridLength(
						this._expanderHeight, 
						GridUnitType.Pixel
					);
				}
				this.RowDefinitions.Add(rd);

				Grid.SetRow(item, i);

				ex = (Expander)item;
				ex.Expanded += this._handlerProvider(i, false);
				ex.Collapsed += this._handlerProvider(i, true);
				if (i == this._defaultExpandedIndex) {
					ex.IsExpanded = true;
					ex.Tag = i;
				}
				ex.Style = this.FindResource("AccordionExpander") as System.Windows.Style;

				i++;
			}
			this._fistTimeRender = true;
		}
		private RoutedEventHandler _handlerProvider (int i, bool collapsed) {
			return (object o, RoutedEventArgs e) => {
				this.RowDefinitions[this._defaultExpandedIndex].Height = new GridLength(
					this._expanderHeight
				);
				if (collapsed && this.Children.Count > 1) {
					int next = i + 1;
					if (next == this.Children.Count) {
						next = i - 1;
					}
					this._expand(next);
				} else {
					this._expand(i);
				}
				//this.UpdateLayout(); // kvůli stretch alignu to radej prekresluji
			};
		}
		private void _expand (int i) {
			this.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
			(this.Children[i] as Expander).IsExpanded = true; // automaticky rozbalí expander - okamžitě
			this._defaultExpandedIndex = i;
		}

		//public void DispatchEvent (object source, string eventName, EventArgs eventArgs) {
		//	EventInfo eventObject = source.GetType().GetEvent(eventName);


		//	if (eventObject != null) {
		//		IEnumerable<FieldInfo> fis = source.GetType().GetRuntimeFields();
		//		foreach (FieldInfo fi in fis) {

		//			if (fi.Name == eventName + "Event") {
		//				System.Delegate del = fi.GetValue(source) as System.Delegate;

		//				List<System.Delegate> invocationList = del.GetInvocationList().ToList();


		//				foreach (System.Delegate invItem in invocationList) {
		//					invItem.DynamicInvoke(source, eventArgs);

		//				}

		//			}
		//		}

		//	}
		//}
	}
}
