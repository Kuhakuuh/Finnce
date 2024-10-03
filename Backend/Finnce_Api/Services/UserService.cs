namespace Finnce_Api.core.Services;

public class UserService
{
    private readonly RepositoryContext context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public UserService(RepositoryContext context,
         UserManager<IdentityUser> userManager,
         RoleManager<IdentityRole> roleManager,
         IConfiguration configuration)
    {
        this.context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }


    public IEnumerable<User> GetAllUsers()
    {
        if (context.Users != null)
        {

            var userModels = context.Users.ToList();



            return userModels;
        }
        return null;
    }

    public async Task<User> CreateUser(UserModelCreate model)
    {
        try
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);

            if (userExists != null)
            {

                return null;
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                BirthDate = model.BirthDate,
                PhoneNumber = model.Phone


            };


            var result = await _userManager.CreateAsync(user, model.Password);
            _ = await _userManager.AddToRoleAsync(user, "user");

            if (result.Succeeded)
            {


                return user;
            }
            else
            {

                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public UserModel GetUser(UserModel newUser)
    {
        var userById = context.Users.FirstOrDefault(u => u.Id == newUser.Id);


        var userByEmail = context.Users.FirstOrDefault(u => u.Email == newUser.Email);

        var foundUser = userById ?? userByEmail;
        var userModel = new UserModel
        {

            Id = foundUser.Id,
            Name = foundUser.Name,
            Email = foundUser.Email,
            //Phone = foundUser.Phone,
            //Password = foundUser.Password,


        };

        return userModel;
    }

    public string GetIdUserTink(string id)
    {
        var userById = context.Users.FirstOrDefault(u => u.Id == id);
        if (userById.TinkToken == null)
        {
            return string.Empty;

        }
        return userById.TinkToken;

    }
    public async Task<User> AddIdTink(string userId, string accessToken)
    {
        var ParseaccessToken = JObject.Parse(accessToken)["access_token"].ToString();



        var user = context.Users.FirstOrDefault(u => u.Id == userId);


        if (user != null)
        {
            var tinkToken = ParseaccessToken;

            if (!string.IsNullOrEmpty(tinkToken))
            {
                user.TinkToken = tinkToken;
                context.SaveChanges();

                return user;
            }
        }


        return null;
    }



}





