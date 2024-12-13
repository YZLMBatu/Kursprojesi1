﻿using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Kursprojesi.WebUI.Models;
using Microsoft.AspNetCore.Authentication;// login kütüp
using Microsoft.AspNetCore.Authorization;// login kütüp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims; // login için

namespace Kursprojesi.WebUI.Controllers
{
    public class AccountController : Controller
    {
        //private readonly DatabaseContext _context;
        //public AccountController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<AppUser> _service;
        private readonly IService<Odeme> _serviceOdeme;

        public AccountController(IService<AppUser> service, IService<Odeme> serviceOdeme)
        {
            _service = service;
            _serviceOdeme = serviceOdeme;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        { 
           AppUser user = await _service.GetAsync(x=>x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if(user is null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                SurName = user.SurName,
                
            };
            return View(model);
        }
        [HttpPost,Authorize]
        public async Task<IActionResult> IndexAsync( UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = await _service.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
                    if (user is not null) 
                    {
                        user.SurName = model.SurName;
                        user.Phone = model.Phone;
                        user.Name = model.Name;
                        user.Password = model.Password;
                        user.Email = model.Email;
                        
                        _service.Update(user);
                       var sonuc = _service.SaveChanges();

                        if (sonuc > 0)
                        {
                            TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Bilgileriniz Güncellendi</strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                            //await MailHelper.SendMailAsync(contact);
                            return RedirectToAction("Index");

                        }
                    }
                    
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Orders()
        {
            AppUser user = await _service.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (user is null)
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("SignIn");
            }
            var model = _serviceOdeme.GetQueryable().Where(s=>s.AppUserId == user.Id).Include(o=>o.OdemeLines).ThenInclude(s=>s.Product);
            return View(model);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var account = await _service.GetAsync(x=> x.Email == loginViewModel.Email & x.Password == loginViewModel.Password & x.IsActive);
                    if (account == null)
                    {
                        ModelState.AddModelError("", "Giriş Başarısız");
                    }
                    else
                    {
                        var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Name,account.Name),
                            new(ClaimTypes.Role,account.IsAdmin ? "Admin" :"Customer"),
                            new(ClaimTypes.Email,account.Email),
                            new("UserId",account.Id.ToString()), 
                            new("UserGuid",account.UserGuid.ToString()), 
                        };
                        var userIdentity = new ClaimsIdentity(claims,"Login");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrincipal);
                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl)? "/" :loginViewModel.ReturnUrl);
                    }
                }
                catch (Exception hata)
                {

                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View();
        }
        public IActionResult SignUp() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>SignUpAsync(AppUser appUser)
        {
            appUser.IsAdmin = false;
            appUser.IsActive = true;
            if (ModelState.IsValid)
            {
                await _service.AddAsync(appUser);
                await _service.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(appUser);
        }
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
