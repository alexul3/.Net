using FurnitureAssembly_WorkerBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssembly_WorkerBusinessLogic.OfficePackage.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerBusinessLogic.OfficePackage
{
	public abstract class AbstractSaveToPdf
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
			foreach (var order in info.Orders)
			{
				CreateParagraph(new PdfParagraph
				{
					Text = order.SetName,
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Left
				});
				CreateTable(new List<string> { "2cm", "3cm", "3cm", "3cm" });
				CreateRow(new PdfRowParameters
				{
					Texts = new List<string> { "Номер", "Дата заказа", "Мебельный модуль", "Сумма" },
					Style = "NormalTitle",
					ParagraphAlignment = PdfParagraphAlignmentType.Center
				});
				foreach (var furnitureModule in order.FurnitureModules)
				{
					CreateRow(new PdfRowParameters
					{
						Texts = new List<string> { order.Id.ToString(), order.DateCreate.ToShortDateString(), furnitureModule.Name, furnitureModule.Cost.ToString() },
						Style = "Normal",
						ParagraphAlignment = PdfParagraphAlignmentType.Left
					});
				}
				CreateParagraph(new PdfParagraph
				{
					Text = $"Итого: {info.Orders.Sum(x => x.Sum)}\t",
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Rigth
				});
			}
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
