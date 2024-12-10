using EPA.Models;
using Microsoft.EntityFrameworkCore;
using Telerik.SvgIcons;
// using Telerik.SvgIcons;

namespace EPA.Classes
{
    public class SetDbItems
    {
        EpaContext _dbContext = new EpaContext();
        GetDbItems _getDbItems = new GetDbItems();

        /*
         * Admin View
         */

        public string RemoveGradeReports()
        {
            try
            {
                List<GradingReport> reports = _getDbItems.GetGradingReports().ToList();
                foreach (var report in reports)
                {
                    _dbContext.GradingReport.Remove(report);
                }

                _dbContext.SaveChanges();

                return "SUCCESS!";
                
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string RemoveStudentGradeReports()
        {
            try
            {
                List<StudentSpecificGradingReport> reports = _getDbItems.GetStudentGradingReports().ToList();
                foreach (var report in reports)
                {
                    _dbContext.StudentSpecificGradingReport.Remove(report);
                }

                _dbContext.SaveChanges();

                return "SUCCESS!";

            }
            catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string RequestExternship(ExternshipRegistration registration)
        {
            try
            {
                _dbContext.ExternshipRegistration.Add(registration);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string CreateGradeReports(List<GradingReport> gradeReports)
        {
            try
            {
                foreach(var report in gradeReports)
                {
                    if (report != null)
                    {
                        _dbContext.GradingReport.Add(report);
                    }
                }
                _dbContext.SaveChanges();

                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string CreateStudentSpecificGradeReports(List<StudentSpecificGradingReport> gradeReports)
        {
            try
            {
                foreach (var report in gradeReports)
                {
                    if (report != null)
                    {
                        _dbContext.StudentSpecificGradingReport.Add(report);
                    }
                }
                _dbContext.SaveChanges();

                return "SUCCESS!";
            }
            catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string AddEvaluatorImports(List<Evaluator> evaluators)
        {
            try
            {
                foreach (var newEvaluator in evaluators)
                {
                    var email = newEvaluator.Email;
                    Evaluator evaluator = _getDbItems.GetEvaluatorByEmail(email);

                    if (evaluator == null)
                    {
                        _dbContext.Evaluators.Add(newEvaluator);
                    }
                    
                }

                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
            
        }

        public string AddStudentImports(List<Student> students)
        {
            try
            {
                foreach (var newStudent in students)
                {
                    var pawsId = newStudent.PawsId;
                    Student student = _getDbItems.GetStudentByPawsId(pawsId);
                    if (student == null)
                    {
                        _dbContext.Students.Add(newStudent);
                    }
                }

                _dbContext.SaveChanges();
                return "SUCCESS!";
            }
            catch (Exception ex)
            {
                return "ERROR.";
            }

        }

        public string CreateReflectionAnswer(Reflection reflection)
        {
            try
            {
                _dbContext.Reflection.Add(reflection);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string CreateStudent(Student student)
        {
            try
            {
                student.UpdateDt = DateTime.Now;
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string RemoveStudent(int studentId)
        {
            try
            {
                Student student = _getDbItems.GetStudentById(studentId);
                // student.Active = false;
                _dbContext.Remove(student);
                _dbContext.SaveChanges();
                return "SUCCESS!";

            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string CreateEvaluator(Evaluator evaluator)
        {
            try
            {
                evaluator.UpdateDt = DateTime.Now;
                _dbContext.Evaluators.Add(evaluator);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            }
            catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string UpdateEvaluator(Evaluator evaluator)
        {
            try
            {
                evaluator.UpdateDt = DateTime.Now;
                _dbContext.Evaluators.Update(evaluator);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR";
            }
        }

        public string RemoveEvaluator(int id)
        {
            try
            {
                Evaluator eval = _getDbItems.GetEvaluatorById(id);
                // eval.Active = false;
                _dbContext.Remove(eval);
                _dbContext.SaveChanges();
                return "SUCCESS!";

            }
            catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string UpdateStudent(Student student)
        {
            try
            {
                student.UpdateDt = DateTime.Now;
                _dbContext.Students.Update(student);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        public string CreateResultItem(ResultItem resultItem)
        {
            try
            {
                resultItem.UpdateDt = DateTime.Now;

                _dbContext.ResultItems.Add(resultItem);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string CreateResultComments(ResultComment comment)
        {
            try
            {
                comment.UpdateDt = DateTime.Now;

                _dbContext.ResultComments.Add(comment);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string RemoveEPA(Result result)
        {
            try
            {
                result.Visibility = false;
                result.UpdateDt = DateTime.Now;
                _dbContext.Results.Update(result);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string RemoveExternship(ExternshipRegistration externship)
        {
            try
            {
                externship.Visibility = 0;
                _dbContext.ExternshipRegistration.Update(externship);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            }
            catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        /*
         * Student View
         */
        public string RequestEPA(Result epa)
        {
            try
            {
                epa.UpdateDt = DateTime.Now;
                _dbContext.Results.Add(epa);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            }
            catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string TrashEPA(Result epa)
        {
            try
            {
                _dbContext.Results.Update(epa);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        public string UpdateEPA(Result epa)
        {
            try
            {
                _dbContext.Results.Update(epa);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        // necessary for when student is doing a self evaluation
        public string CreateSelfEvalResult(ResultItem item)
        {
            try
            {
                _dbContext.ResultItems.Add(item);
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch (Exception ex)
            {
                return "ERROR.";
            }
        }

        // for when a student is completing the second part of their self evaluation
        // a result item will have already been created, so we just want to add the score to it
        public string UpdateSelfEvalResult(ResultItem item)
        {
            try
            {
                _dbContext.ResultItems.Update(item);
                                
                _dbContext.SaveChanges();
                return "SUCCESS!";
            } catch(Exception ex)
            {
                return "ERROR.";
            }
        }
    }
}
