using System;
using System.Collections.Generic;
using System.Linq;

namespace StarterKit.Commands
{
    /// <summary>
    /// ErrorList represents a list of errors that is returned by Command Vaolidators.
    /// This is better than just a list of strings because here we can assign what field in command is causing trouble.
    /// It is particularly handy when working with MVC applications and this plugs in nicely into MVC pipeline of ModelState
    /// 
    /// Allso allows us to override ToString() so that we can debug error messages easily in tests 
    /// It provide a ToString implemenation for Error message and it will be included in ToString
    /// when called on ErrorList
    /// </summary>
    public class ErrorList : List<ErrorMessage>
    {
        public override string ToString()
        {
            return this.Aggregate("", (current, next) => current + ", " + next);
        }


        public void AddError(String message)
        {
            this.Add(message);
        }


        public void Add(String message)
        {
            this.Add(new ErrorMessage(message));
        }


        public void Add(string fieldName, string message)
        {
            this.Add(new ErrorMessage(fieldName, message));
        }


        public void Add(String fieldName, String message, params object[] args)
        {
            this.Add(new ErrorMessage(fieldName, message, args));
        }


        public bool IsValid()
        {
            return !this.Any();
        }


        public bool IsSuccess()
        {
            return IsValid();
        }


        public String ToCsv()
        {
            return String.Join(", ", this.Select(e => e.ToString()));
        }


        /// <summary>
        /// Merges two lists of Errors into one
        /// </summary>
        /// <param name="otherErrors"></param>
        /// <returns></returns>
        public ErrorList Merge(ErrorList otherErrors)
        {
            this.AddRange(otherErrors);

            return this;
        }
    }
}
