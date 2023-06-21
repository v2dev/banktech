using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.DataGrid
{
    /// <summary>
    /// Color Provider interface for Background and Text color of each row.
    /// </summary>
    public interface IColorProvider
    {
        /// <summary>
        /// Determines the Color for the row
        /// </summary>
        /// <param name="rowIndex">Index of the row based on DataSource</param>
        /// <param name="item">Item on the index</param>
        /// <returns>Color for the row</returns>
        Color GetColor(int rowIndex, object item);
    }
}
