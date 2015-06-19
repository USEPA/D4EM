Imports System
Imports System.Collections.Generic
Imports System.Text

Public Class ListItem

    Public DisplayValue As String
    Public DataValue As String

    Public Sub New(display As String, value As String)

        DisplayValue = display
        DataValue = value

    End Sub



    Public Overrides Function ToString() As String

        Return DisplayValue

    End Function

End Class


