using System.Collections.Generic;
using Common;
using Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentWebapp.Models;
using ViewModels;

namespace Controllers
{
    public class FacultyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FacultyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> List(string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var faculties = await _unitOfWork.Faculty.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                faculties = await _unitOfWork.Faculty.GetAllBy(f => f.FacultyName.Contains(searchString)
                                       || f.Description.Contains(searchString));
            }
            int pageSize = 3;
            return View(PaginatedList<Faculty>.CreateAsync(faculties, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new FacultyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FacultyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var faculty = new Faculty
                {
                    FacultyName = model.FacultyName,
                    Description = model.Description
                };
                await _unitOfWork.Faculty.Create(faculty);
                await _unitOfWork.SaveChangesAsync();
                TempData["success"] = "Faculty Created Successfully";
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return Content("Faculty Id is null");
            }
            var faculty = await _unitOfWork.Faculty.GetBy(f => f.Id == id);
            if (faculty == null)
            {
                return Content("Faculty Not Found");
            }
            await _unitOfWork.Faculty.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Faculty Deleted Successfully";
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return Content("Faculty Id is null");
            }
            var faculty = await _unitOfWork.Faculty.GetBy(f => f.Id == id);
            if (faculty == null)
            {
                return Content("Faculty Not Found");
            }
            var facultyModel = new FacultyViewModel()
            {
                Id = faculty.Id,
                FacultyName = faculty.FacultyName,
                Description = faculty.Description,
                InitialFacultyName = faculty.FacultyName
            };
            return View(facultyModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FacultyViewModel model)
        {
            if (model == null)
            {
                return Content("Faculty Id is null");
            }
            var checkFaculty = await _unitOfWork.Faculty.CheckExistBy(f => f.Id == model.Id);
            if (!checkFaculty)
            {
                return Content("Faculty Not Found");
            }
            var faculty = new Faculty()
            {
                Id = model.Id,
                FacultyName = model.FacultyName,
                Description = model.Description
            };

            await _unitOfWork.Faculty.Edit(faculty);
            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Faculty Updated Successfully";
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> VerifyFacultyName(string FacultyName, string InitialFacultyName)
        {
            if (FacultyName == InitialFacultyName)
            {
                return Json(true);
            }

            if (await _unitOfWork.Faculty.CheckExistBy(f => f.FacultyName.ToLower() == FacultyName.ToLower()))
            {
                return Json($"Faculty Name: {FacultyName} already exists!");
            }
            return Json(true);
        }
    }
}