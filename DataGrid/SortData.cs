﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.DataGrid
{
    /// <summary>
    /// Creates SortData for DataGrid
    /// </summary>
    [TypeConverter(typeof(SortDataTypeConverter))]
    public sealed class SortData
    {
        public static implicit operator SortData(int index) => new()
        {
            Index = Math.Abs(index),
            Order = index < 0 ? SortingOrder.Descendant : SortingOrder.Ascendant
        };

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is SortData other)
            {
                return other.Index == Index && other.Order == Order;
            }

            return false;
        }

        #region ctor

        public SortData()
        { }

        public SortData(int index, SortingOrder order)
        {
            Index = index;
            Order = order;
        }

        #endregion ctor

        #region Properties

        /// <summary>
        /// Sorting order for the column
        /// </summary>
        public SortingOrder Order { get; set; }

        /// <summary>
        /// Column Index to sort
        /// </summary>
        public int Index { get; set; }

        #endregion Properties

        /// <inheritdoc/>
        public override int GetHashCode() => throw new NotImplementedException();
    }
}
