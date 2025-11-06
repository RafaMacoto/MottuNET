using Microsoft.ML;
using System.IO;
using System.Collections.Generic;

namespace MottuNET.ML
{
    public class MotoAlaModel
    {
        private readonly string _modelPath = Path.Combine("ML", "motoAlaModel.zip");
        private readonly MLContext _mlContext;

        public MotoAlaModel()
        {
            _mlContext = new MLContext();
        }

        public void TreinarModelo()
        {
            var dadosTreinamento = new List<MotoAlaData>
            {
                new() { Problema = "farol quebrado", Status = "MANUTENCAO", Ala = "Manutenção" },
                new() { Problema = "sem placa", Status = "RECUPERACAO", Ala = "Recuperação" },
                new() { Problema = "pneu furado", Status = "MANUTENCAO", Ala = "Manutenção" },
                new() { Problema = "sem problema", Status = "DISPONIVEL", Ala = "Disponível" },
                new() { Problema = "arranhão na lateral", Status = "RECUPERACAO", Ala = "Recuperação" },
                new() { Problema = "moto completa", Status = "DISPONIVEL", Ala = "Disponível" }
            };

            var dataView = _mlContext.Data.LoadFromEnumerable(dadosTreinamento);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(MotoAlaData.Ala))
                .Append(_mlContext.Transforms.Text.FeaturizeText("ProblemaFeaturized", nameof(MotoAlaData.Problema)))
                .Append(_mlContext.Transforms.Text.FeaturizeText("StatusFeaturized", nameof(MotoAlaData.Status)))
                .Append(_mlContext.Transforms.Concatenate("Features", "ProblemaFeaturized", "StatusFeaturized"))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var modelo = pipeline.Fit(dataView);

            Directory.CreateDirectory("ML");
            _mlContext.Model.Save(modelo, dataView.Schema, _modelPath);
        }

        public string PreverAla(string problema, string status)
        {
            if (!File.Exists(_modelPath))
                TreinarModelo();

            var modeloCarregado = _mlContext.Model.Load(_modelPath, out _);
            var engine = _mlContext.Model.CreatePredictionEngine<MotoAlaData, MotoAlaPrediction>(modeloCarregado);

            var entrada = new MotoAlaData
            {
                Problema = problema,
                Status = status
            };

            var resultado = engine.Predict(entrada);
            return resultado.AlaPrevista;
        }
    }
}
