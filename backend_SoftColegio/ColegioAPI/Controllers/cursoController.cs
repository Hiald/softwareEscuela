﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class cursoController : ApiController
    {
        tdCurso itdCurso;


        [HttpGet]
        public int wsInsertarCursoDocente(int wsidusuario, int wsidnivel, int wsidgrado, int wsidcurso, Int16 wsiestado)
        {
            int iresultado = -1;
            try
            {
                itdCurso = new tdCurso();
                iresultado = itdCurso.tdInsertarCursoDocente(wsidusuario, wsidnivel, wsidgrado, wsidcurso, wsiestado);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarCurso(int wsitipousuario)
        {
            List<edCurso> wsenCurso = new List<edCurso>();
            try
            {
                itdCurso = new tdCurso();
                wsenCurso = itdCurso.tdListarCurso(wsitipousuario);
                return JsonConvert.SerializeObject(wsenCurso);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

    }
}