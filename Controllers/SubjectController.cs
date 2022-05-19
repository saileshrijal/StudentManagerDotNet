using Common;
using Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using StudentWebapp.Models;
using ViewModels;

namespace Controllers
{
    public class SubjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectController(IUnitOfWork unitOfWork)
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
            var subjects = await _unitOfWork.Subject.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                subjects = await _unitOfWork.Subject.GetAllBy(f => f.SubjectName.Contains(searchString)
                                       || f.Description.Contains(searchString));
            }
            int pageSize = 3;
            return View(PaginatedList<Subject>.CreateAsync(subjects, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SubjectViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subject = new Subject
                {
                    SubjectName = model.SubjectName,
                    Description = model.Description
                };
                await _unitOfWork.Subject.Create(subject);
                await _unitOfWork.SaveChangesAsync();
                TempData["success"] = "Subject Created Successfully";
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return Content("Subject Id is null");
            }
            var subject = await _unitOfWork.Subject.GetBy(f => f.Id == id);
            if (subject == null)
            {
                return Content("Subject Not Found");
            }
            await _unitOfWork.Subject.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Subject Deleted Successfully";
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return Content("Subject Id is null");
            }
            var subject = await _unitOfWork.Subject.GetBy(f => f.Id == id);
            if (subject == null)
            {
                return Content("Subject Not Found");
            }
            var subjectModel = new SubjectViewModel()
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
                Description = subject.Description,
                InitialSubjectName = subject.SubjectName
            };
            return View(subjectModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SubjectViewModel model)
        {
            if (model == null)
            {
                return Content("Subject Id is null");
            }
            var checkSubject = await _unitOfWork.Subject.CheckExistBy(f => f.Id == model.Id);
            if (!checkSubject)
            {
                return Content("Subject Not Found");
            }
            var subject = new Subject()
            {
                Id = model.Id,
                SubjectName = model.SubjectName,
                Description = model.Description
            };

            await _unitOfWork.Subject.Edit(subject);
            await _unitOfWork.SaveChangesAsync();
            TempData["success"] = "Subject Updated Successfully";
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> VerifySubjectName(string SubjectName, string InitialSubjectName)
        {
            if (SubjectName == InitialSubjectName)
            {
                return Json(true);
            }

            if (await _unitOfWork.Subject.CheckExistBy(f => f.SubjectName.ToLower() == SubjectName.ToLower()))
            {
                return Json($"Subject Name: {SubjectName} already exists!");
            }
            return Json(true);
        }

    }
}