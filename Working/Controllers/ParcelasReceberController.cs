﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Working.Models;

namespace Working.Controllers
{
    public class ParcelasReceberController : Controller
    {
        private readonly ParcelasReceberService _parcelasReceberService;
        private readonly ContratoService _contratoService;

        public ParcelasReceberController()
        {
            _parcelasReceberService = Dependencia.Resolver<ParcelasReceberService>();
            _contratoService = Dependencia.Resolver<ContratoService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarParcelasContrato(int id)
        {
            var contrato = _contratoService.ObterPorId(id);
            var parcelas = _parcelasReceberService.Listar(e => e.Contrato == contrato);
            var lista = parcelas.Select(i => new ObjetoParcela
            {
                Id = i.Id,
                DataVencimento = i.DataVencimento.Day.ToString("D2") + "/" + i.DataVencimento.ToString("MMM"),
                Valor = i.Valor,
                Situacao = i.Situacao,
                NumeroParcela = i.NumeroParcela,
                DataRegistro = i.DataRegistro
            })
                .OrderByDescending(e => e.NumeroParcela)
                .ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public void Receber(int id, string valor)
        {
            var decimalValor = Convert.ToDouble(valor);
            var parcela = _parcelasReceberService.ObterPorId(id);
            parcela.Situacao = "Recebida";
            parcela.ValorRecebido = Convert.ToDecimal(decimalValor);
            parcela.DataRegistro = DateTime.Now;
            _parcelasReceberService.Cadastrar(parcela);
        }
    }
}
