﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.DataGrid
{
    internal static class ReflectionUtils
    {
        private const char PropertyOfOp = '.';

        public static object GetValueByPath(object obj, string path)
        {
            if (obj is null || string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            var tokens = path.Split(PropertyOfOp);

            var result = obj;

            foreach (var token in tokens)
            {
                if (result is null)
                {
                    break;
                }

                result = GetPropertyValue(result, token);
            }

            return result;
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(obj)[propertyName];

            if (propertyDescriptor is null)
            {
                return null;
            }

            return propertyDescriptor.GetValue(obj);
        }
    }
}
