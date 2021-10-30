### DbContext Configuration

**How do I use a separate migrations project ?**

Open a terminal, navigate to your startup project (in this case Plutus/Plutus.Api).
Then use this command where `--project` is followed by your migrations destination (in this case Plutus/Plutus.Infrastructure).
`dotnet ef migrations add <MigrationName> --project ../Plutus.Infrastructure`

See [Using a Separate Migrations Project](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli)

**Why are the DbSet suffixed with `=null!` ?**

Since this solution is using nullable reference type, the common practice of having uninitialized DbSet properties on context types is problematic, as the compiler will now emit warnings for them.
Below are two solutions fixing this problem, in both cases the DbContext base constructor ensures that all DbSet properties will get initialized, and null will never be observed on them.

```c#
  // First solution, using the null-forgiving operator (!)
  public DbSet<Account> Accounts { get; set; } = null!;
  
  // Second solution as seen in Microsoft documentation
  public DbSet<Account> Accounts { get; set; } => Set<Account>();
```


See [DbContext and DbSet](https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset)