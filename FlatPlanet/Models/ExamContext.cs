using Microsoft.EntityFrameworkCore;

namespace FlatPlanet.Models
{
    public class ExamContext : DbContext
    {
        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {
            
        }

        public DbSet<Exam> Exams { get; set; }
    }
}