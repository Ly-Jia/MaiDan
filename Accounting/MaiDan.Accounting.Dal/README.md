## Database migration

Create a new migration (= new database schema version):

```dotnet ef migrations add <MigrationName>```

This adds the needed files to the Migrations folder.

Create or migrate the database (already done automatically when starting the MaiDan API):

```dotnet ef database update```

More information: https://docs.microsoft.com/fr-fr/ef/core/managing-schemas/migrations/