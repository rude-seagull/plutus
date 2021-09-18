### DbContext Configuration

**Why are the DbSet suffixed with `=null!` ?**

Since this solution is using nullable reference type, the common practice of having uninitialized DbSet properties on context types is problematic, as the compiler will now emit warnings for them.
Below are two solutions fixing this problem, in both cases the DbContext base constructor ensures that all DbSet properties will get initialized, and null will never be observed on them.

```c#
  // First solution, using the null-forgiving operator (!)
  public DbSet<Account> Accounts { get; set; } = null!;
  
  // Second solution as seen in Microsoft documentation
  public DbSet<Account> Accounts { get; set; } => Set<Account>();
```


See [Microsoft Docs](https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset)