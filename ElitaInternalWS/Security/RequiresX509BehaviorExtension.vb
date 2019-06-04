Imports System.ServiceModel.Configuration

Namespace Security
    Public Class RequiresX509BehaviorExtension
        Inherits BehaviorExtensionElement

        Public Overrides ReadOnly Property BehaviorType As Type
            Get
                Return GetType(RequiresX509Attribute)
            End Get
        End Property

        Protected Overrides Function CreateBehavior() As Object
            Return New RequiresX509Attribute()
        End Function
    End Class

End Namespace