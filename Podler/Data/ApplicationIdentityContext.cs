﻿using Podler.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Podler.Data
{
    public class ApplicationIdentityContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationIdentityContext(
            DbContextOptions<ApplicationIdentityContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
