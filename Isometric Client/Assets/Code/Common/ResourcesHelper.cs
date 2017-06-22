﻿using System.Linq;
using System.Runtime.InteropServices;
using Isometric.Core;

namespace Assets.Code.Common
{
    public static class ResourcesHelper
    {
        public static string ToFormattedString(this Resources resources)
        {
            var result = "";
            var i = 0;
            foreach (var resource in resources.GetResourcesArray().Where(r => r != 0))
            {
                result += resource + " " + ((ResourceType) i++).ToString("F");
            }

            return result;
        }
    }
}