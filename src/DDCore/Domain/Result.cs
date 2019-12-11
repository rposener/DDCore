using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDCore.Domain
{
    public class Result
    {

        const string ErrorMessagesSeparator = ", ";

        public bool IsFailure { get; }

        public bool IsSuccess { get; }

        public string Error { get; }

        protected Result(bool success, string error)
        {
            IsFailure = !success;
            IsSuccess = success;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, String.Empty);
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }

        public static Result Combine(IEnumerable<Result> results, string errorMessagesSeparator = null)
        {
            List<Result> failedResults = results.Where(x => x.IsFailure).ToList();

            if (failedResults.Count == 0)
                return Success();

            string errorMessage = string.Join(errorMessagesSeparator ?? ErrorMessagesSeparator, failedResults.Select(x => x.Error));
            return Failure(errorMessage);
        }

        public static Result Combine<T>(IEnumerable<Result<T>> results, string errorMessagesSeparator = null)
        {
            var untyped = results.Select(result => (Result)result);
            return Combine(untyped, errorMessagesSeparator);
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(bool success, string error, T value)
            :base(success, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, String.Empty, value);
        }

        public new static Result<T> Success()
        {
            return new Result<T>(true, String.Empty, default(T));
        }

        public static new Result<T> Failure(string error)
        {
            return new Result<T>(false, error, default(T));
        }
    }
}
