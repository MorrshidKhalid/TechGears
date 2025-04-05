using Microsoft.AspNetCore.Mvc;
using TechGears.Web.Models;
using TechGears.Web.Models.Lead;
using TechGears.Web.Service.IService;
using Newtonsoft.Json;

namespace TechGears.Web.Controllers;
public class ContactController : Controller
{
    private readonly ILeadService _leadService;
    public ContactController(ILeadService leadService)
    {
        _leadService = leadService;
    }

    public async Task<IActionResult> Index()
    {
        ResponseDTO? response = await _leadService.GetAllLeadsAsync();
        List<LeadDTO>? models = null;

        if (response != null)
        {
            _ = (response.IsSuccess) ?
                models = JsonConvert.DeserializeObject<List<LeadDTO>>(Convert.ToString(response.Result)) 
                : TempData["error"] = response?.Message;
        }
        else
            TempData["error"] = response?.Message;

        return View(models);
    }

    public IActionResult NewContent() => View();

    [HttpPost]
    public async Task<IActionResult> NewContent(InsertUpdateLead model) 
    {
        if (ModelState.IsValid)
        {
            var response = await _leadService.CreateAsync(model);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else 
                TempData["error"] = response?.Message;
                
        }

        return View(model);
    }

    // To display info before delete.
    public async Task<IActionResult> Delete(int leadId)
    {
        ResponseDTO? response = await _leadService.GetLeadByIdAsync(leadId);

        if (response != null)
        {
            LeadDTO? lead = (response.IsSuccess) ?
                JsonConvert.DeserializeObject<LeadDTO>(Convert.ToString(response.Result)) : null;

            return View(lead);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(LeadDTO leadDTO)
    {
        ResponseDTO? response = await _leadService.DeleteAsync(leadDTO.LeadId);

        if (response != null && response.IsSuccess)
        {
            TempData["success"] = response?.Message;
            return RedirectToAction(nameof(Index));
        }
        else TempData["error"] = response?.Message;

        return View(leadDTO);
    }

}