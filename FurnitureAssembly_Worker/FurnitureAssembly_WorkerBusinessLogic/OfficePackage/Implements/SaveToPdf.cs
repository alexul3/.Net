﻿using FurnitureAssembly_WorkerBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssembly_WorkerBusinessLogic.OfficePackage.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssembly_WorkerBusinessLogic.OfficePackage.Implements
{
	public class SaveToPdf : AbstractSaveToPdf
	{
		private Document? _document;
		private Section? _section;
		private Table? _table;
		private static ParagraphAlignment
		GetParagraphAlignment(PdfParagraphAlignmentType type)
		{
			return type switch
			{
				PdfParagraphAlignmentType.Center => ParagraphAlignment.Center,
				PdfParagraphAlignmentType.Left => ParagraphAlignment.Left,
				PdfParagraphAlignmentType.Rigth => ParagraphAlignment.Right,
				_ => ParagraphAlignment.Justify,
			};
		}
		/// <summary>
		/// Создание стилей для документа
		/// </summary>
		/// <param name="document"></param>
		private static void DefineStyles(Document document)
		{
			var style = document.Styles["Normal"];
			style.Font.Name = "Times New Roman";
			style.Font.Size = 14;
			style = document.Styles.AddStyle("NormalTitle", "Normal");
			style.Font.Bold = true;
		}
		protected override void CreatePdf(PdfInfo info)
		{
			_document = new Document();
			DefineStyles(_document);
			_section = _document.AddSection();
		}
		protected override void CreateParagraph(PdfParagraph pdfParagraph)
		{
			if (_section == null)
			{
				return;
			}
			var paragraph = _section.AddParagraph(pdfParagraph.Text);
			paragraph.Format.SpaceAfter = "1cm";
			paragraph.Format.Alignment =
			GetParagraphAlignment(pdfParagraph.ParagraphAlignment);
			paragraph.Style = pdfParagraph.Style;
		}
		protected override void CreateTable(List<string> columns)
		{
			if (_document == null)
			{
				return;
			}
			_table = _document.LastSection.AddTable();
			foreach (var elem in columns)
			{
				_table.AddColumn(elem);
			}
		}
		protected override void CreateRow(PdfRowParameters rowParameters)
		{
			if (_table == null)
			{
				return;
			}
			var row = _table.AddRow();
			for (int i = 0; i < rowParameters.Texts.Count; ++i)
			{
				row.Cells[i].AddParagraph(rowParameters.Texts[i]);
				if (!string.IsNullOrEmpty(rowParameters.Style))
				{
					row.Cells[i].Style = rowParameters.Style;
				}
				Unit borderWidth = 0.5;
				row.Cells[i].Borders.Left.Width = borderWidth;
				row.Cells[i].Borders.Right.Width = borderWidth;
				row.Cells[i].Borders.Top.Width = borderWidth;
				row.Cells[i].Borders.Bottom.Width = borderWidth;
				row.Cells[i].Format.Alignment =
				GetParagraphAlignment(rowParameters.ParagraphAlignment);
				row.Cells[i].VerticalAlignment = VerticalAlignment.Center;
			}
		}
		protected override void SavePdf(PdfInfo info)
		{
			var renderer = new PdfDocumentRenderer(true)
			{
				Document = _document
			};
			renderer.RenderDocument();
			renderer.PdfDocument.Save(info.FileName);
		}
	}
}
