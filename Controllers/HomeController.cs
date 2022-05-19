using System.Diagnostics;
using Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using StudentWebapp.Models;
using ViewModels;

namespace StudentWebapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var homeViewModel = new HomeViewModel()
        {
            SubjectCount = await _unitOfWork.Subject.TotalCount(),
            FacultyCount = await _unitOfWork.Faculty.TotalCount()
        };
        return View(homeViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
