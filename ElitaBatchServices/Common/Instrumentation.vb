Public Module Instrumentation
    Public Sub WriteLog(log As String)
        Dim sw As System.IO.StreamWriter

        Try
            Dim logPath As String = "E:\Applications\ElitaBatchServices\log"
            Dim fileName As String = String.Format("{1}\{0}.txt", "BATCHSERVICE_" & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day, logPath)

            Dim st As New System.Diagnostics.StackTrace()

            Dim logString As String = String.Format("{0},{1},{2},{3},{4},{5}",
                                                    DateTime.Now.ToShortDateString(),
                                                    DateTime.Now.ToShortTimeString(),
                                                    st.GetFrame(1).GetFileLineNumber().ToString(),
                                                    st.GetFrame(1).GetMethod().DeclaringType.FullName,
                                                    st.GetFrame(1).GetMethod().Name,
                                                    log.PadLeft(log.Length + st.FrameCount * 2, " "))

            If Not System.IO.File.Exists(fileName) Then
                sw = System.IO.File.CreateText(fileName)
                sw.WriteLine(logString)
            Else
                sw = System.IO.File.AppendText(fileName)
                sw.WriteLine(logString)
            End If
        Catch ex As Exception
            Throw
        Finally
            sw.Flush()
            sw.Close()
        End Try
    End Sub

End Module
