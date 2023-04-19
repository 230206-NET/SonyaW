using DataAccess;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class FCServices {
    private readonly FlashcardContext _context;

    public FCServices(FlashcardContext context) {
        this._context = context;
    }

    public List<Card> getAllFCs() {
        return this._context.Flashcards.ToList();
    }

    public Card editFC(Card fc) {
        _context.Flashcards.Update(fc);
        _context.SaveChanges();
        return fc;
    }

    public Card removeFC(Card fc) {
        _context.Flashcards.Remove(fc);
        _context.SaveChanges();
        return fc;
    }

    public Card createFC(Card fc) {
        _context.Flashcards.Add(fc);
        _context.SaveChanges();
        return fc;
    }

}