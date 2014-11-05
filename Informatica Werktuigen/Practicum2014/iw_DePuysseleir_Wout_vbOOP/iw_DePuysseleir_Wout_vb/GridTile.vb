Option Explicit On
Option Strict On

Public Class GridTile
    Inherits Label
    Private m_widthPos As Integer
    Private m_heightPos As Integer
    Private m_x As Integer
    Private m_y As Integer
    Public Sub New(i As Integer, j As Integer)
        m_widthPos = i
        m_heightPos = j
    End Sub
    Public Property widthPos() As Integer
        Get
            Return m_widthPos
        End Get
        Private Set(ByVal value As Integer)
            m_widthPos = value
        End Set
    End Property
    Public Property heightPos() As Integer
        Get
            Return m_heightPos
        End Get
        Private Set(ByVal value As Integer)
            m_heightPos = value
        End Set
    End Property
End Class
