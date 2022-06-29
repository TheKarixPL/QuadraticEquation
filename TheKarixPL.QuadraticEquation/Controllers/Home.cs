using Microsoft.AspNetCore.Mvc;
using TheKarixPL.QuadraticEquation.Services;
using TheKarixPL.QuadraticEquation.ViewModels;

namespace TheKarixPL.QuadraticEquation.Controllers;

public class HomeController : Controller
{
    private readonly GraphService GraphService;

    public HomeController(GraphService graphService)
    {
        GraphService = graphService;
    }
    
    // GET
    /// <summary>
    /// Get main page
    /// </summary>
    /// <returns>Action result with main page</returns>
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
    
    /// <summary>
    /// Get standard form result
    /// </summary>
    /// <param name="f">Standard form ViewModel</param>
    /// <returns>Result view</returns>
    [HttpGet]
    public ActionResult StandardForm(StandardFormViewModel f)
        => View("Result", Library.QuadraticEquation.FromStandardForm(f.A, f.B, f.C));
    
    /// <summary>
    /// Get vertex form result
    /// </summary>
    /// <param name="f">Vertex form ViewModel</param>
    /// <returns>Result view</returns>
    [HttpGet]
    public ActionResult VertexForm(VertexFormViewModel f)
    {
        var equation = Library.QuadraticEquation.FromVertexForm(f.A, f.P, f.Q);
        return RedirectToAction("StandardForm", new StandardFormViewModel { A = equation.A, B = equation.B, C = equation.C });
    }
    
    /// <summary>
    /// Get factored form result
    /// </summary>
    /// <param name="f">Factored form ViewModel</param>
    /// <returns>Result view</returns>
    [HttpGet]
    public ActionResult FactoredForm(FactoredFormViewModel f)
    {
        var equation = Library.QuadraticEquation.FromFactoredForm(f.A, f.X1, f.X2);
        return RedirectToAction("StandardForm", new StandardFormViewModel { A = equation.A, B = equation.B, C = equation.C });
    }
    
    /// <summary>
    /// Get chart image
    /// </summary>
    /// <param name="f">Standard form ViewModel</param>
    /// <returns>Chart image</returns>
    [HttpGet]
    public ActionResult Chart(StandardFormViewModel f)
    {
        using var image = new MemoryStream();
        GraphService.GenerateGraph(f.A, f.B, f.C, 100, 100, image);
        return File(image.ToArray(), "image/png");
    }
}