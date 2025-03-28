using TeamGenerator2.Models;
using Microsoft.AspNetCore.Mvc;


namespace TeamGenerator2.Controllers;

public class HomeController : Controller
{
    private readonly Random _random;

    // Constructor accepting Random instance from DI
    public HomeController(Random random)
    {
        _random = random;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Groups(Names model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        if (string.IsNullOrEmpty(model.NameInput))
        {
            ModelState.AddModelError(string.Empty, "Name input cannot be empty.");
            return View("Index", model);
        }

        var names = model.NameList;

        if (names.Count < 2)
        {
            ModelState.AddModelError(string.Empty, "Please enter at least two names.");
            return View("Index", model);
        }

        int n = names.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = _random.Next(0, i + 1);
            (names[j], names[i]) = (names[i], names[j]);
        }

        model.NameInput = string.Join("\n", names);

        return View(model);
    }
}
