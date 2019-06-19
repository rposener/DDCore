using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDCore.Configuration
{
    public class DDBuilder
    {
        /// <summary>
        /// Internal only constructor to Create a Builder Class
        /// </summary>
        /// <param name="services"></param>
        internal DDBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        /// <summary>
        /// Access to the Service Collection
        /// </summary>
        internal IServiceCollection services;
    }
}
