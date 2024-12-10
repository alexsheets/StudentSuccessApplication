using EPA.Classes;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;
using ExcelDataReader;
using System.Data;
using EPA.Models;
using EPA.Models.ViewModels;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Text;
using Telerik.SvgIcons;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EPA.Controllers.AdminViewController
{
    public class AdminViewController : Controller
    {

        private GetDbItems _getDbItems = new GetDbItems();
        private SetDbItems _setDbItems = new SetDbItems();
        private PrincipalContext _cntxt = new PrincipalContext(ContextType.Domain);
        private GetUserInfo _userInfo = new GetUserInfo();
        private const string ERRORMSG = "An error has occurred. Please try again.";
        private const string NoAccessMsg = "You do not have the proper access to perform this action.";

        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public IActionResult Index()
        {
            return View();
        }

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

        /*
         * Functions for returning the associated view.
         */
        public ActionResult UploadStudents()
        {
            if (_userInfo.IsValidUser())
            {
                return View("UploadStudents");
            } else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult UploadEvaluators()
        {
            if (_userInfo.IsValidUser())
            {
                return View("UploadEvaluators");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }
        
        public ActionResult EditEvaluatorsGrid()
        {
            if (_userInfo.IsValidUser())
            {
                return View("EditEvaluatorsGrid");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditStudentsGrid()
        {
            if (_userInfo.IsValidUser())
            {
                return View("EditStudentsGrid");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GradingSummaryReport()
        {
            if (_userInfo.IsValidUser())
            {
                return View("GradingSummaryReport");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GradingSummaryReportChoices()
        {
            if (_userInfo.IsValidUser())
            {
                return View("GradingSummaryReportChoices");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SpecificGradingSummaryReport()
        {
            if (_userInfo.IsValidUser())
            {
                return View("SpecificGradingSummaryReport");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SpecificGradingSummaryReportChoices()
        {
            if (_userInfo.IsValidUser())
            {
                return View("SpecificGradingSummaryReportChoices");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditExternshipsGrid()
        {
            if (_userInfo.IsValidUser())
            {
                return View("EditExternshipsGrid");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public List<GradingReportViewModel> GetGradingReportViewModels(List<GradingReport> grades)
        {
            List<GradingReportViewModel> vms = grades.Select(a => new GradingReportViewModel
            {
                // Year = a.Year,
                Name = a.Name,
                PawsId = a.PawsId,
                CountOfSelfEvals = a.CountOfSelfEvals,
                LastSelfEvalDate = a.LastSelfEvalDate,
                ReflectionSubmissionDate = a.ReflectionSubmissionDate,
            }).ToList();

            return vms;
        }

        public List<StudentSpecificGradingReportViewModel> GetStudentGradingReportViewModels(List<StudentSpecificGradingReport> grades)
        {
            List<StudentSpecificGradingReportViewModel> vms = grades.Select(a => new StudentSpecificGradingReportViewModel
            {
                // Year = a.Year,
                Name = a.Name,
                PawsId = a.PawsId,
                Class = a.Class,
                Epa1Comp = a.Epa1Epa,
                Epa1Grade = a.Epa1Grade,
                Epa1Strengths = a.Epa1Strengths,
                Epa1Improve = a.Epa1Improve,
                Epa1Plan = a.Epa1Plan,
                Epa2Comp = a.Epa2Epa,
                Epa2Grade = a.Epa2Grade,
                Epa2Improve = a.Epa2Improve,
                Epa2Plan = a.Epa2Plan,
                Epa2Strengths = a.Epa2Strengths,
                Epa3Comp = a.Epa3Epa,
                Epa3Grade = a.Epa3Grade,
                Epa3Improve = a.Epa3Improve,
                Epa3Plan = a.Epa3Plan,
                Epa3Strengths = a.Epa3Strengths,
                Epa4Comp = a.Epa4Epa,
                Epa4Grade = a.Epa4Grade,
                Epa4Improve = a.Epa4Improve,
                Epa4Plan = a.Epa4Plan,
                Epa4Strengths = a.Epa4Strengths,
                Epa5Comp = a.Epa5Epa,
                Epa5Grade = a.Epa5Grade,
                Epa5Improve = a.Epa5Improve,
                Epa5Plan = a.Epa5Plan,
                Epa5Strengths = a.Epa5Strengths,
            }).ToList();

            return vms;
        }

        public ActionResult ReadGrades([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                String start = _httpContextAccessor.HttpContext.Session.GetString("Start");
                DateTime startDate = DateTime.Parse(start);

                String end = _httpContextAccessor.HttpContext.Session.GetString("End");
                DateTime endDate = DateTime.Parse(end);

                List<GradingReport> grades = _getDbItems.GetGradingReports().Where(e => (e.LastSelfEvalDate > startDate) && (e.LastSelfEvalDate < endDate)).ToList(); 
                List<GradingReportViewModel> vms = GetGradingReportViewModels(grades);
                return Json(vms.OrderByDescending(x => x.Name).ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return Json(new { });
            }
        }

        public ActionResult ReadStudentSpecificGrades([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                String start = _httpContextAccessor.HttpContext.Session.GetString("StartStudentReportDate");
                DateTime startDate = DateTime.Parse(start);

                String end = _httpContextAccessor.HttpContext.Session.GetString("EndStudentReportDate");
                DateTime endDate = DateTime.Parse(end);

                List<StudentSpecificGradingReport> grades = _getDbItems.GetStudentGradingReports().Where(e => (e.DateOfLastEval > startDate) && (e.DateOfLastEval < endDate)).ToList();
                List<StudentSpecificGradingReportViewModel> vms = GetStudentGradingReportViewModels(grades);
                return Json(vms.OrderByDescending(x => x.Name).ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return Json(new { });
            }
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string b64, string fileName)
        {
            try
            {
                var fileContents = Convert.FromBase64String(b64);

                return File(fileContents, contentType, fileName);
            } catch (Exception ex)
            {
                return Json(new { });
            }
            
        }

        [HttpPost]
        public ActionResult SubmitInitReportChoices([DataSourceRequest] DataSourceRequest request, GradingReportChoicesViewModel model)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Start", model.StartDate.ToString());
            _httpContextAccessor.HttpContext.Session.SetString("End", model.EndDate.ToString());

            // remove current grade reports from database
            string response = _setDbItems.RemoveGradeReports();

            if (response == "SUCCESS!")
            {
                // use process scores function with submitted start and end date
                ProcessScores(model.StartDate, model.EndDate, model.Class);
                return View("GradingSummaryReport");
            } else
            {
                TempData["ValidationMsg"] = response;
                return View("SubmitInitReportChoices");
            }

            
        }

        [HttpPost]
        public ActionResult SubmitInitStudentReportChoices([DataSourceRequest] DataSourceRequest request, GradingReportChoicesViewModel model)
        {
            _httpContextAccessor.HttpContext.Session.SetString("StartStudentReportDate", model.StartDate.ToString());
            _httpContextAccessor.HttpContext.Session.SetString("EndStudentReportDate", model.EndDate.ToString());

            // remove current grade reports from database
            string response = _setDbItems.RemoveStudentGradeReports();

            if (response == "SUCCESS!")
            {
                // use process scores function with submitted start and end date
                ProcessSpecificStudentScoresSelfEvals(model.StartDate, model.EndDate, model.Class);
                return View("SpecificGradingSummaryReport");
            }
            else
            {
                TempData["ValidationMsg"] = response;
                return View("SubmitInitStudentReportChoices");
            }
        }

        [HttpPost]
        public void ProcessScores(DateTime start, DateTime end, string year)
        {
            // get all results between these dates
            List<Result> results = _getDbItems.GetResults().Where(e => (e.DateOfEval > start) && (e.DateOfEval < end)).ToList();

            // create list of students who have results within this list
            List<long> studentIds = results.Select(x => x.StudentId).Distinct().ToList();
            List<Student> students = new List<Student>();

            // for each id, retrieve student associated and add them to a list
            foreach(long id in studentIds)
            {
                int num = Int32.Parse(id.ToString());
                Student student = _getDbItems.GetStudentById(num);

                Class c = _getDbItems.GetClassByStr(year);
                if (student.ClassId == c.ClassId)
                {
                    students.Add(_getDbItems.GetStudentById(num));
                }
            }

            // instantiate list of grading reports
            List<GradingReport> reports = new List<GradingReport>();

            // for each student, we have to gather data and create a grading report entry/new row in the grading report
            foreach(Student student in students)
            {
                // grab number of self evals for said student where they have given themselves a score
                int numOfSelfEvals = _getDbItems.GetResultsByStudentId(student.StudentId).Where(e => e.AgScore != null).Count();

                if (numOfSelfEvals > 0)
                {
                    // grab most recent self evaluation date
                    DateTime recentEval = (DateTime)_getDbItems.GetResultsByStudentId(student.StudentId).Where(e => e.AgScore != null).LastOrDefault().DateOfEval;
                    List<Reflection> reflections = _getDbItems.GetReflectionByPawsId(student.PawsId).ToList();

                    if (reflections.Count > 0)
                    {
                        // grab time of most recent reflection question
                        DateTime timeSubmittedReflection = (DateTime)_getDbItems.GetReflectionByPawsId(student.PawsId).LastOrDefault().DateSubmitted;

                        if (recentEval != null)
                        {

                            // process their information and scores 
                            GradingReport gReport = new GradingReport()
                            {
                                Year = DateTime.Now.Year.ToString(),
                                Name = student.LastName + ", " + student.FirstName,
                                PawsId = student.PawsId,
                                CountOfSelfEvals = numOfSelfEvals,
                                LastSelfEvalDate = recentEval,
                                ReflectionSubmissionDate = timeSubmittedReflection,
                            };
                            // add to list of grading reports
                            reports.Add(gReport);
                        }
                    } else
                    {
                        if (recentEval != null)
                        {

                            // process their information and scores 
                            GradingReport gReport = new GradingReport()
                            {
                                Year = DateTime.Now.Year.ToString(),
                                Name = student.LastName + ", " + student.FirstName,
                                PawsId = student.PawsId,
                                CountOfSelfEvals = numOfSelfEvals,
                                LastSelfEvalDate = recentEval,
                                ReflectionSubmissionDate = null,
                            };
                            // add to list of grading reports
                            reports.Add(gReport);
                        }
                    }
                }
            }

            // upload the entire list to db
            string resp = _setDbItems.CreateGradeReports(reports);



        }

        [HttpPost]
        public void ProcessSpecificStudentScoresSelfEvals(DateTime start, DateTime end, string year)
        {
            // get all results between these dates
            List<Result> results = _getDbItems.GetResults().Where(e => (e.DateOfEval > start) && (e.DateOfEval < end)).ToList();

            // create list of students who have results within this list
            List<long> studentIds = results.Select(x => x.StudentId).Distinct().ToList();
            List<Student> students = new List<Student>();

            // for each id, retrieve student associated and add them to a list
            foreach (long id in studentIds)
            {
                int num = Int32.Parse(id.ToString());
                Student student = _getDbItems.GetStudentById(num);

                Class c = _getDbItems.GetClassByStr(year);
                if (student.ClassId == c.ClassId)
                {
                    students.Add(_getDbItems.GetStudentById(num));
                }

            }

            // instantiate list of grading reports
            List<StudentSpecificGradingReport> reports = new List<StudentSpecificGradingReport>();

            // for each student, we have to gather data and create a grading report entry/new row in the grading report
            foreach (Student student in students)
            {
                // grab number of self evals for said student where they have given themselves a score between the given dates
                int numOfSelfEvals = _getDbItems.GetResultsByStudentId(student.StudentId).Where(e => (e.AgScore != null) && (e.DateOfEval > start) && (e.DateOfEval < end)).Count();

                if (numOfSelfEvals > 0)
                {
                    // grab most recent self evaluation date
                    DateTime recentEval = (DateTime)_getDbItems.GetResultsByStudentId(student.StudentId).Where(e => e.AgScore != null).LastOrDefault().DateOfEval;

                    List<Reflection> reflections = _getDbItems.GetReflectionByPawsId(student.PawsId).ToList();
                    List<Result> selfEvals = _getDbItems.GetResultsByStudentId(student.StudentId).Where(e => (e.AgScore != null) && (e.DateOfEval > start) && (e.DateOfEval < end)).ToList();

                    StudentSpecificGradingReport report = new StudentSpecificGradingReport();

                    // need to process each self eval using number of self evals
                    for (int i = 0; i < numOfSelfEvals; i++)
                    {
                        // retrieve result at this index of self evals
                        Result result = selfEvals[i];
                        ResultItem item = _getDbItems.GetResultItems().Where(e => e.ResultId == result.ResultsId).FirstOrDefault();

                        string selfEval1 = item.SelfEvalReflection1.Replace("\r\n", string.Empty);
                        string selfEval2 = item.SelfEvalReflection2.Replace("\r\n", string.Empty);
                        string selfEval3 = item.SelfEvalReflection3.Replace("\r\n", string.Empty);

                        

                        // assign each result if they exist
                        if ((result != null) && (i == 0))
                        {
                            report.Epa1Epa = _getDbItems.GetEPAById(result.Epaid).SectionTag == null ? "N/A" : _getDbItems.GetEPAById(result.Epaid).SectionTag;
                            report.Epa1Grade = (float?)result.AgScore;
                            report.Epa1Strengths = selfEval1;
                            report.Epa1Improve = selfEval2;
                            report.Epa1Plan = selfEval3;
                        }
                        else if ((result != null) && (i == 1))
                        {
                            report.Epa2Epa = _getDbItems.GetEPAById(result.Epaid).SectionTag == null ? "N/A" : _getDbItems.GetEPAById(result.Epaid).SectionTag;
                            report.Epa2Grade = (float?)result.AgScore;
                            report.Epa2Strengths = selfEval1;
                            report.Epa2Improve = selfEval2;
                            report.Epa2Plan = selfEval3;
                        }
                        // every student will have at least 2 epas for semester (should have 3 but we start to handle null cases here)
                        else if (i == 2)
                        {
                            if (result != null)
                            {
                                report.Epa3Epa = _getDbItems.GetEPAById(result.Epaid).SectionTag;
                                report.Epa3Grade = (float?)result.AgScore;
                                report.Epa3Strengths = selfEval1;
                                report.Epa3Improve = selfEval2;
                                report.Epa3Plan = selfEval3;
                            } else
                            {
                                report.Epa3Epa = "N/A";
                                report.Epa3Grade = 0;
                                report.Epa3Strengths = "N/A";
                                report.Epa3Improve = "N/A";
                                report.Epa3Plan = "N/A";
                            }
                        }
                        else if (i == 3)
                        {
                            if (result != null)
                            {
                                report.Epa4Epa = _getDbItems.GetEPAById(result.Epaid).SectionTag;
                                report.Epa4Grade = (float?)result.AgScore;
                                report.Epa4Strengths = selfEval1;
                                report.Epa4Improve = selfEval2;
                                report.Epa4Plan = selfEval3;
                            }
                            else
                            {
                                report.Epa4Epa = "N/A";
                                report.Epa4Grade = 0;
                                report.Epa4Strengths = "N/A";
                                report.Epa4Improve = "N/A";
                                report.Epa4Plan = "N/A";
                            }
                        }
                        else if (i == 4)
                        {
                            if (result != null)
                            {
                                report.Epa5Epa = _getDbItems.GetEPAById(result.Epaid).SectionTag;
                                report.Epa5Grade = (float?)result.AgScore;
                                report.Epa5Strengths = selfEval1;
                                report.Epa5Improve = selfEval2;
                                report.Epa5Plan = selfEval3;
                            } else
                            {
                                report.Epa5Epa = "N/A";
                                report.Epa5Grade = 0;
                                report.Epa5Strengths = "N/A";
                                report.Epa5Improve = "N/A";
                                report.Epa5Plan = "N/A";
                            }
                        }
                    }
                    if (reflections != null)
                    {

                        report.Reflection1 = reflections[0].ReflectionAnswer.Replace("\r\n", string.Empty) == null ? "N/A" : reflections[0].ReflectionAnswer.Replace("\r\n", string.Empty);
                        report.Reflection2 = reflections[1].ReflectionAnswer.Replace("\r\n", string.Empty) == null ? "N/A" : reflections[1].ReflectionAnswer.Replace("\r\n", string.Empty);
                        report.Reflection3 = reflections[2].ReflectionAnswer.Replace("\r\n", string.Empty) == null ? "N/A" : reflections[2].ReflectionAnswer.Replace("\r\n", string.Empty);
                    }

                    // assign student information to this report
                    report.PawsId = student.PawsId;
                    report.Class = _getDbItems.GetClassById(student.ClassId).Name;
                    report.Name = student.LastName + ", " + student.FirstName;
                    report.DateOfLastEval = recentEval;
                    
                    // add this student specific grading report to the list of reports
                    reports.Add(report);
                }
            }
            // upload the entire list to db
            string resp = _setDbItems.CreateStudentSpecificGradeReports(reports);
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
                    var body = "You have been added to the LSU Vet Med Clinical Assessment System (CAS). You will utilize this system to complete evaluations of students on Entrustable Professional Activities (EPAs). \r\nYou can access the system here: cas.vetmed.lsu.edu\r\nYour username and password are your LSU credentials.\r\nIf you have questions or issues logging into the system, please contact Dr. Emily Erwin at ecampbell1@lsu.edu \r\n";
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
                        Subject = "Added to EPA system",
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
        public ActionResult SendStudentEmail(string email)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var senderEmail = new MailAddress("relay@vetmed.lsu.edu");
                    var receiverEmail = new MailAddress(email);
                    var body = "You have been added to the LSU Vet Med Clinical Assessment System (CAS). You will utilize this system to conduct self-assessments and request evaluator assessments of Entrustable Professional Activities (EPAs).";
                    body = body + "You can access the system here: cas.vetmed.lsu.edu\r\n" + "Your username and password are your LSU credentials.\r\nIf you have questions or issues logging into the system, please contact Dr. Emily Erwin at ecampbell1@lsu.edu \r\n";

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
                        Subject = "Added to EPA system",
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
        public ActionResult SendEmailNonLSU(string email, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var senderEmail = new MailAddress("relay@vetmed.lsu.edu");
                    var receiverEmail = new MailAddress(email);
                    var body = "You have been added to the LSU School of Veterinary Medicine’s Clinical Assessment System (CAS) because our records indicate that an LSU Vet Med student is working with you or at your clinic. You will utilize this system to complete evaluations of LSU students on Entrustable Professional Activities (EPAs) at the student’s request. ";
                    body = body + "You can access the system here: cas.vetmed.lsu.edu\r\nYour username is your email and your password is: " + password + "\r\n";
                    body = body + "If you have questions or issues logging into the system, please contact Dr. Emily Erwin at ecampbell1@lsu.edu ";
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
                        Subject = "Added to EPA system",
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

        // function used to pull information from excel sheet containing information on evaluators and create records for them in DB
        [HttpPost]
        public ActionResult ImportEvaluatorsFromExcel(IFormFile formFile)
        {
            try
            {

                if (_userInfo.IsValidUser() && (_userInfo.IsAdminUser()))
                {

                    using (var stream = formFile.OpenReadStream())
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet();
                            var tables = result.Tables;

                            List<Evaluator> newEvaluators = new List<Evaluator>();

                            int numColumns = tables[0].Columns.Count;
                            DataRowCollection rows = tables[0].Rows;
                            DataColumnCollection columns = tables[0].Columns;

                            Dictionary<int, string> colValues = new Dictionary<int, string>();
                            bool firstRow = true;

                            foreach(DataRow row in rows)
                            {
                                if (firstRow)
                                {
                                    var values = row.ItemArray;
                                    int i = 0;
                                    foreach(var val in values)
                                    {
                                        colValues.Add(i, val.ToString().Trim());
                                        i++;
                                    }
                                    firstRow = false;

                                } else
                                {
                                    var values = row.ItemArray;
                                    int i = 0;

                                    Evaluator evaluator = new Evaluator();

                                    foreach(var val in values)
                                    {
                                        string col = colValues.FirstOrDefault(e => e.Key == i).Value;


                                        switch (col)
                                        {
                                            case "Email":
                                                evaluator.Email = val.ToString();
                                                SendEmail(val.ToString());
                                                break;
                                            case "FirstName":
                                                evaluator.FirstName = val.ToString();
                                                break;
                                            case "LastName":
                                                evaluator.LastName = val.ToString();
                                                break;
                                            //case "Password":
                                            //    evaluator.Password = val.ToString();
                                            //    break;
                                            case "LSUInd":
                                                if (val.ToString() == "0")
                                                {
                                                    evaluator.Lsuind = false;
                                                } else
                                                {
                                                    evaluator.Lsuind = true;
                                                }
                                                break;
                                            case "Clinic":
                                                evaluator.Clinic = val.ToString();
                                                break;
                                            
                                        }
                                        i++;
                                    }

                                    if (evaluator.Lsuind == false)
                                    {
                                        // create password and set it
                                        string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
                                        Random randNum = new Random();
                                        char[] chars = new char[8];
                                        int allowedCharCount = _allowedChars.Length;
                                        for (int j = 0; j < 8; j++)
                                        {
                                            chars[j] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                                        }

                                        evaluator.Password = new string(chars);
                                    }

                                    newEvaluators.Add(evaluator);
                                }
                            }
                            string insertResult = _setDbItems.AddEvaluatorImports(newEvaluators);
                            if (insertResult != "SUCCESS!")
                            {
                                TempData["ValidationMsg"] = "An error occurred when trying to import the evaluators.";
                            }
                            else
                            {
                                foreach (var evaluator in newEvaluators)
                                {
                                    if (evaluator.Lsuind == true)
                                    {
                                        SendEmail(evaluator.Email);
                                    } else
                                    {
                                        // send email specifically for outside of LSU individuals
                                        SendEmailNonLSU(evaluator.Email, evaluator.Password);
                                    }
                                    
                                }

                                TempData["ValidationMsg"] = "The evaluators were successfully imported.";
                            }


                        }
                    }                    

                }
                return RedirectToAction("UploadEvaluators");
            } catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("UploadEvaluators");
            }       

        }

        // function used to pull information from excel sheet containing information on students and create records for them in DB
        [HttpPost]
        public ActionResult ImportStudentsFromExcel(IFormFile formFile)
        {
            try
            {

                if (_userInfo.IsValidUser() && (_userInfo.IsAdminUser()))
                {

                    using (var stream = formFile.OpenReadStream())
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet();
                            var tables = result.Tables;

                            List<Student> newStudents = new List<Student>();

                            int numColumns = tables[0].Columns.Count;
                            DataRowCollection rows = tables[0].Rows;
                            DataColumnCollection columns = tables[0].Columns;

                            Dictionary<int, string> colValues = new Dictionary<int, string>();
                            bool firstRow = true;

                            foreach (DataRow row in rows)
                            {
                                if (firstRow)
                                {
                                    var values = row.ItemArray;
                                    int i = 0;
                                    foreach (var val in values)
                                    {
                                        colValues.Add(i, val.ToString().Trim());
                                        i++;
                                    }
                                    firstRow = false;

                                }
                                else
                                {
                                    var values = row.ItemArray;
                                    int i = 0;

                                    Student student = new Student();

                                    foreach (var val in values)
                                    {
                                        string col = colValues.FirstOrDefault(e => e.Key == i).Value;


                                        switch (col)
                                        {
                                            case "PawsID":
                                                student.PawsId = val.ToString();
                                                var pawsEmail = val.ToString() + "@lsu.edu";
                                                SendEmail(pawsEmail);
                                                break;
                                            case "FirstName":
                                                student.FirstName = val.ToString();
                                                break;
                                            case "LastName":
                                                student.LastName = val.ToString();
                                                break;
                                            case "Class":
                                                var value = val.ToString();
                                                long? id = _getDbItems.GetClassByStr(value).ClassId;
                                                student.ClassId = id;
                                                break;

                                        }
                                        i++;
                                    }
                                    newStudents.Add(student);
                                    
                                }
                            }
                            string insertResult = _setDbItems.AddStudentImports(newStudents);
                            if (insertResult != "SUCCESS!")
                            {
                                TempData["ValidationMsg"] = "An error occurred when trying to import the students.";
                            }
                            else
                            {
                                foreach(var student in newStudents)
                                {
                                    var email = student.PawsId + "@lsu.edu";
                                    SendEmail(email);
                                }

                                TempData["ValidationMsg"] = "The students were successfully imported.";
                            }


                        }
                    }

                }
                return RedirectToAction("UploadStudents");
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return RedirectToAction("UploadStudents");
            }
        }

        public virtual JsonResult ReadExternships([DataSourceRequest] DataSourceRequest dataSourceRequest)
        {

            List<ExternalRegistrationViewModel> viewmodels = new List<ExternalRegistrationViewModel>();


            List<ExternshipRegistration> externships = _getDbItems.GetExternshipRegistrations().ToList();

            viewmodels = GetExternshipViewModels(externships);
                

            return Json(viewmodels.OrderByDescending(x => x.StartDate).ToDataSourceResult(dataSourceRequest));
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
                EvaluatorTitle = a.EvaluatorTitle
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

        // functions for reading information and returning viewmodels
        public ActionResult ReadStudents([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<Student> students = _getDbItems.GetAllStudents().ToList();
                List<StudentViewModel> vms = GetStudentViewModelList(students);
                return Json(vms.OrderByDescending(x => x.StudentId).ToDataSourceResult(request));
            } catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return Json(new { });
            }
            
        }

        public List<StudentViewModel> GetStudentViewModelList(List<Student> students)
        {

            
            List<StudentViewModel> vms = students.Select(a => new StudentViewModel
            {
                StudentId = a.StudentId,
                PawsId = a.PawsId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                ConcentrationId = a.ConcentrationId,
                ConcentrationStr = _getDbItems.GetConcentrationById(a.ConcentrationId).Description,
                //UpdateDt = a.UpdateDt,
                ClassId = a.ClassId,
                ClassStr = _getDbItems.GetClassById(a.ClassId).Name,
            }).ToList();

            return vms;
        }

        [HttpPost]
        public void CreateStudent([DataSourceRequest] DataSourceRequest req, StudentViewModel svm)
        {
            if (_userInfo.IsValidUser())
            {

                long id = 1;

                string classStr = svm.ClassStr;
                if (classStr != null)
                {
                    id = _getDbItems.GetClassByStr(classStr).ClassId;
                }

                Student student = new Student()
                {
                    PawsId = svm.PawsId,
                    FirstName = svm.FirstName,
                    LastName = svm.LastName,
                    ConcentrationId = svm.ConcentrationId,
                    UpdateDt = svm.UpdateDt,
                    ClassId = id,
                    
                    Active = true
                };
                string result = _setDbItems.CreateStudent(student);

                if (result == "SUCCESS!")
                {
                    var email = svm.PawsId + "@lsu.edu";
                    SendStudentEmail(email);
                    TempData["ValidationMsg"] = "The student was successfully added to the system.";
                }
                else
                {
                    TempData["ValidationMsg"] = ERRORMSG + " Please make sure you the student you are trying to add is not missing a required value.";
                }
            }
            else
            {
                TempData["ValidationMsg"] = NoAccessMsg;
            }
        }

        [HttpPost]
        public void UpdateStudent([DataSourceRequest] DataSourceRequest request, StudentViewModel svm)
        {
            try
            {
                if (_userInfo.IsAdminUser())
                {
                    Student student = _getDbItems.GetAllStudents().FirstOrDefault(e => e.StudentId == svm.StudentId);

                    if (student != null)
                    {
                        Student updated_student = new Student()
                        {
                            StudentId = svm.StudentId,
                            PawsId = svm.PawsId,
                            FirstName = svm.FirstName,
                            LastName = svm.LastName,
                            ConcentrationId = svm.ConcentrationId,
                            UpdateDt = svm.UpdateDt,
                            ClassId = svm.ClassId
                        };
                        string res = _setDbItems.UpdateStudent(updated_student);

                        if (res == "SUCCESS!")
                        {
                            TempData["ValidationMsg"] = "The student was succesfully updated.";
                        }
                        else
                        {
                            TempData["ValidationMsg"] = ERRORMSG;
                        }
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = NoAccessMsg;
                }
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = NoAccessMsg;
            }
        }


        [HttpPost]
        public void RemoveStudent([DataSourceRequest] DataSourceRequest request, StudentViewModel svm)
        {
            try
            {
                if (_userInfo.IsValidUser() && (_userInfo.IsAdminUser()))
                {
                    string result = _setDbItems.RemoveStudent((int)svm.StudentId);

                    if (result == "SUCCESS!")
                    {
                        TempData["ValidationMsg"] = "The student was successfully removed.";
                    }
                    else
                    {
                        TempData["ValidationMsg"] = ERRORMSG;
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = NoAccessMsg;
                }

            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
            }
        }

        public virtual JsonResult ReadEvaluators([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<Evaluator> evaluators = _getDbItems.GetEvaluators();
                List<EvaluatorViewModel> vms = GetEvaluatorViewModelList(evaluators);
                return Json(vms.OrderByDescending(x => x.EvaluatorId).ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return Json(new { });
            }
        }

        public List<EvaluatorViewModel> GetEvaluatorViewModelList(List<Evaluator> evaluators)
        {
            List<EvaluatorViewModel> vms = evaluators.Select(a => new EvaluatorViewModel
            {
                EvaluatorId = a.EvaluatorId,
                Email = a.Email,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Password = a.Password,
                Lsuind = a.Lsuind,
                Clinic = a.Clinic,
                UpdateDt = a.UpdateDt
            }).ToList();

            return vms;
        }

        [HttpPost]
        public void CreateEvaluator([DataSourceRequest] DataSourceRequest req, EvaluatorViewModel evm)
        {
            if (_userInfo.IsValidUser())
            {
                Evaluator evaluator = new Evaluator()
                {
                    Email = evm.Email,
                    FirstName = evm.FirstName,
                    LastName= evm.LastName,
                    Password = evm.Password,
                    Lsuind = evm.Lsuind,
                    Clinic = evm.Clinic,
                    UpdateDt = evm.UpdateDt,
                    Active = true
                };
                string result = _setDbItems.CreateEvaluator(evaluator);

                if (result == "SUCCESS!")
                {
                    if (evm.Lsuind == true)
                    {
                        SendEmail(evm.Email);
                    } else
                    {
                        // create password and set it
                        string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
                        Random randNum = new Random();
                        char[] chars = new char[8];
                        int allowedCharCount = _allowedChars.Length;
                        for (int j = 0; j < 8; j++)
                        {
                            chars[j] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                        }

                        var password = new string(chars);

                        SendEmailNonLSU(evm.Email, password);
                    }

                   
                    TempData["ValidationMsg"] = "The evaluator was successfully added to the system.";
                }
                else
                {
                    TempData["ValidationMsg"] = ERRORMSG + " Please make sure you the evaluator you are trying to add is not missing a required value.";
                }
            }
            else
            {
                TempData["ValidationMsg"] = NoAccessMsg;
            }
        }

        [HttpPost]
        public void UpdateEvaluator([DataSourceRequest] DataSourceRequest request, EvaluatorViewModel evm)
        {
            try
            {
                if (_userInfo.IsAdminUser())
                {
                    Evaluator evaluator = _getDbItems.GetEvaluators().FirstOrDefault(e => e.EvaluatorId == evm.EvaluatorId);

                    if (evaluator != null)
                    {
                        Evaluator updated_evaluator = new Evaluator()
                        {
                            EvaluatorId = evm.EvaluatorId,
                            Email = evm.Email,
                            FirstName = evm.FirstName,
                            LastName = evm.LastName,
                            Password = evm.Password,
                            Lsuind = evm.Lsuind,
                            Clinic = evm.Clinic,
                            UpdateDt = evm.UpdateDt
                        };
                        string res = _setDbItems.UpdateEvaluator(updated_evaluator);

                        if (res == "SUCCESS!")
                        {
                            TempData["ValidationMsg"] = "The evaluator was succesfully updated.";
                        }
                        else
                        {
                            TempData["ValidationMsg"] = ERRORMSG;
                        }
                    }
                } else
                {
                    TempData["ValidationMsg"] = NoAccessMsg;
                }
            } catch (Exception ex)
            {
                TempData["ValidationMsg"] = NoAccessMsg;
            }
        }

        [HttpPost]
        public void RemoveEvaluator([DataSourceRequest] DataSourceRequest request, EvaluatorViewModel evm)
        {
            try
            {
                if (_userInfo.IsValidUser() && (_userInfo.IsAdminUser()))
                {
                    int j = Convert.ToInt32(evm.EvaluatorId);
                    Evaluator eval = _getDbItems.GetEvaluatorById(j);
                    if(eval.FirstName == "Self")
                    {
                        TempData["ValidationMsg"] = "Cannot remove entry for self-evaluation.";
                    } else
                    {
                        int k = Convert.ToInt32(evm.EvaluatorId);
                        string result = _setDbItems.RemoveEvaluator(k);

                        if (result == "SUCCESS!")
                        {
                            TempData["ValidationMsg"] = "The evaluator was successfully removed.";
                        }
                        else
                        {
                            TempData["ValidationMsg"] = ERRORMSG;
                        }
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = NoAccessMsg;
                }

            }
            catch (Exception ex)
            {
                TempData["ValidationMsg"] = ERRORMSG;
            }
        }



    }
}

