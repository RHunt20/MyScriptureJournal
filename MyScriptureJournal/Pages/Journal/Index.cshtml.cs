using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Journal
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Models.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Models.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<JournalEntry> JournalEntry { get;set; }

        // Search Functionality
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }     
       
        [BindProperty(SupportsGet = true)]
        public string SearchSelect { get; set; }

        //Sorting
        public string BookSort { get; set; }
        public string EntryDateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            var entry = from m in _context.JournalEntry
                         select m;
            //This is Searching
            if (SearchSelect == "Books")
            {
                if (!string.IsNullOrEmpty(SearchString))
                {
                    entry = entry.Where(s => s.Book.Contains(SearchString));
                }
            }
            else if (SearchSelect == "Notes")
                if (!string.IsNullOrEmpty(SearchString))
                {
                    entry = entry.Where(s => s.Notes.Contains(SearchString));
                }

            //This is sorting
            BookSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            EntryDateSort = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    entry = entry.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    entry = entry.OrderBy(s => s.EntryDate);
                    break;
                case "date_desc":
                    entry = entry.OrderByDescending(s => s.EntryDate);
                    break;
                default:
                    entry = entry.OrderBy(s => s.Book);
                    break;
            }

            JournalEntry = await entry.ToListAsync();
        }
    }
}
