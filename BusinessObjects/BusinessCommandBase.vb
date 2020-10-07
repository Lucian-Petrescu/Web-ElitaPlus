

Public Interface IBusinessCommand
    ReadOnly Property CanExecute As Boolean
    Sub Execute()
End Interface

Public MustInherit Class BusinessCommandBase(Of TType)
    Implements IBusinessCommand

    Private _businessObject As TType

    Protected Property BusinessObject As TType
        Get
            Return _businessObject
        End Get
        Set(ByVal value As TType)
            _businessObject = value
        End Set
    End Property

    Protected Friend Sub New(ByVal businessObject As TType)
        Me.BusinessObject = businessObject
    End Sub

    Public ReadOnly Property CanExecute As Boolean Implements IBusinessCommand.CanExecute
        Get
            Return True
        End Get
    End Property

    Public Overridable Sub Execute() Implements IBusinessCommand.Execute
        If (Not CanExecute) Then
            Throw New InvalidOperationException
        End If
    End Sub
End Class