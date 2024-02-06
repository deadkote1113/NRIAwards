using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using NRIAwards.PL.Ui.Models;
using NRIAwards.PL.Ui.Models.ViewModels;
using NRIAwards.PL.Ui.Models.ViewModels.FilterModels;
using NRIAwards.PL.Ui.Other;
using System.Security.Claims;

namespace NRIAwards.PL.Ui.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly ICrudUsersService _crudUsersService;
    private readonly IExtendedUsersService _extendedUsersService;

    public UsersController(ICrudUsersService crudUsersService, IExtendedUsersService extendedUsersService)
    {
        _crudUsersService = crudUsersService;
        _extendedUsersService = extendedUsersService;
    }

    public IActionResult Login(string returnUrl)
    {
        return View(new LogOnModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LogOnModel model)
    {
        var user = UserModel.FromEntity(await _extendedUsersService.VerifyPasswordAsync(model.Login, model.Password));
        if (user != null)
        {
            if (user.IsBlocked)
            {
                TempData[OperationResultType.Error.ToString()] = "Пользователь заблокирован";
            }
            else
            {
                var identity = new CustomUserIdentity(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        IsPersistent = model.Remember
                    });
                return Redirect(string.IsNullOrEmpty(model.ReturnUrl) ? "~/" : model.ReturnUrl);
            }
        }
        else
        {
            TempData[OperationResultType.Error.ToString()] = "Указаны неверные данные для входа";
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Index(UsersFilterModel filterModel, int page = 1)
    {
        const int objectsPerPage = 10;
        var roles = typeof(UserRole).GetEnumValues().Cast<UserRole>();
        var searchResult = await _crudUsersService.GetAsync(new UsersSearchParams
        {
            SearchQuery = filterModel.SearchQuery,
            StartIndex = (page - 1) * objectsPerPage,
            ObjectsCount = objectsPerPage,
            Roles = User.IsInRole(nameof(UserRole.Developer)) ? roles
                : roles.Where(role => role != UserRole.Developer),
        });
        var viewModel = new SearchResultViewModel<UserModel, UsersFilterModel>(
            UserModel.FromEntitiesList(searchResult.Objects), filterModel,
            searchResult.Total, searchResult.RequestedStartIndex, searchResult.RequestedObjectsCount, 5);
        return View(viewModel);
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Update(int? id)
    {
        var model = new UserModel();
        if (id != null)
        {
            model = UserModel.FromEntity(await _crudUsersService.GetAsync(id.Value));
            if (model == null)
            {
                return NotFound();
            }
            if (!User.IsInRole(nameof(UserRole.Developer)) && model.Role == UserRole.Developer)
            {
                return Forbid();
            }
        }
        model.Password = null;
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UserModel model)
    {
        var oldUser = UserModel.FromEntity(await _crudUsersService.GetFirstAsync(new UsersSearchParams()
        {
            Login = model.Login
        }));
        if (oldUser != null && oldUser.Id != model.Id)
        {
            ModelState.AddModelError(nameof(model.Login), "Пользователь с таким логином уже существует");
        }
        if (!User.IsInRole(nameof(UserRole.Developer)) && (model.Role == UserRole.Developer || oldUser?.Role == UserRole.Developer))
        {
            return Forbid();
        }
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _crudUsersService.AddOrUpdateAsync(UserModel.ToEntity(model));
        TempData[OperationResultType.Success.ToString()] = "Данные сохранены";
        return RedirectToAction("Index");
    }
}