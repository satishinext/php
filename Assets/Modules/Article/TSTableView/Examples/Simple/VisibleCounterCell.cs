using UnityEngine;
using System.Collections;
using Tacticsoft;
using UnityEngine.UI;
using Module.Article;

namespace Tacticsoft.Examples
{
    //Inherit from TableViewCell instead of MonoBehavior to use the GameObject
    //containing this component as a cell in a TableView
    public class VisibleCounterCell : TableViewCell
    {
        public Text m_rowNumberText;
		public Text m_visibleCountText;
		public DataController dataController;
		[HideInInspector]
		public int cellNumber;
		public void SetRowNumber(int rowNumber) 
        {
			this.GetComponent<ButtonId>().GetIndex(rowNumber);
		//	 m_rowNumberText.text = rowNumber.ToString();
			dataController.AssignData(rowNumber);
        }

        private int m_numTimesBecameVisible;
		public void NotifyBecameVisible() {
			m_numTimesBecameVisible++;
			m_visibleCountText.text = "# rows this cell showed : " + m_numTimesBecameVisible.ToString();
		}
    }
 
}
