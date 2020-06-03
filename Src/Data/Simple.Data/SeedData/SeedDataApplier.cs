// Copyright (c) simple. All rights reserved.

namespace Simple.Data.SeedData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Simple.Data.SeedData.Mappers;
    using Simple.Domain.Entities.Books;
    using Simple.Infrastructure.Entities;
    using Simple.Infrastructure.Repository;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public static class SeedDataApplier
    {
        private const string JSONPATH = "JsonPath";

        public static async Task Apply(IServiceProvider serviceProvider)
        {
            using (var services = serviceProvider.CreateScope())
            {
                var config = services.ServiceProvider.GetService<IConfiguration>();
                var env = services.ServiceProvider.GetService<IHostingEnvironment>();
                var log = services.ServiceProvider.GetService<ILogger<Book>>();
                var uow = services.ServiceProvider.GetService<IUnitOfWork>();
                var bookRepository = services.ServiceProvider.GetService<IRepository<Book, Guid>>();
                var contentRootPath = env.ContentRootPath;
                var jsonPath = config.GetValue<string>(JSONPATH);

                await Apply(log, uow, bookRepository, contentRootPath, jsonPath);
            }
        }

        public static async Task Apply(
            ILogger<Book> log,
            IUnitOfWork uow,
            IRepository<Book, Guid> bookRepository,
            string contentRootPath,
            string jsonPath)
        {
            log.LogInformation("-- Seed Data starting");

            var json = GetFromJson(log, contentRootPath, jsonPath);

            if (json.IsFailure)
            {
                log.LogError("--     Cannot load json");
                throw new Exception("Cannot load json");
            }

            var books = MapFromJson(json.Value, log);

            var booksSuccess = books.Where(x => x.IsSuccess).Select(x => x.Value);
            log.LogInformation($"--     Total books success: {books.Count()}");

            await InsertEntities(bookRepository, log, booksSuccess, GetEntitiesToInsert);

            var result = await uow.SaveChangesAsync();
            log.LogInformation($"--     Entities Saved: {result}");
            log.LogInformation("-- Seed Data finished");
        }

        private static async Task InsertEntities<TEntity, TKey>(IRepository<TEntity, TKey> repository, ILogger log, IEnumerable<TEntity> entities, Func<IRepository<TEntity, TKey>, ILogger, List<TEntity>, Task<List<TEntity>>> func)
            where TEntity : AggregateRoot<TKey>
        {
            if (entities.Any())
            {
                var entitiesToInsert = await func(repository, log, entities.ToList());
                entitiesToInsert.ForEach(repository.Add);
                log.LogInformation($"--     {typeof(TEntity).Name} Added to repository");
            }
            else
            {
                log.LogInformation($"--     Any {typeof(TEntity).Name} to add");
            }
        }

        private static async Task<List<TEntity>> GetEntitiesToInsert<TEntity, TKey>(IRepository<TEntity, TKey> repository, ILogger log, List<TEntity> entitiesFromJson)
            where TEntity : AggregateRoot<TKey>
        {
            var entities = await repository.GetAsync();
            log.LogInformation($"--     Total {typeof(TEntity).Name} in DB: {entities.Count()}");
            var entitiesIds = entities.Select(x => x.Id);
            var entitiesToInsert = entitiesFromJson.Where(x => !entitiesIds.Contains(x.Id));
            log.LogInformation($"--     Total {typeof(TEntity).Name} from json to insert: {entitiesToInsert.Count()}");

            return entitiesToInsert.ToList();
        }

        private static Result<string> GetFromJson(ILogger log, string contentRootPath, string jsonPath)
        {
            var path = $"{contentRootPath}/{jsonPath}";
            log.LogInformation($"--     Path of the seedData: {path}");
            var json = File.ReadAllText(path);
            return Result.Ok(json);
        }

        private static List<Result<Book>> MapFromJson(string json, ILogger log)
        {
            var desserialized = JsonConvert.DeserializeObject<RootJsonDto>(json);
            log.LogInformation($"--     Total desserialized: {desserialized?.Books?.Count()}");
            var books = desserialized.Books.Select(x => x.MapToEntity()).ToList();
            log.LogInformation($"--     Total mapped to entities: {books.Count()}");
            return books;
        }
    }
}
