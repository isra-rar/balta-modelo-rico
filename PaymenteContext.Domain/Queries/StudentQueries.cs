using System;
using System.Linq.Expressions;
using PaymenteContext.Domain.Entities;

namespace PaymenteContext.Domain.Queries
{
    public static class StudentQueries
    {
        public static Expression<Func<Student, bool>> GetStudentInfo(string document)
        {
            return x => x.Document.Number == document;
        }

    }
}