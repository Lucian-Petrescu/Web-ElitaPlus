using Assurant.ElitaPlus.BusinessObjectsNew;
using Assurant.ElitaPlus.External.Interfaces;
using System;
using System.Linq;
using System.ServiceModel;


namespace Assurant.ElitaPlus.External.Falabella
{
    public class FalabellaServiceManager : IFalabellaServiceManager
    {
        /// <summary>
        /// Update Falabella when a claim is Approved or Denied
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UpdateClaimInfoResponse OrdenTrabajoEstadoModificarOp(UpdateClaimInfoRequest request)
        {
            var oWebPasswd = new WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_FALABELLA_SERVICE_WORKORDER), true);

            if (oWebPasswd == null || string.IsNullOrWhiteSpace(oWebPasswd.Url))
            {
                throw new FaultException($"Url {oWebPasswd.Url} not found or invalid");
            }

            var reference = new OrdenTrabajoEstadoModificarService
            {
                Url = oWebPasswd.Url
            };

           
            var input = new ordenDeTrabajoEstadoModificarExpReq_TYPE
            {
                ordenDeTrabajo = new ordenDeTrabajo_TYPE
                {
                    numeroServicioTecnico = request.WorkOrderNumber,
                    estadoOrdenTrabajo = (request.ClaimStatus == BasicClaimStatus.Denied.ToString() ? ordenDeTrabajo_TYPEEstadoOrdenTrabajo.Anulada : ordenDeTrabajo_TYPEEstadoOrdenTrabajo.ReemplazoAutorizado),
                    fechaModificacionEstado = request.StatusChangeDate,
                    descripcionRechazoOReparacion = (request.ClaimStatus == "D" ? request.DenialReason : string.Empty),
                    numeroOTServicioTecnico = request.ClaimNumber
                },
                producto = new producto_TYPE
                {
                     precioProducto = request.AuthorizedAmount
                }
            };
            Logger.AddInfo("OrdenTrabajoEstadoModificarOp WorkOrderNumber " + request.WorkOrderNumber);
            Logger.AddInfo("OrdenTrabajoEstadoModificarOp ClaimStatus " + (request.ClaimStatus == "D" ? ordenDeTrabajo_TYPEEstadoOrdenTrabajo.Anulada : ordenDeTrabajo_TYPEEstadoOrdenTrabajo.ReemplazoAutorizado));
            Logger.AddInfo("OrdenTrabajoEstadoModificarOp StatusChangeDate " + request.StatusChangeDate.ToString());
            Logger.AddInfo("OrdenTrabajoEstadoModificarOp ClaimStatus " + (request.ClaimStatus == "D" ? request.DenialReason : string.Empty));
            Logger.AddInfo("OrdenTrabajoEstadoModificarOp ClaimNumber " + request.ClaimNumber);



            reference.ClientService = new ClientService_TYPE
            {
                country = Country_TYPE.CL,
                commerce = Commerce_TYPE.Falabella,
                channel = Channel_TYPE.Cardif,
                date = DateTime.Now,
                hour = DateTime.Now,
                dateSpecified = true,
                hourSpecified = true
            };

            var output = reference.OrdenTrabajoEstadoModificarOp(input);

            var outputInfo = output.estadoConsulta;

            var response = new UpdateClaimInfoResponse
            {
                ErrorCode = outputInfo.codigoRespuesta,
                ErrorMessage = outputInfo.descripcionRespuesta
            };

            return response;
        }


        /// <summary>
        /// Retrieve work order number from Falabella
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetWorkOrderNumberResponse ServicioTecnicoDatosCrearOp(GetWorkOrderNumberRequest request)
        {
            var oWebPasswd = new WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_FALABELLA_SERVICE_CONFIRMATION), true);

            if (oWebPasswd == null || string.IsNullOrWhiteSpace(oWebPasswd.Url))
            {
                throw new FaultException($"Url {oWebPasswd.Url} not found or invalid");
            }

            var reference = new ServicioTecnicoDatosCrearService
            {
                Url = oWebPasswd.Url
            };
            

            var input = new ServicioTecnicoDatosCrearInput_TYPE()
            {
                ListaSolicitudServicioTecnicoRequest = new ListaSolicitudServicioTecnicoRequest_TYPE()
            };
            
            
            //Email
            var email = new Email_TYPE[0];

            email.ToList().Add(new Email_TYPE
            {
                email = request.Email
            });


            //Phone
            var phone = new Telefono_TYPE[0];
            phone.ToList().Add(new Telefono_TYPE
            {
                tipo = "PRINCIPAL",
                codigoArea = "56",
                numeroTelefono = request.WorkPhone
            });

            //phone[0].tipo = "PRINCIPAL";
            //phone[0].codigoPais = "56";
            //phone[0].numeroTelefono = request.WorkPhone;

            //Direction
            var direction = new Direccion_TYPE[0];
            direction.ToList().Add(new Direccion_TYPE
            {
                tipo = "PARTICULAR",
                ciudad = request.City,
                pais = request.Country,
                comuna = request.ComunaCode,
                calle = request.Address1
            });


            //Service Technico
            var serviceList = new ListaServicioTecnico_TYPE();
            var serviceTechnico = new ServicioTecnico_TYPE
            {
                origen = "Assurant",
                categoria = "Garantia Connect",
                descripcion = request.ProblemDescription,
                estadoF11 = "Abierto",
                subEstado = "Ingresado",
                estadoProducto = "Robo",
                detalleEstadoProducto = request.ProblemDescription,
                codigoFalla = "ROB01",
                fechaOcurrenciaSiniestro = request.DateOfLoss,
                numeroReclamoExterno = request.ClaimNumber
            };
            serviceList.ServicioTecnico = serviceTechnico;

            //Active 
            var activo = new Activo_TYPE[0];
            activo.ToList().Add(new Activo_TYPE
            {
                numeroActivo = request.CertificateNumber,
                serialProducto = request.SerialNumber,
                ean = request.SerialNumber,
                sku = request.SerialNumber,
                fechaCompra = request.WarrantySalesDate,
                nombreGarantia = request.ProductDescription,
                tipoGarantia = "Connect"
            });
            

            input.ListaSolicitudServicioTecnicoRequest = new ListaSolicitudServicioTecnicoRequest_TYPE()
            {
                Cuenta = new Cuenta_TYPE()
                {
                    organizacion = "Falabella - Chile",
                    tipoIdentificacion = "RUT",
                    numeroIdentificacion = request.IdentificationNumber,
                    digitoVerificador = request.VerificationNumber,
                    nombre = request.FirstName,
                    apellidoPaterno = request.LastName,
                    nacionalidad = request.Nationality,
                    ListaEmail = email,
                    ListaTelefono = phone,
                    ListaDireccion = direction,
                    ListaServicioTecnico = serviceList,
                    ListaActivo = activo
                }
            };

            try
            {
                //web service call
                var output = reference.ServicioTecnicoDatosCrearOp(input);

                var workOrderResponse = output.ListaSolicitudServicioTecnicoResponse;

                if (workOrderResponse != null)
                {
                    var response = new GetWorkOrderNumberResponse
                    {
                        WorkOrderNumber = workOrderResponse[0].numeroServicioTecnico,
                        ErrorCode = workOrderResponse[0].codigoError,
                        ErrorMessage = workOrderResponse[0].mensajerError
                    };

                    return response;
                }
               
            }

            catch (Exception ex)
            {
                throw;
            }

            return null;
                        
        }
    }
}
