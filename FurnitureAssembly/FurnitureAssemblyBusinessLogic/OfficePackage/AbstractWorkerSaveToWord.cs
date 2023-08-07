using FurnitureAssemblyBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage
{
	public abstract class AbstractWorkerSaveToWord
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
			foreach (var set in info.SetsFurnitureModules)
			{
				CreateParagraph(new WordParagraph
				{
					Texts = new List<(string, WordTextProperties)>
					{
						(set.SetName, new WordTextProperties { Bold = true, Size = "24", })
					},
					TextProperties = new WordTextProperties
					{
						Size = "24",
						JustificationType = WordJustificationType.Both
					}
				});
                foreach (var furnitureModule in set.FurnitureModules)
                {
					CreateParagraph(new WordParagraph
					{
						Texts = new List<(string, WordTextProperties)>
					{
						(furnitureModule.Item1 + " " + furnitureModule.Count.ToString(), new WordTextProperties { Bold = false, Size = "20", })
					},
						TextProperties = new WordTextProperties
						{
							Size = "20",
							JustificationType = WordJustificationType.Both
						}
					});
				}
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)>
                    {
                        ("Итого " + set.TotalCount.ToString(), new WordTextProperties { Bold = false, Size = "24", })
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
