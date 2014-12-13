﻿Imports System.Threading

Public Class Program
    Public Shared Sub Main()
        Randomize()

        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnError2
        AddHandler Application.ThreadException, AddressOf OnError

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)

        Application.Run(New GameForm)
    End Sub

    Private Shared Sub OnError(sender As Object, args As ThreadExceptionEventArgs)
        Debug.WriteLine(args.Exception.ToString())
    End Sub

    Private Shared Sub OnError2(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        Debug.WriteLine(e.ExceptionObject.ToString())
    End Sub
End Class
