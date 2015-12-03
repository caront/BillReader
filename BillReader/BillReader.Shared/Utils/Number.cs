using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BillReader.Utils
{
    public enum DecimalType
    {
        EN,
        FR,
        ALL
    }

    public enum SymbolCurrency
    {
        EURO,
        DOLLARD,
        POUND,
    }
    public static class Number
    {
        public static bool ContainNumber(this string s)
        {
            return !Regex.IsMatch(s, @"^[^0-9]+$");
        }

        public static bool ContainDouble(this string s, DecimalType type = DecimalType.ALL)
        {
            if (type == DecimalType.EN)
                return s.Contains(".");
            if (type == DecimalType.FR)
                return s.Contains(",");
            return s.Contains(",") || s.Contains(".");
        }

        public static bool ContainSymbolCurrency(this string s, SymbolCurrency symb = SymbolCurrency.DOLLARD)
        {
            if (symb == SymbolCurrency.DOLLARD)
                return s.Contains("$");
            if (symb == SymbolCurrency.POUND)
                return s.Contains("£");
            return s.Contains("€");
        }

    }
}
