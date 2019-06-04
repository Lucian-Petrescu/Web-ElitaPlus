' NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
Namespace Test


    Public Class Service1
        Implements Test.IService1

        Public Function DoWork() As String Implements Test.IService1.DoWork
            Return "success"
        End Function

    End Class

End Namespace