using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.DataGrid
{
    internal static class LayoutOptionsExtensions
    {
        internal static TextAlignment ToTextAlignment(this LayoutOptions layoutAlignment) => layoutAlignment.Alignment switch
        {
            LayoutAlignment.Start => TextAlignment.Start,
            LayoutAlignment.End => TextAlignment.End,
            _ => TextAlignment.Center,
        };
    }
}
