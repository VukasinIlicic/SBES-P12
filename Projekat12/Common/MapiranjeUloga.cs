using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MapiranjeUloga
    {
        private static Dictionary<string, string[]> roles = new Dictionary<string, string[]>()
        {
            {"reader", new string[] { "PrikazInformacija", "SrednjaVrednostPotrosnje" } },
            {"editor", new string[] { "AzurirajPotrosnju" } },
            {"admin", new string[] { "DodajEntitet", "ObrisiEntitet" } }
        };

        public static bool Provera(string imeFunkcije, List<string> grupe)
        {
            bool odobrenPristup = false;

            foreach(var g in grupe)
            {
                if(roles[g].Contains(imeFunkcije))
                {
                    odobrenPristup = true;
                    break;
                }
            }

            return odobrenPristup;
        }
    }
}
