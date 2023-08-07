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
    public abstract class AbstractSaveToPdf
    {
		public void CreateDoc(PdfStoreKeeperInfo info)
		{
			CreatePdf(info);
			CreateParagraph(new PdfParagraph
			{
				Text = info.Title,
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});
			CreateParagraph(new PdfParagraph
			{
				Text = $"с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
				Style = "Normal",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});
			CreateRow(new PdfRowParameters
			{
				Texts = new List<string> { "Материал", "Стоимость" },
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});
			double sum = 0;
			CreateTable(new List<string> { "6cm", "3cm" });
			foreach (var material in info.Materials)
			{
				//CreateParagraph(new PdfParagraph
				//{
				//	Text = material.MaterialName,
				//	Style = "Normal",
				//	ParagraphAlignment = PdfParagraphAlignmentType.Left
				//});
				
				sum += material.Sum;
				CreateRow(new PdfRowParameters
				{
					Texts = new List<string> { material.MaterialName, material.Sum.ToString() },
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Left
				});
			}
			
			CreateParagraph(new PdfParagraph
			{
				Text = $"Итого: {sum}\t",
				Style = "Normal",
				ParagraphAlignment = PdfParagraphAlignmentType.Left
			});
			SavePdf(info);
		}
		/// <summary>
		/// Создание doc-файла
		/// </summary>
		/// <param name="info"></param>
		protected abstract void CreatePdf(PdfStoreKeeperInfo info);
		/// <summary>
		/// Создание параграфа с текстом
		/// </summary>
		/// <param name="title"></param>
		/// <param name="style"></param>
		protected abstract void CreateParagraph(PdfParagraph paragraph);
		/// <summary>
		/// Создание таблицы
		/// </summary>
		/// <param name="title"></param>
		/// <param name="style"></param>
		protected abstract void CreateTable(List<string> columns);
		/// <summary>
		/// Создание и заполнение строки
		/// </summary>
		/// <param name="rowParameters"></param>
		protected abstract void CreateRow(PdfRowParameters rowParameters);
		/// <summary>
		/// Сохранение файла
		/// </summary>
		/// <param name="info"></param>
		protected abstract void SavePdf(PdfStoreKeeperInfo PdfStoreKeeperInfo);
	}
}

