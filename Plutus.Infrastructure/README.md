### DbContext Configuration

**How do I run EF Core migration command ?**

From your terminal, you must be in the directory of Plutus ASP.NET Core project (Plutus.Api).
Then run the following command : ` dotnet ef migrations add <MigrationName> --output-dir ..\Plutus.Infrastructure\Migrations`

**How do I apply the migration against a database ?**

The recommended way to deploy migrations to a production database is by generating SQL scripts.
This is one of several reasons as to why I don't apply migrations programmatically.
For more information, see [Applying Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli)

**Why is the model snapshot file located in the API assembly ?**

I do not know and I have not been able to found an answer. However it seem safe to manually move this file to the infrastructure layer after each new migration.

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