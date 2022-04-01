using HttpModels;
using Microsoft.AspNetCore.Identity;

namespace HttpApiServer;

public class AccountService
{
    private readonly IUnitOfWork _unit;
    private readonly IPasswordHasher<Account> _hasher;
    private readonly ITokenService _tokenService;
    
    public AccountService(IUnitOfWork unit, IPasswordHasher<Account> hasher, ITokenService tokenService)
    {
        _unit = unit;
        _hasher = hasher;
        _tokenService = tokenService;
    }

    public async Task Register(string email, string login, string password)
    {
        
        if (await _unit.AccountRepository.IsEmailExist(email))
            throw new DuplicateEmailException();

        if (await _unit.AccountRepository.IsLoginExist(login))
            throw new DuplicateLoginException();

        Account account = new()
        {
            Id = Guid.NewGuid(),
            Login = login,
            Email = email
        };
        
        var hashedPassword = _hasher.HashPassword(account, password);
        account.PasswordHash = hashedPassword;

        var cart = new Cart() { Id = Guid.NewGuid(), AccountId = account.Id };

        await _unit.AccountRepository.Add(account);
        await _unit.CartRepository.Add(cart);
        await _unit.SaveChangesAsync();
    }
    
    public async Task<(Account, string)> Authorization(string login, string password, string role)
    {
        Account? account = await _unit.AccountRepository.FindByLogin(login);
        if (account == null)
            throw new InvalidLoginException();
        
        var result = _hasher.VerifyHashedPassword(
            account, account.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
            throw new InvalidPasswordException();

        return (account, _tokenService.GenerateToken(account, role));
    }


    public Task<bool> IsEmailExist(string email)
    {
        return _unit.AccountRepository.IsEmailExist(email);
    }

    public Task<bool> IsLoginExist(string login)
    {
        return _unit.AccountRepository.IsLoginExist(login);
    }

    public async Task<Account> GetById(Guid Id)
    {
        return await _unit.AccountRepository.GetById(Id);
    }

    public async Task<IReadOnlyList<Account>> GetAll()
    {
        return await _unit.AccountRepository.GetAll();
    }
}