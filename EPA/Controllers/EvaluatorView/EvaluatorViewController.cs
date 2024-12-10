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

namespace EPA.Controllers.EvaluatorView
{
    public class EvaluatorViewController : Controller
    {
        private GetDbItems _getDbItems = new GetDbItems();
        private SetDbItems _setDbItems = new SetDbItems();
        private PrincipalContext _cntxt = new PrincipalContext(ContextType.Domain);
        private GetUserInfo _userInfo = new GetUserInfo();
        private const string ERRORMSG = "An error has occurred. Please try again.";
        private const string NoAccessMsg = "You do not have the proper access to perform this action.";

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

        //public ActionResult ViewAssignedEPAs()
        //{
        //    if (_userInfo.IsValidUser())
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        public ActionResult ViewAssignedEPAs()
        {
            return View("ViewAssignedEPAs");
        }

        public ActionResult ScoreEPAPage1()
        {
            if (_userInfo.IsValidUser())
            {
                // a string will have been set with the corresponding resultId, parse to long
                long resultId = long.Parse(HttpContext.Session.GetString("resultId"));
                // need to retrieve the questions associated with the EPA this result is associated with
                Result result = _getDbItems.GetResultById(resultId);
                // retrieve id of epa (not requested EPA but actual standard EPA)
                long epaId = _getDbItems.GetEPAById(result.Epaid).Epaid;

                List<QuestionToEpa> listOfQuestionIds = _getDbItems.GetQuestionsToEpasByEpaId(epaId);
                List<Question> questions = new List<Question>();

                foreach (var question in listOfQuestionIds)
                {
                    Question retrieved_question = _getDbItems.GetQuestionById(question.QuestionId);
                    if (retrieved_question != null)
                    {
                        questions.Add(retrieved_question);
                    }
                }

                ViewBag.Questions = questions;

                return View("ScoreEPAPage1");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ScoreEPAPage2()
        {
            if (_userInfo.IsValidUser())
            {
                // a string will have been set with the corresponding resultId, parse to long
                long resultId = long.Parse(HttpContext.Session.GetString("resultId"));
                // need to retrieve the questions associated with the EPA this result is associated with
                Result result = _getDbItems.GetResultById(resultId);
                // retrieve id of epa (not requested EPA but actual standard EPA)
                long epaId = _getDbItems.GetEPAById(result.Epaid).Epaid;

                List<QuestionToEpa> listOfQuestionIds = _getDbItems.GetQuestionsToEpasByEpaId(epaId);
                List<Question> questions = new List<Question>();

                foreach (var question in listOfQuestionIds)
                {
                    Question retrieved_question = _getDbItems.GetQuestionById(question.QuestionId);
                    if (retrieved_question != null)
                    {
                        questions.Add(retrieved_question);
                    }
                }

                List<ResultItem> listOfResultItems = _getDbItems.GetResultItems().Where(e => e.ResultId == resultId).ToList();

                if (listOfResultItems.Count == questions.Count)
                {
                    return View("ScoreEPAPage2");
                } else
                {
                    TempData["ValidationMsg"] = "Make sure all questions for this EPA have been given scores before progressing.";
                    return RedirectToAction("ScoreEPAPage1");
                }


                
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ScoreEPAPage3()
        {
            if (_userInfo.IsValidUser())
            {
                return View("ScoreEPAPage3");
            }
            else
            {
                TempData["ValidationMsg"] = "Your session has expired. Please sign in.";
                return RedirectToAction("Index", "Home");
            }
        }

        public virtual JsonResult ReadEPAs([DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            // when admin logs in it sets the email as a string, retrieve it here
            // byte[] email = HttpContext.Session.GetString("Email");
            string email = HttpContext.Session.GetString("Email");

            Evaluator evaluator = _getDbItems.GetEvaluatorByEmail(email);

            List<EPAViewModel> viewmodels = new List<EPAViewModel>();
            List<Result> epas = _getDbItems.GetResultsByEvaluatorId(evaluator.EvaluatorId);
            viewmodels = GetEPAViewModelList(epas);


            return Json(viewmodels.OrderByDescending(x => x.ResultId).ToDataSourceResult(dataSourceRequest));
        }

        public List<EPAViewModel> GetEPAViewModelList(List<Result> epas)
        {
            List<EPAViewModel> vms = epas.Select(a => new EPAViewModel
            {
                // getDBItems returns most as a list so we pull from 0th index to get returned item
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

        [HttpPost]
        public string ScoreEPA(long resultId)
        {
            try
            {
                
                // there should be an EPA with the corresponding result ID.
                // results are created when the student requests the EPA
                // so this is simply matching the ID which was chosen from the grid row to what we have in the database
                // gets 0th index because the results are returned as list
                Result result = _getDbItems.GetResultById(resultId);

                if (result != null)
                {
                    HttpContext.Session.SetString("resultId", resultId.ToString());
                    return "Continue to View EPA";
                }
                else
                {
                    TempData["ValidationMsg"] = "Error - cannot find associated EPA.";
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
        public ActionResult SubmitEPAScorePage1([DataSourceRequest] DataSourceRequest request, ResultItem item)
        {
            
            // a string will have been set with the corresponding resultId, parse to long
            long resultId = long.Parse(HttpContext.Session.GetString("resultId"));
            // need to retrieve the questions associated with the EPA this result is associated with
            Result epa = _getDbItems.GetResultById(resultId);
            // retrieve id of epa (not requested EPA but actual standard EPA)
            long epaId = _getDbItems.GetEPAById(epa.Epaid).Epaid;
            string result = "";

            ResultItem newResult = new ResultItem()
            {
                ResultId = epa.ResultsId,
                QuestionId = item.QuestionId,
                Score = item.Score
            };

            ResultItem retrieved_item = _getDbItems.GetResultItems().Where(e => (e.ResultId == resultId) && (e.QuestionId == item.QuestionId)).FirstOrDefault();

            List<QuestionToEpa> listOfQuestionIds = _getDbItems.GetQuestionsToEpasByEpaId(epaId);
            List<long?> questionIds = new List<long?>();

            foreach (var question in listOfQuestionIds)
            {
                Question retrieved_question = _getDbItems.GetQuestionById(question.QuestionId);
                if (retrieved_question != null)
                {
                    int id = retrieved_question.QuestionId;
                    long questionId = (long)id;
                    questionIds.Add(questionId);
                }
            }

            if (!questionIds.Contains(item.QuestionId))
            {
                TempData["ValidationMsg"] = "This question does not need to be scored for this EPA.";
                return ScoreEPAPage1();
            }

            if (retrieved_item != null)
            {
                TempData["ValidationMsg"] = "This question has already been scored for this EPA.";
                return ScoreEPAPage1();
            }
            else
            {
                result = _setDbItems.CreateResultItem(newResult);
            }


            if (result == "SUCCESS!")
            {
                // this page works in such a way that each question will have its own form submitted
                // as such, the returned view should be the same page
                // this might have to be changed though 
                TempData["ValidationMsg"] = "The question and score were successfully saved. Make sure all are submitted before progressing.";
                return ScoreEPAPage1();
            }
            else
            {
                TempData["ValidationMsg"] = ERRORMSG;
                return View("ScoreEPAPage1");
            }
        
            
        }

        [HttpPost]
        public string HideEPA([DataSourceRequest] DataSourceRequest request, long resultId)
        {
            try
            {
                if (_userInfo.IsAdminUser())
                {
                    // there should be an EPA with the corresponding result ID.
                    // results are created when the student requests the EPA
                    // so this is simply matching the ID which was chosen from the grid row to what we have in the database
                    // gets 0th index because the results are returned as list
                    Result result = _getDbItems.GetResultById(resultId);

                    if (result != null)
                    {
                        string response = _setDbItems.RemoveEPA(result);
                        return "EPA was removed";
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

        [HttpPost]
        public ActionResult SubmitEPAScorePage2Comments([DataSourceRequest] DataSourceRequest request, EPASecondPageScoringViewModel comments)
        {
            if (_userInfo.IsValidUser())
            {
                // a string will have been set with the corresponding resultId, parse to long
                long resultId = long.Parse(HttpContext.Session.GetString("resultId"));
                // need to retrieve the questions associated with the EPA this result is associated with
                Result epa = _getDbItems.GetResultById(resultId);
                // retrieve id of epa (not requested EPA but actual standard EPA)
                long epaId = _getDbItems.GetEPAById(epa.Epaid).Epaid;

                ResultComment newComments = new ResultComment();
                newComments.ResultId = resultId;

                if (comments.Comment1 != null)
                {
                    newComments.Comment1 = comments.Comment1;
                }
                else
                {
                    TempData["ValidationMsg"] = "Comment 1 was left empty.";
                    return View("ScoreEPAPage2");
                }

                if (comments.Comment2 != null)
                {
                    newComments.Comment2 = comments.Comment2;
                }
                else
                {
                    TempData["ValidationMsg"] = "Comment 2 was left empty.";
                    return View("ScoreEPAPage2");
                }

                ResultComment retrieved_item = _getDbItems.GetResultComments().Where(e => e.ResultId == resultId).FirstOrDefault();

                if (retrieved_item != null)
                {
                    TempData["ValidationMsg"] = "This requested EPA already has comments attributed to it.";
                    return View("ScoreEPAPage2");
                }

                string result = _setDbItems.CreateResultComments(newComments);

                if (result == "SUCCESS!")
                {
                    TempData["ValidationMsg"] = "The comments associated with this EPA have been saved successfully.";
                    return View("ScoreEPAPage2");
                }
                else
                {
                    TempData["ValidationMsg"] = ERRORMSG + "There was an error saving your comments. Please try again.";
                    return View("ScoreEPAPage2");
                }
            }
            else
            {
                TempData["ValidationMsg"] = NoAccessMsg;
                return View("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult SubmitEPAScorePage2Score([DataSourceRequest] DataSourceRequest request, EPASecondPageScoringViewModel score)
        {
            if (_userInfo.IsValidUser())
            {
                // a string will have been set with the corresponding resultId, parse to long
                long resultId = long.Parse(HttpContext.Session.GetString("resultId"));
                Result epa = _getDbItems.GetResultById(resultId);
                // retrieve id of epa (not requested EPA but actual standard EPA)
                long epaId = _getDbItems.GetEPAById(epa.Epaid).Epaid;

                ResultComment comments = _getDbItems.GetCommentsByResultId(resultId);

                if (comments == null)
                {
                    TempData["ValidationMsg"] = "Comments associated with this result need to be submitted before giving an overall score.";
                    return View("ScoreEPAPage2");
                }



                if (epa != null)
                {
                    epa.AgScore = score.AgScore;
                }


                string result = _setDbItems.UpdateEPA(epa);

                if (result == "SUCCESS!")
                {
                    TempData["ValidationMsg"] = "The overall score associated with this EPA have been saved successfully.";
                    return View("ViewAssignedEPAs");
                }
                else
                {
                    TempData["ValidationMsg"] = ERRORMSG + "There was an error saving the score. Please try again.";
                    return View("ScoreEPAPage2");
                }
            }
            else
            {
                TempData["ValidationMsg"] = NoAccessMsg;
                return View("Index", "Home");
            }
        }

    }
}
