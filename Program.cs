using EntityFramework03.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework03
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new OnlineLearningContext();

            //Migration------------------------------------------------
             context.Database.Migrate();


            //Create-------------------------------------------------- -

           var student = new Student()
           {
               FullName = "Ahmed Mohamed",
               Email = "ahmedMohamed@gmail.com"
           };

            context.Students.Add(student);

            Console.WriteLine(context.Entry(student).State);

            await context.SaveChangesAsync();

            Console.WriteLine(context.Entry(student).State);


            //Read---------------------------------------------------- -

           var studentsList = await context.Students
               .AsNoTracking()
               .ToListAsync();

            foreach (var Student in studentsList)
            {
                Console.WriteLine(
                    $"{Student.StudentId} ---- {Student.FullName} ");
            }


            //Update-------------------------------------------------- -

           var studentUpdate = await context.Students
               .FirstOrDefaultAsync(s => s.StudentId == 1);

            if (studentUpdate != null)
            {
                studentUpdate.FullName = "Omar";

                Console.WriteLine(context.Entry(studentUpdate).State);

                await context.SaveChangesAsync();

                Console.WriteLine(context.Entry(studentUpdate).State);
            }


            //Delete-------------------------------------------------- -

           var studentDelete = await context.Students
               .FirstOrDefaultAsync(s => s.StudentId == 1);

            if (studentDelete != null)
            {
                context.Students.Remove(studentDelete);

                Console.WriteLine(context.Entry(studentDelete).State);

                await context.SaveChangesAsync();

                Console.WriteLine(context.Entry(studentDelete).State);
            }


            //Many To Many--------------------------------------------

            var enrollment = new Enrollment()
            {
                StudentId = 1,
                CourseId = 1,
                EnrollmentDate = DateTime.Now,
                FinalGrade = 90
            };

            context.Enrollments.Add(enrollment);

            await context.SaveChangesAsync();


            //Self Referencing----------------------------------------

           var category = new Category()
           {
               Name = "Programming",
               ParentCategoryId = 1
           };

            context.Categories.Add(category);

            await context.SaveChangesAsync();


            //Bulk Update---------------------------------------------

           await context.Courses
               .Where(c => c.CourseId == 1)
               .ExecuteUpdateAsync(s =>
                   s.SetProperty(c => c.IsPublished, true));


            //Bulk Delete---------------------------------------------

           await context.Enrollments
               .Where(e => e.FinalGrade < 50)
               .ExecuteDeleteAsync();


            //Seeding------------------------------------------------ -

            if (!await context.Students.AnyAsync())
            {
                context.Students.AddRange(
                    new Student()
                    {
                        FullName = "Ahmed Hassan",
                        Email = "ahmedhassan@gmail.com"
                    },
                    new Student()
                    {
                        FullName = "Mona Mohamed",
                        Email = "monaMohamed@gmail.com"
                    }
                );

                await context.SaveChangesAsync();
            }

        }
    }
}
