using Microsoft.AspNetCore.Authentication;
using System.DirectoryServices.AccountManagement;

namespace EPA.Classes
{
    public class GetUserInfo
    {
        private PrincipalContext _cntxt = new PrincipalContext(ContextType.Domain);
        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public bool IsValidUser()
        {
            string currentUser = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
            string isValidUser = _httpContextAccessor.HttpContext.Session.GetString("IsValidUser");

            //return (currentUser != null && currentUser.Trim() != "") && (isValidUser != null && isValidUser == "Yes");
            return (isValidUser != null && isValidUser == "Yes");
        }

        public string GetPaws()
        {
            string currentUser = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
            return currentUser;
        }

        public bool IsAdminUser()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("UserType") == "Admin";
        }

        public bool IsStudentUser()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("UserType") == "Student";
        }

        public bool IsEvaluatorUser()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("UserType") == "Evaluator";
        }

        public static List<string> GetVmedPawsIds()
        {
            try
            {
                GetUserInfo g = new GetUserInfo();
                GroupPrincipal sch = GroupPrincipal.FindByIdentity(g._cntxt, "VMED-SCH");
                List<string> ids = g.GetGroupMembers(sch, new List<string>());
                return ids;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<string> GetGroupMembers(GroupPrincipal gp, List<string> ids)
        {
            try
            {
                var members = gp.Members.ToList();

                foreach (var member in members)
                {
                    if (member.StructuralObjectClass == "group")
                    {
                        ids.AddRange(GetGroupMembers(GroupPrincipal.FindByIdentity(_cntxt, member.Name), ids));
                    }
                    else
                    {
                        if (!ids.Contains(member.Name) && member.Description != "Functional" && member.StructuralObjectClass != "computer" && member.StructuralObjectClass != "group")
                        {
                            ids.Add(member.Name);
                        }
                    }
                }

                return ids;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

