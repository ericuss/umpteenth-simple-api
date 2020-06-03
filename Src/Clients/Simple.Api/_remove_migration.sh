
echo Executing: "$0"

context="Simple.Data.Contexts.Context"
project="../../Data/Simple.Data/"

echo parameters... context: $context, project: $project

dotnet ef migrations remove -c $context -p $project