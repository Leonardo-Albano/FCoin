﻿using FCoin.Models;
using Microsoft.EntityFrameworkCore;

namespace FCoin
{
    public class FDbContext : DbContext
    {
        public FDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Selector> Selectors { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Validator> Validators { get; set; }
        public DbSet<TransactionLink> TransactionsLink { get; set; }

    }
}
