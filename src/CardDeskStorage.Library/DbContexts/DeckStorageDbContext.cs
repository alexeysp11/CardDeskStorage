using Microsoft.EntityFrameworkCore;
using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.DbContexts;

public class DeckStorageDbContext : DbContext
{
    public DeckStorageDbContext(DbContextOptions<DeckStorageDbContext> options) : base(options)
    {
    }

    public DbSet<Deck> Decks { get; set; }
    public DbSet<Card> Cards { get; set; }
}