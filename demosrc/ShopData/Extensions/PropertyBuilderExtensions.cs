using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopData.Configuration;
using ShopDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopData
{
    /// <summary>
    /// Extensions to Provide Value Type Conversions
    /// </summary>
    public static class PropertyBuilderExtensions
    {
        /// <summary>
        /// Converts from <seealso cref="Date"></seealso> and <seealso cref="DateTime"/>
        /// </summary>
        static ValueConverter<Date, DateTime> DateConverter = new ValueConverter<Date, DateTime>(date => date.Value, dateTime => new Date(dateTime));

        /// <summary>
        /// Extension Method to Configure a Property that is a <seealso cref="Date"/> ValueObject
        /// </summary>
        /// <param name="propertyBuilder">Property to Configure</param>
        /// <returns></returns>
        public static PropertyBuilder<Date> IsValueType(this PropertyBuilder<Date> propertyBuilder)
        {
            return propertyBuilder.HasColumnType("date").HasConversion(DateConverter);
        }

    }
}
