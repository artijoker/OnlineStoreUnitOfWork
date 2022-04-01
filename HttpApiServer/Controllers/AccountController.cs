using HttpModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HttpApiServer;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    [HttpPost("registration")]
    public async Task<ActionResult<ResponseModel<Account>>> Register(AccountRegistrationModel model)
    {
        try
        {
            await _service.Register(model.Email, model.Login, model.Password);
            return new ResponseModel<Account>() { Succeeded = true };
        }
        catch (DuplicateEmailException)
        {
            return new ResponseModel<Account>()
            {
                Succeeded = false,
                Message = "Пользователь с таким email уже существует"
            };
        }
        catch (DuplicateLoginException)
        {
            return new ResponseModel<Account>()
            {
                Succeeded = false,
                Message = "Пользователь с таким логином уже существует"
            };
        }
        catch (Exception)
        {
            return new ResponseModel<Account>()
            {
                Succeeded = false,
                Message = "Ошибка регистрации. Попробуйте зарегистрироваться позже."
            };
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<LogInResponse>> LogInUser(AccountLogInModel accountModel)
    {
        return await LogIn(accountModel, "user");
    }

    [HttpPost("login_admin")]
    public async Task<ActionResult<LogInResponse>> LogInAdmin(AccountLogInModel accountModel)
    {
        return await LogIn(accountModel, "admin");
    }

    [Authorize]
    [HttpGet("get_account")]
    public async Task<ActionResult<ResponseModel<Account>>> GetAccount()
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        return new ResponseModel<Account>()
        {
            Succeeded = true,
            Result = await _service.GetById(id)
        };

    }

    [Authorize(Roles = "admin")]
    [HttpGet("get_all_accounts")]
    public async Task<ActionResult<ResponseModel<IReadOnlyList<Account>>>> GetAllAccounts()
    {
        return new ResponseModel<IReadOnlyList<Account>>()
        {
            Succeeded = true,
            Result = await _service.GetAll()
        };
    }



    private async Task<ActionResult<LogInResponse>> LogIn(AccountLogInModel accountModel, string role)
    {
        try
        {
            var (account, token) = await _service.Authorization(accountModel.Login, accountModel.Password, role);
            return new LogInResponse()
            {
                Succeeded = true,
                Result = account,
                Token = token
            };
        }
        catch (InvalidLoginException)
        {
            return new LogInResponse()
            {
                Succeeded = false,
                Message = "Неверный логин"
            };
        }
        catch (InvalidPasswordException)
        {
            return new LogInResponse()
            {
                Succeeded = false,
                Message = "Неверный пароль"
            };
        }
        catch (Exception)
        {
            return new LogInResponse()
            {
                Succeeded = false,
                Message = "Ошибка авторизации. Попробуйте зайти позже."
            };
        }
    }
}