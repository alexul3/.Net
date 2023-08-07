using FurnitureAssemblyBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels.StorekeeperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.StorekeeperSaveToFile
{
    public abstract class AbstractSaveToWord
    {
		public void CreateDoc(WordStoreKeeperInfo info)
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
			foreach (var furniture in info.FurnitureMaterialsList)
			{
				CreateParagraph(new WordParagraph
				{
					Texts = new List<(string, WordTextProperties)>
					{
						(furniture.FurnitureName, new WordTextProperties { Bold = true, Size = "24", })
					},
					TextProperties = new WordTextProperties
					{
						Size = "24",
						JustificationType = WordJustificationType.Both
					}
				});
				foreach (var materials in furniture.Materials)
				{
					CreateParagraph(new WordParagraph
					{
						Texts = new List<(string, WordTextProperties)>
					{
						(materials.Item1 + " " + materials.Item2.ToString(), new WordTextProperties { Bold = false, Size = "20", })
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
						("Итого " + furniture.TotalCount.ToString(), new WordTextProperties { Bold = false, Size = "24", })
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
		protected abstract void CreateWord(WordStoreKeeperInfo info);
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
		protected abstract void SaveWord(WordStoreKeeperInfo info);
	}
}
