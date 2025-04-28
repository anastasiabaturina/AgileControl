﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AgileControl.Client.Feature.Projects;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
        if (memberInfo.Length > 0)
        {
            var attr = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
            if (attr != null)
                return attr.Name ?? enumValue.ToString();
        }
        return enumValue.ToString();
    }
}