
## PermissionsAuther [![Nuget](https://img.shields.io/nuget/v/AndroThink.Identity.PermissionsAuther)](https://www.nuget.org/packages/AndroThink.Identity.PermissionsAuther/1.0.1)
Help in securing endpoints using permissions-based authorization.

![](https://raw.githubusercontent.com/AndroThink/PermissionsAuther/main/AndroThink.Identity.PermissionsAuther/Images/andro_think.png)

## How to use 

 ### In Program.cs
```c#
builder.Services.UsePermissionsAuther();
 
 .....

app.UseAuthentication();
app.UseAuthorization();
```

#### In the controller above the endpoint that we want to protect it
```c#
[HasPermission({ID_Of_Section}, AndroThink.Identity.PermissionsAuther.Enums.Permissions.CanView)]
public IActionResult Index()
{
    return View();
}
```


#### Or above the controller itself that we want to protect all the endpoints in it
```c#
[HasPermission({ID_Of_Section}, AndroThink.Identity.PermissionsAuther.Enums.Permissions.CanView)]
public class HomeController : Controller
{
     ...........
     
     public IActionResult Index()
     {
          return View();
     }
}
```

#### In login controller we have to add the permissions claims
##### A model that implements `AndroThink.Identity.PermissionsAuther.Interfaces.ISectionRole` interface is required so we can store permissions for every user for example 
```c#
    public class UserPermission : BasEntity, AndroThink.Identity.PermissionsAuther.Interfaces.ISectionRole
    {
        public long UserId { get; set; }
        public short SectionId { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanSoftDelete { get; set; }
        public bool CanView { get; set; }
    }
```

```c#

public class LoginController : Controller
{
     ...........
     
     [HasPermission({ID_Of_Section}, AndroThink.Identity.PermissionsAuther.Enums.Permissions.CanView)]
     [HttpPost]
     public IActionResult Login(string username,string password)
     {
          ....
          
          var permissions = (List<AndroThink.Identity.PermissionsAuther.Interfaces.ISectionRole>)loggedUsers.UserPermissions;
          var permissionsClaim = AndroThink.Identity.PermissionsAuther.PermissionUtils.CreatePermissionClaim(permissions);
          
          List<Claim> claims = new List<Claim>
        {
            new("Claim1", "TestValue1"),
            new("Claim2","TestValue2"),
            permissionsClaim
        };

          var claimsIdentity = new ClaimsIdentity(claims);
          
           ....   
     }
}
```