Public Interface IObservable(Of T)
    ReadOnly Property Value As T

    Event ValueChanged(oldValue As T, newValue As T)
End Interface

Public MustInherit Class Observable(Of T)
    Implements IObservable(Of T)

    Private _value As T

    Public Sub New(initialValue As T)
        _value = initialValue
    End Sub

    Public ReadOnly Property Value As T Implements IObservable(Of T).Value
        Get
            Return _value
        End Get
    End Property

    Protected Shared Function AreEqual(oldValue As T, newValue As T) As Boolean
        If oldValue Is Nothing Then
            Return newValue Is Nothing
        Else
            Return oldValue.Equals(newValue)
        End If
    End Function

    Protected Sub SetValue(value As T)
        If Not AreEqual(_value, value) Then
            Dim oldValue = _value
            _value = value
            RaiseEvent ValueChanged(oldValue, value)
        End If
    End Sub

    Public Event ValueChanged(oldValue As T, newValue As T) Implements IObservable(Of T).ValueChanged
End Class

Public Class ObservableVariable(Of T)
    Inherits Observable(Of T)

    Public Sub New(initialValue As T)
        MyBase.New(initialValue)
    End Sub

    Public Shadows Sub SetValue(value As T)
        MyBase.SetValue(value)
    End Sub
End Class

Public Class DerivedObservable(Of T, U)
    Inherits Observable(Of U)

    Private ReadOnly _obs As IObservable(Of T)

    Private ReadOnly _function As Func(Of T, U)

    Public Sub New(obs As IObservable(Of T), func As Func(Of T, U))
        MyBase.New(func(obs.Value))

        _obs = obs
        _function = func

        AddHandler _obs.ValueChanged, AddressOf OnChange
    End Sub

    Private Sub OnChange(oldValue As T, newValue As T)
        SetValue(_function(_obs.Value))
    End Sub
End Class