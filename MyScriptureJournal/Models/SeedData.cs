using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MyScriptureJournal.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyScriptureJournalContext>>()))
            {
                // Look for any movies.
                if (context.JournalEntry.Any())
                {
                    return;   // DB has been seeded
                }

                context.JournalEntry.AddRange(
                    new JournalEntry
                    {
                        Title = "Sacrament Prayers",
                        EntryDate = DateTime.Parse("2019-11-01"),
                        Book = "Doctrine and Covenants",
                        Reference = "20: 77, 79",
                        Notes = "Sacrament",
                    },

                    new JournalEntry
                    {
                        Title = "Lord hath Commanded",
                        EntryDate = DateTime.Parse("2019-10-31"),
                        Book = "1 Nephi",
                        Reference = "3: 7",
                        Notes = "I will go, I will do",
                    },

                    new JournalEntry
                    {
                        Title = "Service of your God",
                        EntryDate = DateTime.Parse("2019-10-29"),
                        Book = "Mosiah",
                        Reference = "2: 17",
                        Notes = "Serving others brings us closer to God",
                    },

                    new JournalEntry
                    {
                        Title = "Seek ye for the Kingdom of God",
                        EntryDate = DateTime.Parse("2019-11-02"),
                        Book = "Jacob",
                        Reference = "2: 18-19",
                        Notes = "Kingdom of God",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}