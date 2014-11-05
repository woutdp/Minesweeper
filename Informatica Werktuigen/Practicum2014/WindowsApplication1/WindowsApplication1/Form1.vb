Option Explicit On
Option Strict On

Public Class Form1

    Private Declare Function QueryPerformanceCounter Lib "kernel32" (ByRef lpPerformanceCount As Long) As Integer
    Private Declare Function QueryPerformanceFrequency Lib "kernel32" (ByRef lpFrequency As Long) As Integer

    Private Milliseconds As Single
    Private Get_Frames_Per_Second As Integer
    Private Frame_Count As Integer

    Private Running As Boolean

    Private Ticks_Per_Second As Long
    Private Start_Time As Long

    Private Sub Hi_Res_Timer_Initialize()
        QueryPerformanceFrequency(Ticks_Per_Second)
    End Sub

    Private Function Get_Elapsed_Time() As Single
        Dim Current_Time As Long

        QueryPerformanceCounter(Current_Time)
        Return CSng(Current_Time / Ticks_Per_Second)
    End Function

    Private Sub Lock_Framerate(ByVal Target_FPS As Long)

        Static Last_Time As Long
        Dim Current_Time As Long
        Dim FPS As Single

        Do
            QueryPerformanceCounter(Current_Time)
            FPS = CSng(Ticks_Per_Second / (Current_Time - Last_Time))
        Loop While (FPS > Target_FPS)

        QueryPerformanceCounter(Last_Time)

    End Sub

    Private Function Get_FPS() As String
        Frame_Count = Frame_Count + 1

        If Get_Elapsed_Time() - Milliseconds >= 1 Then
            Get_Frames_Per_Second = Frame_Count
            Frame_Count = 0
            Milliseconds = Convert.ToInt32(Get_Elapsed_Time)
        End If

        Return "Frames Per Second: " & Convert.ToString(Get_Frames_Per_Second)
    End Function

    Private Sub Game_Loop()
        Do While Running = True
            'Game Code Here

            Lock_Framerate(60)
            Me.Text = Get_FPS()
            Application.DoEvents()
        Loop
    End Sub

    Private Sub Main()
        Me.Show()
        Hi_Res_Timer_Initialize()
        Milliseconds = Get_Elapsed_Time()
        Running = True
        Game_Loop()
    End Sub

    Private Sub Shutdown()
        Running = False
        Application.Exit()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.FormClosing
        Shutdown()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Main()
    End Sub
End Class