/* AUTHOR'S
PROPIETARIO: TECH SOLUTIONS PERU 
AUTOR : RONALD EDUARDO IBARRA ZAPATA 
AÑO : 2017
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class OperationResult
    {
        public OperationResultEnum code_result;
        public List<Exception> exceptions = new List<Exception>();
        public string title = "";
        public string message = "";
        public long data = 0;
        public string idUser = "";
        public object information = "";
        public object objeto = "";

        public OperationResult()
        {
            setResult(OperationResultEnum.Success);
        }

        public OperationResult(OperationResultEnum code)
        {
            setResult(code);
        }

        public OperationResult(OperationResultEnum code, string result_description)
        {
            this.title = result_description;
            this.code_result = code;
        }
        public OperationResult(OperationResultEnum code, string result_description, long dato)
        {
            this.title = result_description;
            this.code_result = code;
            this.data = dato;
        }
        public OperationResult(OperationResultEnum code, string result_description, string message)
        {
            this.title = result_description;
            this.code_result = code;
            this.message = message;
        }
        public OperationResult(string idUser, OperationResultEnum code, string result_description)
        {
            this.title = result_description;
            this.code_result = code;
            this.idUser = idUser;
        }
        public OperationResult(Exception e)
        {
            this.code_result = OperationResultEnum.Error;
            this.title = OperationResultSettings.Default.OperationResultErrorDescription;
            this.exceptions.Add(e);
            this.message = e.Message;

        }

        public OperationResult(Exception e, string message)
        {
            this.code_result = OperationResultEnum.Error;
            this.title = OperationResultSettings.Default.OperationResultErrorDescription;
            this.message = message;
            this.exceptions.Add(e);
        }

        public void setResult(OperationResultEnum result)
        {
            this.code_result = result;

            switch (result)
            {
                case OperationResultEnum.Success:
                    this.title = OperationResultSettings.Default.OperationResultSuccessDescription;
                    break;
                case OperationResultEnum.Error:
                    this.title = OperationResultSettings.Default.OperationResultErrorDescription;
                    break;
                //todo: usar Reflexion para determinar e incluir el nombre del invocador en la descripcion.
                default:
                    this.title = "";
                    break;
            }
        }

        /// <summary>
        /// devuelve un tipo anonimo que evita las referencias circulares
        /// </summary>
        /// <returns></returns>
        public object simplificar()
        {
            return (new { codigo = this.code_result, descripcion = this.title, excepciones = this.exceptions.Select(m => m.Message).ToList() });
        }
    }
}
