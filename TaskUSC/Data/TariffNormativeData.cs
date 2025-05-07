using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskUSC.Data
{
    internal class TariffNormativeData
    {
        //cold
        public decimal cTariff { get; }// = 35.78m;
        public decimal cNormative { get; }// = 4.85m;

        //hot
        //tn
        public decimal tnTariff { get; }// = 35.78m;
        public decimal tnNormative { get; }// = 4.01m;

        //te
        public decimal teTariff { get; }// = 998.69m;
        public decimal teNormative { get; }// = 0.05349m;

        //ee
        public decimal eeDayTariff { get; }// = 4.9m;
        public decimal eeNightTariff { get; }// = 2.31m;
        
        public decimal eeNormative { get; }// = 164m;
        public decimal eeTariff { get; }// = 4.28m;

        public TariffNormativeData
            (decimal cTariff, 
            decimal cNormative, 
            decimal tnTariff, 
            decimal tnNormative, 
            decimal teTariff,
            decimal teNormative,
            decimal eeDayTariff, 
            decimal eeNightTariff,
            decimal eeNormative,
            decimal eeTariff)
        {
            this.cTariff = cTariff;
            this.cNormative = cNormative;
            this.tnTariff = tnTariff;
            this.tnNormative = tnNormative;
            this.teTariff = teTariff;
            this.teNormative = teNormative;
            this.eeDayTariff = eeDayTariff;
            this.eeNightTariff = eeNightTariff;
            this.eeNormative = eeNormative;
            this.eeTariff = eeTariff;
        }
    }
}
