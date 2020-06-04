// Copyright (c) Simple. All rights reserved.

namespace Simple.Infrastructure.Data.UpdateEntries
{
    using System;
    using System.Linq;
    using Simple.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public static class UpdateEntriesByInterface
    {
        public static void UpdateTimestamp(this ChangeTracker changeTracker)
        {
            const string ModificationProperty = "Modified";

            UpdateDateInEntry<IModificationDate>(changeTracker, ModificationProperty, EntityState.Added, EntityState.Modified);
        }

        public static void UpdateCreationDate(this ChangeTracker changeTracker)
        {
            const string ModificationProperty = "Created";

            UpdateDateInEntry<ICreationDate>(changeTracker, ModificationProperty, EntityState.Added);
        }

        private static void UpdateDateInEntry<T>(ChangeTracker changeTracker, string propertyName, params EntityState[] states)
        {
            var entries = changeTracker.Entries().Where(p =>
                                                            states.Contains(p.State)
                                                            && p.Entity.GetType().Assembly.DefinedTypes.Any(x => typeof(T).IsAssignableFrom(x))
                                                            && p.Metadata.FindProperty(propertyName) != null);
            foreach (var entry in entries)
            {
                entry.Property(propertyName).CurrentValue = DateTimeOffset.UtcNow;
            }
        }
    }
}
