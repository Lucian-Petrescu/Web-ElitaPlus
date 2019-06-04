Public Interface IFalabellaServiceManager
    Function ServicioTecnicoDatosCrearOp(request As GetWorkOrderNumberRequest) As GetWorkOrderNumberResponse

    Function OrdenTrabajoEstadoModificarOp(request As UpdateClaimInfoRequest) As UpdateClaimInfoResponse

End Interface
