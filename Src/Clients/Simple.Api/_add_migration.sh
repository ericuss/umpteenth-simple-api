
echo Executing: "$0"

migration=$1
context="Simple.Data.Contexts.LibraryContext"
project="../../Data/Simple.Data/"
migrationsPath="Contexts/Migrations"

echo parameters... migration: $migration, context: $context, project: $project, migrationsPath: $migrationsPath

dotnet ef migrations add $migration -c $context -o $migrationsPath -p $project