using System;
using System.Collections.Generic;
using CsvHelper;
using DIO.Series.Interfaces;

namespace DIO.Series
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> ListaSerie = new List<Serie>();

        public void Atualiza(int id, Serie entidade)
        {
            ListaSerie[id] = entidade;
        }

        public void Exclui(int id)
        {
            if (id < ListaSerie.Count)
            {
                ListaSerie[id].Excluir();
                System.Console.WriteLine("Série excluida com sucesso!");
            }
            else
            {
                System.Console.WriteLine("Numero do Id invalido!");
            }
        }

        public void Insere(Serie entidade)
        {
            ListaSerie.Add(entidade);
            EscreverCSV(ListaSerie);

        }

        public List<Serie> Lista()
        {
            return ListaSerie;
        }

        public int ProximoId()
        {
            return ListaSerie.Count;
        }

        public Serie retornaPorId(int id)
        {
                return ListaSerie[id];
    
        }

         public void EscreverCSV( List<Serie> Series)
         {
             var path = Diretorio();

             using var dadosInseridos = new StreamWriter(path);
             using var csvEscrever = new CsvWriter(dadosInseridos,System.Globalization.CultureInfo.InvariantCulture);

            csvEscrever.WriteRecords(Series);
                 
         }    

         public void Logar()
         {
            var path = Diretorio();
            var arquivoCsv = new FileInfo(path);
            if(arquivoCsv.Exists)
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
        
                ListaSerie = csv.GetRecords<Serie>().ToList();
            }
         }
       
         private string Diretorio()
         {
            var path = Path.Combine(Environment.CurrentDirectory,"BancoDados");
            var diretorioDados = new DirectoryInfo(path);
            if(!diretorioDados.Exists)
                diretorioDados.Create();

            path = Path.Combine(path,"seriesBD.csv");

            return path;
         }

        
    }
}