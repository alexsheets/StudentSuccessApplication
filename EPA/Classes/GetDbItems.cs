using System;
using EPA.Models;
using Newtonsoft.Json.Linq;
using Telerik.SvgIcons;

namespace EPA.Classes
{
    public class GetDbItems
    {
        EpaContext dbContext = new EpaContext();

        public List<GradingReport> GetGradingReports()
        {
            try
            {
                return dbContext.GradingReport.ToList();
            }
            catch (Exception ex)
            {
                return new List<GradingReport>();
            }
        }

        public ExternshipRegistration GetExternshipRegistration(int externshipId)
        {
            try
            {
                return dbContext.ExternshipRegistration.Where(e => e.ExternshipId == externshipId).FirstOrDefault();
            }catch (Exception ex)
            {
                return new ExternshipRegistration();
            }
        }

        public List<ExternshipRegistration> GetExternshipRegistrations()
        {
            try
            {
                return dbContext.ExternshipRegistration.ToList();
            }
            catch (Exception ex)
            {
                return new List<ExternshipRegistration>();
            }
        }

        public List<StudentSpecificGradingReport> GetStudentGradingReports()
        {
            try
            {
                return dbContext.StudentSpecificGradingReport.ToList();
            }
            catch (Exception ex)
            {
                return new List<StudentSpecificGradingReport>();
            }
        }

        public List<Reflection> GetReflectionByPawsId(string pawsId)
        {
            try
            {
                return dbContext.Reflection.Where(e => e.PawsId == pawsId).ToList();
            }
            catch (Exception ex)
            {
                return new List<Reflection>();
            }
        }

        public Student GetStudentByPawsId(string pawsId)
        {
            try
            {
                return dbContext.Students.Where(e => e.PawsId == pawsId).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Student();
            }
        }

        public Student GetStudentById(int id)
        {
            try
            {
                return dbContext.Students.Where(e => e.StudentId == id).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Student();
            }
        }

        public List<Student> GetAllStudents()
        {
            try
            {
                return dbContext.Students.ToList();
            } catch (Exception ex)
            {
                return new List<Student>();
            }
        }

        public Epa GetEPAById(long? id)
        {
            try
            {
                return dbContext.Epas.Where(e => e.Epaid == id).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Epa();
            }
        }

        public List<Block> GetBlocks()
        {
            try
            {
                return dbContext.Blocks.ToList();
            } catch (Exception ex)
            {
                return new List<Block>();
            }
        }

        public List<Rotation> GetRotations()
        {
            try
            {
                return dbContext.Rotations.ToList();
            } catch (Exception ex)
            {
                return new List<Rotation>();
            }
        }

        public List<Concentration> GetConcentrations()
        {
            try
            {
                return dbContext.Concentrations.ToList();
            } catch(Exception ex)
            {
                return new List<Concentration>();
            }
        }

        public Concentration GetConcentrationById(long? id)
        {
            try
            {
                return dbContext.Concentrations.Where(e => e.ConcentrationId == id).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Concentration();
            }
        }

        public List<Question> GetQuestions()
        {
            try
            {
                return dbContext.Questions.ToList();
            } catch (Exception ex)
            {
                return new List<Question>();
            }
        }

        public List<Epa> GetAllEpas()
        {
            try
            {
                return dbContext.Epas.ToList();
            } catch (Exception ex )
            {
                return new List<Epa>();
            }
        }

        public List<Result> GetResults()
        {
            try
            {
                return dbContext.Results.ToList();
            } catch (Exception ex )
            {
                return new List<Result>();
            }
        } 

        public List<Result> GetResultsFromSameClassId(long? classId)
        {
            try
            {
                // retrieve all students from same class
                List<Student> students = dbContext.Students.Where(e => e.ClassId == classId).ToList();
                List<Result> results = new List<Result>();
                // for each student retrieved
                foreach (Student student in students)
                {
                    // retrieve list of all their results
                    List<Result> studentResults = dbContext.Results.Where(e => e.StudentId == student.StudentId).ToList();
                    // add them all to a main collection of results
                    foreach (Result result in studentResults)
                    {
                        results.Add(result);
                    }
                }
                return results;
            } catch (Exception ex)
            {
                return new List<Result>();
            }
           
        }


