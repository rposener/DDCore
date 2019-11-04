﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDomain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property("_reviewId").HasColumnName("ReviewId").UseIdentityColumn();
            builder.HasKey("_reviewId");
            builder.Property("_reviewer").HasColumnName("Reviewer");
            builder.Property("_reviewText").HasColumnName("ReviewText");
            builder.Property("_reviewDate").HasColumnName("ReviewDate").HasColumnType("date");
            builder.Property("_rating").HasColumnName("Rating");
        }
    }
}
