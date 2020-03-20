using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CaseGenesis.Utils
{
	public static class ModelStateDictionaryExtentions
	{
		public static void AppendStatefulValidation(this ModelStateDictionary modelState, ValidationResult[] validationResults)
		{
			if (validationResults is null || !validationResults.Any()) return;

			foreach(var validationResult in validationResults)
			{
				if (validationResult != null)
					modelState.AddModelError("Errors", validationResult.ErrorMessage);
			}
		}
	}
}
