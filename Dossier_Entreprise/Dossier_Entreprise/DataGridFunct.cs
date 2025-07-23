using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Dossier_Entreprise
{
    class DataGridFunct
    {
        public static string getCellStr(DataGrid datagrid, int row, int column)
        {
            //DataGridCell cell = DataGridCell.GetCell(datagrid, 0, 4);
            DataGridCell cell = DataGridFunct.GetCell(datagrid, row, column);
            TextBlock tb = cell.Content as TextBlock;
            return tb.Text;

        }

        public static DataGridCell GetCell(DataGrid datagrid, int row, int column)
        {
            DataGridRow rowData = GetRow(datagrid, row);
            if (rowData != null)
            {
                DataGridCellsPresenter cellPresenter = GetVisualChild<DataGridCellsPresenter>(rowData);
                DataGridCell cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    datagrid.ScrollIntoView(rowData, datagrid.Columns[column]);
                    cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public static DataGridRow GetRow(DataGrid datagrid, int index)
        {
            DataGridRow row = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                datagrid.UpdateLayout();
                datagrid.ScrollIntoView(datagrid.Items[index]);
                row = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }


        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
