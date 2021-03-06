﻿Imports System.IO
Imports System.Text

Public Class Session

    Dim allocated As List(Of StringBuilder) = New List(Of StringBuilder)
    Private Shared filePath As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "aemJzj4tVP1zc0TE-pKCtOr7QwYAQAJt4EtAtest.txt")

    'cria a variavel de sessao e guarda no computador na pasta myDocuments
    Shared Sub SetSession_ID(ByRef id As String)
        File.Create(System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "aemJzj4tVP1zc0TE-pKCtOr7QwYAQAJt4EtAtest.txt")).Dispose()
        Using writer As StreamWriter = New StreamWriter(filePath)
            writer.Write(id)
            writer.Close()
        End Using
    End Sub

    'obtem a variavel da sessao
    Shared Function GetSession_ID() As String
        Dim id As String
        Using reader As StreamReader = New StreamReader(filePath)
            id = reader.ReadLine
        End Using
        Return id
    End Function

    'destroi a variavel da sessao apagando o da pasta de myDocuments
    Shared Sub DestroySession()
        If System.IO.File.Exists(filePath) = True Then
            System.IO.File.Delete(filePath)
            MsgBox("You have Logged Out Sucessfully.")
        End If
    End Sub

    'termina a sessao
    Shared Sub LogoutApp(ByRef exe As Object)
        Session.DestroySession()
        Login.Close()
        exe.Hide()
        Login.Show()
        exe.Close()
    End Sub

    'fecha a app toda e destroi a sessao
    Shared Sub CloseApp(ByRef exe As Object)
        Session.DestroySession()
        exe.Close()
        Login.Close()
    End Sub

    'verifica se existe sessao ou nao
    Shared Function CheckSession_ID() As Boolean
        If (GetSession_ID() = "") Then
            Return False
        Else
            Return True
        End If
    End Function

    'cria uma string a ser usada como session string
    Public Function generateSessionString() As String
        Dim s As String = "abcdefghijklmnopqrstuvwxz=_/|\-ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As Random = New Random
        Dim sb As StringBuilder = New StringBuilder
        Do
            For i As Integer = 1 To 20
                Dim idx As Integer = r.Next(0, 35) '26 letters + 10 digits
                sb.Append(s.Substring(idx, 1))
            Next
        Loop Until Not allocated.Contains(sb)
        allocated.Add(sb)
        Return sb.ToString()
    End Function

End Class
