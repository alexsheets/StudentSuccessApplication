using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EPA.Models;

public partial class EpaContext : DbContext
{
    // to download new models/changes to DB
    // Scaffold-DbContext "Data Source=vmed-dbtest;Initial Catalog=EPA;Integrated Security=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force


    private string connString;

    public EpaContext() :base()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", optional: false);

        var _configuration = builder.Build();

        connString = _configuration.GetConnectionString("EPADb");
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Block> Blocks { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Competency> Competencies { get; set; }

    public virtual DbSet<Concentration> Concentrations { get; set; }

    public virtual DbSet<EmailSetting> EmailSettings { get; set; }

    public virtual DbSet<Epa> Epas { get; set; }

    public virtual DbSet<EpatoCompetency> EpatoCompetencies { get; set; }

    public virtual DbSet<Evaluator> Evaluators { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionToEpa> QuestionToEpas { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<ResultComment> ResultComments { get; set; }

    public virtual DbSet<ResultItem> ResultItems { get; set; }

    public virtual DbSet<Rotation> Rotations { get; set; }

    public virtual DbSet<Rubric> Rubrics { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Reflection> Reflection { get; set; }

    public virtual DbSet<GradingReport> GradingReport { get; set; }

    public virtual DbSet<StudentSpecificGradingReport> StudentSpecificGradingReport { get; set; }

    public virtual DbSet<ExternshipRegistration> ExternshipRegistration { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connString, builder =>
        {
            builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AdmintId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AdmintID");
            entity.Property(e => e.PawsId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PawsID");
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Block>(entity =>
        {
            entity.Property(e => e.BlockId).HasColumnName("BlockID");
            entity.Property(e => e.RotationId).HasColumnName("RotationID");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Reflection>(entity =>
        {
            entity.Property(e => e.ReflectionId).HasColumnName("ReflectionId");
            entity.Property(e => e.PawsId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReflectionQuestionId).HasColumnName("ReflectionQuestionId");
            entity.Property(e => e.ReflectionAnswer)
                .IsUnicode(false);
            entity.Property(e => e.Season)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Year)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DateSubmitted)
                .HasColumnType("datetime")
                .HasColumnName("DateSubmitted");
        });

        modelBuilder.Entity<GradingReport>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Semester)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Year)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PawsId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CountOfSelfEvals).HasColumnName("CountOfSelfEvals");
            entity.Property(e => e.LastSelfEvalDate)
                .HasColumnType("datetime")
                .HasColumnName("LastSelfEvalDate");
            entity.Property(e => e.ReflectionSubmissionDate)
                .HasColumnType("datetime")
                .HasColumnName("ReflectionSubmissionDate");

        });

        modelBuilder.Entity<StudentSpecificGradingReport>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Semester)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Year)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsUnicode(false);
            entity.Property(e => e.PawsId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Class)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DateOfLastEval)
                .HasColumnType("datetime")
                .HasColumnName("DateOfLastEval");
            entity.Property(e => e.Reflection1)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Reflection2)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Reflection3)
                .HasMaxLength(1000)
                .IsUnicode(false);
            // epa 1
            entity.Property(e => e.Epa1Strengths)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa1Improve)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa1Plan)
                .HasMaxLength(1000)
                .IsUnicode(false);
            // epa 2
            entity.Property(e => e.Epa2Strengths)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa2Improve)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa2Plan)
                .HasMaxLength(1000)
                .IsUnicode(false);
            // epa 3
            entity.Property(e => e.Epa3Strengths)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa3Improve)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa3Plan)
                .HasMaxLength(1000)
                .IsUnicode(false);
            // epa 4
            entity.Property(e => e.Epa4Strengths)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa4Improve)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa4Plan)
                .HasMaxLength(1000)
                .IsUnicode(false);
            // epa 5
            entity.Property(e => e.Epa5Strengths)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa5Improve)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Epa5Plan)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ExternshipRegistration>(entity =>
        {
            entity.Property(e => e.ExternshipId).HasColumnName("ExternshipId");
            entity.Property(e => e.StudentId).HasColumnName("StudentId");
            entity.Property(e => e.Visibility).HasColumnName("Visibility");
            entity.Property(e => e.PracticeType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("StartDate");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("EndDate");
            entity.Property(e => e.NumWeeks)
                .IsUnicode(false);
            entity.Property(e => e.Blocks)
                .IsUnicode(false);
            entity.Property(e => e.NameOfPractice)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.MailingAddress)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TelephoneNum)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EvaluatorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EvaluatorTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EvaluatorEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Competency>(entity =>
        {
            entity.Property(e => e.CompetencyId).HasColumnName("CompetencyID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Identifier)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Concentration>(entity =>
        {
            entity.HasKey(e => e.ConcentrationId).HasName("PK_Consentrations");

            entity.Property(e => e.ConcentrationId).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<EmailSetting>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.EmailSettingsId).HasColumnName("EmailSettingsID");
            entity.Property(e => e.FromEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Smtpclient)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("SMTPClient");
        });

        modelBuilder.Entity<Epa>(entity =>
        {
            entity.HasKey(e => e.Epaid).HasName("PK_Sections");

            entity.ToTable("EPAS");

            entity.Property(e => e.Epaid).HasColumnName("EPAID");
            entity.Property(e => e.SectionName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SectionTag)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<EpatoCompetency>(entity =>
        {
            entity.HasKey(e => e.EpacompId);

            entity.ToTable("EPAToCompetency");

            entity.Property(e => e.EpacompId).HasColumnName("EPACompID");
            entity.Property(e => e.CompetencyId).HasColumnName("CompetencyID");
            entity.Property(e => e.Epaid).HasColumnName("EPAID");
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Evaluator>(entity =>
        {
            entity.HasKey(e => e.EvaluatorId).HasName("PK_Evalutators");

            entity.ToTable("Evaluator");

            entity.Property(e => e.EvaluatorId).HasColumnName("EvaluatorID");
            entity.Property(e => e.Clinic)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lsuind).HasColumnName("LSUInd");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<QuestionToEpa>(entity =>
        {
            entity.HasKey(e => e.QuestionToEpasId);

            entity.ToTable("QuestionToEPAs");

            entity.Property(e => e.QuestionToEpasId)
                .ValueGeneratedNever()
                .HasColumnName("QuestionToEPAsID");
            entity.Property(e => e.Epaid).HasColumnName("EPAID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultsId);

            entity.Property(e => e.ResultsId).HasColumnName("ResultsID");
            entity.Property(e => e.BlockId).HasColumnName("BlockID");
            entity.Property(e => e.DateOfEval).HasColumnType("datetime");
            entity.Property(e => e.Epaid).HasColumnName("EPAID");
            entity.Property(e => e.EvaluatorId).HasColumnName("EvaluatorID");
            entity.Property(e => e.RotationId).HasColumnName("RotationID");
            entity.Property(e => e.Semester)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
            entity.Property(e => e.Visibility).HasDefaultValue(false);
            entity.Property(e => e.RequestToEvaluator)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ResultComment>(entity =>
        {
            entity.HasKey(e => e.ResultsCommentId);

            entity.Property(e => e.ResultsCommentId).HasColumnName("ResultsCommentID");
            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.Comment1).IsUnicode(false);
            entity.Property(e => e.Comment2).IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<ResultItem>(entity =>
        {
            entity.HasKey(e => e.ResultsItemId);

            entity.Property(e => e.ResultsItemId).HasColumnName("ResultsItemID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.SelfEvalReflection1)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SelfEvalReflection2)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SelfEvalReflection3)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Rotation>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Abbreviation)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RotationId).HasColumnName("RotationID");
            entity.Property(e => e.Supervisors)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Rubric>(entity =>
        {
            entity.HasKey(e => e.RubricId).HasName("PK_Options");

            entity.ToTable("Rubric");

            entity.Property(e => e.RubricId).HasColumnName("RubricID");
            entity.Property(e => e.Description)
                .HasMaxLength(350)
                .IsUnicode(false);
            entity.Property(e => e.Text)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ConcentrationId).HasColumnName("ConcentrationID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PawsId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PawsID");
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDT");
            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
