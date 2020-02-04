using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ShopDapperData.Profiles
{
    internal class UnderscoreCamelCaseNamingConvention : INamingConvention
    {
        public Regex SplittingExpression => new Regex(@"[\p{Ll}\p{Lu}0-9]+(?=_?)");

        public string SeparatorCharacter => "";

        public string ReplaceValue(Match match) => $"_{match.Value.Substring(0).ToLower()}{match.Value.Substring(1)}";
    }
}
