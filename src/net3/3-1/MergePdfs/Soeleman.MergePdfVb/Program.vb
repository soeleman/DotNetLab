Imports System.IO
Imports System.Text

Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO

Module Program

    Public Sub Main()
        Dim output = Path.Combine("D:\", $"MergeFile{DateTime.Now.ToString("yyyyMMddhhmmss")}.pdf")
        Dim files = {"D:\file1.pdf", "D:\file2.pdf"}
        MergePdf(output, files)
    End Sub

    Public Sub MergePdf( _
        ByVal targetFile As String, _
        ParamArray pdfFiles As String())

        If String.IsNullOrEmpty(targetFile) Then
            Throw New ArgumentNullException(NameOf(targetFile))
        End If

        For Each pdf In pdfFiles

            If Not File.Exists(pdf) Then
                Throw New FileNotFoundException($"File: {pdf} Not Found!")
            End If
        Next

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)

        Using targetDoc = New PdfDocument()

            For Each pdf In pdfFiles

                Using pdfDoc = PdfReader.Open(pdf, PdfDocumentOpenMode.Import)

                    For i = 0 To pdfDoc.PageCount - 1
                        targetDoc.AddPage(pdfDoc.Pages(i))
                    Next

                End Using
            Next

            targetDoc.Save(targetFile)
        End Using
    End Sub

End Module



