using Microsoft.AspNetCore.Mvc;
using TechGears.Web.Models;
using TechGears.Web.Service.IService;
using TechGears.Web.Models.Lead;
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
            models = (response.IsSuccess) ?
                JsonConvert.DeserializeObject<List<LeadDTO>>(Convert.ToString(response.Result)) : null;
        }

        return View(models);
    }

}