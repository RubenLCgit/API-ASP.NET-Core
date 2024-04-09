using PetPalApp.Domain;
using System.Security.Claims;

namespace PetPalApp.Business;

public static class ControlUserAccess
{
  public static bool UserHasAccess(string roleToken, string idToken, int idUser)
  {
    return roleToken == "Admin" || idUser.ToString() == idToken;
  }
}