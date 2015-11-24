using UnityEngine;
using System.Collections;
using Tacticsoft;
using Module.Article;
using UnityEngine.UI;

namespace Tacticsoft.Examples
{
    //An example implementation of a class that communicates with a TableView
    public class SimpleTableViewController : MonoBehaviour, ITableViewDataSource
    {
        public VisibleCounterCell m_cellPrefab;
        public TableView m_tableView;
		JsonBase jsonBase;
        private int m_numInstancesCreated = 0;
		VisibleCounterCell cellCheck;
		int rowCheck;
        //Register as the TableView's delegate (required) and data source (optional)
        //to receive the calls
        void Start() {
            m_tableView.dataSource = this;
			jsonBase = JsonBase.instance;
        }

     
        #region ITableViewDataSource

        //Will be called by the TableView to know how many rows are in this table
        public int GetNumberOfRowsForTableView(TableView tableView) {
			return jsonBase.totalData;
        }

        //Will be called by the TableView to know what is the height of each row
        public float GetHeightForRowInTableView(TableView tableView, int row) {
            return (m_cellPrefab.transform as RectTransform).rect.height;
        }

        //Will be called by the TableView when a cell needs to be created for display
        public TableViewCell GetCellForRowInTableView(TableView tableView, int row) {
            VisibleCounterCell cell = tableView.GetReusableCell(m_cellPrefab.reuseIdentifier) as VisibleCounterCell;
            if (cell == null) {
                cell = (VisibleCounterCell)GameObject.Instantiate(m_cellPrefab);
                cell.name = "VisibleCounterCellInstance_" + (++m_numInstancesCreated).ToString();
            			
            }
            cellCheck = cell;
            rowCheck = row;
			//cell.SetRowNumber(row);
            Invoke("Display",0.1f);
          //	controlManagerMod3.rowValue.Add(row); ,	
           
            return cell;
        }
		void Display()
		{	
			cellCheck.SetRowNumber(rowCheck);
		}
        #endregion
        #region Table View event handlers

        //Will be called by the TableView when a cell's visibility changed
        public void TableViewCellVisibilityChanged(int row, bool isVisible) {
            //Debug.Log(string.Format("Row {0} visibility changed to {1}", row, isVisible));
            if (isVisible) {
                VisibleCounterCell cell = (VisibleCounterCell)m_tableView.GetCellAtRow(row);
                cellCheck = cell;
                cell.NotifyBecameVisible();
            }
        }
        #endregion
     

    }
}