        public List<Result> GetResultByEpaId(int epaId)
        {
            try
            {
                return dbContext.Results.Where(e => e.Epaid == epaId).ToList();
            } catch (Exception ex)
            {
                return new List<Result>();
            }
        }

        public List<Result> GetResultsByStudentId(long studentId)
        {

            List<Result> init_results = new List<Result>();
            try
            {
                init_results = dbContext.Results.Where(e => e.StudentId == studentId).ToList();
                return init_results.Where(e => e.Visibility == true).ToList();
            } catch(Exception ex)
            {
                return new List<Result>();
            }
        }

        public List<Result> GetResultsByEvaluatorId(long evaluatorId)
        {
            try
            {
                return dbContext.Results.Where(e => e.EvaluatorId == evaluatorId).ToList();
            } catch (Exception ex )
            {
                return new List<Result>();
            }
        }

        public Result GetResultById(long? resultId)
        {
            try
            {
                return dbContext.Results.Where(e => e.ResultsId == resultId).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Result();
            }
        }

        public List<ResultItem> GetResultItems()
        {
            try
            {
                return dbContext.ResultItems.ToList();
            }
            catch (Exception ex)
            {
                return new List<ResultItem>();
            }
        }

        public List<ResultComment> GetResultComments()
        {
            try
            {
                return dbContext.ResultComments.ToList();
            }
            catch (Exception ex)
            {
                return new List<ResultComment>();
            }
        }

        public List<Evaluator> GetEvaluators()
        {
            try
            {
                return dbContext.Evaluators.ToList();
            } catch (Exception ex)
            {
                return new List<Evaluator>();
            }
        }

        public Evaluator GetEvaluatorById(int id)
        {
            try
            {
                return dbContext.Evaluators.Where(e => e.EvaluatorId == id).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Evaluator();
            }
        }

        public Evaluator GetEvaluatorByIdLong(long? id)
        {
            try
            {
                int new_id = Convert.ToInt32(id);
                return dbContext.Evaluators.Where(e => e.EvaluatorId == id).FirstOrDefault();
            } catch (Exception e)
            {
                return new Evaluator();
            }
        }

        public Evaluator GetEvaluatorByEmail(string email)
        {
            try
            {
                return dbContext.Evaluators.Where(e => e.Email == email).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Evaluator();
            }
        }
       

        public Rotation GetRotationById(long? rotationId)
        {
            try
            {
                return dbContext.Rotations.Where(e => e.RotationId == rotationId).FirstOrDefault();
            } catch(Exception ex)
            {
                return new Rotation();
            }
        }

        public Block GetBlockById(long? blockId)
        {
            try
            {
                return dbContext.Blocks.Where(e => e.BlockId == blockId).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Block();
            }
        }

        public Class GetClassById(long? classId)
        {
            try
            {
                return dbContext.Classes.Where(e => e.ClassId == classId).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Class();
            }
        }

        public Class GetClassByStr(string name)
        {
            try
            {
                return dbContext.Classes.Where(e => e.Name == name).FirstOrDefault();
            } catch (Exception ex)
            {
                return new Class();
            }
        }

        public ResultComment GetCommentsByResultId(long? resultId)
        {
            try
            {
                return dbContext.ResultComments.Where(e => e.ResultId == resultId).FirstOrDefault();
            } catch (Exception ex)
            {
                return new ResultComment();
            }
        }

        public List<QuestionToEpa> GetQuestionsToEpasByEpaId(long? epaId)
        {
            try
            {
                return dbContext.QuestionToEpas.Where(e => e.Epaid == epaId).ToList();
            } catch (Exception ex)
            {
                return new List<QuestionToEpa>();
            }
        }

        public Question GetQuestionById(long? id)
        {
            try
            {
                return dbContext.Questions.Where(e => e.QuestionId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Question();
            }
        }

        public List<ExternshipRegistration> GetExternshipsByStudentId(long studentId)
        {
            try
            {
                return dbContext.ExternshipRegistration.Where(e => e.StudentId == studentId).ToList();
            } catch (Exception ex)
            {
                return new List<ExternshipRegistration>();
            }
        }
    }
}
