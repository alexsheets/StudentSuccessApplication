using EPA.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using EPA.Classes;
using EPA.Models.ViewModels;
using System.Net.Mail;
using System.Net;
using Telerik.SvgIcons;
using System.Web;

namespace EPA.Controllers.Home
{
    public class HomeController : Controller
    {
        private GetDbItems _getDbItems = new GetDbItems();

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        [HttpGet]
        public IActionResult FirstTimeLogin()
        {
            HttpContext.Session.Clear();
            return View("FirstTimeLogin");
        }

        [HttpPost]
        public ActionResult SendEmail(FirstTimeLoginViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    

                    var senderEmail = new MailAddress("relay@vetmed.lsu.edu");
                    var receiverEmail = new MailAddress("ecampbell1@lsu.edu");
                    var body = "User first name: " + vm.FirstName + "\n" + "User last name: " + vm.LastName + "\n" + "User email: " + vm.Email + "\n" + "User type: " + vm.UserType + "\n";
                    body += "User concentration (if student): " + vm.Concentration + "\n" + "User class (if student): " + vm.Class + "\n" + "User based at LSU (if evaluator): " + vm.BasedAtLSU + "\n" + "User clinic (if evaluator): " + vm.Clinic + "\n";
                    var smtp = new SmtpClient
                    {
                        Host = "relay.lsu.edu",
                        Port = 25,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                        // Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = "New user information",
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    TempData["ValidationMsg"] = "Email successfully sent.";
                    return View("Index");
                }
            }
            catch (Exception)
            {
                TempData["ValidationMsg"] = "Error occurred.";
                return View("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult SignIn(SignIn credentials)
        {
            try
            {
                bool isValid;
                if (credentials.Username == null || credentials.Password == null)
                {
                    TempData["ValidationMsg"] = "Missing username or password.";
                    return View("Index");
                }
                else
                {
                    PrincipalContext cntxt = new PrincipalContext(ContextType.Domain);

                    // student directories
                    GroupPrincipal studentGroup28 = GroupPrincipal.FindByIdentity(cntxt, "VMED-STU-Cls2028");
                    GroupPrincipal studentGroup27 = GroupPrincipal.FindByIdentity(cntxt, "VMED-STU-Cls2027");
                    GroupPrincipal studentGroup26 = GroupPrincipal.FindByIdentity(cntxt, "VMED-STU-Cls2026");
                    GroupPrincipal studentGroup25 = GroupPrincipal.FindByIdentity(cntxt, "VMED-STU-Cls2025");
                    GroupPrincipal studentGroup24 = GroupPrincipal.FindByIdentity(cntxt, "VMED-STU-Cls2024");
                    GroupPrincipal studentGroup23 = GroupPrincipal.FindByIdentity(cntxt, "VMED-STU-Cls2023");
                    GroupPrincipal gradStudents = GroupPrincipal.FindByIdentity(cntxt, "VMED-STU-Graduate");

                    // admin directory
                    GroupPrincipal adminGroup = GroupPrincipal.FindByIdentity(cntxt, "EPA-Admins");

                    GroupPrincipal evalGroup = GroupPrincipal.FindByIdentity(cntxt, "EPA-Evaluators");

                    isValid = cntxt.ValidateCredentials(credentials.Username, credentials.Password);

                    bool isStudent = false;
                    bool isAdmin = false;

                    var usr = UserPrincipal.FindByIdentity(cntxt, credentials.Username);

                    if (isValid)
                    {

                        if (usr.IsMemberOf(gradStudents) || usr.IsMemberOf(studentGroup28) || usr.IsMemberOf(studentGroup27) || usr.IsMemberOf(studentGroup26) || usr.IsMemberOf(studentGroup25) || usr.IsMemberOf(studentGroup24) || usr.IsMemberOf(studentGroup23))
                        {
                            isStudent = true;
                        }

                        if(usr.IsMemberOf(adminGroup))
                        {
                            isAdmin = true;
                        }

                        // might need to do something different for this in the future considering we could have external evaluators
                        HttpContext.Session.SetString("CurrentUser", usr.EmailAddress.Replace("@lsu.edu", "").Trim().ToLower());

                        HttpContext.Session.SetString("IsValidUser", "Yes");

                        if (isStudent)
                        {
                            HttpContext.Session.SetString("UserType", "Student");

                            // grab from db by paws ID
                            // only allow them to see if something was grabbed otherwise direct back to login page
                            Student student = _getDbItems.GetStudentByPawsId(usr.EmailAddress.Replace("@lsu.edu", "").Trim().ToLower());

                            if (student != null)
                            {
                                HttpContext.Session.SetString("StudentId", student.StudentId.ToString());
                                return RedirectToAction("ViewEPAs", "StudentView");
                            } else
                            {
                                TempData["ValidationMsg"] = "Unable to log you in. Please fill out this form to be sent to Emily Erwin.";
                                return RedirectToAction("FirstTimeLogin", "Home");
                            }
                        }
                        else if(isAdmin)
                        {
                            HttpContext.Session.SetString("Email", usr.EmailAddress);
                            HttpContext.Session.SetString("UserType", "Admin");
                            return RedirectToAction("EditStudentsGrid", "AdminView");
                        } else
                        {
                            // user is an evaluator
                            HttpContext.Session.SetString("Email", usr.EmailAddress);
                            HttpContext.Session.SetString("UserType", "Evaluator");

                            // grab from db by the email address
                            // if nothing retrieved direct back to login page
                            Evaluator evaluator = _getDbItems.GetEvaluatorByEmail(usr.EmailAddress);

                            if (evaluator != null)
                            {
                                return RedirectToAction("ViewAssignedEPAs", "EvaluatorView");
                            } else
                            {
                                TempData["ValidationMsg"] = "Unable to log you in. Please fill out this form to be sent to Emily Erwin.";
                                return RedirectToAction("FirstTimeLogin", "Home");
                            }
                        }
                    }
                    else
                    {
                        HttpContext.Session.SetString("CurrentUser", usr.EmailAddress);
                        HttpContext.Session.SetString("IsValidUser", "Yes");
                        HttpContext.Session.SetString("UserType", "Evaluator");


                        // user is not 'valid', ie not an LSU user, but still may be an evaluator outside of LSU
                        Evaluator evaluator = _getDbItems.GetEvaluatorByEmail(usr.EmailAddress);
                        if (evaluator != null)
                        {
                            var pass = evaluator.Password;
                            if (pass == credentials.Password)
                            {
                                
                                return RedirectToAction("ViewAssignedEPAs", "EvaluatorView");
                            } else
                            {
                                TempData["ValidationMsg"] = "Please double check your username and password.";
                                return View("Index");
                            }
                        }
                        TempData["ValidationMsg"] = "Please double check your username and password.";
                        return View("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = "An error has occurred. Please try again.";
                return View("Index");
            }
        }

        
    }
}