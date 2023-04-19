using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Models;
using Services;
using System.Collections.Generic;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class FlashcardController : ControllerBase
{
    private readonly FCServices _services;

    public FlashcardController(FCServices service)
    {
        _services = service;
    }

    // get all flashcards
    [HttpGet("/")]
    public IEnumerable<Card> Get()
    {
        return _services.getAllFCs();
    }

    // edit a flashcard [ params: Flashcard ]
    [HttpPut("/")]
    public Card edit(Card fc) {
        return _services.editFC(fc);
    }

    // delete a flashcard
    [HttpDelete("/")]
    public Card removeFC(Card fc) {
        return _services.removeFC(fc);
    }

    [HttpPost("/")]
    public Card createFC(Card fc) {
        return _services.createFC(fc);
    }
}
