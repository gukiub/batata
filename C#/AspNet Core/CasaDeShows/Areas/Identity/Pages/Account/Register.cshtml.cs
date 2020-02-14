using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CasaDeShows.Areas.Identity.Users;
using CasaDeShows.Data;
using CasaDeShows.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;


namespace CasaDeShows.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AdminUser> _signInManager;
        private readonly UserManager<AdminUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        
        //private readonly IEmailSender _emailSender;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _ctx;

        public RegisterModel(
            UserManager<AdminUser> userManager,
            SignInManager<AdminUser> signInManager,
            ILogger<RegisterModel> logger
            //IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage="Digite um nome")]
            [StringLength(100, ErrorMessage = "O {0} deve conter pelo menos {2} e no máximo {1} caracteres", MinimumLength = 4)]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Required(ErrorMessage="Digite um email")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage="Digite uma senha")]
            [StringLength(100, ErrorMessage = "A {0} deve conter pelo menos {2} e no máximo {1} caracteres maiusculos.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "As senhas não são iguais")]
            public string ConfirmPassword { get; set; }

            [Required]
            public bool Admin { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AdminUser { UserName = Input.Email, Email = Input.Email };
                
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuário criou uma nova conta com senha");
                    
                    //await _userManager.AddClaimAsync(user, new Claim("Admin", Input.Admin.ToString()));
                    
                    //await _userManager.AddClaimAsync(user, new Claim("Nome", Input.Nome.ToString()));
                    

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    // var callbackUrl = Url.Page(
                    //     "/Account/ConfirmEmail",
                    //     pageHandler: null,
                    //     values: new { area = "Identity", userId = user.Id, code = code },
                    //     protocol: Request.Scheme);

                    // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    // {
                    //     return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    // }
                    // else
                    // {
                        await _roleManager.CreateAsync(new IdentityRole(Roles.ROLE_CASA_DE_SHOW));
                        var userIdentity = _userManager.FindByNameAsync(Input.Email).Result;
                
                        await _userManager.AddToRoleAsync(userIdentity, Roles.ROLE_CASA_DE_SHOW);

                        returnUrl = returnUrl ?? Url.Content("~/api/Login/" + Input.Email + "/" + Input.Password);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    // }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
