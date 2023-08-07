using FurnitureAssembly_WorkerBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssembly_WorkerBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerBusinessLogic.OfficePackage
{
	public abstract class AbstractSaveToWord
	{
		public void CreateDoc(WordInfo info)
		{
			CreateWord(info);
			CreateParagraph(new WordParagraph
			{
				Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
				TextProperties = new WordTextProperties
				{
					Size = "24",
					JustificationType = WordJustificationType.Center
				}
			});
			foreach (var set in info.Sets)
			{
				CreateParagraph(new WordParagraph
				{
					Texts = new List<(string, WordTextProperties)>
					{
						(set.Name, new WordTextProperties { Bold = true, Size = "24", }),
						(", price: " + set.Cost.ToString(), new WordTextProperties { Size = "24", })
					},
					TextProperties = new WordTextProperties
					{
						Size = "24",
						JustificationType = WordJustificationType.Both
					}
				});
			}
			SaveWord(info);
		}
		/// <summary>
		/// Создание doc-файла
		/// </summary>
		/// <param name="info"></param>
		protected abstract void CreateWord(WordInfo info);
		/// <summary>
		/// Создание абзаца с текстом
		/// </summary>
		/// <param name="paragraph"></param>
		/// <returns></returns>
		protected abstract void CreateParagraph(WordParagraph paragraph);
		/// <summary>
		/// Сохранение файла
		/// </summary>
		/// <param name="info"></param>
		protected abstract void SaveWord(WordInfo info);
	}
}
