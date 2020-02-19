using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DDCore.Domain
{
    /// <summary>
    /// A Result from an Operation.  
    /// Check <seealso cref="IsSuccess"/> and <seealso cref="IsFailure"/> for Status
    /// See <seealso cref="Value"/> for any value returned from a Successful Operation
    /// </summary>
    public class Result : ValueObject
    {
        /// <summary>
        /// The Value from Successful Result
        /// </summary>
        public dynamic Value { get; }

        /// <summary>
        /// Returns <seealso cref="Value"/> as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to return as - returns default(T) if different type</typeparam>
        /// <returns><see cref="Value"/> as <typeparamref name="T"/></returns>
        public T ValueAs<T>()
        {
            if (Value is T)
                return (T)Value;
            return default(T);
        }

        const string ErrorMessagesSeparator = ", ";

        /// <summary>
        /// Is this Result a Failure
        /// </summary>
        public bool IsFailure { get; }

        /// <summary>
        /// Is this Result a Success
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Error Message from an Error
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Validation Errors if Provided
        /// </summary>
        public IEnumerable<ValidationResult> ValidationResults { get; }

        /// <summary>
        /// Constructor for Result
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        /// <param name="value"></param>
        /// <param name="validationResults"></param>
        protected Result(bool success, string error, dynamic value = null, IEnumerable<ValidationResult> validationResults = null)
        {
            IsFailure = !success;
            IsSuccess = success;
            Error = error;
            Value = value;
            ValidationResults = validationResults ?? new ValidationResult[0];
        }

        /// <summary>
        /// Returns a Success Result with no <seealso cref="Value"/>
        /// </summary>
        /// <returns></returns>
        public static Result Success()
        {
            return new Result(true, String.Empty);
        }

        /// <summary>
        /// Returns a Success Result with a <seealso cref="Value"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(true, String.Empty, value);
        }

        /// <summary>
        /// Validates <paramref name="toValidate"/> and returns a <seealso cref="Result"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate">Object to Validate</param>
        /// <returns></returns>
        public static Result<T> Validate<T>(T toValidate)
        {
            if (toValidate == null)
                throw new ArgumentNullException(nameof(toValidate), "Nothing passed to Validate<T> method.");

            ValidationContext vc = new ValidationContext(toValidate);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var isSuccess = Validator.TryValidateObject(toValidate, vc, results, true);
            if (isSuccess)
                return new Result<T>(true, String.Empty, toValidate);
            return new Result<T>(false, String.Empty, results);
        }

        /// <summary>
        /// Validates <paramref name="toValidate"/> providing <paramref name="serviceProvider"/> to the Validation Methods
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate">Object to Validate</param>
        /// <param name="serviceProvider">Service Provider to use for resolving validation services</param>
        /// <returns></returns>
        public static Result<T> Validate<T>(T toValidate, IServiceProvider serviceProvider)
        {
            if (toValidate == null)
                throw new ArgumentNullException(nameof(toValidate), "Nothing passed to Validate<T> method.");

            ValidationContext vc = new ValidationContext(toValidate, serviceProvider, null);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var isSuccess = Validator.TryValidateObject(toValidate, vc, results, true);
            return new Result<T>(isSuccess, String.Empty, results);
        }

        /// <summary>
        /// Returns a failure Result
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Result Failure(string error)
        {
            return new Result(false, error);
        }

        /// <summary>
        /// Returns a Failure Result typed as <typeparamref name="T"/> Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Result<T> Failure<T>(string error)
        {
            return new Result<T>(false, error, default(T));
        }

        /// <summary>
        /// Combines any number of Results
        /// </summary>
        /// <param name="results"></param>
        /// <param name="errorMessagesSeparator">Separator for Error Messages</param>
        /// <returns></returns>
        public static Result Combine(IEnumerable<Result> results, string errorMessagesSeparator = null)
        {
            List<Result> failedResults = results.Cast<Result>().Where(x => x.IsFailure).ToList();

            if (failedResults.Count == 0)
                return Success();

            string errorMessage = string.Join(errorMessagesSeparator ?? ErrorMessagesSeparator, failedResults.Select(x => x.Error));
            return new Result(false,errorMessage, validationResults: failedResults.SelectMany(r => r.ValidationResults));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return IsSuccess;
            yield return IsSuccess ? Value : Error;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The Type of <seealso cref="Value"/></typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// The Value of a Successful Result
        /// </summary>
        public new T Value { get => ValueAs<T>(); }

        internal Result(bool success, string error, T value)
            :base(success, error, value)
        {
        }
        internal Result(bool success, string error, IEnumerable<ValidationResult> results)
            : base(success, error, validationResults:results)
        {
        }
    }
}
