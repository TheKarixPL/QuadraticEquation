using Microsoft.AspNetCore.Mvc;
using TheKarixPL.QuadraticEquation.Services;
using TheKarixPL.QuadraticEquation.ViewModels;

namespace TheKarixPL.QuadraticEquation.Controllers;

public class HomeController : Controller
{
    protected readonly GraphService GraphService;

    public HomeController(GraphService graphService)
    {
        GraphService = graphService;
    }
    
    // GET
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public ActionResult StandardForm(StandardFormViewModel f)
        => View("Result", Library.QuadraticEquation.FromStandardForm(f.A, f.B, f.C));

    [HttpGet]
    public ActionResult VertexForm(VertexFormViewModel f)
    {
        var equation = Library.QuadraticEquation.FromVertexForm(f.A, f.P, f.Q);
        return RedirectToAction("StandardForm", new StandardFormViewModel { A = equation.A, B = equation.B, C = equation.C });
    }
    
    [HttpGet]
    public ActionResult FactoredForm(FactoredFormViewModel f)
    {
        var equation = Library.QuadraticEquation.FromFactoredForm(f.A, f.X1, f.X2);
        return RedirectToAction("StandardForm", new StandardFormViewModel { A = equation.A, B = equation.B, C = equation.C });
    }

    [HttpGet]
    public ActionResult Chart(StandardFormViewModel f)
    {
        using var image = new MemoryStream();
        GraphService.GenerateGraph(f.A, f.B, f.C, 100, 100, image);
        return File(image.ToArray(), "image/png");
    }
}