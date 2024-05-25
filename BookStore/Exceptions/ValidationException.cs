using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Exceptions
{
    public class ValidationException : Exception
    {

        protected IDictionary _errors = new Dictionary<string, string[]>();
        //TODO: Resosurces
        private readonly static string _GeneralMessage = "Validation summary: ";

        /// <summary>
        /// Throws validation exception with the error message.
        /// </summary>
        /// <param name="message">Validation error message, not binded to any specific property.</param>
        public ValidationException(string message)
            : base(message, null)
        {
            _errors = new Dictionary<string, string[]> { { String.Empty, new string[] { message } } };
        }

        /// <summary>
		/// Throws validation exception with the errors collection as message.
		/// </summary>
		/// <param name="errors">Collection of errors where each element is validatable property with list of validation messages.</param>
		public ValidationException(Dictionary<string, string[]> errors)
            : base($"{_GeneralMessage}  {string.Join("; ", errors.SelectMany(kvp => kvp.Value))}", null)
        {
            _errors = errors;
        }
    }
}
