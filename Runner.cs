using System.Net;
using AWS.Consumer.Interfaces;
using Serilog;

namespace AWS.Consumer
{
    public class Runner : IRunner
    {
        private readonly ISQSService _SQSService;

        public Runner(ISQSService service)
        {
            _SQSService = service;
        }

        //TODO implementação de um JOB para ler a fila de X tempos
        public async void IniciarLeitura()
        {
            try
            {
                Log.Information("Iniciando a leitura da fila");
                var response = await _SQSService.GetMessageFromSQS();
                foreach (var message in response)
                {
                    Log.Information("Mensagem encontrada");
                    Log.Information(message.Body);
                }
                Log.Information("Mensagem(ns) lida(s) iniciando exclusão");

                foreach (var message in response)
                {
                    Log.Information("Deletando a mensagem {0}", message.Body);
                    var retorno = await _SQSService.DeleteMessageFromSQS(message);

                    if (retorno == HttpStatusCode.OK)
                    {
                        Log.Information("Mensagem deletada com sucesso");
                    }
                    else
                    {
                        Log.Information("Erro ao deletar mensagem: {0}", retorno);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao ler a fila: {0} ", ex.Message);
            }
        }

    }
}