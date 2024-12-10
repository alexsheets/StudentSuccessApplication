using EPA.Classes;
using EPA.Models;
using EPA.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Net.Mail;

// using System.Web.Mvc;
using System.Web;
using Telerik.SvgIcons;

namespace EPA.Controllers.StudentViewController
{
    public class StudentViewController : Controller
    {

        EpaContext context = new EpaContext();
        GetDbItems _getDbItems = new GetDbItems();
        SetDbItems _setDbItems = new SetDbItems();
        private GetUserInfo _userInfo = new GetUserInfo();
        private const string ERRORMSG = "An error has occurred. Please try again.";
        private const string NoAccessMsg = "You do not have the proper access to perform this action.";
        private const string ExpiredSession = "Your session has expired. Please sign in.";

        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string email)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var senderEmail = new MailAddress("relay@vetmed.lsu.edu");
                    var receiverEmail = new MailAddress(email);
                    var body = "An EPA has been requested for your evaluation. You should now be able to log in and see it at epa.vetmed.lsu.edu";
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
                        Subject = "EPA Requested",
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

        public List<Evaluator> GetEvaluators()
        {
            return _getDbItems.GetEvaluators();
        }

        // used to return the status of the EPA; if it has scoring, then return complete, otherwise return incomplete
        public string CheckIfEPAHasBeenScored(long resultId)
        {
            Result result = _getDbItems.GetResultById(resultId);

            if (result.AgScore == null)
            {
                return "Incomplete";
            }
            else
            {
                return "Complete";
            }
        }

        /*
         * Utility functions for returning dropdowns to the request EPA page.
         */
        public List<Evaluator> GetListOfEvaluators()
        {
            // need to add self evaluator to this list
            try
            {
                return _getDbItems.GetEvaluators();
            }
            catch (Exception ex)
            {
                return new List<Evaluator>();
            }
        }

        public List<Rotation> GetListOfRotations()
        {
            try
            {
                return _getDbItems.GetRotations();
            }
            catch (Exception ex)
            {
                return new List<Rotation>();
            }
        }

        public List<Block> GetListOfBlocks()
        {
            try
            {
                return _getDbItems.GetBlocks();
            }
            catch (Exception ex)
            {
                return new List<Block>();
            }
        }

        public List<Epa> GetListOfEpas()
        {
            try
            {


                return _getDbItems.GetAllEpas();
            }
            catch (Exception ex)
            {
                return new List<Epa>();
            }
        }

        [HttpGet]
        public JsonResult getData()
        {
            try
            {
                List<SelectListItem> Choices = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "Self-Evaluations", Value = "self" },
                    new SelectListItem() { Text = "Evaluations by Evaluators", Value = "other" }
                };


                return Json(Choices);
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        public JsonResult getPracticeTypes()
        {
            try
            {
                List<SelectListItem> Years = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "Mixed Animal", Value = "Mixed Animal" },
                    new SelectListItem() { Text = "Small Animal", Value = "Small Animal" },
                    new SelectListItem() { Text = "Equine", Value = "Equine" },
                    new SelectListItem() { Text = "Food Animal", Value = "Food Animal" },
                    new SelectListItem() { Text = "Exotics/Wildlife", Value = "Exotics/Wildlife" },
                    new SelectListItem() { Text = "Other", Value = "Other" },
                };

                return Json(Years);
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        [HttpGet]
        public JsonResult getDataForInitChoices()
        {
            try
            {
                List<SelectListItem> Choices = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "Self-Comparison", Value = "self" },
                    new SelectListItem() { Text = "Comparison with Others", Value = "other" }
                };


                return Json(Choices);
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        [HttpGet]
        public JsonResult getDataEpas()
        {
            try
            {
                List<SelectListItem> Choices = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "1a", Value = "3" },
                    new SelectListItem() { Text = "1b", Value = "4" },
                    new SelectListItem() { Text = "1c", Value = "5" },
                    new SelectListItem() { Text = "2", Value = "6" },
                    new SelectListItem() { Text = "2.1", Value = "7" },
                    new SelectListItem() { Text = "3", Value = "8" },
                    new SelectListItem() { Text = "4", Value = "9" },
                    new SelectListItem() { Text = "5", Value = "10" },
                    new SelectListItem() { Text = "6", Value = "11" },
                    new SelectListItem() { Text = "7", Value = "12" },
                    new SelectListItem() { Text = "7", Value = "13" },
                };


                return Json(Choices);
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        public JsonResult getSeasons()
        {
            try
            {
                List<SelectListItem> Seasons = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "Fall", Value = "Fall" },
                    new SelectListItem() { Text = "Spring", Value = "Spring" },
                    new SelectListItem() { Text = "Summer", Value = "Summer" }
                };

