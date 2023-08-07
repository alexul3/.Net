using FurnitureAssemblyBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage
{
	public abstract class AbstractWorkerSaveToPdf
	{
		public void CreateDoc(PdfInfo info)
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
			foreach (var order in info.Orders.Item1)
			{
				CreateParagraph(new PdfParagraph
				{
					Text = order.SetName,
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Left
				});
				CreateTable(new List<string> { "2cm", "3cm", "7cm", "3cm" });
				CreateRow(new PdfRowParameters
				{
					Texts = new List<string> { "Номер", "Дата заказа", "Мебельный модуль", "Сумма" },
					Style = "NormalTitle",
					ParagraphAlignment = PdfParagraphAlignmentType.Center
				});
				int i = 1;
				foreach (var furnitureModule in order.FurnitureModules)
				{
					CreateRow(new PdfRowParameters
					{
						Texts = new List<string> { i.ToString(), order.DateCreate.ToShortDateString(), furnitureModule.Name, furnitureModule.Cost.ToString() },
						Style = "Normal",
						ParagraphAlignment = PdfParagraphAlignmentType.Left
					});
					i++;
				}
				CreateParagraph(new PdfParagraph
				{
					Text = $"Итого: {order.Sum}\t",
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Rigth
				});
			}
			CreateParagraph(new PdfParagraph
			{
				Text = $"Итого: {info.Orders.Item2}\t",
				Style = "Normal",
				ParagraphAlignment = PdfParagraphAlignmentType.Left
			});
			SavePdf(info);
		}
		/// <summary>
		/// Создание doc-файла
		/// </summary>
		/// <param name="info"></param>
		protected abstract void CreatePdf(PdfInfo info);
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
		protected abstract void SavePdf(PdfInfo info);
	}
}
