using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Soeleman
{
    public static class Program
    {
        public static void Main()
        {
            var output = Path.Combine(@"D:\", $"MergeFile{DateTime.Now:yyyyMMddhhmmss}.pdf");

            var files = new[]
            {
                @"D:\file1.pdf",
                @"D:\file2.pdf"
            };

            MergePdf(output, files);
        }

        public static void MergePdf(
            string targetFile,
            params string[] pdfFiles)
        {
            if (string.IsNullOrEmpty(targetFile))
            {
                throw new ArgumentNullException(nameof(targetFile));
            }

            foreach (var pdf in pdfFiles)
            {
                if (!File.Exists(pdf))
                {
                    throw new FileNotFoundException($"File: {pdf} Not Found!");
                }
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var targetDoc = new PdfDocument();

            foreach (var pdf in pdfFiles)
            {
                using var pdfDoc = PdfReader.Open(pdf, PdfDocumentOpenMode.Import);

                for (var i = 0; i < pdfDoc.PageCount; i++)
                {
                    targetDoc.AddPage(pdfDoc.Pages[i]);
                }
            }

            targetDoc.Save(targetFile);
        }

        public static byte[] MergePdf(
            List<byte[]> pdfs)
        {
            var lstDocuments = pdfs
                .Select(s =>
                    PdfReader.Open(
                        new MemoryStream(s), 
                        PdfDocumentOpenMode.Import))
                .ToList();

            using var outPdf = new PdfDocument();

            for (var i = 1; i <= lstDocuments.Count; i++)
            {
                foreach (var page in lstDocuments[i - 1].Pages)
                {
                    outPdf.AddPage(page);
                }
            }

            var stream = new MemoryStream();
            outPdf.Save(stream, false);

            return stream.ToArray();
        }
    }
}