                return Json(Seasons);
            } catch (Exception ex)
            {
                return Json("");
            }
        }

        public JsonResult getYears()
        {
            try
            {
                List<SelectListItem> Years = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "2024", Value = "2024" },
                    new SelectListItem() { Text = "2025", Value = "2025" },
                    new SelectListItem() { Text = "2026", Value = "2026" },
                    new SelectListItem() { Text = "2027", Value = "2027" },
                    new SelectListItem() { Text = "2028", Value = "2028" },
                    new SelectListItem() { Text = "2029", Value = "2029" },
                    new SelectListItem() { Text = "2030", Value = "2030" }
                };

                return Json(Years);
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        public JsonResult getBlocksJson()
        {
            try
            {
                List<SelectListItem> Blocks = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "1A", Value = "1A" },
                    new SelectListItem() { Text = "1C", Value = "1C" },
                    new SelectListItem() { Text = "2A", Value = "2A" },
                    new SelectListItem() { Text = "2C", Value = "2C" },
                    new SelectListItem() { Text = "3A", Value = "3A" },
                    new SelectListItem() { Text = "3C", Value = "3C" },
                    new SelectListItem() { Text = "4A", Value = "4A" },
                    new SelectListItem() { Text = "4C", Value = "4C" },
                    new SelectListItem() { Text = "5A", Value = "5A" },
                    new SelectListItem() { Text = "5C", Value = "5C" },
                    new SelectListItem() { Text = "6A", Value = "6A" },
                    new SelectListItem() { Text = "6C", Value = "6C" },
                    new SelectListItem() { Text = "7A", Value = "7A" },
                    new SelectListItem() { Text = "7C", Value = "7C" },
                    new SelectListItem() { Text = "8A", Value = "8A" },
                    new SelectListItem() { Text = "8C", Value = "8C" },
                    new SelectListItem() { Text = "9A", Value = "9A" },
                    new SelectListItem() { Text = "9C", Value = "9C" },
                    new SelectListItem() { Text = "10A", Value = "10A" },
                    new SelectListItem() { Text = "10C", Value = "10C" },
                    new SelectListItem() { Text = "11A", Value = "11A" },
                    new SelectListItem() { Text = "11C", Value = "11C" },
                    new SelectListItem() { Text = "12A", Value = "12A" },
                    new SelectListItem() { Text = "12C", Value = "12C" },

                };

                return Json(Blocks);
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        public List<ResultInformationViewModel> GetListOfResults()
        {
            string currUser = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
            Student stu = _getDbItems.GetStudentByPawsId(currUser);
            List<Result> results = _getDbItems.GetResultsByStudentId(stu.StudentId).Where(e => e.SelfEval == false).ToList();

            Console.WriteLine(results);

            List<ResultInformationViewModel> vms = new List<ResultInformationViewModel>();

            foreach (var result in results)
            {
                ResultInformationViewModel vm = new ResultInformationViewModel()
                {
                    ResultId = result.ResultsId,
                    EvaluatorOfEPALastName = _getDbItems.GetEvaluatorById(Convert.ToInt32(result.EvaluatorId)).LastName,
                    LastUpdatedDateTime = result.UpdateDt
                };
                vms.Add(vm);

            }

            return vms;
        }

        /*
         * Functions for returning the page the user is attempting to be directed to.
         */

        public ActionResult ReflectionPage()
        {

            return View();
        }

        public ActionResult DashboardChoices()
        {
            if (_userInfo.IsValidUser())
            {
                string user = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");



                // ViewBag.choices = new SelectList(Choices, "Value", "Text");

                return View();
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult FirstDashboardChoices()
        {
            if (_userInfo.IsValidUser())
            {
                string user = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");



                // ViewBag.choices = new SelectList(Choices, "Value", "Text");

                return View("FirstDashboardChoices");
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ViewEPAs()
        {
            if (_userInfo.IsValidUser())
            {
                


                return View("ViewEPAs");
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult RequestEPA()
        {

            if (_userInfo.IsValidUser())
            {
                return View("RequestEPA");
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult RequestExternship()
        {
            if (_userInfo.IsValidUser())
            {
                return View("RequestExternship");
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Resources()
        {
            if (_userInfo.IsValidUser())
            {
                return View("Resources");
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ViewComments()
        {
            if (_userInfo.IsValidUser())
            {
                return View("ViewComments");
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SelfEvaluationPage1()
        {
            if (_userInfo.IsValidUser())
            {

                // a string will have been set with the corresponding resultId, parse to long
                long resultId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("resultId"));

                _httpContextAccessor.HttpContext.Session.SetString("resultId", resultId.ToString());

                // need to retrieve the questions associated with the EPA this result is associated with
                Result result = _getDbItems.GetResultById(resultId);

                long? epaId = result.Epaid;

                List<Question> questions = new List<Question>();

                if (epaId != null)
                {
                    List<QuestionToEpa> listOfQuestionIds = _getDbItems.GetQuestionsToEpasByEpaId(epaId);

                    foreach (var question in listOfQuestionIds)
                    {
                        Question retrieved_question = _getDbItems.GetQuestionById(question.QuestionId);
                        if (retrieved_question != null)
                        {
                            questions.Add(retrieved_question);
                        }
                    }

                    ViewBag.Questions = questions;
                }

                return View("SelfEvaluationPage1");
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SelfEvaluationPage2()
        {
            if (_userInfo.IsValidUser())
            {
                return View("SelfEvaluationPage2");
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ViewSpecificEPA()
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    return View("ViewSpecificEPA");
                }
                else
                {
                    TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("ValidationMsg");
            }
        }

        public ActionResult ViewEPAResultsPage1()
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    // a string will have been set with the corresponding resultId, parse to long
                    long resultId = long.Parse(HttpContext.Session.GetString("resultId"));

                    // need to retrieve the questions associated with the EPA this result is associated with
                    Result result = _getDbItems.GetResultById(resultId);

                    // if the result does not yet have an aggregate score then it hasnt been graded. redirect them back to the view EPAs
                    if (result.AgScore == null)
                    {
                        TempData["ValidationMsg"] = "This EPA has not yet been graded.";
                        return View("ViewEPAs");
                    }

                    // retrieve id of epa (not requested EPA but actual standard EPA)
                    long epaId = _getDbItems.GetEPAById(result.Epaid).Epaid;

                    List<QuestionToEpa> listOfQuestionIds = _getDbItems.GetQuestionsToEpasByEpaId(epaId);
                    List<Question> questions = new List<Question>();

                    foreach (var question in listOfQuestionIds)
                    {
                        Question retrieved_question = _getDbItems.GetQuestions().Where(e => e.QuestionId == question.QuestionId).FirstOrDefault();
                        if (retrieved_question != null)
                        {
                            questions.Add(retrieved_question);
                        }
                    }

                    // need to also retrieve resultitems by using the question Ids and result Id
                    List<ResultItem> resultItems = new List<ResultItem>();
                    foreach (var question in questions)
                    {
                        ResultItem resultItem = _getDbItems.GetResultItems().Where(e => (e.QuestionId == question.QuestionId) && (e.ResultId == resultId)).FirstOrDefault();
                        if (resultItem != null)
                        {
                            resultItems.Add(resultItem);

                        }
                    }

                    ViewBag.Questions = questions;
                    ViewBag.ResultItems = resultItems;


                    return View("ViewEPAResultsPage1");
                }
                else
                {
                    TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("ValidationMsg");
            }
        }

        public ActionResult ViewEPAResultsPage2()
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    // a string will have been set with the corresponding resultId, parse to long
                    long resultId = long.Parse(HttpContext.Session.GetString("resultId"));
                    // need to retrieve the questions associated with the EPA this result is associated with
                    Result result = _getDbItems.GetResultById(resultId);

                    ResultComment comments = _getDbItems.GetCommentsByResultId(resultId);
                    EPASecondPageScoringViewModel vm = new EPASecondPageScoringViewModel();

                    if (result != null)
                    {
                        if (comments != null)
                        {
                            vm.Comment1 = comments.Comment1;
                            vm.Comment2 = comments.Comment2;
                            vm.AgScore = result.AgScore;
                        }
                        else
                        {
                            vm.AgScore = result.AgScore;
                        }

                    }

                    return View("ViewEPAResultsPage2", vm);
                }
                else
                {
                    TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("ValidationMsg");
            }
        }

        public ActionResult ViewSelfEvalEPAResultsPage1()
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    // a string will have been set with the corresponding resultId, parse to long
                    long resultId = long.Parse(HttpContext.Session.GetString("resultId"));

                    // need to retrieve the questions associated with the EPA this result is associated with
                    Result result = _getDbItems.GetResultById(resultId);

                    ResultItem item = _getDbItems.GetResultItems().Where(e => e.ResultId == resultId).FirstOrDefault();

                    ViewBag.Comment1 = item.SelfEvalReflection1;
                    ViewBag.Comment2 = item.SelfEvalReflection2;
                    ViewBag.Comment3 = item.SelfEvalReflection3;

                    if (result.AgScore != null)
                    {
                        ViewBag.AgScore = result.AgScore;
                    }

                    return View("ViewSelfEvalEPAResultsPage1");
                }
                else
                {
                    TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("ValidationMsg");
            }
        }

        public ActionResult ViewSpecificComments()
        {
            if (_userInfo.IsValidUser())
            {

                string comment1 = HttpContext.Session.GetString("comment1");
                string comment2 = HttpContext.Session.GetString("comment2");


                ViewBag.Comment1 = comment1;
                ViewBag.Comment2 = comment2;

                return View("ViewSpecificComments");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ViewExternships()
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    return View("ViewExternships");
                }
                else
                {
                    TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("ValidationMsg");
            }
        }

        [HttpPost]
        public ActionResult ExternshipRequest(ExternalRegistrationViewModel externship)
        {
            if (_userInfo.IsValidUser())
            {

                string blockStr = "";

                // simple and quick way to ensure all necessary string values are present
                if (externship.Blocks == null || externship.PracticeType == null || externship.StartDate == null || externship.EndDate == null || externship.NumWeeks == null || externship.NameOfPractice == null
                    || externship.City == null || externship.State == null || externship.EvaluatorName == null || externship.EvaluatorEmail == null)
                {
                    TempData["ValidationMsg"] = ERRORMSG + " Please make sure the externship you are trying to request is not missing a required value.";
                    return View("RequestExternship");
                }

                string[] blocks = Request.Form["Blocks"].ToString().Split(',');
                int numBlocks = blocks.Length;

                for (int i = 0; i < numBlocks; i++)
                {
                    if (i == numBlocks-1)
                    {
                        blockStr = blockStr + blocks[i];
                    } else
                    {
                        blockStr = blockStr + blocks[i] + ", ";
                    }
                }

                string currUser = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
                Student student = _getDbItems.GetStudentByPawsId(currUser);

                ExternshipRegistration ext = new ExternshipRegistration()
                {
                    StudentId = (int)student.StudentId,
                    PracticeType = externship.PracticeType,
                    StartDate = (DateTime)externship.StartDate,
                    EndDate = (DateTime)externship.EndDate,
                    NumWeeks = externship.NumWeeks,
                    Blocks = blockStr,
                    NameOfPractice = externship.NameOfPractice,
                    MailingAddress = externship.MailingAddress,
                    City = externship.City,
                    State = externship.State,
                    ZipCode = externship.ZipCode,
                    TelephoneNum = externship.TelephoneNum,
                    EvaluatorName = externship.EvaluatorName,
                    EvaluatorEmail = externship.EvaluatorEmail,
                    EvaluatorTitle = externship.EvaluatorTitle,
                    Visibility = 1
                };

                string result = _setDbItems.RequestExternship(ext);
                if (result == "SUCCESS!")
                {
                    TempData["ValidationMsg"] = "The externship was successfully requested and added to the system.";
                    return View("ViewExternships");
                }
                else
                {
                    TempData["ValidationMsg"] = ERRORMSG + " Please make sure the externship you are trying to request is not missing a required value.";
                    return View("RequestExternship");
                }
            }
            else
            {
                TempData["ValidationMsg"] = NoAccessMsg;
                return View("Index", "Home");
            }
        }

        public virtual JsonResult ReadExternships([DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            // retrieve results associated with a certain student ID
            // we can retrieve the student paws ID from the http context
            string currUser = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
            List<ExternalRegistrationViewModel> viewmodels = new List<ExternalRegistrationViewModel>();

            if (!string.IsNullOrEmpty(currUser))
            {
                Student student = _getDbItems.GetStudentByPawsId(currUser);

                // the student we are looking for will be first position in returned array
                if (student != null)
                {
                    List<ExternshipRegistration> externships = _getDbItems.GetExternshipsByStudentId(student.StudentId).Where(x => x.Visibility == 1).ToList();

                    viewmodels = GetExternshipViewModels(externships);
                }
            }

            return Json(viewmodels.OrderByDescending(x => x.EndDate).ToDataSourceResult(dataSourceRequest));
        }

        public List<ExternalRegistrationViewModel> GetExternshipViewModels(List<ExternshipRegistration> externships)
        {
            List<ExternalRegistrationViewModel> vms = externships.Select(a => new ExternalRegistrationViewModel
            {
                ExternshipId = a.ExternshipId,
                PracticeType = a.PracticeType,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                NumWeeks = a.NumWeeks,
                Blocks = a.Blocks,
                NameOfPractice = a.NameOfPractice,
                MailingAddress = a.MailingAddress,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode,
                TelephoneNum = a.TelephoneNum,
                EvaluatorName = a.EvaluatorName,
                EvaluatorEmail = a.EvaluatorEmail,
                EvaluatorTitle  = a.EvaluatorTitle
            }).ToList();

            return vms;
        }

        [HttpPost]
        public string HideExternship(int externshipId)
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    ExternshipRegistration externship = _getDbItems.GetExternshipRegistration(externshipId);

                    if (externship != null)
                    {
                        
                        string response = _setDbItems.RemoveExternship(externship);
                        TempData["ValidationMsg"] = "Externship was removed.";
                        return "Externship was removed";
                        
                    }
                    else
                    {
                        TempData["ValidationMsg"] = "Error - cannot find associated externship.";
                        return "Reload";
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = NoAccessMsg;
                    return "Reload";
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return "Reload";
            }
        }

        [HttpPost]
        public ActionResult SubmitReflectionAnswers(ReflectionQuestions vm)
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    string PawsId = _userInfo.GetPaws();

                    string result1 = null;
                    string result2 = null;
                    string result3 = null;

                    if (vm.Season == null || vm.Year == null)
                    {
                        TempData["ValidationMsg"] = "Make sure you have selected the semester and year for your reflections.";
                        return View("ReflectionPage");
                    }

                    if (vm.Answer1 == null || vm.Answer2 == null || vm.Answer3 == null)
                    {
                        TempData["ValidationMsg"] = "Make sure you have answered all reflections before submitting.";
                        return View("ReflectionPage");
                    }

                    if (PawsId != null)
                    {
                        if (vm.Answer1 != null)
                        {
                            Reflection reflection1 = new Reflection() {
                                PawsId = PawsId,
                                ReflectionQuestionId = 1,
                                ReflectionAnswer = vm.Answer1,
                                Season = vm.Season,
                                Year = vm.Year,
                                DateSubmitted = DateTime.Now
                            };

                            result1 = _setDbItems.CreateReflectionAnswer(reflection1);

                        }
                        if (vm.Answer2 != null)
                        {
                            Reflection reflection2 = new Reflection()
                            {
                                PawsId = PawsId,
                                ReflectionQuestionId = 2,
                                ReflectionAnswer = vm.Answer2,
                                Season = vm.Season,
                                Year = vm.Year,
                                DateSubmitted = DateTime.Now
                            };

                            result2 = _setDbItems.CreateReflectionAnswer(reflection2);

                        }
                        if (vm.Answer3 != null)
                        {
                            Reflection reflection3 = new Reflection()
                            {
                                PawsId = PawsId,
                                ReflectionQuestionId = 3,
                                ReflectionAnswer = vm.Answer3,
                                Season = vm.Season,
                                Year = vm.Year,
                                DateSubmitted = DateTime.Now
                            };

                            result3 = _setDbItems.CreateReflectionAnswer(reflection3);

                        }

                        if (result1 == "SUCCESS!" &&  result2 == "SUCCESS!" && result3 == "SUCCESS!")
                        {
                            TempData["ValidationMsg"] = "Successfully saved your reflection answers.";
                            return View("ViewEPAs");
                        } else
                        {
                            TempData["ValidationMsg"] = "Error submitting one or more of your reflection answers. Please try again.";
                            return View("ReflectionPage");
                        }
                        
                    }
                    else
                    {
                        TempData["ValidationMsg"] = "Error - cannot find user.";
                        return View("ReflectionPage");
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = NoAccessMsg;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult RetrieveComments(long resultId)
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    HttpContext.Session.SetString("resultId", resultId.ToString());
                    ResultComment comments = _getDbItems.GetCommentsByResultId(resultId);

                    if (comments != null)
                    {
                        HttpContext.Session.SetString("comment1", comments.Comment1);
                        HttpContext.Session.SetString("comment2", comments.Comment2);
                        return ViewSpecificComments();
                    }
                    else
                    {
                        TempData["ValidationMsg"] = "Error - cannot find comments associated with EPA.";
                        return View("ViewComments");
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = NoAccessMsg;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("Index", "Home");
            }
        }

        // function to read in viewmodels and express them as json result
        public virtual JsonResult ReadEPAs([DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            // retrieve results associated with a certain student ID
            // we can retrieve the student paws ID from the http context
            string currUser = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
            List<EPAViewModel> viewmodels = new List<EPAViewModel>();

            if (!string.IsNullOrEmpty(currUser))
            {
                Student student = _getDbItems.GetStudentByPawsId(currUser);

                // the student we are looking for will be first position in returned array
                if (student != null)
                {
                    List<Result> epas = _getDbItems.GetResultsByStudentId(student.StudentId);

                    viewmodels = GetEPAViewModelList(epas);
                }
            }

            return Json(viewmodels.OrderByDescending(x => x.ResultId).ToDataSourceResult(dataSourceRequest));
        }

        public List<EPAViewModel> GetEPAViewModelList(List<Result> epas)
        {
            List<EPAViewModel> vms = epas.Select(a => new EPAViewModel
            {
                ResultId = a.ResultsId,
                EvaluatorName = _getDbItems.GetEvaluatorByIdLong(a.EvaluatorId).LastName,
                EvaluatorEmail = _getDbItems.GetEvaluatorByIdLong(a.EvaluatorId).Email,
                Rotation = _getDbItems.GetRotationById(a.RotationId).Title,
                Block = _getDbItems.GetBlockById(a.BlockId).Title,
                // activity tag is the associated EPA's section tag (ie 1b)
                ActivityTag = _getDbItems.GetEPAById(a.Epaid).SectionTag,
                Status = CheckIfEPAHasBeenScored(a.ResultsId),
                DateRequested = a.UpdateDt
            }).ToList();

            return vms;
        }

        // method for posting information and creating a result associated with said request
        [HttpPost]
        public ActionResult RequestEPA([DataSourceRequest] DataSourceRequest req, Result requested_epa)
        {
            if (_userInfo.IsValidUser())
            {

                bool self_evaluation = false;

                // created first evaluator row to be for self-evaluation
                // may need to add something to block at the top to choose if they are doing self evaluation, or hopefully
                // remove the block texbox if they are choosing to selfevaluate somehow
                int id = Convert.ToInt32(requested_epa.EvaluatorId);
                Evaluator evaluator = _getDbItems.GetEvaluatorById(id);
                if (evaluator.FirstName == "Self")
                {
                    self_evaluation = true;
                }

                // if rotation ID = 0, the user did not choose one
                // force return back to page to retry
                if (requested_epa.RotationId == 0 || requested_epa.EvaluatorId == 0 || requested_epa.BlockId == 0)
                {
                    TempData["ValidationMsg"] = ERRORMSG + " Please make sure the EPA you are trying to request is not missing a required value.";
                    return View("RequestEPA");
                }

                Student student = _getDbItems.GetStudentByPawsId(_httpContextAccessor.HttpContext.Session.GetString("CurrentUser"));

                if (self_evaluation == false)
                {
                    Result res = new Result()
                    {
                        Epaid = requested_epa.Epaid,
                        StudentId = student.StudentId,
                        AgScore = requested_epa.AgScore,
                        DateOfEval = requested_epa.DateOfEval,
                        EvaluatorId = requested_epa.EvaluatorId,
                        RotationId = requested_epa.RotationId,
                        BlockId = requested_epa.BlockId,
                        Semester = requested_epa.Semester,
                        SelfEval = false,
                        Visibility = true,
                        UpdateDt = DateTime.Now
                    };

                    string result = _setDbItems.RequestEPA(res);
                    if (result == "SUCCESS!")
                    {
                        // get evaluator requested by ID, if not self evaluation, trigger email
                        Evaluator evtr = _getDbItems.GetEvaluatorById((int)requested_epa.EvaluatorId);
                        if (evtr.FirstName != "Self")
                        {
                            SendEmail(evtr.Email);
                        }

                        TempData["ValidationMsg"] = "The EPA was successfully requested and added to the system.";
                        return View("ViewEPAs");
                    }
                    else
                    {
                        TempData["ValidationMsg"] = ERRORMSG + " Please make sure the EPA you are trying to request is not missing a required value.";
                        return View("RequestEPA");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("EPAId", requested_epa.Epaid.ToString());

                    // moved line to home controller where students log in so that the student ID is set in context upon log in rather than requesting an EPA
                    // HttpContext.Session.SetString("StudentId", student.StudentId.ToString());

                    // if self evaluation, block is not necessary
                    Result res = new Result()
                    {
                        Epaid = requested_epa.Epaid,
                        StudentId = student.StudentId,
                        AgScore = requested_epa.AgScore,
                        DateOfEval = requested_epa.DateOfEval,
                        EvaluatorId = requested_epa.EvaluatorId,
                        RotationId = requested_epa.RotationId,
                        // if self evaluation, block id will always be N/A which has an id of 1
                        BlockId = 1,
                        Semester = requested_epa.Semester,
                        SelfEval = true,
                        Visibility = true,
                        UpdateDt = DateTime.Now
                    };

                    string result = _setDbItems.RequestEPA(res);
                    if (result == "SUCCESS!")
                    {
                        TempData["ValidationMsg"] = "The EPA was successfully requested and added to the system.";
                        return View("ViewEPAs");
                    }
                    else
                    {
                        TempData["ValidationMsg"] = ERRORMSG + " Please make sure the EPA you are trying to request is not missing a required value.";
                        return View("RequestEPA");
                    }
                }


            }
            else
            {
                TempData["ValidationMsg"] = NoAccessMsg;
                return View("Index", "Home");
            }
        }

        // function for removing a chosen epa from visible EPAs
        [HttpPost]
        public string HideEPA(long resultId)
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    Result result = _getDbItems.GetResultById(resultId);

                    if (result != null)
                    {
                        if (result.SelfEval == true)
                        {
                            string response = _setDbItems.RemoveEPA(result);
                            TempData["ValidationMsg"] = "EPA was removed.";
                            return "EPA was removed";
                        } else
                        {
                            if (result.AgScore == null)
                            {
                                string response = _setDbItems.RemoveEPA(result);
                                TempData["ValidationMsg"] = "EPA was removed.";
                                return "EPA was removed";
                            } else
                            {
                                TempData["ValidationMsg"] = "Cannot trash EPA which has been scored by an evaluator.";
                                return "Reload";
                            }
                        }
                    }
                    else
                    {
                        TempData["ValidationMsg"] = "Error - cannot find associated EPA.";
                        return "Reload";
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = NoAccessMsg;
                    return "Reload";
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return "Reload";
            }
        }

        public ActionResult SubmitSelfEvaluationPage1(ResultItem item)
        {

            // a string will have been set with the corresponding resultId, parse to long
            long resultId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("resultId"));

            // need to retrieve the questions associated with the EPA this result is associated with
            Result result = _getDbItems.GetResultById(resultId);

            // make sure that there isnt already a result item with this result ID
            ResultItem resultItem = _getDbItems.GetResultItems().Where(e => e.ResultId == resultId).FirstOrDefault();

            if (_userInfo.IsValidUser())
            {
                if (item.SelfEvalReflection1 == null || item.SelfEvalReflection2 == null || item.SelfEvalReflection3 == null)
                {
                    TempData["ValidationMsg"] = ERRORMSG + " Please make sure the reflection questions have all been answered when you submit.";
                    return View("ViewEPAs");
                }

                ResultItem new_item = new ResultItem()
                {
                    ResultId = result.ResultsId,
                    SelfEvalReflection1 = item.SelfEvalReflection1,
                    SelfEvalReflection2 = item.SelfEvalReflection2,
                    SelfEvalReflection3 = item.SelfEvalReflection3,
                };

                if (resultItem == null)
                {
                    string response = _setDbItems.CreateSelfEvalResult(new_item);
                    if (response == "SUCCESS!")
                    {
                        TempData["ValidationMsg"] = "Saved responses for self-evaluation reflection questions.";
                        return View("SelfEvaluationPage2");
                    }
                    else
                    {
                        TempData["ValidationMsg"] = ERRORMSG + " Please make sure the reflection questions have all been answered.";
                        return View("RequestEPA");
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = ERRORMSG + " Reflection questions for this EPA have already been answered.";
                    return View("SelfEvaluationPage2");
                }
            }
            else
            {
                TempData["ValidationMsg"] = NoAccessMsg;
                return View("Index", "Home");
            }
        }

        public ActionResult SubmitSelfEvaluationPage2(ResultItem item)
        {
            string student = HttpContext.Session.GetString("StudentId");
            long studentId = Convert.ToInt64(student);

            // retrieval of result ID which is stored in http context whether they went to self eval page 1 first or not
            long? resultId = long.Parse(HttpContext.Session.GetString("resultId"));
            Result result = _getDbItems.GetResultById(resultId);


            if (_userInfo.IsValidUser())
            {

                if (result != null)
                {
                    // attribute the score also to the retrieved EPA so it can be updated with the score
                    result.AgScore = item.Score;
                    
                    
                }

                // retrieve the result item which has already been started by answering reflection questions
                // List<ResultItem> resultItem = _getDbItems.GetResultItems().Where(e => e.ResultId == result.ResultsId).ToList();
                ResultItem retrieved_item = _getDbItems.GetResultItems().Where(e => e.ResultId == result.ResultsId).FirstOrDefault();
                // attribute the score to the result item we have retrieved
                retrieved_item.Score = item.Score;

                // update the associated result item as well as associated EPA
                string response = _setDbItems.UpdateSelfEvalResult(retrieved_item);
                // save date of eval once they submit ag score
                result.DateOfEval = DateTime.Now;
                string response2 = _setDbItems.UpdateEPA(result);

                if (response == "SUCCESS!")
                {
                    if (response2 == "SUCCESS!")
                    {
                        TempData["ValidationMsg"] = "Final Score was successfully saved.";
                        return View("ViewEPAs");
                    }
                    else
                    {
                        TempData["ValidationMsg"] = ERRORMSG + " Please make sure score was correctly recorded";
                        return View("SelfEvaluationPage2");
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = ERRORMSG + " Please make sure score was correctly recorded";
                    return View("SelfEvaluationPage2");
                }
            }
            else
            {
                TempData["ValidationMsg"] = NoAccessMsg;
                return View("Index", "Home");
            }
        }

        [HttpPost]
        public string ViewResultsOfEPA([DataSourceRequest] DataSourceRequest request, long? resultId)
        {
            if (resultId != null)
            {
                // retrieve associated result/epa
                Result result = _getDbItems.GetResultById(resultId);
                // set epa result ID as a string value to retrieve it upon viewing first page
                _httpContextAccessor.HttpContext.Session.SetString("resultId", resultId.ToString());

                if (result != null)
                {
                    if (result.SelfEval == true)
                    {
                        return "Continue to View EPA Self Evaluated";
                    }
                    else
                    {
                        return "Continue to View EPA Evaluated by Evaluator";
                    }


                }
                else
                {
                    return "Reload";
                }
            }
            else
            {
                return "Reload";
            }

        }

        [HttpPost]
        public string SelfEvalEPA([DataSourceRequest] DataSourceRequest request, long? resultId)
        {
            if (resultId != null)
            {
                bool isSelfEval = false;
                bool hasBeenScored = false;

                // save result ID to http context once they click to score
                _httpContextAccessor.HttpContext.Session.SetString("resultId", resultId.ToString());

                // retrieve associated result/epa
                Result result = _getDbItems.GetResultById(resultId);

                ResultItem item = _getDbItems.GetResultItems().Where(e => e.ResultId == result.ResultsId).FirstOrDefault();

                if (result.SelfEval == true)
                {
                    isSelfEval = true;
                }

                if (result.AgScore != null)
                {
                    hasBeenScored = true;
                }


                if (item == null && result != null && isSelfEval == true && hasBeenScored == false)
                {
                    return "Proceed to score EPA Page 1";
                } else if (item != null && result != null && isSelfEval == true && hasBeenScored == false)
                {
                    return "Proceed to score EPA Page 2";
                }
                else
                {
                    return "Reload";
                }
            }
            else
            {
                return "Reload";
            }

        }

        [HttpPost]
        public ActionResult SubmitDashboardChoices([DataSourceRequest] DataSourceRequest request, DashboardChoicesViewModel model)
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    string epa = _httpContextAccessor.HttpContext.Session.GetString("Epa");
                    long? num = Convert.ToInt64(epa);

                    _httpContextAccessor.HttpContext.Session.SetString("Choice", model.SelfOrAllResults);

                    // validate that they exist before sending user to dashboard
                    String user = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
                    Student stu = _getDbItems.GetStudentByPawsId(user);


                    List<Result> results = new List<Result>();

                    if (model.SelfOrAllResults == "self")
                    {
                        results = _getDbItems.GetResultsByStudentId(stu.StudentId).Where(e => (e.Epaid == num) && (e.SelfEval == true)).ToList();
                        Console.WriteLine(results);
                        if (results.Count > 0)
                        {
                            return RedirectToAction("Dashboard");
                        }
                        else
                        {
                            TempData["ValidationMsg"] = "You do not have any self-evaluated results for this EPA.";
                            return View("DashboardChoices");
                        }
                    }
                    else
                    {
                        results = _getDbItems.GetResultsByStudentId(stu.StudentId).Where(e => (e.Epaid == num) && (e.SelfEval == false)).ToList();
                        if (results.Count > 0)
                        {
                            return RedirectToAction("Dashboard");
                        }
                        else
                        {
                            TempData["ValidationMsg"] = "You do not have any non self-evaluated results for this EPA.";
                            return View("DashboardChoices");
                        }
                    }
                }
                else
                {
                    return View("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return View("DashboardChoices");
            }

        }

        [HttpPost]
        public ActionResult SubmitInitDashboardChoices([DataSourceRequest] DataSourceRequest request, InitDashboardChoicesViewModel model)
        {
            try
            {
                if (_userInfo.IsValidUser())
                {
                    long? num = Convert.ToInt64(model.EpaChoiceId);


                    // set choices in http context for dashboard screen
                    _httpContextAccessor.HttpContext.Session.SetString("Epa", model.EpaChoiceId.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("InitChoice", model.StudentOrSelfComparison);

                    // validate that they exist before sending user to dashboard
                    String user = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
                    Student stu = _getDbItems.GetStudentByPawsId(user);


                    List<Result> results = new List<Result>();

                    // if the student wants to see individual growth
                    if (model.StudentOrSelfComparison == "self")
                    {
                        // see if they have results for the chosen EPA
                        results = _getDbItems.GetResultsByStudentId(stu.StudentId).Where(e => (e.Epaid == num)).ToList();
                        if (results.Count > 0)
                        {
                            // if so, direct to dashboard for individual growth
                            return RedirectToAction("DashboardIndividualGrowth");
                        }
                        else
                        {
                            // no results for EPA
                            TempData["ValidationMsg"] = "An error occurred. Please try again.";
                            return View("FirstDashboardChoices");
                        }
                    }
                    else
                    {
                        // student wants to see comparisons with all students results
                        results = _getDbItems.GetResultsByStudentId(stu.StudentId).Where(e => (e.Epaid == num)).ToList();
                        if (results.Count > 0)
                        {
                            // if results exist, go to dashboard to select which comparison they want to see (self eval or evaluators)
                            return RedirectToAction("DashboardChoices");
                        }
                        else
                        {
                            TempData["ValidationMsg"] = "An error occurred. Please try again.";
                            return View("FirstDashboardChoices");
                        }
                    }
                }
                else
                {
                    return View("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return View("DashboardChoices");
            }

        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (_userInfo.IsValidUser())
            {
                string user = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
                string initChoice = _httpContextAccessor.HttpContext.Session.GetString("Choice");
                Student student = _getDbItems.GetStudentByPawsId(user);

                List<Result> studentsResults = new List<Result>();

                double? allStudentAvg = 0;
                double? userScore = 0;

                if (student != null)
                {
                    string epa = _httpContextAccessor.HttpContext.Session.GetString("Epa");
                    long? epaId = Convert.ToInt64(epa);
                    string choice = _httpContextAccessor.HttpContext.Session.GetString("Choice");

                    if (choice == "self")
                    {
                        long? classId = student.ClassId;
                        // results for all students from same classs Id with same EPA and also self evaluated
                        List<Result> results = _getDbItems.GetResultsFromSameClassId(classId).Where(e => (e.Epaid == epaId) && (e.SelfEval == true) && (e.AgScore != null)).ToList();
                        // will retrieve the results under same guidelines but for the specific student we are focused on
                        studentsResults = _getDbItems.GetResults().Where(e => (e.Epaid == epaId) && (e.SelfEval == true) && (e.StudentId == student.StudentId) && (e.AgScore != null)).ToList();

                        // now we need to average these results
                        double? totalScore = 0;
                        for (int i = 0; i < results.Count; i++)
                        {
                            totalScore += results[i].AgScore;

                        }

                        allStudentAvg = totalScore / results.Count;
                    }
                    else
                    {
                        long? classId = student.ClassId;
                        // results for all students from same classs Id with same EPA and also self evaluated
                        List<Result> results = _getDbItems.GetResultsFromSameClassId(classId).Where(e => (e.Epaid == epaId) && (e.SelfEval == false) && (e.AgScore != null)).ToList();
                        // will retrieve the results under same guidelines but for the specific student we are focused on
                        studentsResults = _getDbItems.GetResults().Where(e => (e.Epaid == epaId) && (e.SelfEval == false) && (e.StudentId == student.StudentId) && (e.AgScore != null)).ToList();

                        // now we need to average these results
                        double? totalScore = 0;
                        for (int i = 0; i < results.Count; i++)
                        {
                            totalScore += results[i].AgScore;
                        }

                        allStudentAvg = totalScore / results.Count;
                    }

                    double count = studentsResults.Count;
                    double? aggStudentScore = 0;

                    for (int i = 0; i < studentsResults.Count; i++)
                    {
                        if (studentsResults[i] != null)
                        {
                            aggStudentScore += studentsResults[i].AgScore;
                        }

                    }

                    // work out how to create graph in view
                    ViewBag.all_users_avg_score = allStudentAvg;
                    ViewBag.specific_user_avg_score = aggStudentScore / count;

                }

                return View();
            }
            else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult DashboardIndividualGrowth()
        {
            if (_userInfo.IsValidUser())
            {
                List<double> list_of_self_scores = new List<double>();
                List<double> list_of_eval_scores = new List<double>();

                string user = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");
                Student student = _getDbItems.GetStudentByPawsId(user);

                string epa = _httpContextAccessor.HttpContext.Session.GetString("Epa");
                long? epaId = Convert.ToInt64(epa);

                List<Result> studentsResultsSelfEval = new List<Result>();
                List<Result> studentsResultsNonSelfEval = new List<Result>();

                studentsResultsSelfEval = _getDbItems.GetResults().Where(e => (e.Epaid == epaId) && (e.SelfEval == true) && (e.StudentId == student.StudentId) && (e.AgScore != null)).ToList();
                studentsResultsNonSelfEval = _getDbItems.GetResults().Where(e => (e.Epaid == epaId) && (e.SelfEval == false) && (e.StudentId == student.StudentId) && (e.AgScore != null)).ToList();

                List<DateTime> listOfSelfEvalDates = new List<DateTime>();
                List<DateTime> listOfNonSelfEvalDates = new List<DateTime>();

                if (student != null)
                {
                    foreach(var result in studentsResultsSelfEval)
                    {
                        list_of_self_scores.Add((double)result.AgScore);
                        //listOfSelfEvalDates.Add((DateTime)result.DateOfEval);
                    }

                    foreach(var result in studentsResultsNonSelfEval)
                    {
                        list_of_eval_scores.Add((double)result.AgScore);
                        //listOfNonSelfEvalDates.Add((DateTime)result.DateOfEval);
                    }

                    ViewBag.list_of_self_scores = list_of_self_scores;
                    ViewBag.list_of_eval_scores = list_of_eval_scores;
                    //ViewBag.self_eval_dates = listOfSelfEvalDates;
                    //ViewBag.non_self_eval_dates = listOfNonSelfEvalDates;

                    return View();
                }

                else
                {
                    TempData["ValidationMsg"] = ExpiredSession;
                    return RedirectToAction("Index", "Home");
                }
            } else
            {
                TempData["ValidationMsg"] = ExpiredSession;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
