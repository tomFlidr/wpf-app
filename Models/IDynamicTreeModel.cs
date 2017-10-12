using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Components {
	public interface IDynamicTreeModel {
		List<IDynamicTreeModel> GetChildsByParentId (string id = "");
		bool GetHashChilds ();
		string GetId ();
		string GetTreeNodeHeader ();
	}
}
